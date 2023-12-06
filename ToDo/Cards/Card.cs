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
    public class Card : MaterialDesignThemes.Wpf.Card
    {
        protected StackPanel stackPanel;
        protected ClickCard clickCard;
        public int stdHeight;
        public int stdWidth;
        public int row;
        public int colomn;
        protected int typeOfCard;
        protected static int indexOfCard = 0;
        protected void MouseEnterLeaveInitialize()
        {
            MouseEnter += Card_MouseEnter;
            MouseLeave += Card_MouseLeave;
        }
        protected void MouseDownUpInitialize()
        {
            MouseDown += Card_MouseDown;
            MouseUp += Card_MouseUp;
            MouseMove += Card_MouseMove;
        }
        protected virtual void MenuInitialize()
        {
            ContextMenu contextMenu = new ContextMenu();
            //MenuItem moveItem = new MenuItem();
            //moveItem.Header = "Move Card";
            //moveItem.Click += MoveItem_Click;
            //contextMenu.Items.Add(moveItem);
            MenuItem deleteItem = new MenuItem();
            deleteItem.Header = "Delete Card";
            deleteItem.Click += DeleteItem_Click;
            contextMenu.Items.Add(deleteItem);
            this.ContextMenu = contextMenu;
            this.MouseRightButtonDown += Card_MouseRightButtonDown;
        }
        protected virtual void ClickCardInitialize()
        {
            MouseDoubleClick += Card_DoubleClick;
            clickCard = new ClickCard();
            blurmask.MaskClicked += Mask_ClickClose;
        }
        protected void BasicLookInitialize()
        {
            //ShadowAssist.SetShadowDepth(this, 0);
            UniformCornerRadius = 15;
            BorderThickness = new Thickness(5);
            BorderBrush = Brushes.White;
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F6F6F8"));
        }
        public Card()
        {
            typeOfCard = 0;
            MouseEnterLeaveInitialize();
            MouseDownUpInitialize();
            BasicLookInitialize();
            Content = stackPanel;
        }
        protected virtual void Mask_ClickClose(object sender, EventArgs e)
        {
            blurmask.Disappear(Dashboard.outGrid);
            clickCard.Disappear(Dashboard.overallGrid);
        }
        private static BlurMask blurmask= new BlurMask(Dashboard.mainGrid, Dashboard.outGrid);
        protected virtual void Card_DoubleClick(object sender, EventArgs e)
        {
            blurmask.Appear(Dashboard.outGrid);
            clickCard.Appear(Dashboard.overallGrid);
            //clickCard.Show();
            //blurmask.Disappear(Dashboard.outGrid);
            //blurmask.Appear(Dashboard.outGrid);
        }
        public void outGrid_Click(object sender, RoutedEventArgs e)
        {
            blurmask.Disappear(Dashboard.outGrid);
            clickCard.Disappear(Dashboard.overallGrid);
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
            return ((DateTime.Now - mouseDownTime).TotalSeconds > 1) && Dashboard.editmode;
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
            IgnoredCard ignoredCard = new IgnoredCard(this, this.typeOfCard);
            Dashboard.loadDashJson.RemoveCard(ignoredCard);
            this.SetPosition(grid,row,column);
            /*
            Grid.SetRow(this, row);
            Grid.SetColumn(this, colomn);
            this.colomn = column;
            this.row = row;

            if ((int)this.Width == Constants.BIG_CARD_LENGTH)
                Grid.SetColumnSpan(this, 2);
            if ((int)this.Height == Constants.BIG_CARD_LENGTH)
                Grid.SetRowSpan(this, 2);
            grid.Children.Add(this);*/
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
                IgnoredCard ignoredCard = new IgnoredCard(this,this.typeOfCard);
                Dashboard.loadDashJson.RemoveCard(ignoredCard);
            }
        }
        public void MoveItem_Click(object sender, RoutedEventArgs e)
        {
            
        }
        public virtual void SetPosition(Grid grid, int row,int column)
        {
            Grid.SetRow(this, row);
            Grid.SetColumn(this, column);
            this.colomn = column;
            this.row = row;

            if((int)this.Width == Constants.BIG_CARD_LENGTH)
                Grid.SetColumnSpan(this, 2);
            if((int)this.Height == Constants.BIG_CARD_LENGTH)
                Grid.SetRowSpan(this, 2);
            grid.Children.Add(this);
        }
        protected void Card_MouseEnter(object sender, MouseEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesHeight = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame()
            {
                Value = this.stdHeight,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame()
            {
                Value = this.stdHeight + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFramesHeight.KeyFrames.Add(easingDoubleKeyFrame1);
            doubleAnimationUsingKeyFramesHeight.KeyFrames.Add(easingDoubleKeyFrame2);

            storyboard.Children.Add(CreatDoubleAnimation(this, "Height", doubleAnimationUsingKeyFramesHeight.Clone()));

            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesWidth = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrameWidth1 = new EasingDoubleKeyFrame()
            {
                Value = this.stdWidth,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrameWidth2 = new EasingDoubleKeyFrame()
            {
                Value = this.stdWidth + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFramesWidth.KeyFrames.Add(easingDoubleKeyFrameWidth1);
            doubleAnimationUsingKeyFramesWidth.KeyFrames.Add(easingDoubleKeyFrameWidth2);
            storyboard.Children.Add(CreatDoubleAnimation(this, "Width", doubleAnimationUsingKeyFramesWidth.Clone()));

            storyboard.Begin();
        }

        protected Timeline CreatDoubleAnimation(UIElement uIElement, string propertyPath, DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames)
        {

            Storyboard.SetTarget(doubleAnimationUsingKeyFrames, uIElement);
            Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames, new PropertyPath(propertyPath));
            return doubleAnimationUsingKeyFrames;
        }

        protected void Card_MouseLeave(object sender, MouseEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesHeight = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame()
            {
                Value = this.stdHeight + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame()
            {
                Value = this.stdHeight,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFramesHeight.KeyFrames.Add(easingDoubleKeyFrame1);
            doubleAnimationUsingKeyFramesHeight.KeyFrames.Add(easingDoubleKeyFrame2);

            storyboard.Children.Add(CreatDoubleAnimation(this, "Height", doubleAnimationUsingKeyFramesHeight.Clone()));

            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesWidth = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame easingDoubleKeyFrameWidth1 = new EasingDoubleKeyFrame()
            {
                Value = this.stdWidth + 10,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                KeyTime = TimeSpan.FromSeconds(0)
            };
            EasingDoubleKeyFrame easingDoubleKeyFrameWidth2 = new EasingDoubleKeyFrame()
            {
                Value = this.stdWidth,
                EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                KeyTime = TimeSpan.FromSeconds(0.3)
            };
            doubleAnimationUsingKeyFramesWidth.KeyFrames.Add(easingDoubleKeyFrameWidth1);
            doubleAnimationUsingKeyFramesWidth.KeyFrames.Add(easingDoubleKeyFrameWidth2);
            storyboard.Children.Add(CreatDoubleAnimation(this, "Width", doubleAnimationUsingKeyFramesWidth.Clone()));
            storyboard.Begin();
        }
    }
    public class SmallSquareCard :Card
    {
        public SmallSquareCard() :base()
        {
            Width = stdWidth = Constants.SMALL_CARD_LENGTH;
            Height = stdHeight = Constants.SMALL_CARD_LENGTH;
        }
    }
    public class BigRectangleCard :Card
    {
        public BigRectangleCard() :base() 
        {
            Width = stdWidth = Constants.BIG_CARD_LENGTH;
            Height = stdHeight = Constants.SMALL_CARD_LENGTH;
        }
    }
    public class BigSquareCard : Card
    {
        public BigSquareCard()
        {
            Width = stdWidth = Constants.BIG_CARD_LENGTH;
            Height = stdHeight = Constants.BIG_CARD_LENGTH;
        }
    }
}