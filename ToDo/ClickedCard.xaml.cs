using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UIDisplay
{
    /// <summary>
    /// ClickedCard.xaml 的交互逻辑
    /// </summary>
    public partial class ClickedCard : Window
    {
        public ClickedCard()
        {
            InitializeComponent();
            this.Width = 200;
            this.Height = 200;
        }
        public ClickedCard(Card fromCard)
        {
            InitializeComponent();
            this.Width = fromCard.Width*2;
            this.Height = fromCard.Height*2;
        }
    }
}
