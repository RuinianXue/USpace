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
    public class ClickCard : MaterialDesignThemes.Wpf.Card
    {
        private Grid gridOfClickCard;
        public ClickCard()
        {
            //MouseEnter+=Card_MouseEnter;
            //MouseLeave+=Card_MouseLeave;
            //stackPanel = new StackPanel();
            //stackPanel.Margin = new Thickness(10);
            gridOfClickCard = new Grid();
            //InitializeMenu();
            Panel.SetZIndex(gridOfClickCard, 1);
            gridOfClickCard.Width = this.Width = Constants.BIG_CARD_LENGTH * 2;
            gridOfClickCard.Height = this.Height = Constants.BIG_CARD_LENGTH * 2;
            BorderThickness = new Thickness(5);
            BorderBrush = Brushes.White;
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F6F6F8"));
            gridOfClickCard.Children.Add(this);

            //Content = stackPanel;
        }
        public void Appear(Grid overallGrid)
        {
            overallGrid.Children.Add(gridOfClickCard);
        }
        public void Disappear(Grid overallGrid)
        {
            if (overallGrid.Children.Contains(gridOfClickCard))
            {
                overallGrid.Children.Remove(gridOfClickCard);
            }
        }
    }
}
