using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using arXiv_Crawller.ViewModel;

namespace arXiv_Crawller
{
    /// <summary>
    /// arXivCardWindowDetailed.xaml 的交互逻辑
    /// </summary>
    public partial class arXivCardWindowDetailed : UserControl
    {
        public arXivCardWindowDetailed()
        {
            DataContext = arXivViewModel.Instance;
            InitializeComponent();
        }

        private void OpenLink(object sender, RoutedEventArgs e)
        {
            try
            {
                    string name = (sender as Button).Name.ToString();
                switch (name)
                {
                    case "pdf": OpenInBrowser(arXivViewModel.Instance.Pdf);break;
                    case "doi": OpenInBrowser(arXivViewModel.Instance.Doi);break;
                    case "home": OpenInBrowser(arXivViewModel.Instance.Home);break;
                }
                
            }catch(Exception error)
            {
                MessageBox.Show(error.Message);
                Console.WriteLine(error.Message);
            }
        }

        static void OpenInBrowser(string url)
        {
            try
            {
                // 使用默认的 Web 浏览器打开链接
                Process.Start(url);
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
