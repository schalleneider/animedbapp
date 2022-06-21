using AnimeDB.Data;
using AnimeDB.Service;
using Microsoft.UI.Xaml;

namespace AnimeDB.App
{
    public partial class App : Application
    {
        public Window Window;
        public SettingsService SettingsService;
        public AnimeDBContext AnimeDBContext;

        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            this.Window = new MainWindow();
            
            this.SettingsService = new SettingsService(this.Window);
            this.SettingsService.SettingsChanged += this.SettingsService_SettingsChanged;

            this.SettingsService.LoadSettings();

            this.InitializeDBContext(this.SettingsService.DatabasePath);

            this.Window.Activate();
        }

        private void InitializeDBContext(string databasePath)
        {
            this.AnimeDBContext = new AnimeDBContext(databasePath);
        }

        private void SettingsService_SettingsChanged(SettingsService sender, SettingsServiceSettingsChangedEventArgs args)
        {
            this.InitializeDBContext(args.DatabasePath);
        }
    }
}
