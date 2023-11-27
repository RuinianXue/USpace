using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows;
using UIDisplay.Pages;

namespace UIDisplay.Components
{
    internal class BlurMask
    {
        private Grid gridOfMask;
        private Border border;
        private VisualBrush brush;
        private RectangleGeometry clipGeometry;
        public event EventHandler MaskClicked;

        public bool windowClosed;
        public bool WindowClosed
        {
            get { return windowClosed; }
            set
            {
                if(value == true)
                {
                    BlurClickedtoClose();
                }
                windowClosed = value;
            }
        }

        protected virtual void BlurClickedtoClose()
        {
            MaskClicked?.Invoke(this, EventArgs.Empty);
        }
        public BlurMask(Grid inGrid, Grid outGrid)
        {
            WindowClosed = false;
            gridOfMask = new Grid();
            gridOfMask.MouseLeftButtonDown += gridOfMask_Click;
            gridOfMask.Width = inGrid.Width;
            gridOfMask.Height = inGrid.Height;
            gridOfMask.Margin = new Thickness(-100, -100, -100, -100);
            Panel.SetZIndex(gridOfMask, 1);

            border = new Border();
            brush = new VisualBrush();
            brush.Visual = inGrid;
            brush.Stretch = Stretch.Uniform;
            border.Background = brush;

            border.Effect = new BlurEffect()
            {
                Radius = 100,
                RenderingBias = RenderingBias.Performance
            };

            border.Margin = new Thickness(-gridOfMask.Margin.Left, -gridOfMask.Margin.Top, 0, 0);
            border.ClipToBounds = true;
            clipGeometry = new RectangleGeometry(new Rect(-100, -30, 1000, 1000)); // 创建一个100x100的矩形裁剪区域
            border.Clip = clipGeometry; // 将矩形裁剪区域赋值给Border的Clip属性
            gridOfMask.Children.Add(border);
            //outGrid.Children.Clear();
            //outGrid.Children.Add(gridOfMask);
        }
        public void Appear(Grid outGrid)
        {
            outGrid.Children.Add(gridOfMask);
        }
        public void Disappear(Grid outGrid)
        {
            if (outGrid.Children.Contains(gridOfMask))
            {
                outGrid.Children.Remove(gridOfMask);
            }
        }
        public void gridOfMask_Click(object sender, RoutedEventArgs e)
        {
            WindowClosed = true;
        }
    }
}
