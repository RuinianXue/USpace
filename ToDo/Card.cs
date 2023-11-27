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
using ConsoleApp2;
using System.Windows.Media.Imaging;
using System.Windows.Media.Effects;
using UIDisplay.Pages;
using UIDisplay.Components;

namespace UIDisplay
{
    public class Card : MaterialDesignThemes.Wpf.Card
    {
        public StackPanel stackPanel;
        public void EventsInitialize()
        {
            MouseEnter += Card_MouseEnter;
            MouseLeave += Card_MouseLeave;
            MouseDown += Card_MouseDown;
            MouseUp += Card_MouseUp;
            MouseMove += Card_MouseMove;
            MouseDoubleClick += Card_DoubleClick;
            //Card_DoubleEvent += this.Parent.Parent.OnEventOfCard_DoubleClick;
        }
        public Card()
        {
            EventsInitialize();
            // Set default values
            //ShadowAssist.SetShadowDepth(this, 0);
            UniformCornerRadius = 15;
            BorderThickness = new Thickness(5);
            BorderBrush = Brushes.White;
            Width = Constants.SMALL_CARD_LENGTH;
            Height = Constants.SMALL_CARD_LENGTH;
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F6F6F8"));
            ContextMenu contextMenu = new ContextMenu();
            #region MenuItem of right click
            MenuItem moveItem = new MenuItem();
            moveItem.Header = "Move Card";
            moveItem.Click += MoveItem_Click;
            contextMenu.Items.Add(moveItem);
            MenuItem deleteItem = new MenuItem();
            deleteItem.Header = "Delete Card";
            deleteItem.Click += DeleteItem_Click;
            contextMenu.Items.Add(deleteItem);
            this.ContextMenu = contextMenu;
            this.MouseRightButtonDown += Card_MouseRightButtonDown;
            #endregion
            Content = stackPanel;
        }
        static BlurMask blurmask= new BlurMask(Dashboard.mainGrid, Dashboard.outGrid);
        protected void Card_DoubleClick(object sender, EventArgs e)
        {
            blurmask.Appear(Dashboard.outGrid);
            ClickCard clickCard = new ClickCard(this);
            clickCard.Appear(Dashboard.overallGrid);
            //clickCard.Show();
            //blurmask.Disappear(Dashboard.outGrid);
            //blurmask.Appear(Dashboard.outGrid);
        }
        private DateTime mouseDownTime;
        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseDownTime = DateTime.Now;
            ((Card)sender).CaptureMouse();
        }
        private void Card_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ((Card)sender).ReleaseMouseCapture();
        }
        //长按进入编辑模式
        private bool Check_EditMode()
        {
            return (DateTime.Now - mouseDownTime).TotalSeconds > 1;
        }
        private int PlaceCardMode(Grid grid,int row, int column)
        { return 1; }
        private void Card_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && Check_EditMode())
            {
                if (Parent is Grid grid)
                {

                    // 获取当前鼠标位置所在的行列
                    Point mousePosition = e.GetPosition(grid);
                    int row = (int)(mousePosition.Y / grid.RowDefinitions[0].ActualHeight);
                    int column = (int)(mousePosition.X / grid.ColumnDefinitions[0].ActualWidth);

                    switch (PlaceCardMode(grid, row, column))
                    {
                        case 1:
                            UnbinAndRebindGrid(grid, row, column);
                            break;
                        case 0:
                            break;
                    } 
                }
            }
        }
        private void UnbinAndRebindGrid(Grid grid, int row, int column)
        {
            grid.Children.Remove(this);
            SetPosition(grid, row, column);
        }
        private void Card_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((Card)sender).ContextMenu.IsOpen = true;
        }

        public void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (this.Parent is Panel panel)
            {
                panel.Children.Remove(this);
            }
        }
        public void MoveItem_Click(object sender, RoutedEventArgs e)
        {
            
        }
        public void SetPosition(Grid grid, int row,int colomn)
        {
            Grid.SetRow(this, row);
            Grid.SetColumn(this, colomn);
            grid.Children.Add(this);
        }
        private void Card_MouseEnter(object sender, MouseEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame()
            {
                Value = Constants.SMALL_CARD_LENGTH,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame()
            {
                Value = Constants.SMALL_CARD_LENGTH + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame1);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame2);

            storyboard.Children.Add(CreatDoubleAnimation(this, "Height", doubleAnimationUsingKeyFrames.Clone()));
            storyboard.Children.Add(CreatDoubleAnimation(this, "Width", doubleAnimationUsingKeyFrames));

            storyboard.Begin();
        }

        private Timeline CreatDoubleAnimation(UIElement uIElement, string propertyPath, DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames)
        {

            Storyboard.SetTarget(doubleAnimationUsingKeyFrames, uIElement);
            Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames, new PropertyPath(propertyPath));
            return doubleAnimationUsingKeyFrames;
        }

        private void Card_MouseLeave(object sender, MouseEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame()
            {
                Value = Constants.SMALL_CARD_LENGTH + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame()
            {
                Value = Constants.SMALL_CARD_LENGTH,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame1);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame2);

            storyboard.Children.Add(CreatDoubleAnimation(this, "Height", doubleAnimationUsingKeyFrames.Clone()));
            storyboard.Children.Add(CreatDoubleAnimation(this, "Width", doubleAnimationUsingKeyFrames));

            storyboard.Begin();
        }
    }
    public class BigRectangleCard :Card
    {
        public BigRectangleCard()
        {
            MouseEnter += Card_MouseEnter;
            MouseLeave += Card_MouseLeave;
            UniformCornerRadius = 15;
            BorderThickness = new Thickness(5);
            BorderBrush = Brushes.White;
            Width = Constants.BIG_CARD_LENGTH;
            Height = Constants.SMALL_CARD_LENGTH;
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F6F6F8"));

            // Create the StackPanel
            //stackPanel = new StackPanel();
            //stackPanel.Margin = new Thickness(10);
        }
        public new void SetPosition(Grid grid, int row, int colomn)
        {
            Grid.SetRow(this, row);
            Grid.SetColumn(this, colomn);
            Grid.SetColumnSpan(this, 2);
            grid.Children.Add(this);
        }
        private void Card_MouseEnter(object sender, MouseEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame()
            {
                Value = Constants.SMALL_CARD_LENGTH,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame()
            {
                Value = Constants.SMALL_CARD_LENGTH + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame1);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame2);

            storyboard.Children.Add(CreatDoubleAnimation(this, "Height", doubleAnimationUsingKeyFrames.Clone()));

            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesWidth = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrameWidth1 = new EasingDoubleKeyFrame()
            {
                Value = Constants.BIG_CARD_LENGTH,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrameWidth2 = new EasingDoubleKeyFrame()
            {
                Value = Constants.BIG_CARD_LENGTH + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFramesWidth.KeyFrames.Add(easingDoubleKeyFrameWidth1);
            doubleAnimationUsingKeyFramesWidth.KeyFrames.Add(easingDoubleKeyFrameWidth2);
            storyboard.Children.Add(CreatDoubleAnimation(this, "Width", doubleAnimationUsingKeyFramesWidth.Clone()));

            storyboard.Begin();
        }

        private Timeline CreatDoubleAnimation(UIElement uIElement, string propertyPath, DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames)
        {

            Storyboard.SetTarget(doubleAnimationUsingKeyFrames, uIElement);
            Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames, new PropertyPath(propertyPath));
            return doubleAnimationUsingKeyFrames;
        }

        private void Card_MouseLeave(object sender, MouseEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame()
            {
                Value = Constants.SMALL_CARD_LENGTH + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame()
            {   
                Value = Constants.SMALL_CARD_LENGTH,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame1);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame2);

            storyboard.Children.Add(CreatDoubleAnimation(this, "Height", doubleAnimationUsingKeyFrames.Clone()));

            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesWidth = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrameWidth1 = new EasingDoubleKeyFrame()
            {
                Value = Constants.BIG_CARD_LENGTH + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrameWidth2 = new EasingDoubleKeyFrame()
            {
                Value = Constants.BIG_CARD_LENGTH,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFramesWidth.KeyFrames.Add(easingDoubleKeyFrameWidth1);
            doubleAnimationUsingKeyFramesWidth.KeyFrames.Add(easingDoubleKeyFrameWidth2);
            storyboard.Children.Add(CreatDoubleAnimation(this, "Width", doubleAnimationUsingKeyFramesWidth.Clone()));
            storyboard.Begin();
        }
    }
    public class BigSquareCard : Card
    {
        public BigSquareCard()
        {
            MouseEnter += Card_MouseEnter;
            MouseLeave += Card_MouseLeave;
            UniformCornerRadius = 15;
            BorderThickness = new Thickness(5);
            BorderBrush = Brushes.White;
            Width = Constants.BIG_CARD_LENGTH;
            Height = Constants.BIG_CARD_LENGTH;
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F6F6F8"));

            // Create the StackPanel
            stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(10);
        }
        public new void SetPosition(Grid grid, int row, int colomn)
        {
            Grid.SetRow(this, row);
            Grid.SetRowSpan(this, 2);
            Grid.SetColumn(this, colomn);
            Grid.SetColumnSpan(this, 2);
            grid.Children.Add(this);
        }

        private void Card_MouseEnter(object sender, MouseEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame()
            {
                Value = Constants.BIG_CARD_LENGTH,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame()
            {
                Value = Constants.BIG_CARD_LENGTH + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame1);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame2);

            storyboard.Children.Add(CreatDoubleAnimation(this, "Height", doubleAnimationUsingKeyFrames.Clone()));
            storyboard.Children.Add(CreatDoubleAnimation(this, "Width", doubleAnimationUsingKeyFrames));

            storyboard.Begin();
        }

        private Timeline CreatDoubleAnimation(UIElement uIElement, string propertyPath, DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames)
        {

            Storyboard.SetTarget(doubleAnimationUsingKeyFrames, uIElement);
            Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames, new PropertyPath(propertyPath));
            return doubleAnimationUsingKeyFrames;
        }

        private void Card_MouseLeave(object sender, MouseEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame()
            {
                Value = Constants.BIG_CARD_LENGTH + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            //if (IsChecked) target_num = 190;
            EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame()
            {
                Value = Constants.BIG_CARD_LENGTH,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame1);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame2);

            storyboard.Children.Add(CreatDoubleAnimation(this, "Height", doubleAnimationUsingKeyFrames.Clone()));
            storyboard.Children.Add(CreatDoubleAnimation(this, "Width", doubleAnimationUsingKeyFrames));

            storyboard.Begin();
        }

    }
    public class ArxivCard : BigSquareCard
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

        public ArxivCard() :base() 
        {
            ArxivArticle arxivArticle = arXivCrawl.GetOneRandomArticle();
            this.Authors = arxivArticle.Authors;
            this.Title = arxivArticle.Title;
            this.Homepage = arxivArticle.Homepage;
            this.Pdfpage = arxivArticle.Pdfpage;
            this.PublishDate = arxivArticle.PublishDate;

            stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(10);

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
            stackPanel.Children.Add(textBoxTitle);

            ContextMenu contextMenu = new ContextMenu();

            // Add menu items for moving and deleting the Card
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

            // Attach the context menu to the Card
            this.ContextMenu = contextMenu;

            Image imageArxiv = new Image();
            BitmapImage bitmapArxiv = new BitmapImage(new Uri("../Images/arxiv.png", UriKind.RelativeOrAbsolute));
            imageArxiv.Source = bitmapArxiv;
            imageArxiv.Width = Constants.SMALL_CARD_LENGTH / 3;
            //imageArxiv.HorizontalAlignment = HorizontalAlignment.Stretch;
            //stackPanel.Children.Add(imageArxiv);

            Content = stackPanel;

            /*
            TextBox textBox2 = new TextBox();
            textBox2.Margin = new Thickness(10);
            textBox2.Text = "Textbox 2";

            TextBox textBox3 = new TextBox();
            textBox3.Margin = new Thickness(10);
            textBox3.Text = "Textbox 3";*/
        }
        private void ChangePapaer_Click(object sender, EventArgs e)
        {
            ArxivArticle arxivArticle = arXivCrawl.GetOneRandomArticle();
            this.Authors = arxivArticle.Authors;
            this.Title = arxivArticle.Title;
            this.Homepage = arxivArticle.Homepage;
            this.Pdfpage = arxivArticle.Pdfpage;
            this.PublishDate = arxivArticle.PublishDate;
            textBoxTitle.Text = this.Title;
        }
    }
    public class ClickCard : MaterialDesignThemes.Wpf.Card
    {
        private Grid gridOfClickCard;
        public ClickCard()
        {
            gridOfClickCard = new Grid();
            gridOfClickCard.Width = Constants.SMALL_CARD_LENGTH * 2;
            gridOfClickCard.Height = Constants.SMALL_CARD_LENGTH * 2; 
            this.Width = Constants.SMALL_CARD_LENGTH * 2;
            this.Height = Constants.SMALL_CARD_LENGTH * 2;
            gridOfClickCard.Children.Add(this);
        }
        public ClickCard(Card fromCard)
        {
            //MouseEnter+=Card_MouseEnter;
            //MouseLeave+=Card_MouseLeave;
            //stackPanel = new StackPanel();
            //stackPanel.Margin = new Thickness(10);
            gridOfClickCard = new Grid();
            Panel.SetZIndex(gridOfClickCard, 1);
            gridOfClickCard.Width = fromCard.Width * 2;
            gridOfClickCard.Height = fromCard.Height * 2;
            this.Width = fromCard.Width * 2;
            this.Height = fromCard.Height * 2;
            BorderThickness = new Thickness(5);
            BorderBrush = Brushes.White;
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F6F6F8"));
            gridOfClickCard.Children.Add(this);

            //Content = stackPanel;
        }
        public void Appear(Grid overallGrid)
        {
            overallGrid.Children.Add(gridOfClickCard);
        }
        public void Disappear(Grid overallGrid)
        {
            if (overallGrid.Children.Contains(gridOfClickCard))
            {
                overallGrid.Children.Remove(gridOfClickCard);
            }
        }
        private void Card_MouseEnter(object sender, MouseEventArgs e)
        {

        }
        private void Card_MouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}