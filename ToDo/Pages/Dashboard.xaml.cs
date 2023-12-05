using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UIDisplay.Components;
using UIDisplay.Cards;

namespace UIDisplay.Pages
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection LastHourSeries { get; set; }
        public SeriesCollection LastHourSeries1 { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public static Grid mainGrid = new Grid();
        public static Grid inGrid = new Grid();
        public static Grid outGrid = new Grid();
        public static Grid overallGrid = new Grid();
        private TodoCard todoCard;  //临时写在这，主要每次Load的时候得刷新内容
        public void InitializeDashboard()
        {
            overallGrid.Children.Add(outGrid);
            outGrid.Children.Add(mainGrid);
            inGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f3f3f3")); // 将整个Grid填充为蓝色
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(Constants.INSIDE_WIDTH, GridUnitType.Pixel) });
            mainGrid.Children.Add(inGrid);
            inGrid.ClipToBounds = false;
            inGrid.Margin = new Thickness(Constants.EDGE);
            inGrid.Height = Constants.MAX_ROW * Constants.SQUARE_GRID_LENGTH ;
            inGrid.Width = Constants.MAX_COLOMN * Constants.SQUARE_GRID_LENGTH;
            for (int i=1;i<=Constants.MAX_COLOMN;i++)
            {
                inGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            }
            for (int i = 1; i <= Constants.MAX_ROW; i++)
            {
                inGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(4, GridUnitType.Star) });
            }

            inGrid.ClipToBounds = false;
            Grid.SetColumn(inGrid, 0);
            this.Content = overallGrid;
        }
        /*
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadInAnimation(sender);
        }
        */
        private void LoadInAnimation(object sender)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                From = 0.4,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.6),
                DecelerationRatio = 0.6
            };
            DoubleAnimation doubleAnimation2 = new DoubleAnimation()
            {
                From = 50,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.8),
                DecelerationRatio = 0.6
            };
            Storyboard.SetTarget(doubleAnimation, (Page)sender);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(doubleAnimation);
            Storyboard.SetTarget(doubleAnimation2, (Page)sender);
            Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("RenderTransform.(TranslateTransform.Y)"));
            storyboard.Children.Add(doubleAnimation2);
            storyboard.Begin();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            todoCard.Refresh();
        }

        public void Answer_CardDoubleClick()
        {
            //BlurMask blurMask = new BlurMask(mainGrid,outGrid);
        }
        public static void OnEventOfCard_DoubleClick(object sender, EventArgs e)
        {
            //this.maskGrid.Visibility = Visibility.Visible;
        }
        public void DrawinGridBorder(int i,int j)
        {
            // 创建边框元素
            Border border = new Border();
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1);

            // 创建单元格内容
            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Cell " + (i + 1) + "-" + (j + 1);
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;

            // 将内容添加到边框中
            border.Child = textBlock;

            // 将边框添加到Grid中
            Grid.SetRow(border, i);
            Grid.SetColumn(border, j);
            inGrid.Children.Add(border);
        }
        public Dashboard()
        {
            this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f3f3f3"));
            InitializeComponent();
            this.Height = Constants.INSIDE_HEIGHT;
            this.Width = Constants.INSIDE_WIDTH;
            InitializeDashboard();
            
            BigSquareCard tmpbig1 = new BigSquareCard();
            //tmpbig1.SetPosition(inGrid, 0, 0);
            TomatoCard tmpbig2 = new TomatoCard();
            tmpbig2.SetPosition(inGrid, 0, 2);
            ArxivCard tmp1 = new ArxivCard();
            tmp1.SetPosition(inGrid,0,0);

            //Card tmp2 = new Card();
            //tmp2.SetPosition(inGrid,1,4);
            //BigRectangleCard tmpbig3 = new BigRectangleCard();
            //tmpbig3.SetPosition(inGrid, 2, 0);
            todoCard = new TodoCard();
            todoCard.SetPosition(inGrid, 2, 0);
            todoCard.TodoCardDoubleClicked += TodoDoubleClick;

            WeatherCardSmall weatherCardSmall = new WeatherCardSmall();
            //weatherCardSmall.SetPosition(inGrid, 2, 2);

            WeatherCardBig wtc2 = new WeatherCardBig();
            wtc2.SetPosition(inGrid, 2, 2);

            BatteryCard batteryCardSmall = new BatteryCard();
            //batteryCardSmall.SetPosition(inGrid, 2, 3);
            TomatoRectCard tmpbig4 = new TomatoRectCard();
            //tmpbig4.SetPosition(inGrid, 3, 2);

            /*
            for (int i=1;i<=Constants.MAX_ROW;i++)
            {
                for(int j=1;j<=Constants.MAX_COLOMN;j++)
                {
                    Card tmp = new Card();
                    BigRectangleCard tmpbig = new BigRectangleCard();
                    tmp.SetPosition(inGrid, i-1, j-1);
                    //DrawinGridBorder(i, j);
                }
            }*/
        }

        #region drag and drop
        private UIElement initUE;
        private Point initPt;
        private Popup _dragdropPopup = null;
        private bool isDown = false;
        private double moveOpacity = 0.5;
        private void Element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!isDown) return;
            isDown = false;
            if (this._dragdropPopup != null)
            {
                this._dragdropPopup.IsOpen = false;
                this._dragdropPopup.Child = null;
                this._dragdropPopup = null;
            }
            foreach (UIElement element in inGrid.Children)
            {
                if (element != initUE)
                {
                    element.Opacity = 1;
                }
            }
            initUE.ReleaseMouseCapture();

            double y = e.GetPosition(inGrid).Y;
            double start = 0.0;
            int row = 0;
            foreach (RowDefinition rd in inGrid.RowDefinitions)
            {
                start += rd.ActualHeight;
                if (y < start)
                {
                    break;
                }
                row++;
            }
            double x = e.GetPosition(inGrid).X;
            double cstart = 0.0;
            int column = 0;
            foreach (ColumnDefinition cd in inGrid.ColumnDefinitions)
            {
                cstart += cd.ActualWidth;
                if (x < cstart)
                {
                    break;
                }
                column++;
            }
            var initRow = Grid.GetRow(initUE);
            var initCol = Grid.GetColumn(initUE);
            UIElement uIElement = null;
            if (row != initRow || column != initCol)
            {
                uIElement = GetChildren(inGrid, row, column);
            }
            if (uIElement != null)
            {
                inGrid.Children.Remove(uIElement);
                Grid.SetColumn(initUE, column);
                Grid.SetRow(initUE, row);
                inGrid.Children.Add(uIElement);
                Grid.SetColumn(uIElement, initCol);
                Grid.SetRow(uIElement, initRow);

            }
        }

        private void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isDown)
            {
                isDown = false;
                return;
            }
            initUE = (UIElement)e.Source;
            initUE.CaptureMouse();
            initPt = new Point(e.GetPosition(initUE).X, e.GetPosition(initUE).Y - SystemParameters.CaptionHeight);
            foreach (UIElement element in inGrid.Children)
            {
                if (element != initUE)
                {
                    element.Opacity = moveOpacity;
                }
            }
            CreateDragDropPopup(initUE, e);
            isDown = true;

        }

        private void Element_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (isDown == false) return;
            double y = e.GetPosition(inGrid).Y;
            double x = e.GetPosition(inGrid).X;

            if (_dragdropPopup != null)
            {
                _dragdropPopup.HorizontalOffset = x - initPt.X;
                _dragdropPopup.VerticalOffset = y - initPt.Y;
            }

            double start = 0.0;
            int row = 0;
            foreach (RowDefinition rd in inGrid.RowDefinitions)
            {
                start += rd.ActualHeight;
                if (y < start)
                {
                    break;
                }
                row++;
            }
            double cstart = 0.0;
            int column = 0;
            foreach (ColumnDefinition cd in inGrid.ColumnDefinitions)
            {
                cstart += cd.ActualWidth;
                if (x < cstart)
                {
                    break;
                }
                column++;
            }
            UIElement uIElement = GetChildren(inGrid, row, column);
            foreach (UIElement element in inGrid.Children)
            {
                if (element != initUE && element != uIElement)
                {
                    element.Opacity = moveOpacity;
                }
                else
                {
                    element.Opacity = 1;
                }
            }

        }

        private UIElement GetChildren(Grid grid, int row, int column)
        {
            foreach (UIElement child in grid.Children)
            {
                if (Grid.GetRow(child) == row && Grid.GetColumn(child) == column)
                {
                    return child;
                }
            }
            return null;

        }

        private void CreateDragDropPopup(Visual dragElement, MouseButtonEventArgs e)
        {
            this._dragdropPopup = new Popup();

            Rectangle r = new Rectangle();
            r.Width = ((FrameworkElement)dragElement).ActualWidth;
            r.Height = ((FrameworkElement)dragElement).ActualHeight;
            r.Fill = new VisualBrush(dragElement);
            this._dragdropPopup.Child = r;
            double y = e.GetPosition(inGrid).Y;
            double x = e.GetPosition(inGrid).X;
            _dragdropPopup.HorizontalOffset = x - initPt.X;
            _dragdropPopup.VerticalOffset = y - initPt.Y;
            this._dragdropPopup.IsOpen = true;
        }
        #endregion

        public void TodoDoubleClick(object sender, EventArgs e)
        {
            DoubleClick_Dash();
        }
        public event EventHandler TodoCardDoubleClicked_Dash;
        protected virtual void DoubleClick_Dash()
        {
            TodoCardDoubleClicked_Dash?.Invoke(this, EventArgs.Empty);
        }
    }
}
