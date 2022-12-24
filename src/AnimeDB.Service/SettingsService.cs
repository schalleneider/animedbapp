using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace AnimeDB.Service
{
    public class SettingsServiceSettingsChangedEventArgs : EventArgs
    {
        public string DatabasePath { get; set; }

        public List<string> ChannelBlacklistList { get; set; }

        public List<string> ChannelWhitelistList { get; set; }
    }

    public class SettingsService
    {
        public event TypedEventHandler<SettingsService, SettingsServiceSettingsChangedEventArgs> SettingsChanged;
        
        private Window Window;

        private ApplicationDataContainer Container;

        private ApplicationDataCompositeValue Settings;

        public string DatabasePath 
        { 
            get
            {
                return (string)this.Settings["DatabasePath"];
            }
            set
            {
                this.Settings["DatabasePath"] = value;
            } 
        }

        public string ChannelBlacklist
        {
            get
            {
                return (string)this.Settings["ChannelBlacklist"];
            }
            set
            {
                this.Settings["ChannelBlacklist"] = value;
            }
        }

        public string ChannelWhitelist
        {
            get
            {
                return (string)this.Settings["ChannelWhitelist"];
            }
            set
            {
                this.Settings["ChannelWhitelist"] = value;
            }
        }

        public List<string> ChannelBlacklistList { get; set; }

        public List<string> ChannelWhitelistList { get; set; }

        public SettingsService(Window window)
        {
            this.Window = window;
            this.Container = ApplicationData.Current.LocalSettings;

            this.ChannelBlacklistList = new List<string>();
            this.ChannelWhitelistList = new List<string>();
        }

        public void LoadSettings()
        {
            ApplicationDataCompositeValue composite = (ApplicationDataCompositeValue)this.Container.Values["AnimeDBSettings"];

            if (composite == null)
            {
                composite = new ApplicationDataCompositeValue();
                this.Container.Values["AnimeDBSettings"] = composite;
            }

            this.Settings = composite;

            if (!string.IsNullOrEmpty(this.ChannelBlacklist))
            {
                this.ChannelBlacklistList.Clear();
                this.ChannelBlacklistList.AddRange(this.ChannelBlacklist.Split('|'));
            }

            if (!string.IsNullOrEmpty(this.ChannelWhitelist))
            {
                this.ChannelWhitelistList.Clear();
                this.ChannelWhitelistList.AddRange(this.ChannelWhitelist.Split('|'));
            }
        }

        public void SaveSettings()
        {
            this.ChannelBlacklist = string.Join('|', this.ChannelBlacklistList);
            this.ChannelWhitelist = string.Join('|', this.ChannelWhitelistList);

            this.Container.Values["AnimeDBSettings"] = this.Settings;

            if (this.SettingsChanged != null)
            {
                this.SettingsChanged(this, new SettingsServiceSettingsChangedEventArgs() 
                { 
                    DatabasePath = this.DatabasePath,
                    ChannelBlacklistList = this.ChannelBlacklistList,
                    ChannelWhitelistList = this.ChannelWhitelistList
                });
            }
        }

        public async Task<StorageFile> ChooseDatabaseFile()
        {
            var filePicker = new FileOpenPicker();
            var hwnd = WindowNative.GetWindowHandle(this.Window);
            
            InitializeWithWindow.Initialize(filePicker, hwnd);
            
            filePicker.FileTypeFilter.Add(".sqlite3");

            return await filePicker.PickSingleFileAsync();
        }
    }
}
