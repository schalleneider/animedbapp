using AnimeDB.Service.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.Foundation;

namespace AnimeDB.App.Controls
{
    public sealed partial class ThemeInfoAttributeToggledEventArgs : EventArgs
    {
        public long ThemeId { get; set; }
        public bool IsOn { get; set; }
    }
    
    public sealed partial class ThemeInfoControl : UserControl
    {
        public event TypedEventHandler<ThemeInfoControl, ThemeInfoAttributeToggledEventArgs> AppHiddenToggled;
        public event TypedEventHandler<ThemeInfoControl, ThemeInfoAttributeToggledEventArgs> IsFavoriteToggled;
        public event TypedEventHandler<ThemeInfoControl, ThemeInfoAttributeToggledEventArgs> IsBadToggled;

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
            
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AppHidden_Toggled(InfoBlockToggleControl sender, InfoBlockToggleControlToggledEventArgs args)
        {
            var themeId = this.Theme.Id;

            if (this.AppHiddenToggled != null)
            {
                this.AppHiddenToggled(this, new ThemeInfoAttributeToggledEventArgs() { ThemeId = themeId, IsOn = args.IsOn });
            }

            this.Theme.AppHidden = args.IsOn;
        }

        private void IsFavorite_Toggled(InfoBlockToggleControl sender, InfoBlockToggleControlToggledEventArgs args)
        {
            var themeId = this.Theme.Id;

            if (this.IsFavoriteToggled != null)
            {
                this.IsFavoriteToggled(this, new ThemeInfoAttributeToggledEventArgs() { ThemeId = themeId, IsOn = args.IsOn });
            }

            this.Theme.IsFavorite = args.IsOn;
        }

        private void IsBad_Toggled(InfoBlockToggleControl sender, InfoBlockToggleControlToggledEventArgs args)
        {
            var themeId = this.Theme.Id;

            if (this.IsBadToggled != null)
            {
                this.IsBadToggled(this, new ThemeInfoAttributeToggledEventArgs() { ThemeId = themeId, IsOn = args.IsOn });
            }

            this.Theme.IsBad = args.IsOn;
        }
    }
}
