using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomatoClock.ViewModel
{
    /// <summary>
    /// 实现 INotifyPropertyChanged 接口的基础通知对象类。
    /// </summary>
    class NotificationObject : INotifyPropertyChanged
    {
        /// <summary>
        /// 属性更改事件。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 触发属性更改事件。
        /// </summary>
        /// <param name="propertyName">属性名称。</param>
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
