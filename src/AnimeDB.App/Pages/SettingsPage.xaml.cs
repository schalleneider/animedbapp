using AnimeDB.Service;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;

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

            this.RefreshListBoxChannelBlacklist();
            this.RefreshListBoxChannelWhitelist();
        }

        private void RefreshListBoxChannelBlacklist()
        {
            this.ListBoxChannelBlacklist.Items.Clear();

            this.SettingsService.ChannelBlacklistList.ForEach(item => this.ListBoxChannelBlacklist.Items.Add(item));
        }

        private void RefreshListBoxChannelWhitelist()
        {
            this.ListBoxChannelWhitelist.Items.Clear();

            this.SettingsService.ChannelWhitelistList.ForEach(item => this.ListBoxChannelWhitelist.Items.Add(item));
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

        private void ButtonAddChannelBlacklist_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TextBoxAddChannelBlacklist.Text))
            {
                this.SettingsService.ChannelBlacklistList.Add(this.TextBoxAddChannelBlacklist.Text);

                this.SettingsService.SaveSettings();
                this.RefreshPage();

                this.TextBoxAddChannelBlacklist.Text = string.Empty;

                this.ShowMessage("Channel Blacklist list setting updated", InfoBarSeverity.Success);
            }
        }

        private void ButtonRemoveChannelBlacklist_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TextBoxAddChannelBlacklist.Text))
            {
                this.SettingsService.ChannelBlacklistList.Remove(this.TextBoxAddChannelBlacklist.Text);

                this.SettingsService.SaveSettings();
                this.RefreshPage();

                this.TextBoxAddChannelBlacklist.Text = string.Empty;

                this.ShowMessage("Channel Blacklist list setting updated", InfoBarSeverity.Success);
            }
        }

        private void ListBoxChannelBlacklist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListBoxChannelBlacklist.SelectedItem != null)
            {
                this.TextBoxAddChannelBlacklist.Text = this.ListBoxChannelBlacklist.SelectedItem.ToString();
            }
        }

        private void ButtonAddChannelWhitelist_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TextBoxAddChannelWhitelist.Text))
            {
                this.SettingsService.ChannelWhitelistList.Add(this.TextBoxAddChannelWhitelist.Text);

                this.SettingsService.SaveSettings();
                this.RefreshPage();

                this.TextBoxAddChannelWhitelist.Text = string.Empty;

                this.ShowMessage("Channel Whitelist list setting updated", InfoBarSeverity.Success);
            }
        }

        private void ButtonRemoveChannelWhitelist_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TextBoxAddChannelWhitelist.Text))
            {
                this.SettingsService.ChannelWhitelistList.Remove(this.TextBoxAddChannelWhitelist.Text);

                this.SettingsService.SaveSettings();
                this.RefreshPage();

                this.TextBoxAddChannelWhitelist.Text = string.Empty;

                this.ShowMessage("Channel Whitelist list setting updated", InfoBarSeverity.Success);
            }
        }

        private void ListBoxChannelWhitelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListBoxChannelWhitelist.SelectedItem != null)
            {
                this.TextBoxAddChannelWhitelist.Text = this.ListBoxChannelWhitelist.SelectedItem.ToString();
            }
        }
    }  
}
