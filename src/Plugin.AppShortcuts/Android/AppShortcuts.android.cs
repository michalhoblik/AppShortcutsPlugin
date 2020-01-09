using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Plugin.AppShortcuts.Android;
using Plugin.AppShortcuts.Icons;
using AUri = Android.Net.Uri;

namespace Plugin.AppShortcuts
{
    [Preserve(AllMembers = true)]
    internal class AppShortcutsImplementation : IAppShortcuts, IPlatformSupport
    {
        private readonly string NOT_SUPPORTED_ERROR_MESSAGE
            = $"Operation not supported on Android API 24 or below. Use {nameof(CrossAppShortcuts)}.{nameof(CrossAppShortcuts.IsSupported)} to check if the current device supports this feature.";

        private readonly IIconProvider _embeddedIconProvider;
        private readonly IIconProvider _customIconProvider;
        private readonly ShortcutManager _manager;
        private Type _drawableClass;

        public AppShortcutsImplementation()
        {
            _embeddedIconProvider = new EmbeddedIconProvider();
            _customIconProvider = new CustomIconProvider();
            _manager = (ShortcutManager)Application.Context.GetSystemService(Context.ShortcutService);

            IsSupportedByCurrentPlatformVersion = Build.VERSION.SdkInt >= BuildVersionCodes.NMr1;

            Init();
        }

        public void Init()
        {
            _drawableClass = Assembly.GetCallingAssembly().GetTypes().FirstOrDefault(x => x.Name == "Drawable" || x.Name == "Resource_Drawable");
            (_customIconProvider as CustomIconProvider)?.Init(_drawableClass);
        }

        public bool IsSupportedByCurrentPlatformVersion { get; }

        public void AddShortcut(Shortcut shortcut)
        {
            if (!IsSupportedByCurrentPlatformVersion)
                throw new NotSupportedOnDeviceException(NOT_SUPPORTED_ERROR_MESSAGE);

            var context = Application.Context;
            var builder = new ShortcutInfo.Builder(context, shortcut.ShortcutId);

            var uri = AUri.Parse(shortcut.Uri);

            builder.SetIntent(new Intent(Intent.ActionView, uri));
            builder.SetShortLabel(shortcut.Label);
            builder.SetLongLabel(shortcut.Description);

            var icon = CreateIcon(shortcut.Icon);
            if (icon != null)
                builder.SetIcon(icon);

            var scut = builder.Build();

            if (_manager.DynamicShortcuts == null || !_manager.DynamicShortcuts.Any())
                _manager.SetDynamicShortcuts(new List<ShortcutInfo> { scut });
            else
                _manager.AddDynamicShortcuts(new List<ShortcutInfo> { scut });
        }

        public IEnumerable<Shortcut> GetShortcuts()
        {
            if (!IsSupportedByCurrentPlatformVersion)
                throw new NotSupportedOnDeviceException(NOT_SUPPORTED_ERROR_MESSAGE);

            var dynamicShortcuts = _manager.DynamicShortcuts;
            var shortcuts = dynamicShortcuts.Select(s => new Shortcut(s.Id)
            {
                Label = s.ShortLabel,
                Description = s.LongLabel,
                Uri = s.Intent.ToUri(IntentUriType.AllowUnsafe)
            });
            return shortcuts;
        }

        public void RemoveShortcut(string shortcutId)
        {
            if (!IsSupportedByCurrentPlatformVersion)
                throw new NotSupportedOnDeviceException(NOT_SUPPORTED_ERROR_MESSAGE);

            _manager.RemoveDynamicShortcuts(new List<string> { shortcutId });
        }

        private Icon CreateIcon(IShortcutIcon icon)
        {
            try
            {
                return ((icon is CustomIcon) ? _customIconProvider.CreatePlatformIcon(icon) : _embeddedIconProvider.CreatePlatformIcon(icon)) as Icon;
            }
            catch (Exception ex)
            {
                Log.Error(nameof(AppShortcutsImplementation), ex.Message, ex);
                return null;
            }
        }
    }
}
