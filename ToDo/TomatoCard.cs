using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TomatoClock;

namespace UIDisplay
{
    internal class TomatoCard:BigSquareCard
    {
        public TomatoCard()
        {
            stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(10);
            stackPanel.Height = Constants.BIG_CARD_LENGTH - 30;
            stackPanel.Width = Constants.BIG_CARD_LENGTH - 30;
            ClockMainWindow clk = new ClockMainWindow();
            Viewbox vb = new Viewbox();
            vb.Child = clk;
            stackPanel.Children.Add(vb);
            Content = stackPanel;
            Console.WriteLine("A?");
        }
    }
}
