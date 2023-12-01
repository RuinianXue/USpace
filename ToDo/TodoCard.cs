using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UIDisplay.Components;

namespace UIDisplay
{
    internal class TodoCard : BigSquareCard
    {
        public TodoCard()
        {
            stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(10);
            stackPanel.Height = Constants.BIG_CARD_LENGTH-30;
            stackPanel.Width = Constants.BIG_CARD_LENGTH-30;
            TodoList todoList = new TodoList();
            Viewbox vb = new Viewbox();
            vb.Child = todoList;
            stackPanel.Children.Add(vb);
            Content = stackPanel;
        }
    }
}
