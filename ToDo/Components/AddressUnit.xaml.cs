using UIDisplay.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using UIDisplay.Model;

namespace UIDisplay.Components
{
    /// <summary>
    /// 表示一个联系人信息的用户界面组件。
    /// </summary>
    public partial class AddressUnit : UserControl
    {
        // 用于存储颜色信息的结构体
        struct MyColor
        {
            public byte r, g, b, a;
            public MyColor(byte r, byte g, byte b, byte a)
            {
                this.r = r;
                this.g = g;
                this.b = b;
                this.a = a;
            }
        }

        private readonly List<MyColor> myColors = new List<MyColor>();
        private readonly int mode = 0;
        public bool IsChecked { get; set; } = false;
        public Contact ContactInfo { get; set; }

        /// <summary>
        /// 初始化 <see cref="AddressUnit"/> 类的新实例。
        /// </summary>
        public AddressUnit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 使用指定的联系人信息和模式初始化 <see cref="AddressUnit"/> 类的新实例。
        /// </summary>
        /// <param name="uI">联系人信息。</param>
        /// <param name="mode">模式。</param>
        public AddressUnit(Contact uI, int mode = 0)
        {
            InitializeComponent();
            myColors.Add(new MyColor(255, 85, 85, 255));
            myColors.Add(new MyColor(102, 221, 209, 255));
            this.mode = mode;
            ContactInfo = uI;
            nameLabel.Text = ContactInfo.Name;
            phoneLabel.Text = ContactInfo.Phone;
            emailLabel.Text = ContactInfo.Email;
            BitmapImage bitmapImage = ContactInfo.GetImg();
            img.Source = bitmapImage;
            bitmapImage.DownloadCompleted += (o, earg) =>
            {
                Console.WriteLine("width: " + bitmapImage.PixelWidth + ",height: " + bitmapImage.PixelHeight);
                Console.WriteLine((double)bitmapImage.PixelWidth / bitmapImage.PixelHeight);
                if ((double)bitmapImage.PixelWidth / bitmapImage.PixelHeight < 1.2)
                {
                    imgBorder.Margin = new Thickness(40, 5, 40, 4);
                }
            };
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            // 鼠标进入时的动画效果
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame()
            {
                Value = mainBorder.Width,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame()
            {
                Value = 195,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame1);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame2);

            storyboard.Children.Add(CreatDoubleAnimation(mainBorder, "Height", doubleAnimationUsingKeyFrames.Clone()));
            storyboard.Children.Add(CreatDoubleAnimation(mainBorder, "Width", doubleAnimationUsingKeyFrames));

            storyboard.Begin();
        }

        private Timeline CreatDoubleAnimation(UIElement uIElement, string propertyPath, DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames)
        {
            Storyboard.SetTarget(doubleAnimationUsingKeyFrames, uIElement);
            Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames, new PropertyPath(propertyPath));
            return doubleAnimationUsingKeyFrames;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            // 鼠标离开时的动画效果
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame()
            {
                Value = 195,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            int target_num = 180;
            if (IsChecked) target_num = 190;
            EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame()
            {
                Value = target_num,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame1);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame2);

            storyboard.Children.Add(CreatDoubleAnimation(mainBorder, "Height", doubleAnimationUsingKeyFrames.Clone()));
            storyboard.Children.Add(CreatDoubleAnimation(mainBorder, "Width", doubleAnimationUsingKeyFrames));

            storyboard.Begin();
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // 鼠标点击时的动画效果
            IsChecked = !IsChecked;
            Console.WriteLine(IsChecked.ToString());
            if (IsChecked)
            {
                Storyboard storyboard = new Storyboard();
                ColorAnimationUsingKeyFrames colorAnimationUsingKeyFrames = new ColorAnimationUsingKeyFrames();
                EasingColorKeyFrame easingColorKeyFrame1 = new EasingColorKeyFrame()
                {
                    Value = new Color()
                    {
                        R = 249,
                        G = 249,
                        B = 249,
                        A = 255
                    },
                    EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseInOut },
                    KeyTime = TimeSpan.FromSeconds(0)
                };
                EasingColorKeyFrame easingColorKeyFrame2 = new EasingColorKeyFrame()
                {
                    Value = new Color()
                    {
                        R = myColors[mode].r,
                        G = myColors[mode].g,
                        B = myColors[mode].b,
                        A = myColors[mode].a
                    },
                    EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseInOut },
                    KeyTime = TimeSpan.FromSeconds(0.5)
                };
                colorAnimationUsingKeyFrames.KeyFrames.Add(easingColorKeyFrame1);
                colorAnimationUsingKeyFrames.KeyFrames.Add(easingColorKeyFrame2);

                storyboard.Children.Add(CreatColorAnimation(mainBorder, "Background.(SolidColorBrush.Color)", colorAnimationUsingKeyFrames));

                storyboard.Begin();
            }
            else
            {
                Storyboard storyboard = new Storyboard();
                ColorAnimationUsingKeyFrames colorAnimationUsingKeyFrames = new ColorAnimationUsingKeyFrames();
                EasingColorKeyFrame easingColorKeyFrame1 = new EasingColorKeyFrame()
                {
                    Value = new Color()
                    {
                        R = myColors[mode].r,
                        G = myColors[mode].g,
                        B = myColors[mode].b,
                        A = myColors[mode].a
                    },

                    EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseInOut },
                    KeyTime = TimeSpan.FromSeconds(0)
                };
                EasingColorKeyFrame easingColorKeyFrame2 = new EasingColorKeyFrame()
                {
                    Value = new Color()
                    {
                        R = 249,
                        G = 249,
                        B = 249,
                        A = 255
                    },
                    EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseInOut },
                    KeyTime = TimeSpan.FromSeconds(0.5)
                };
                colorAnimationUsingKeyFrames.KeyFrames.Add(easingColorKeyFrame1);
                colorAnimationUsingKeyFrames.KeyFrames.Add(easingColorKeyFrame2);

                storyboard.Children.Add(CreatColorAnimation(mainBorder, "Background.(SolidColorBrush.Color)", colorAnimationUsingKeyFrames));

                storyboard.Begin();
            }
        }

        private Timeline CreatColorAnimation(UIElement uIElement, string propertyPath, ColorAnimationUsingKeyFrames colorAnimationUsingKeyFrames)
        {
            Storyboard.SetTarget(colorAnimationUsingKeyFrames, uIElement);
            Storyboard.SetTargetProperty(colorAnimationUsingKeyFrames, new PropertyPath(propertyPath));
            return colorAnimationUsingKeyFrames;
        }
    }
}
