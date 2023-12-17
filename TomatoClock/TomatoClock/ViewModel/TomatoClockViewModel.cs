using System;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using TomatoClock.Command;


/// <summary>
/// 表示番茄时钟的视图模型类。
/// </summary>
namespace TomatoClock.ViewModel
{
    internal class TomatoClockViewModel : NotificationObject
    {
        /// <summary>
        /// 获取 TomatoClockViewModel 的单例实例。
        /// </summary>
        /// <returns>TomatoClockViewModel 的单例实例。</returns>
        private static TomatoClockViewModel instance;

        public static TomatoClockViewModel Instance()
        {
            if (instance == null) { return new TomatoClockViewModel(); }
            return instance;
        }

        private static System.Timers.Timer countTimer = new System.Timers.Timer(1000);

        private static MyCommand _cmdFormLoaded;

        /// <summary>
        /// 在窗体加载完成时执行的命令。
        /// </summary>
        public MyCommand CmdFormLoaded
        {
            get
            {
                if (_cmdFormLoaded == null)
                {
                    _cmdFormLoaded = new MyCommand(new Action<object>(
                        o =>
                        {
                            CurrentTime = MinuterSet * 60;
                            countTimer.Elapsed += TimeCountElapsed;
                        }));
                }
                return _cmdFormLoaded;
            }
        }

        private async Task UpdateCurrentTimeAsync(int newTime)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                CurrentTime = newTime;
            });
        }

        private void TimeCountElapsed(object sender, EventArgs e)
        {
            if (CurrentTime > 0)
            {
                CurrentTime--;
                TimeSpan ts = TimeSpan.FromSeconds(CurrentTime);
                TimeCount = $"{ts.Hours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}";

                // 异步更新 CurrentTime
                _ = UpdateCurrentTimeAsync(CurrentTime);
            }
            else
            {
                countTimer.Stop();
                Ending();
            }
        }

        private void Ending()
        {
            TimeCount = "--:--:--";
            IsStop = true;
            IsCounting = false;

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string audioFilePath = Path.Combine(baseDirectory, "Resources", "finished.wav");

            SoundPlayer player = new SoundPlayer(audioFilePath);

            try
            {
                player.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing sound: " + ex.Message);
            }
        }

        private static int _currentTime;
        /// <summary>
        /// 获取或设置当前剩余时间（以秒为单位）。
        /// </summary>
        public int CurrentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value;
                this.RaisePropertyChanged("CurrentTime");
            }
        }

        private static string _timeCount = "--:--:--";
        /// <summary>
        /// 获取或设置格式化后的当前剩余时间。
        /// </summary>
        public string TimeCount
        {
            get { return _timeCount; }
            set
            {
                _timeCount = value;
                this.RaisePropertyChanged("TimeCount");
            }
        }

        private static MyCommand _cmdStart;
        /// <summary>
        /// 获取启动番茄时钟计时器的命令。
        /// </summary>
        public MyCommand CmdStart
        {
            get
            {
                if (_cmdStart == null)
                {
                    _cmdStart = new MyCommand(new Action<object>(
                        o =>
                        {
                            countTimer.Start();
                            IsCounting = true;
                            IsStop = false;
                        }));
                }
                return _cmdStart;
            }
        }

        private static MyCommand _cmdReset;
        /// <summary>
        /// 获取重置番茄时钟计时器的命令。
        /// </summary>
        public MyCommand CmdReset
        {
            get
            {
                if (_cmdReset == null)
                {
                    _cmdReset = new MyCommand(new Action<object>(
                        o =>
                        {
                            countTimer.Stop();
                            TimeCount = "--:--:--";
                        }));
                }
                return _cmdReset;
            }
        }

        // MinuterSet 属性，用于设置番茄时钟的分钟数
        private static int _minuterSet = 25;
        /// <summary>
        /// 获取或设置番茄时钟的设定分钟数。
        /// </summary>
        public int MinuterSet
        {
            get { return _minuterSet; }
            set
            {
                _minuterSet = value;
                this.RaisePropertyChanged("MinuterSet");
            }
        }

        private static MyCommand _cmdSet;
        /// <summary>
        /// 获取设置番茄时钟分钟数的命令。
        /// </summary>
        public MyCommand CmdSet
        {
            get
            {
                if (_cmdSet == null)
                {
                    Console.WriteLine("设定时间，分钟：" + MinuterSet.ToString());
                    _cmdSet = new MyCommand(new Action<object>(o =>
                    {
                        if (MinuterSet > 0)
                        {
                            CurrentTime = MinuterSet * 60;
                            Console.WriteLine("设定时间完成，秒数：" + CurrentTime.ToString());
                        }
                        else
                        {
                            MinuterSet = 25;
                            CurrentTime = MinuterSet * 60;
                            MessageBox.Show("时间输入错误！");
                        }
                    }));
                }
                return _cmdSet;
            }
        }

        private static MyCommand _cmdStop;
        /// <summary>
        /// 获取停止番茄时钟计时器的命令。
        /// </summary>
        public MyCommand CmdStop
        {
            get
            {
                if (_cmdStop == null)
                {
                    _cmdStop = new MyCommand(new Action<object>(o =>
                    {
                        countTimer.Stop();
                        IsCounting = false;
                        IsStop = true;
                        CurrentTime = MinuterSet * 60;
                        TimeCount = "--:--:--";
                    }));
                }
                return _cmdStop;
            }
        }


        // IsCounting 属性，指示番茄时钟计时器是否正在计时中
        private static bool _isCounting = false;
        public bool IsCounting
        {
            get { return _isCounting; }
            set
            {
                _isCounting = value;
                this.RaisePropertyChanged("IsCounting");
            }
        }

        // IsStop 属性，指示番茄时钟是否处于停止状态
        private static bool _isStop = true;
        public bool IsStop
        {
            get { return _isStop; }
            set
            {
                _isStop = value;
                this.RaisePropertyChanged("IsStop");
            }
        }
    }
}
