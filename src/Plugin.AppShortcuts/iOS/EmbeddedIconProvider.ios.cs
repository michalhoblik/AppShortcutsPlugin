using System;
using Foundation;
using Plugin.AppShortcuts.Icons;
using UIKit;

namespace Plugin.AppShortcuts.iOS
{
    internal class EmbeddedIconProvider : IIconProvider
    {
        public object CreatePlatformIcon(IShortcutIcon shortcutIcon)
        {
            var isParseSuccessful = Enum.TryParse(shortcutIcon.IconName, out UIApplicationShortcutIconType type);

            if (!isParseSuccessful)
                type = UIApplicationShortcutIconType.Favorite;

            UIApplicationShortcutIcon icon = null;
            new NSObject().BeginInvokeOnMainThread(() =>
            {
                icon = UIApplicationShortcutIcon.FromType(type);
            });

            return icon;
        }
    }
}
