using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UIDisplay.Components;
using System.Management;

namespace UIDisplay.Cards
{
    public class BatteryCard: SmallSquareCard
    {
        public BatteryCard() : base()
        {
            MenuInitialize();
            stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(10);
            stackPanel.Height = Constants.SMALL_CARD_LENGTH - 25;
            stackPanel.Width = Constants.SMALL_CARD_LENGTH - 25;
            Battery battery = new Battery();
            Viewbox vb = new Viewbox();
            vb.Child = battery;
            stackPanel.Children.Add(vb);
            Content = stackPanel;
        }
    }
}
