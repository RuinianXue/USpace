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

        //public MainViewModel ViewModel { get; set; }

        //public Battery()
        //{
        //    InitializeComponent();
        //    ViewModel = new MainViewModel();
        //    DataContext = ViewModel;
        //    ViewModel.BatteryLevel = GetBatteryInfo();
        //}

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

    //public class MainViewModel : INotifyPropertyChanged
    //{
    //    private string _batteryLevel;

    //    public string BatteryLevel
    //    {
    //        get { return _batteryLevel; }
    //        set
    //        {
    //            if (_batteryLevel != value)
    //            {
    //                _batteryLevel = value;
    //                OnPropertyChanged(nameof(BatteryLevel));
    //            }
    //        }
    //    }

    //    private BatteryStatus _batteryStatus;

    //    public BatteryStatus BatteryStatus
    //    {
    //        get { return _batteryStatus; }
    //        set
    //        {
    //            if (_batteryStatus != value)
    //            {
    //                _batteryStatus = value;
    //                OnPropertyChanged(nameof(BatteryStatus));
    //            }
    //        }
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    protected virtual void OnPropertyChanged(string propertyName)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //    }
    //}

    //public enum BatteryStatus
    //{
    //    Charging,
    //    Discharging,
    //    Full,
    //    Low,
    //    High,
    //    Unknown
    //}
}
