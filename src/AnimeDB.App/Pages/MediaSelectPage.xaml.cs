using AnimeDB.App.Controls;
using AnimeDB.Service;
using AnimeDB.Service.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimeDB.App.Pages
{
    public sealed partial class MediaSelectPage : Page
    {
        #region InfoBarStatus
        
        private DispatcherTimer InfoBarStatusTimer;
        
        private void InfoSettingsTimer_Tick(object sender, object e)
        {
            this.InfoBarStatus.IsOpen = false;
            this.InfoBarStatusTimer.Stop();
        }

        private void SetupInfoBarStatusTimer()
        {
            this.InfoBarStatusTimer = new DispatcherTimer();
            this.InfoBarStatusTimer.Interval = new TimeSpan(0, 0, 5);
            this.InfoBarStatusTimer.Tick += this.InfoSettingsTimer_Tick;
        }
        
        private void ShowMessage(string message, InfoBarSeverity severity)
        {
            this.InfoBarStatus.Title = message;
            this.InfoBarStatus.Severity = severity;
            this.InfoBarStatus.IsOpen = true;

            this.InfoBarStatusTimer.Start();
        }

        #endregion

        private AnimeDBService AnimeDBService;

        private List<long> ThemesWithNoDownloadsCollection;
        private long CurrentThemeId;

        private int CurrentIndex;

        public MediaSelectPage()
        {
            this.InitializeComponent();
            this.SetupInfoBarStatusTimer();

            this.AnimeDBService = new AnimeDBService((Application.Current as App)?.AnimeDBContext);

            try
            {
                this.ThemesWithNoDownloadsCollection = this.AnimeDBService.GetThemesWithNoDownloads();
            }
            catch (Exception error)
            {
                this.ShowMessage($"Error loading themes with no downloads: {error.Message}", InfoBarSeverity.Error);
            }

            this.CurrentIndex = 0;

            this.RefreshPage();
        }

        private void RefreshPage()
        {
            if (this.ThemesWithNoDownloadsCollection != null && this.ThemesWithNoDownloadsCollection.Count > 0)
            {
                this.CurrentThemeId = this.ThemesWithNoDownloadsCollection.ElementAt(this.CurrentIndex);

                this.NavigationTextBlock.Text = $"{this.CurrentIndex + 1} of {this.ThemesWithNoDownloadsCollection.Count()}";

                try
                {
                    var mediaSelect = this.AnimeDBService.GetMediaToSelect(this.CurrentThemeId);

                    this.AnimeInfoControl.Anime = mediaSelect.Anime;
                    this.ThemeInfoControl.Theme = mediaSelect.Theme;
                    this.MediaCollectionInfoControl.MediaCollection = mediaSelect.MediaCollection.ToList();
                }
                catch (Exception error)
                {
                    this.ShowMessage($"Error loading medias to select: {error.Message}", InfoBarSeverity.Error);
                }
            }
        }

        private void MediaCollectionInfoControl_MediaSelected(MediaCollectionInfoControl sender, MediaCollectionInfoSelectedEventArgs args)
        {
            try
            {
                var rowsUpdated = this.AnimeDBService.UpdateMediaSelected(this.CurrentThemeId, args.MediaIdSelected);

                if (rowsUpdated > 0)
                {
                    this.ShowMessage($"Media {args.MediaIdSelected} selected.", InfoBarSeverity.Success);
                }

                this.NavigationNextButton_Click(sender, null);
            }
            catch (Exception error)
            {
                this.ShowMessage($"Error updating selected media: {error.Message}", InfoBarSeverity.Error);
            }
        }

        private void ThemeInfoControl_ThemeUnHide(ThemeInfoControl sender, ThemeInfoHideEventArgs args)
        {
            try
            {
                var rowsUpdated = this.AnimeDBService.UpdateThemeHide(this.CurrentThemeId, false);

                if (rowsUpdated > 0)
                {
                    this.ShowMessage($"Theme {this.CurrentThemeId} unhidden.", InfoBarSeverity.Success);
                }

                this.NavigationNextButton_Click(sender, null);
            }
            catch (Exception error)
            {
                this.ShowMessage($"Error updating selected media: {error.Message}", InfoBarSeverity.Error);
            }
        }

        private void ThemeInfoControl_ThemeHide(ThemeInfoControl sender, ThemeInfoHideEventArgs args)
        {
            try
            {
                var rowsUpdated = this.AnimeDBService.UpdateThemeHide(this.CurrentThemeId, true);

                if (rowsUpdated > 0)
                {
                    this.ShowMessage($"Theme {this.CurrentThemeId} hidden.", InfoBarSeverity.Success);
                }

                this.NavigationNextButton_Click(sender, null);
            }
            catch (Exception error)
            {
                this.ShowMessage($"Error updating selected media: {error.Message}", InfoBarSeverity.Error);
            }
        }

        private void NavigationFirstButton_Click(object sender, RoutedEventArgs args)
        {
            if (this.ThemesWithNoDownloadsCollection != null)
            {
                this.CurrentIndex = 0;
                this.RefreshPage();
            }
        }

        private void NavigationPreviousButton_Click(object sender, RoutedEventArgs args)
        {
            if (this.ThemesWithNoDownloadsCollection != null)
            {
                if (this.CurrentIndex > 0)
                {
                    this.CurrentIndex--;
                    this.RefreshPage();
                }
            }
        }

        private void NavigationNextButton_Click(object sender, RoutedEventArgs args)
        {
            if (this.ThemesWithNoDownloadsCollection != null)
            {
                if (this.CurrentIndex < this.ThemesWithNoDownloadsCollection.Count() - 1)
                {
                    this.CurrentIndex++;
                    this.RefreshPage();
                }
            }
        }

        private void NavigationLastButton_Click(object sender, RoutedEventArgs args)
        {
            if (this.ThemesWithNoDownloadsCollection != null)
            {
                this.CurrentIndex = this.ThemesWithNoDownloadsCollection.Count() - 1;
                this.RefreshPage();
            }
        }

        private void Page_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs args)
        {
            if (args.Key == Windows.System.VirtualKey.Left)
            {
                this.NavigationPreviousButton_Click(sender, args);
            }

            if (args.Key == Windows.System.VirtualKey.Right)
            {
                this.NavigationNextButton_Click(sender, args);
            }
        }
    }
}
