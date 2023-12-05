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
            catch (Exception ex)
            {
                return $"获取电池电量失败";
            }
            return string.Empty;
        }

        //static string GetBatteryStatus(ushort status)
        //{
        //    switch (status)
        //    {
        //        case 1:
        //            return "正在充电";
        //        case 2:
        //            return "未充电";
        //        case 3:
        //            return "充电完成";
        //        case 4:
        //            return "低电量";
        //        case 5:
        //            return "高电量";
        //        default:
        //            return "未知状态";
        //    }
        //}
    }

    public class BatteryStatusToStyleConverter : IValueConverter
    {
        //public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    if (value != null && value is string batteryStatus)
        //    {
        //        if (batteryStatus == "正在充电")
        //        {
        //            return new WaveProgressBarStyle { FillColor = Brushes.Green };
        //        }
        //        else if (batteryStatus == "未充电")
        //        {
        //            return new CircleProgressBarStyle { FillColor = Brushes.Red };
        //        }
        //        else
        //        {
        //            return new CircleProgressBarStyle { FillColor = Brushes.Gray };
        //        }
        //    }
        //    return new CircleProgressBarStyle { FillColor = Brushes.Gray };
        //}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is string batteryStatus)
            {
                if (batteryStatus == "正在充电")
                {
                    return new WaveProgressBar();
                }
                else
                {
                    return new CircleProgressBar();
                }
            }
            return new CircleProgressBar();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class WaveProgressBarStyle
    {
        public Brush FillColor { get; set; }
        // Add other properties for WaveProgressBar style
    }

    public class CircleProgressBarStyle
    {
        public Brush FillColor { get; set; }
        // Add other properties for CircleProgressBar style
    }
}
