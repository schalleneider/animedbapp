using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.Foundation;

namespace AnimeDB.App.Controls
{
    public sealed partial class InfoBlockToggleControlToggledEventArgs : EventArgs
    {
        public bool IsOn { get; set; }
    }
    public sealed partial class InfoBlockToggleControl : UserControl
    {
        private bool IsSilentRefresh = false;
        
        public event TypedEventHandler<InfoBlockToggleControl, InfoBlockToggleControlToggledEventArgs> Toggled;

        public static readonly DependencyProperty HeaderProperty = 
            DependencyProperty.Register("Header", typeof(string), typeof(InfoBlockToggleControl), new PropertyMetadata(null, OnHeaderChanged));

        public static readonly DependencyProperty IsToggledProperty =
            DependencyProperty.RegisterAttached("IsToggled", typeof(bool), typeof(InfoBlockToggleControl), new PropertyMetadata(null, OnIsToggledChanged));

        public Visibility HeaderVisibility
        {
            get { return string.IsNullOrEmpty(this.Header) ? Visibility.Collapsed : Visibility.Visible; }
        }

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public bool IsToggled
        {
            get { return (bool)GetValue(IsToggledProperty); }
            set 
            { 
                SetValue(IsToggledProperty, value);
                this.SilentRefreshToggle(value);
            }
        }

        public InfoBlockToggleControl()
        {
            this.InitializeComponent();
        }

        private void SilentRefreshToggle(bool value)
        {
            this.IsSilentRefresh = true;

            this.ToggleSwitchControl.IsOn = value;

            this.IsSilentRefresh = false;
        }

        private static void OnHeaderChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
        }

        private static void OnIsToggledChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            var toggleSwitch = sender as ToggleSwitch;

            if (!this.IsSilentRefresh && this.Toggled != null)
            {
                this.IsToggled = toggleSwitch.IsOn;
                this.Toggled(this, new InfoBlockToggleControlToggledEventArgs { IsOn = toggleSwitch.IsOn });
            }
        }

    }
}
