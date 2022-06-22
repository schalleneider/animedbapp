using Microsoft.UI.Xaml;
using System;
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

        public SettingsService(Window window)
        {
            this.Window = window;
            this.Container = ApplicationData.Current.LocalSettings;
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
        }

        public void SaveSettings()
        {
            this.Container.Values["AnimeDBSettings"] = this.Settings;

            if (this.SettingsChanged != null)
            {
                this.SettingsChanged(this, new SettingsServiceSettingsChangedEventArgs() { DatabasePath = this.DatabasePath });
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
