namespace Plugin.AppShortcuts.Icons
{
    internal enum ShortcutIconType
    {
        Default = 1,
        Add = 2,
        Alarm = 3,
        Audio = 4,
        Bookmark = 5,
        CapturePhoto = 6,
        CaptureVideo = 7,
        Cloud = 8,
        Compose = 9,
        Confirmation = 10,
        Contact = 11,
        Date = 12,
        Favorite = 13,
        Home = 14,
        Invitation = 15,
        Location = 16,
        Love = 17,
        Mail = 18,
        MarkLocation = 19,
        Message = 20,
        Pause = 21,
        Play = 22,
        Prohibit = 23,
        Search = 24,
        Share = 25,
        Shuffle = 26,
        Task = 27,
        TaskCompleted = 28,
        Time = 29,
        Update = 30
    }

    internal static class ShortcutIconTypesHelper
    {
        internal static EmbeddedIcon ResolveEmbeddedIcon(ShortcutIconType iconType)
        {
            return iconType switch
            {
                ShortcutIconType.Add => new AddIcon(),
                ShortcutIconType.Alarm => new AlarmIcon(),
                ShortcutIconType.Audio => new AudioIcon(),
                ShortcutIconType.Bookmark => new BookmarkIcon(),
                ShortcutIconType.CapturePhoto => new CapturePhotoIcon(),
                ShortcutIconType.CaptureVideo => new CaptureVideoIcon(),
                ShortcutIconType.Cloud => new CloudIcon(),
                ShortcutIconType.Compose => new ComposeIcon(),
                ShortcutIconType.Confirmation => new ConfirmationIcon(),
                ShortcutIconType.Contact => new ContactIcon(),
                ShortcutIconType.Date => new DateIcon(),
                ShortcutIconType.Favorite => new FavoriteIcon(),
                ShortcutIconType.Home => new HomeIcon(),
                ShortcutIconType.Invitation => new InvitationIcon(),
                ShortcutIconType.Location => new LocationIcon(),
                ShortcutIconType.Love => new LoveIcon(),
                ShortcutIconType.Mail => new MailIcon(),
                ShortcutIconType.MarkLocation => new MarkLocationIcon(),
                ShortcutIconType.Message => new MessageIcon(),
                ShortcutIconType.Pause => new PauseIcon(),
                ShortcutIconType.Play => new PlayIcon(),
                ShortcutIconType.Prohibit => new ProhibitIcon(),
                ShortcutIconType.Search => new SearchIcon(),
                ShortcutIconType.Share => new ShareIcon(),
                ShortcutIconType.Shuffle => new ShuffleIcon(),
                ShortcutIconType.Task => new TaskIcon(),
                ShortcutIconType.TaskCompleted => new TaskCompletedIcon(),
                ShortcutIconType.Time => new TimeIcon(),
                ShortcutIconType.Update => new UpdateIcon(),
                _ => new DefaultIcon(),
            };
        }
    }
}
