using AnimeDB.Service.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using Windows.Foundation;

namespace AnimeDB.App.Controls
{
    public sealed partial class MediaCollectionInfoSelectedEventArgs : EventArgs
    {
        public long MediaIdSelected { get; set; }
    }

    public sealed partial class MediaCollectionInfoControl : UserControl
    {
        public event TypedEventHandler<MediaCollectionInfoControl, MediaCollectionInfoSelectedEventArgs> MediaSelected;

        public static readonly DependencyProperty MediaCollectionProperty =
            DependencyProperty.Register("MediaCollection", typeof(List<Media>), typeof(MediaCollectionInfoControl), new PropertyMetadata(null, OnMediaCollectionChanged));

        public List<Media> MediaCollection
        {
            get { return (List<Media>)GetValue(MediaCollectionProperty); }
            set { SetValue(MediaCollectionProperty, value); }
        }

        public MediaCollectionInfoControl()
        {
            this.InitializeComponent();
        }

        private static void OnMediaCollectionChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            var mediaIdChose = (long)((Control)sender).Tag;

            if (this.MediaSelected != null)
            {
                this.MediaSelected(this, new MediaCollectionInfoSelectedEventArgs() { MediaIdSelected = mediaIdChose });
            }
        }
    }
}
