using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Controls.Primitives;
using MySqlX.XDevAPI.Relational;
using HandyControl.Controls;
using System.Data.Common;
using arXiv_Crawller;
using System.Windows.Media.Imaging;
using System.Windows.Media.Effects;
using UIDisplay.Pages;
using UIDisplay.Components;

namespace UIDisplay.Cards
{
    public class ArxivCard : BigSquareCard
    {

        private void NewArticle()
        {
        }
        protected override void MenuInitialize()
        {
            ContextMenu contextMenu = new ContextMenu();
            MenuItem moveItem = new MenuItem();
            moveItem.Header = "Move Card";
            moveItem.Click += base.MoveItem_Click;
            contextMenu.Items.Add(moveItem);

            MenuItem deleteItem = new MenuItem();
            deleteItem.Header = "Delete Card";
            deleteItem.Click += DeleteItem_Click;
            contextMenu.Items.Add(deleteItem);

            MenuItem changeItem = new MenuItem();
            changeItem.Header = "Change Paper";
            changeItem.Click += ChangePapaer_Click;
            contextMenu.Items.Add(changeItem);
            this.ContextMenu = contextMenu;
        }
        public ArxivCard() : base()
        {
            ClickCardInitialize();
            stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(10);
            stackPanel.Height = Constants.BIG_CARD_LENGTH - 30;
            stackPanel.Width = Constants.BIG_CARD_LENGTH - 30;

            arXivCardWindow cw = new arXivCardWindow();
            Viewbox vb = new Viewbox();
            vb.Child = cw;
            stackPanel.Children.Add(vb);
            Content = stackPanel;

            clickCard = new arXivClickCard();
        }

        private void ChangePapaer_Click(object sender, EventArgs e)
        {
            NewArticle();
        }
    }

    public class arXivClickCard : ClickCard
    {
        public arXivClickCard()
        {
            StackPanel sp = new StackPanel();
            sp.Margin = new Thickness(10);
            sp.Height = Constants.BIG_CARD_LENGTH * 2 - 30;
            sp.Width = Constants.BIG_CARD_LENGTH * 2 - 30;
            arXivCardWindowDetailed cw = new arXivCardWindowDetailed();
            Viewbox vb = new Viewbox();
            vb.Child = cw;
            sp.Children.Add(vb);
            Content = sp;
        }
    }
}
