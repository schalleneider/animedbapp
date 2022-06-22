using AnimeDB.Service;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace AnimeDB.App.Pages
{
    public sealed partial class SettingsPage : Page
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
        
        private SettingsService SettingsService;

        public SettingsPage()
        {
            this.InitializeComponent();
            this.SetupInfoBarStatusTimer();

            this.SettingsService = (Application.Current as App)?.SettingsService;

            this.RefreshPage();
        }

        private void RefreshPage()
        {
            this.TextBoxDatabasePath.Text = this.SettingsService.DatabasePath;
        }
        
        private async void ButtonChooseDatabasePath_Click(object sender, RoutedEventArgs e)
        {
            var file = await this.SettingsService.ChooseDatabaseFile();

            if (file != null)
            {
                this.SettingsService.DatabasePath = file.Path;

                this.SettingsService.SaveSettings();
                this.RefreshPage();

                this.ShowMessage("Database path setting updated", InfoBarSeverity.Success);
            }
        }
    }
}
