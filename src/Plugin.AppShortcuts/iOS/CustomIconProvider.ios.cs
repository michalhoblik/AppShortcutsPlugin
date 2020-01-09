using Foundation;
using Plugin.AppShortcuts.Icons;
using UIKit;

namespace Plugin.AppShortcuts.iOS
{
    internal class CustomIconProvider : IIconProvider
    {
        public object CreatePlatformIcon(IShortcutIcon shortcutIcon)
        {
            if (string.IsNullOrWhiteSpace(shortcutIcon.IconName))
                return null;

            UIApplicationShortcutIcon icon = null;
            new NSObject().BeginInvokeOnMainThread(() =>
            {
                icon = UIApplicationShortcutIcon.FromTemplateImageName(shortcutIcon.IconName);
            });

            return icon;
        }
    }
}
