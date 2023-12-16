using HandyControl.Controls;
using System;
using System.Management;
using System.Windows;
using System.Windows.Controls;

namespace UIDisplay.Components
{
    /// <summary>
    /// 表示一个显示电池信息的用户界面组件。
    /// </summary>
    public partial class Battery : UserControl
    {
        /// <summary>
        /// 获取或设置电池电量。
        /// </summary>
        public string BatteryLevel { get; set; }

        /// <summary>
        /// 初始化 <see cref="Battery"/> 类的新实例。
        /// </summary>
        public Battery()
        {
            InitializeComponent();
            DataContext = this;
            BatteryLevel = GetBatteryInfo();
        }

        /// <summary>
        /// 获取电池信息。
        /// </summary>
        /// <returns>以字符串形式表示的电池电量。</returns>
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
                return $"无法获取电池电量";
            }
            return string.Empty;
        }
    }
}
