using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace TomatoClock
{
    internal class AnimationHelper
    {
        public static void CreateRotationAnimation(ClockMainWindow window, RotateTransform transform, TimeSpan duration)
        {
            window.rotateAnimation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = duration,
            };
            transform.BeginAnimation(RotateTransform.AngleProperty, window.rotateAnimation);
        }

        public static void StopRotationAnimation(ClockMainWindow window, RotateTransform transform)
        {
            if (window.rotateAnimation != null)
            {

                // 停止动画
                window.SecondHandRotationTransform.BeginAnimation(RotateTransform.AngleProperty, null);
                window.rotateAnimation = null;

                // 将 Angle 属性设置为 0
                window.SecondHandRotationTransform.Angle = 0;
            }
        }
    }
}
