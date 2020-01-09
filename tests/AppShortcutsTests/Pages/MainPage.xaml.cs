using System;
using System.Collections.ObjectModel;
using Plugin.AppShortcuts;
using Xamarin.Forms;

namespace AppShortcutsTests.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Shortcuts = new ObservableCollection<Shortcut>();

            ShortcutsListView.ItemsSource = Shortcuts;
            ShortcutsListView.RefreshCommand = new Command(() => RefreshShortcutsList());
        }

        public ObservableCollection<Shortcut> Shortcuts { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IsSupportedLabel.Text = $"App shortcuts supported: {(CrossAppShortcuts.IsSupported ? "Yes" : "No")}";

            RefreshShortcutsList();
        }

        public async void AddNewShortcut(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AddShortcutPage());
        }

        public async void TestShortcuts(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new TestShortcutsPage());
        }

        public void DeleteShortcut(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var sc = (Shortcut)menuItem.CommandParameter;
            CrossAppShortcuts.Current.RemoveShortcut(sc.ShortcutId);
            RefreshShortcutsList();
        }

        private void RefreshShortcutsList()
        {
            var shortcuts = CrossAppShortcuts.Current.GetShortcuts();

            Device.BeginInvokeOnMainThread(() =>
            {
                Shortcuts.Clear();
                foreach (var sc in shortcuts)
                {
                    Shortcuts.Add(sc);
                }
            });
            ShortcutsListView.IsRefreshing = false;
        }
    }
}
