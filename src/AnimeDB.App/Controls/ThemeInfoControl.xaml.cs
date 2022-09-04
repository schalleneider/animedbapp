using AnimeDB.Service.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.Foundation;

namespace AnimeDB.App.Controls
{
    public sealed partial class ThemeInfoHideEventArgs : EventArgs
    {
        public long ThemeId { get; set; }
    }
    
    public sealed partial class ThemeInfoControl : UserControl
    {
        public event TypedEventHandler<ThemeInfoControl, ThemeInfoHideEventArgs> ThemeUnhide;
        public event TypedEventHandler<ThemeInfoControl, ThemeInfoHideEventArgs> ThemeHide;

        public static readonly DependencyProperty ThemeProperty =
            DependencyProperty.Register("Theme", typeof(Theme), typeof(ThemeInfoControl), new PropertyMetadata(null, OnThemeChanged));

        public Theme Theme
        {
            get { return (Theme)GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }

        public ThemeInfoControl()
        {
            this.InitializeComponent();
        }

        private static void OnThemeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
        }

        private void UnhideButton_Click(object sender, RoutedEventArgs e)
        {
            var themeId = this.Theme.Id;

            if (this.ThemeUnhide != null)
            {
                this.ThemeUnhide(this, new ThemeInfoHideEventArgs() { ThemeId = themeId });
            }

            this.Theme.AppHidden = false;
        }
        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            var themeId = this.Theme.Id;

            if (this.ThemeHide != null)
            {
                this.ThemeHide(this, new ThemeInfoHideEventArgs() { ThemeId = themeId });
            }

            this.Theme.AppHidden = true;
        }
    }
}
