using System;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace TomatoClock
{
    /// <summary>
    /// 提供动画辅助方法的类。
    /// </summary>
    internal class AnimationHelper
    {
        /// <summary>
        /// 创建旋转动画。
        /// </summary>
        /// <param name="window">动画所属的窗口。</param>
        /// <param name="transform">旋转变换。</param>
        /// <param name="duration">动画持续时间。</param>
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

        /// <summary>
        /// 停止旋转动画。
        /// </summary>
        /// <param name="window">动画所属的窗口。</param>
        /// <param name="transform">旋转变换。</param>
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