using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Management;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.ComponentModel;

namespace UIDisplay.Components
{
    /// <summary>
    /// Battery.xaml 的交互逻辑
    /// </summary>
    public partial class Battery : UserControl
    {
        public string BatteryLevel { get; set; }

        public Battery()
        {
            InitializeComponent();
            DataContext = this;
            BatteryLevel = GetBatteryInfo();
        }

        private string GetBatteryInfo()
        {
            try
            {
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Battery");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

                foreach (ManagementObject battery in searcher.Get())
                {
                    Console.WriteLine($"{battery["EstimatedChargeRemaining"]}");
                    return $"{battery["EstimatedChargeRemaining"]}";
                }
            }
            catch
            {
                return $"获取电池电量失败";
            }
            return string.Empty;
        }
    }
}
