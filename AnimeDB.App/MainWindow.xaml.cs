using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace AnimeDB.App
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(this.AnimeDBTitleBar);
        }

        private void NavigationViewMain_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var selectedItem = (NavigationViewItem)args.SelectedItem;
            Type pageType = Type.GetType($"AnimeDB.App.Pages.{selectedItem.Tag}Page");

            if (pageType != null)
            {
                this.FrameMain.Navigate(pageType, null, args.RecommendedNavigationTransitionInfo);
            }
        }
    }
}
