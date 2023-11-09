using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIDisplay
{
    using System.Drawing.Printing;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public class MenuButton : ToggleButton
    {
        public MenuButton()
        {
            /*

             */
            HorizontalAlignment = HorizontalAlignment.Right;
            BorderThickness = new Thickness(0);
            Background = Brushes.Transparent;
            VerticalAlignment = VerticalAlignment.Center;
            Style = (Style)Application.Current.Resources["MenuIconStyle"];
            Margin = new Thickness(0, 0, 15, 0);

            EventTrigger uncheckedTrigger = new EventTrigger(ToggleButton.UncheckedEvent);
            BeginStoryboard hideStoryboard = new BeginStoryboard();
            Storyboard hideStackPanel = new Storyboard { Name = "HideStackPanel" };
            DoubleAnimationUsingKeyFrames hideAnimation = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame hideKeyFrame1 = new EasingDoubleKeyFrame { KeyTime = TimeSpan.FromSeconds(0), Value = 200 };
            EasingDoubleKeyFrame hideKeyFrame2 = new EasingDoubleKeyFrame { KeyTime = TimeSpan.FromSeconds(0.5), Value = 60 };
            CircleEase hideEasingFunction = new CircleEase { EasingMode = EasingMode.EaseOut };

            hideKeyFrame1.EasingFunction = hideEasingFunction;
            hideKeyFrame2.EasingFunction = hideEasingFunction;

            hideAnimation.KeyFrames.Add(hideKeyFrame1);
            hideAnimation.KeyFrames.Add(hideKeyFrame2);

            hideStackPanel.Children.Add(hideAnimation);
            hideStoryboard.Storyboard = hideStackPanel;

            uncheckedTrigger.Actions.Add(hideStoryboard);
            Triggers.Add(uncheckedTrigger);

            EventTrigger checkedTrigger = new EventTrigger(ToggleButton.CheckedEvent);
            BeginStoryboard showStoryboard = new BeginStoryboard();
            Storyboard showStackPanel = new Storyboard { Name = "ShowStackPanel" };
            DoubleAnimationUsingKeyFrames showAnimation = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame showKeyFrame1 = new EasingDoubleKeyFrame { KeyTime = TimeSpan.FromSeconds(0), Value = 60 };
            EasingDoubleKeyFrame showKeyFrame2 = new EasingDoubleKeyFrame { KeyTime = TimeSpan.FromSeconds(0.5), Value = 200 };
            CircleEase showEasingFunction = new CircleEase { EasingMode = EasingMode.EaseOut };

            showKeyFrame1.EasingFunction = showEasingFunction;
            showKeyFrame2.EasingFunction = showEasingFunction;

            showAnimation.KeyFrames.Add(showKeyFrame1);
            showAnimation.KeyFrames.Add(showKeyFrame2);

            showStackPanel.Children.Add(showAnimation);
            showStoryboard.Storyboard = showStackPanel;

            checkedTrigger.Actions.Add(showStoryboard);
            Triggers.Add(checkedTrigger);
        }
    }
}
