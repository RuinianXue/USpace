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
using arXivCrawller;
using System.Windows.Media.Imaging;
using System.Windows.Media.Effects;
using UIDisplay.Pages;
using UIDisplay.Components;
using UIDisplay.Utils;
namespace UIDisplay.Cards
{
    public class ArxivCard : BigSquareCard
        //Type 0
    {
        public List<string> Authors { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public List<string> Categories { get; set; }
        public string Homepage { get; set; }
        public string Pdfpage { get; set; }
        public string Doipage { get; set; }
        public string PublishDate { get; set; }
        public string UpdateDate { get; set; }
        public string Publish { get; set; }

        TextBlock textBoxTitle;
        public override void SetPosition(Grid grid, int row, int colomn)
        {
            base.SetPosition(grid, row, colomn);
            IgnoredCard tmp = new IgnoredCard(this,0);
            Dashboard.loadDashJson.AddCard(tmp);
        }

        private void NewArticle()
        {
            ArxivArticle arxivArticle = arXivCrawl.GetOneRandomArticle();
            this.Authors = arxivArticle.Authors;
            this.Title = arxivArticle.Title;
            this.Homepage = arxivArticle.Homepage;
            this.Pdfpage = arxivArticle.Pdfpage;
            this.PublishDate = arxivArticle.PublishDate;
        }
        private void TitleInitialize()
        {
            //Viewbox viewbox = new Viewbox();
            //TextBlock textBlock = new TextBlock();
            //viewbox.Width = Constants.BIG_CARD_LENGTH - 30;
            //viewbox.Height = Constants.BIG_CARD_LENGTH - 30;
            //textBlock.Text = "Your text goes here";
            //YourGrid.Children.Add(viewbox);
            textBoxTitle = new TextBlock();
            textBoxTitle.Margin = new Thickness(10);
            textBoxTitle.Text = this.Title;
            textBoxTitle.Width = Constants.BIG_CARD_LENGTH - 35;
            textBoxTitle.Height = Constants.BIG_CARD_LENGTH - 35;
            textBoxTitle.TextWrapping = TextWrapping.Wrap;
            textBoxTitle.FontWeight = FontWeights.Bold;
            textBoxTitle.FontFamily = new FontFamily("Arial Black");
            textBoxTitle.FontSize = 25;
            //double newFontSize = (textBoxTitle.Width * textBoxTitle.Height) / (textBoxTitle.Text.Length * 2);
            //textBoxTitle.FontSize = newFontSize;
            //viewbox.Child = textBoxTitle;
            //viewbox.Stretch = Stretch.Uniform;
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
            NewArticle();

            stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(10);
            TitleInitialize();
            stackPanel.Children.Add(textBoxTitle);
            MenuInitialize();
            /*
            Image imageArxiv = new Image();
            BitmapImage bitmapArxiv = new BitmapImage(new Uri("../Images/arxiv.png", UriKind.RelativeOrAbsolute));
            imageArxiv.Source = bitmapArxiv;
            imageArxiv.Width = Constants.SMALL_CARD_LENGTH / 3;
            imageArxiv.HorizontalAlignment = HorizontalAlignment.Stretch;
            stackPanel.Children.Add(imageArxiv);
            */
            Content = stackPanel;
        }
        private void ChangePapaer_Click(object sender, EventArgs e)
        {
            NewArticle();
            textBoxTitle.Text = this.Title;
        }
    }
}
