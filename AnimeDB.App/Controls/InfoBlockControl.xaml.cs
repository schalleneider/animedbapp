using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.Drawing;

namespace AnimeDB.App.Controls
{
    public sealed partial class InfoBlockControl : UserControl
    {
        private enum InfoBlockControlStatus : int
        {
            Worst = 0,
            Bad = 1,
            Neutral = 2,
            Good = 3,
            Best = 4
        }

        private Windows.UI.Color WorstColor = Windows.UI.Color.FromArgb(255, 107, 35, 35);
        private Windows.UI.Color BadColor = Windows.UI.Color.FromArgb(255, 163, 82, 51);
        private Windows.UI.Color NeutralColor = Windows.UI.Color.FromArgb(255, 42, 42, 42);
        private Windows.UI.Color GoodColor = Windows.UI.Color.FromArgb(255, 61, 70, 46);
        private Windows.UI.Color BestColor = Windows.UI.Color.FromArgb(255, 0, 102, 61);

        public static readonly DependencyProperty HeaderProperty = 
            DependencyProperty.Register("Header", typeof(string), typeof(InfoBlockControl), new PropertyMetadata(null, OnHeaderChanged));
        
        public static readonly DependencyProperty TextProperty = 
            DependencyProperty.Register("Text", typeof(string), typeof(InfoBlockControl), new PropertyMetadata(null, OnTextChanged));

        public static readonly DependencyProperty StatusProperty =
           DependencyProperty.Register("Status", typeof(int), typeof(InfoBlockControl), new PropertyMetadata(InfoBlockControlStatus.Neutral, OnStatusChanged));

        public static readonly DependencyProperty StatusBrushProperty =
           DependencyProperty.Register("StatusBrush", typeof(Brush), typeof(InfoBlockControl), new PropertyMetadata(null, OnStatusBrushChanged));

        public Visibility HeaderVisibility
        {
            get { return string.IsNullOrEmpty(this.Header) ? Visibility.Collapsed : Visibility.Visible; }
        }

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public int Status
        {
            get { return (int)GetValue(StatusProperty); }
            set 
            { 
                SetValue(StatusProperty, value);

                switch ((InfoBlockControlStatus)value)
                {
                    case InfoBlockControlStatus.Worst:
                        this.StatusBrush =  this.CreateNewBrush(this.NeutralColor, this.WorstColor);
                        break;

                    case InfoBlockControlStatus.Bad:
                        this.StatusBrush = this.CreateNewBrush(this.NeutralColor, this.BadColor);
                        break;

                    case InfoBlockControlStatus.Good:
                        this.StatusBrush = this.CreateNewBrush(this.NeutralColor, this.GoodColor);
                        break;

                    case InfoBlockControlStatus.Best:
                        this.StatusBrush = this.CreateNewBrush(this.NeutralColor, this.BestColor);
                        break;

                    case InfoBlockControlStatus.Neutral:
                    default:
                        this.StatusBrush = this.CreateNewBrush(this.NeutralColor, this.NeutralColor);
                        break;
                }
            }
        }

        public Brush StatusBrush
        {
            get { return (Brush)GetValue(StatusBrushProperty); }
            set { SetValue(StatusBrushProperty, value); }
        }

        public InfoBlockControl()
        {
            this.InitializeComponent();
        }

        private LinearGradientBrush CreateNewBrush(Windows.UI.Color first, Windows.UI.Color second)
        {
            return new LinearGradientBrush()
            {
                StartPoint = new Windows.Foundation.Point(0.5, 0),
                EndPoint = new Windows.Foundation.Point(0.5, 1),
                GradientStops = new GradientStopCollection()
                    {
                        new GradientStop
                        {
                            Color = first,
                            Offset = 0.0
                        },
                        new GradientStop
                        {
                            Color = second,
                            Offset = 1.0
                        }
                    }
            };
        }

        private static void OnHeaderChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
        }

        private static void OnTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
        }

        private static void OnStatusChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
        }

        private static void OnStatusBrushChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
        }
    }
}
