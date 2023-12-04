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
    public class BatteryCard: BigSquareCard
    {
        public BatteryCard() : base()
        {
            MenuInitialize();
            stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(10);
            stackPanel.Height = Constants.BIG_CARD_LENGTH - 30;
            stackPanel.Width = Constants.BIG_CARD_LENGTH - 30;
            Battery battery = new Battery();
            Viewbox vb = new Viewbox();
            vb.Child = battery;
            stackPanel.Children.Add(vb);
            Content = stackPanel;
        }

        private void SetBatteryView()
        {
            try
            {
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Battery");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

                foreach (ManagementObject battery in searcher.Get())
                {
                    Console.WriteLine($"电量剩余: {battery["EstimatedChargeRemaining"]}%");
                    Console.WriteLine($"电池状态: {GetBatteryStatus((ushort)battery["BatteryStatus"])}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生错误: {ex.Message}");
            }
        }

        static string GetBatteryStatus(ushort status)
        {
            switch (status)
            {
                case 1:
                    return "正在充电";
                case 2:
                    return "未充电";
                case 3:
                    return "充电完成";
                case 4:
                    return "低电量";
                case 5:
                    return "高电量";
                default:
                    return "未知状态";
            }
        }
    }
}
