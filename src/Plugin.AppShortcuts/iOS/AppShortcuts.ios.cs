﻿using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using Plugin.AppShortcuts.Icons;
using Plugin.AppShortcuts.iOS;
using UIKit;

namespace Plugin.AppShortcuts
{
    [Preserve(AllMembers = true)]
    internal class AppShortcutsImplementation : IAppShortcuts, IPlatformSupport
    {
        private readonly string NOT_SUPPORTED_ERROR_MESSAGE 
            = $"Operation not supported on iOS 8 or below. Use {nameof(CrossAppShortcuts)}.{nameof(CrossAppShortcuts.IsSupported)} to check if the current device supports this feature.";

        private readonly IIconProvider _embeddedIconProvider;
        private readonly IIconProvider _customIconProvider;

        public AppShortcutsImplementation()
        {
            _embeddedIconProvider = new EmbeddedIconProvider();
            _customIconProvider = new CustomIconProvider();
            IsSupportedByCurrentPlatformVersion = UIDevice.CurrentDevice.CheckSystemVersion(9, 0);
        }

        public bool IsSupportedByCurrentPlatformVersion { get; }

        public void Init()
        { }

        public void AddShortcut(Shortcut shortcut)
        {
            if (!IsSupportedByCurrentPlatformVersion)
                throw new NotSupportedOnDeviceException(NOT_SUPPORTED_ERROR_MESSAGE);

            var type = shortcut.ShortcutId;
            var icon = shortcut.IsEmbeddedIcon
                ? (UIApplicationShortcutIcon)_embeddedIconProvider.CreatePlatformIcon(shortcut.Icon)
                : (UIApplicationShortcutIcon)_customIconProvider.CreatePlatformIcon(shortcut.Icon);
            var metadata = CreateUriMetadata(shortcut.Uri);

            new NSObject().BeginInvokeOnMainThread(() =>
            {
                var scut = new UIMutableApplicationShortcutItem(type,
                    shortcut.Label,
                    shortcut.Description,
                    icon,
                    metadata);

                var scuts = UIApplication.SharedApplication.ShortcutItems.ToList();
                scuts.Add(scut);

                UIApplication.SharedApplication.ShortcutItems = scuts.ToArray();
            });
        }

        public IEnumerable<Shortcut> GetShortcuts()
        {
            if (!IsSupportedByCurrentPlatformVersion)
                throw new NotSupportedOnDeviceException(NOT_SUPPORTED_ERROR_MESSAGE);

            var dynamicShortcuts = UIApplication.SharedApplication.ShortcutItems.ToList();
            var shortcuts = dynamicShortcuts.Select(ds => new Shortcut(ds.Type)
            {
                Label = ds.LocalizedTitle,
                Description = ds.LocalizedSubtitle,
                Uri = ds?.UserInfo[ArgumentsHelper.ShortcutUriKey]?.ToString() ?? string.Empty,
                Icon = ResolveShortcutIconType(ds?.Icon?.ToString() ?? string.Empty)
            });

            return shortcuts;
        }

        public void RemoveShortcut(string shortcutId)
        {
            if (!IsSupportedByCurrentPlatformVersion)
                throw new NotSupportedOnDeviceException(NOT_SUPPORTED_ERROR_MESSAGE);

            new NSObject().BeginInvokeOnMainThread(() =>
            {
                var shortcutItem =
                    UIApplication.SharedApplication.ShortcutItems.FirstOrDefault(si => si.Type.Equals(shortcutId));

                if (shortcutItem == null)
                    return;

                var updatedItems = UIApplication.SharedApplication.ShortcutItems.ToList();
                updatedItems.Remove(shortcutItem);
                UIApplication.SharedApplication.ShortcutItems = updatedItems.ToArray();
            });
        }

        private NSDictionary<NSString, NSObject> CreateUriMetadata(string uri)
        {
            var metadata = new NSDictionary<NSString, NSObject>(new NSString(ArgumentsHelper.ShortcutUriKey), new NSString(uri));
            return metadata;
        }

        private static readonly Func<string, IShortcutIcon> ResolveShortcutIconType = iconName =>
        {
            var isParseSuccessful = Enum.TryParse(iconName, out ShortcutIconType type);

            if (isParseSuccessful)
                return ShortcutIconTypesHelper.ResolveEmbeddedIcon(type);

            return new CustomIcon(iconName);
        };
    }
}
