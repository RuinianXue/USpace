using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HandyControl.Controls;
using Calendar = UIDisplay.Components.Calendar;

namespace UIDisplay.Cards
{
    public class CalendarCard: BigSquareCard
    {
        public CalendarCard() : base()
        {
            MenuInitialize();
            stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(10);
            stackPanel.Height = Constants.BIG_CARD_LENGTH - 30;
            stackPanel.Width = Constants.BIG_CARD_LENGTH - 30;
            Calendar calendar = new Calendar();
            Viewbox vb = new Viewbox();
            vb.Child = calendar;
            stackPanel.Children.Add(vb);
            Content = stackPanel;
        }
    }

    public class CalendarRectCard : BigRectangleCard
    {
        public CalendarRectCard()
        {
            MenuInitialize();
            stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(10);
            stackPanel.Height = Constants.BIG_CARD_LENGTH - 30;
            stackPanel.Width = Constants.BIG_CARD_LENGTH - 30;
            CalendarWithClock clk = new CalendarWithClock();
            Viewbox vb = new Viewbox();
            vb.Child = clk;
            stackPanel.Children.Add(vb);
            Content = stackPanel;
        }
    }
}
