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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DashBoard
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

        public void InitializeDashboard()
        {
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(Constants.INSIDE_WIDTH, GridUnitType.Pixel) });
            //mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2* Constants.BIG_SQUARE, GridUnitType.Pixel) });

            inGrid.Margin = new Thickness(Constants.EDGE);
            inGrid.Height = Constants.MAX_ROW * Constants.BIG_SQUARE ;
            inGrid.Width = Constants.MAX_COLOMN * Constants.BIG_SQUARE;
            inGrid.Background = new SolidColorBrush(Color.FromRgb(246, 246, 248));
            for(int i=1;i<=Constants.MAX_COLOMN;i++)
            {
                inGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            }
            for (int i = 1; i <= Constants.MAX_ROW; i++)
            {
                inGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(4, GridUnitType.Star) });
            }
           /* inGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            inGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            inGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            inGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(4, GridUnitType.Star) });
            inGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(4, GridUnitType.Star) });
            inGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(4, GridUnitType.Star) });
           */
            Grid.SetColumn(inGrid, 0);

            /*
            rightMainStackPanel.Background = Brushes.White;
            Grid.SetColumn(rightMainStackPanel, 1);
            */
            //Grid.SetRow(inGrid, 1);
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
            InitializeComponent();
            this.Height = Constants.INSIDE_HEIGHT;
            this.Width = Constants.INSIDE_WIDTH;
            InitializeDashboard();
            for(int i=1;i<=Constants.MAX_ROW;i++)
            {
                for(int j=1;j<=Constants.MAX_COLOMN;j++)
                {
                    Card tmp = new Card();
                    tmp.SetPosition(inGrid, i-1, j-1);
                    //DrawinGridBorder(i, j);
                }
            }
        }
    }
}
