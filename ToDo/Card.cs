using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace UIDisplay
{
    public class Card : MaterialDesignThemes.Wpf.Card
    {
        public Card()
        {
            // Set default values
            //ShadowAssist.SetShadowDepth(this, 0);
            UniformCornerRadius = 15;
            BorderThickness = new Thickness(5);
            BorderBrush = Brushes.White;
            Width = Constants.SQUARE_LENGTH;
            Height = Constants.SQUARE_LENGTH;
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F6F6F8"));


            // Create the StackPanel
            StackPanel stackPanel = new StackPanel();
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
    }
}