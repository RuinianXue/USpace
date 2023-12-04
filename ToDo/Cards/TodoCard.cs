﻿using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UIDisplay.Components;
using UIDisplay.Pages;

namespace UIDisplay.Cards
{
    internal class TodoCard : BigSquareCard
    {
        private TodoList todoList;
        public event EventHandler TodoCardDoubleClicked;

        public TodoCard()
        {
            MenuInitialize();
            stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(10);
            stackPanel.Height = Constants.BIG_CARD_LENGTH - 30;
            stackPanel.Width = Constants.BIG_CARD_LENGTH - 30;
            todoList = new TodoList();
            Viewbox vb = new Viewbox();
            vb.Child = todoList;
            stackPanel.Children.Add(vb);
            Content = stackPanel;

            MouseDoubleClick += Card_DoubleClick;
        }
        
        protected virtual void DoubleClick()
        {
            TodoCardDoubleClicked?.Invoke(this, EventArgs.Empty);
        }
        private void Card_DoubleClick(object sender, RoutedEventArgs e)
        {
            DoubleClick();
        }
        public void Refresh()
        {
            todoList.Refresh();
        }
    }
}
