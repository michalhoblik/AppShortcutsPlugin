using System;
using System.Linq;
using Plugin.AppShortcuts;
using Plugin.AppShortcuts.Icons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppShortcutsTests.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddShortcutPage : ContentPage
    {
        private Shortcut _shortcut;

        public AddShortcutPage()
        {
            InitializeComponent();
            _shortcut = new Shortcut();
        }

        public AddShortcutPage(string shortcutId)
        {
            InitializeComponent();
            LoadShortcut(shortcutId);
        }

        private void LoadShortcut(string shortcutId)
        {
            var shortcuts = CrossAppShortcuts.Current.GetShortcuts();
            _shortcut = shortcuts.FirstOrDefault(s => string.Equals(s.ShortcutId, shortcutId));

            if (_shortcut == null)
            {
                _shortcut = new Shortcut();
                return;
            }

            SubmitButton.Text = "Update shortcut";

            TitleEntry.Text = _shortcut.Label;
            SubtitleEntry.Text = _shortcut.Description;
            IconTypePicker.SelectedItem = _shortcut.Icon.ToString();
            CustomIconEntry.Text = _shortcut.Icon.IconName;
        }

        private async void CreateNewShortcut(object sender, EventArgs args)
        {
            var icon = ResolveEmbeddedIcon(IconTypePicker.SelectedItem.ToString());

            if (!string.IsNullOrWhiteSpace(CustomIconEntry.Text))
            {
                icon = new CustomIcon(CustomIconEntry.Text);
            }

            _shortcut.Label = TitleEntry.Text;
            _shortcut.Description = SubtitleEntry.Text;
            _shortcut.Icon = icon;
            _shortcut.Uri = $"stc://{nameof(AppShortcutsTests)}/{nameof(AddShortcutPage)}/{_shortcut.ShortcutId}";

            CrossAppShortcuts.Current.AddShortcut(_shortcut);

            await Navigation.PopAsync();
        }

        private static readonly Func<string, IShortcutIcon> ResolveEmbeddedIcon = iconType =>
        {
            return iconType switch
            {
                "Add" => new AddIcon(),
                "Alarm" => new AlarmIcon(),
                "Audio" => new AudioIcon(),
                "Bookmark" => new BookmarkIcon(),
                "CapturePhoto" => new CapturePhotoIcon(),
                "CaptureVideo" => new CaptureVideoIcon(),
                "Cloud" => new CloudIcon(),
                "Compose" => new ComposeIcon(),
                "Confirmation" => new ConfirmationIcon(),
                "Contact" => new ContactIcon(),
                "Date" => new DateIcon(),
                "Favorite" => new FavoriteIcon(),
                "Home" => new HomeIcon(),
                "Invitation" => new InvitationIcon(),
                "Location" => new LocationIcon(),
                "Love" => new LoveIcon(),
                "Mail" => new MailIcon(),
                "MarkLocation" => new MarkLocationIcon(),
                "Message" => new MessageIcon(),
                "Pause" => new PauseIcon(),
                "Play" => new PlayIcon(),
                "Prohibit" => new ProhibitIcon(),
                "Search" => new SearchIcon(),
                "Share" => new ShareIcon(),
                "Shuffle" => new ShuffleIcon(),
                "Task" => new TaskIcon(),
                "TaskCompleted" => new TaskCompletedIcon(),
                "Time" => new TimeIcon(),
                "Update" => new UpdateIcon(),
                _ => new DefaultIcon(),
            };
        };
    }
}