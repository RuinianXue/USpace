using System;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using TomatoClock.ViewModel;

namespace TomatoClock
{
    public partial class ClockMainWindow : UserControl
    {


        private bool isExecutingCommand = false;

        public DoubleAnimation rotateAnimation;
        public ClockMainWindow()
        {
            InitializeComponent();
            DataContext = TomatoClockViewModel.Instance();
        }

        public double fontSize
        {
            get { return plus.FontSize; }
            set { minus.FontSize = value; plus.FontSize = value; minute.FontSize = value; }
        }
        

        private void MouseEnter_ShowInfo(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Clocking.Effect = new BlurEffect { Radius = 20 };
            HiddenComponents.Visibility = Visibility.Visible;
            if (TomatoClockViewModel.Instance().IsStop)
            {
                LastTime.Visibility = Visibility.Visible;
                ThisTime.Visibility = Visibility.Hidden;
            }else{
                LastTime.Visibility = Visibility.Hidden;
                ThisTime.Visibility = Visibility.Visible;
            }
            HiddenComponents.BeginAnimation(UIElement.OpacityProperty, new DoubleAnimation(1, TimeSpan.FromSeconds(0.5)));
        }

        private void MouseLeave_HideInfo(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Clocking.Effect = null;
            HiddenComponents.BeginAnimation(UIElement.OpacityProperty, new DoubleAnimation(0, TimeSpan.FromSeconds(0.5)));
            HiddenComponents.Visibility = Visibility.Hidden;
        }


        private void StartCount(object sender, RoutedEventArgs e)
        {
            State.Content = "▢";
            Console.WriteLine("start");
            TomatoClockViewModel.Instance().CmdSet.Execute(null);
            AnimationHelper.CreateRotationAnimation(this,SecondHandRotationTransform, TimeSpan.FromSeconds(TomatoClockViewModel.Instance().CurrentTime));
            TomatoClockViewModel.Instance().CmdStart.Execute(null);
        }

        private void FinishCount(object sender, RoutedEventArgs e)
        {
            State.Content = "▷";
            AnimationHelper.StopRotationAnimation(this,SecondHandRotationTransform);
            Console.WriteLine("stop");
            TomatoClockViewModel.Instance().CmdStop.Execute(null);
        }

        private void MinusTime(object sender, RoutedEventArgs e)
        {
            int min = int.Parse(TimeSet.Text);

            if (min - 5 >= 0)
                TimeSet.Text = ( min - 5).ToString();
        }

        private void PlusTime(object sender, RoutedEventArgs e)
        {
            int min = int.Parse(TimeSet.Text);

            if (min + 5 <= 120)
                TimeSet.Text = (min + 5).ToString();
        }
    }
}
