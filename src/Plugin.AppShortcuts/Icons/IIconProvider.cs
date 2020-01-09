namespace Plugin.AppShortcuts.Icons
{
    internal interface IIconProvider
    {
        object CreatePlatformIcon(IShortcutIcon shortcutIcon);
    }
}
