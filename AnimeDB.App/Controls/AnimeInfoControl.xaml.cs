using AnimeDB.Service.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AnimeDB.App.Controls
{
    public sealed partial class AnimeInfoControl : UserControl
    {
        public static readonly DependencyProperty AnimeProperty =
            DependencyProperty.Register("Anime", typeof(Anime), typeof(AnimeInfoControl), new PropertyMetadata(null, OnAnimeChanged));

        public Anime Anime
        {
            get { return (Anime)GetValue(AnimeProperty); }
            set { SetValue(AnimeProperty, value); }
        }

        public AnimeInfoControl()
        {
            this.InitializeComponent();
        }

        private static void OnAnimeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
        }
    }
}
