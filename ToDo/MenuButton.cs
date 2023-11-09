using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkToDo
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
            HorizontalAlignment = HorizontalAlignment.Right;
            BorderThickness = new Thickness(0);
            Background = new SolidColorBrush(Colors.Transparent);
            VerticalAlignment = VerticalAlignment.Center;
            Style = (Style)Application.Current.Resources["MenuIconStyle"];
            Margin = new Thickness(0, 0, 15, 0);

            EventTrigger uncheckedTrigger = new EventTrigger(ToggleButton.UncheckedEvent);
            BeginStoryboard hideStoryboard = new BeginStoryboard();
            Storyboard hideStackPanel = new Storyboard { Name = "HideStackPanel" };
            DoubleAnimationUsingKeyFrames hideAnimation = new DoubleAnimationUsingKeyFrames();
            hideAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(200, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)), new CircleEase { EasingMode = EasingMode.EaseOut }));
            hideAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(60, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5)), new CircleEase { EasingMode = EasingMode.EaseOut }));
            hideStackPanel.Children.Add(hideAnimation);
            hideStoryboard.Storyboard = hideStackPanel;
            uncheckedTrigger.Actions.Add(hideStoryboard);
            Triggers.Add(uncheckedTrigger);

            EventTrigger checkedTrigger = new EventTrigger(ToggleButton.CheckedEvent);
            BeginStoryboard showStoryboard = new BeginStoryboard();
            Storyboard showStackPanel = new Storyboard { Name = "ShowStackPanel" };
            DoubleAnimationUsingKeyFrames showAnimation = new DoubleAnimationUsingKeyFrames();
            showAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(60, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)), new CircleEase { EasingMode = EasingMode.EaseOut }));
            showAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(200, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5)), new CircleEase { EasingMode = EasingMode.EaseOut }));
            showStackPanel.Children.Add(showAnimation);
            showStoryboard.Storyboard = showStackPanel;
            checkedTrigger.Actions.Add(showStoryboard);
            Triggers.Add(checkedTrigger);
        }
    }
}
