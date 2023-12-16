using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UIDisplay.Components;
using UIDisplay.Pages;
using UIDisplay.Utils;

namespace UIDisplay.Cards
{
    public class TodoCard : BigSquareCard
        //Type 2
    {
        private readonly TodoList todoList;
        public event EventHandler TodoCardDoubleClicked;

        /// <summary>
        /// 设置卡片在网格中的位置
        /// </summary>
        /// <param name="grid">所在的网格</param>
        /// <param name="row">行</param>
        /// <param name="column">列</param>
        public override void SetPosition(Grid grid, int row, int column)
        {
            base.SetPosition(grid, row, column);
            IgnoredCard tmp = new IgnoredCard(this, 2);
            Dashboard.loadDashJson.AddCard(tmp);
            Dashboard.AddNewTodoCard(this);
        }

        /// <summary>
        /// 构造函数，初始化TodoCard
        /// </summary>
        public TodoCard()
        {
            typeOfCard = 2;

            MenuInitialize();
            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollViewer.Height = Constants.BIG_CARD_LENGTH - 30;
            scrollViewer.Width = Constants.BIG_CARD_LENGTH - 30;
            todoList = new TodoList();
            scrollViewer.Content = todoList;
            Content = scrollViewer;

            MouseDoubleClick += Card_DoubleClick;
        }

        /// <summary>
        /// 触发双击事件
        /// </summary>
        protected virtual void DoubleClick()
        {
            TodoCardDoubleClicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 处理卡片双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Card_DoubleClick(object sender, RoutedEventArgs e)
        {
            DoubleClick();
        }

        /// <summary>
        /// 刷新TodoList
        /// </summary>
        public void Refresh()
        {
            todoList.Refresh();
        }
    }
}
