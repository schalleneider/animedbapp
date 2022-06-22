using AnimeDB.Service.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AnimeDB.App.Controls
{
    public sealed partial class ThemeInfoControl : UserControl
    {
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
    }
}
