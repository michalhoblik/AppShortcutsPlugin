using System;
using Plugin.AppShortcuts;
using Plugin.AppShortcuts.Icons;
using Xamarin.Forms;

namespace AppShortcutsTests.Pages
{
    public partial class TestShortcutsPage : ContentPage
    {
        public TestShortcutsPage()
        {
            InitializeComponent();
        }

        private async void AddShortcutSet(object sender, EventArgs e)
        {
            var tag = Convert.ToInt32((sender as Button)?.CommandParameter);

            RemoveCurrentShortcuts();

            switch (tag)
            {
                case 1:
                    AddShortcut(new AddIcon());
                    AddShortcut(new AlarmIcon());
                    AddShortcut(new AudioIcon());
                    AddShortcut(new BookmarkIcon());
                    break;
                case 2:
                    AddShortcut(new CapturePhotoIcon());
                    AddShortcut(new CaptureVideoIcon());
                    AddShortcut(new CloudIcon());
                    AddShortcut(new ComposeIcon());

                    break;
                case 3:
                    AddShortcut(new ConfirmationIcon());
                    AddShortcut(new ContactIcon());
                    AddShortcut(new DateIcon());
                    AddShortcut(new FavoriteIcon());

                    break;
                case 4:
                    AddShortcut(new HomeIcon());
                    AddShortcut(new InvitationIcon());
                    AddShortcut(new LocationIcon());
                    AddShortcut(new LoveIcon());
                    break;
                case 5:
                    AddShortcut(new MailIcon());
                    AddShortcut(new MarkLocationIcon());
                    AddShortcut(new MessageIcon());
                    AddShortcut(new PauseIcon());
                    break;
                case 6:
                    AddShortcut(new PlayIcon());
                    AddShortcut(new ProhibitIcon());
                    AddShortcut(new SearchIcon());
                    AddShortcut(new ShareIcon());
                    break;
                case 7:
                    AddShortcut(new ShuffleIcon());
                    AddShortcut(new TaskIcon());
                    AddShortcut(new TaskCompletedIcon());
                    AddShortcut(new TimeIcon());
                    break;
                case 8:
                    AddShortcut(new UpdateIcon());
                    AddShortcut(new DefaultIcon());
                    AddCustomShortcut("ic_beach.png");
                    break;
            }

            await Navigation.PopAsync();
        }

        private void ClearShortcuts(object sender, EventArgs e)
        {
            RemoveCurrentShortcuts();
        }

        private void RemoveCurrentShortcuts()
        {
            var currentShortcuts = CrossAppShortcuts.Current.GetShortcuts();
            foreach (var sc in currentShortcuts)
            {
                CrossAppShortcuts.Current.RemoveShortcut(sc.ShortcutId);
            }
        }

        private void AddShortcut(IShortcutIcon icon)
        {
            var sc = new Shortcut
            {
                Icon = icon,
                Label = $"{icon.IconName}_L",
                Description = $"{icon.IconName}_D",
                Uri = $"stc://AppShortcutTests/Tests/{icon.IconName}"
            };
            CrossAppShortcuts.Current.AddShortcut(sc);
        }

        private void AddCustomShortcut(string iconName)
        {
            var sc = new Shortcut
            {
                Icon = new CustomIcon(iconName),
                Label = $"{iconName}_L",
                Description = $"{iconName}_D",
                Uri = $"stc://AppShortcutTests/Tests/{iconName}"
            };
            CrossAppShortcuts.Current.AddShortcut(sc);
        }
    }
}
