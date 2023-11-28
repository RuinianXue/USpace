using System;
using System.IO;
using System.Media;
using System.Windows;
using TomatoClock.Command;

namespace TomatoClock.ViewModel
{
    internal class TomatoClockViewModel : NotificationObject
    {
        private static TomatoClockViewModel instance;

        public static TomatoClockViewModel Instance()
        {
            if (instance == null) { return new TomatoClockViewModel(); }
            return instance;
        }

        private static System.Timers.Timer countTimer = new System.Timers.Timer(1000);

        private static MyCommand _cmdFormLoaded;

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

        private void TimeCountElapsed(object sender, EventArgs e)
        {
            if (CurrentTime > 0)
            {
                CurrentTime--;
                TimeSpan ts = TimeSpan.FromSeconds(CurrentTime);
                TimeCount = ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds;
            }
            else
            {
                countTimer.Stop();
                Ending();
                TimeCount = "--:--:--";
                IsStop = true;
                IsCounting = false;
            }
        }

        private void Ending()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // 构建音频文件的相对路径
            Console.WriteLine(baseDirectory);
            string audioFilePath = Path.Combine(baseDirectory, "resource", "finished.wav");

            // 创建 SoundPlayer 实例
            SoundPlayer player = new SoundPlayer(audioFilePath);

            try
            {
                player.Play();
            }
            catch (Exception ex)
            {
                // 处理播放异常
                Console.WriteLine("Error playing sound: " + ex.Message);
            }
        }

        private static int _currentTime;
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

        private static int _minuterSet = 25;
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

        private static MyCommand _cmdPause;

        public MyCommand CmdPause
        {
            get
            {
                if(_cmdPause == null)
                {
                    _cmdPause = new MyCommand(new Action<object>(o =>
                    {
                        IsPause = true;
                        countTimer.Stop();
                    }));
                }
                return _cmdPause;
            }
        }

        private static MyCommand _cmdContinue;

        public MyCommand CmdContinue
        {
            get
            {
                if (_cmdContinue == null)
                {
                    _cmdContinue = new MyCommand(new Action<object>(o =>
                    {
                        IsPause = false;
                        countTimer.Start();
                    }));
                }
                return _cmdContinue;
            }
        }

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
        
        private static bool _isPause = false;
        public bool IsPause
        {
            get { return _isPause; }
            set
            {
                _isStop = value;
                this.RaisePropertyChanged("IsP");
            }
        }
    }
}
