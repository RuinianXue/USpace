﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace UIDisplay
{
    public class Card : MaterialDesignThemes.Wpf.Card
    {
        public StackPanel stackPanel;
        public Card()
        {
            MouseEnter += Card_MouseEnter;
            MouseLeave += Card_MouseLeave;
            // Set default values
            //ShadowAssist.SetShadowDepth(this, 0);
            UniformCornerRadius = 15;
            BorderThickness = new Thickness(5);
            BorderBrush = Brushes.White;
            Width = Constants.SMALL_CARD_LENGTH;
            Height = Constants.SMALL_CARD_LENGTH;
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F6F6F8"));


            // Create the StackPanel
            stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(10);

            
            // Create the PackIcon
            PackIcon packIcon = new PackIcon();
            packIcon.Kind = PackIconKind.Twitter;
            packIcon.Foreground = Brushes.LightBlue;
            stackPanel.Children.Add(packIcon);

            // Create the TextBlock for followers count
            TextBlock followersTextBlock = new TextBlock();
            followersTextBlock.FontWeight = FontWeights.SemiBold;
            followersTextBlock.FontSize = 25;
            followersTextBlock.Text = "280K";
            followersTextBlock.Margin = new Thickness(0, 10, 0, 0);
            stackPanel.Children.Add(followersTextBlock);

            // Create the TextBlock for "Followers" label
            TextBlock label = new TextBlock();
            label.FontSize = 12;
            label.Text = "Followers";
            stackPanel.Children.Add(label);

            // Create the PackIcon for ellipsis
            PackIcon ellipsisIcon = new PackIcon();
            ellipsisIcon.Kind = PackIconKind.EllipsisHorizontal;
            ellipsisIcon.HorizontalAlignment = HorizontalAlignment.Right;
            stackPanel.Children.Add(ellipsisIcon);
            
            // Set the Content of the Card to the StackPanel
            Content = stackPanel;
        }
        public void SetPosition(Grid grid, int row,int colomn)
        {
            Grid.SetRow(this, row);
            Grid.SetColumn(this, colomn);
            grid.Children.Add(this);
        }
        private void Card_MouseEnter(object sender, MouseEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame()
            {
                Value = Constants.SMALL_CARD_LENGTH,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame()
            {
                Value = Constants.SMALL_CARD_LENGTH + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame1);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame2);

            storyboard.Children.Add(CreatDoubleAnimation(this, "Height", doubleAnimationUsingKeyFrames.Clone()));
            storyboard.Children.Add(CreatDoubleAnimation(this, "Width", doubleAnimationUsingKeyFrames));

            storyboard.Begin();
        }

        private Timeline CreatDoubleAnimation(UIElement uIElement, string propertyPath, DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames)
        {

            Storyboard.SetTarget(doubleAnimationUsingKeyFrames, uIElement);
            Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames, new PropertyPath(propertyPath));
            return doubleAnimationUsingKeyFrames;
        }

        private void Card_MouseLeave(object sender, MouseEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame()
            {
                Value = Constants.SMALL_CARD_LENGTH + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame()
            {
                Value = Constants.SMALL_CARD_LENGTH,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame1);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame2);

            storyboard.Children.Add(CreatDoubleAnimation(this, "Height", doubleAnimationUsingKeyFrames.Clone()));
            storyboard.Children.Add(CreatDoubleAnimation(this, "Width", doubleAnimationUsingKeyFrames));

            storyboard.Begin();
        }
    }
    public class BigRectangleCard :Card
    {
        public BigRectangleCard()
        {
            MouseEnter += Card_MouseEnter;
            MouseLeave += Card_MouseLeave;
            UniformCornerRadius = 15;
            BorderThickness = new Thickness(5);
            BorderBrush = Brushes.White;
            Width = Constants.BIG_CARD_LENGTH;
            Height = Constants.SMALL_CARD_LENGTH;
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F6F6F8"));

            // Create the StackPanel
            stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(10);
        }
        public new void SetPosition(Grid grid, int row, int colomn)
        {
            Grid.SetRow(this, row);
            Grid.SetColumn(this, colomn);
            Grid.SetColumnSpan(this, 2);
            grid.Children.Add(this);
        }
        private void Card_MouseEnter(object sender, MouseEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame()
            {
                Value = Constants.SMALL_CARD_LENGTH,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame()
            {
                Value = Constants.SMALL_CARD_LENGTH + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame1);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame2);

            storyboard.Children.Add(CreatDoubleAnimation(this, "Height", doubleAnimationUsingKeyFrames.Clone()));

            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesWidth = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrameWidth1 = new EasingDoubleKeyFrame()
            {
                Value = Constants.BIG_CARD_LENGTH,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrameWidth2 = new EasingDoubleKeyFrame()
            {
                Value = Constants.BIG_CARD_LENGTH + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFramesWidth.KeyFrames.Add(easingDoubleKeyFrameWidth1);
            doubleAnimationUsingKeyFramesWidth.KeyFrames.Add(easingDoubleKeyFrameWidth2);
            storyboard.Children.Add(CreatDoubleAnimation(this, "Width", doubleAnimationUsingKeyFramesWidth.Clone()));

            storyboard.Begin();
        }

        private Timeline CreatDoubleAnimation(UIElement uIElement, string propertyPath, DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames)
        {

            Storyboard.SetTarget(doubleAnimationUsingKeyFrames, uIElement);
            Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames, new PropertyPath(propertyPath));
            return doubleAnimationUsingKeyFrames;
        }

        private void Card_MouseLeave(object sender, MouseEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame()
            {
                Value = Constants.SMALL_CARD_LENGTH + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame()
            {   
                Value = Constants.SMALL_CARD_LENGTH,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame1);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame2);

            storyboard.Children.Add(CreatDoubleAnimation(this, "Height", doubleAnimationUsingKeyFrames.Clone()));

            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesWidth = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrameWidth1 = new EasingDoubleKeyFrame()
            {
                Value = Constants.BIG_CARD_LENGTH + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrameWidth2 = new EasingDoubleKeyFrame()
            {
                Value = Constants.BIG_CARD_LENGTH,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFramesWidth.KeyFrames.Add(easingDoubleKeyFrameWidth1);
            doubleAnimationUsingKeyFramesWidth.KeyFrames.Add(easingDoubleKeyFrameWidth2);
            storyboard.Children.Add(CreatDoubleAnimation(this, "Width", doubleAnimationUsingKeyFramesWidth.Clone()));
            storyboard.Begin();
        }
    }
    public class BigSquareCard : Card
    {
        public BigSquareCard()
        {
            MouseEnter += Card_MouseEnter;
            MouseLeave += Card_MouseLeave;
            UniformCornerRadius = 15;
            BorderThickness = new Thickness(5);
            BorderBrush = Brushes.White;
            Width = Constants.BIG_CARD_LENGTH;
            Height = Constants.BIG_CARD_LENGTH;
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F6F6F8"));

            // Create the StackPanel
            stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(10);
        }
        public new void SetPosition(Grid grid, int row, int colomn)
        {
            Grid.SetRow(this, row);
            Grid.SetRowSpan(this, 2);
            Grid.SetColumn(this, colomn);
            Grid.SetColumnSpan(this, 2);
            grid.Children.Add(this);
        }

        private void Card_MouseEnter(object sender, MouseEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame()
            {
                Value = Constants.BIG_CARD_LENGTH,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame()
            {
                Value = Constants.BIG_CARD_LENGTH + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame1);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame2);

            storyboard.Children.Add(CreatDoubleAnimation(this, "Height", doubleAnimationUsingKeyFrames.Clone()));
            storyboard.Children.Add(CreatDoubleAnimation(this, "Width", doubleAnimationUsingKeyFrames));

            storyboard.Begin();
        }

        private Timeline CreatDoubleAnimation(UIElement uIElement, string propertyPath, DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames)
        {

            Storyboard.SetTarget(doubleAnimationUsingKeyFrames, uIElement);
            Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames, new PropertyPath(propertyPath));
            return doubleAnimationUsingKeyFrames;
        }

        private void Card_MouseLeave(object sender, MouseEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame()
            {
                Value = Constants.BIG_CARD_LENGTH + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            //if (IsChecked) target_num = 190;
            EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame()
            {
                Value = Constants.BIG_CARD_LENGTH,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame1);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame2);

            storyboard.Children.Add(CreatDoubleAnimation(this, "Height", doubleAnimationUsingKeyFrames.Clone()));
            storyboard.Children.Add(CreatDoubleAnimation(this, "Width", doubleAnimationUsingKeyFrames));

            storyboard.Begin();
        }
    }
}