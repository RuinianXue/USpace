using arXiv_Crawller.ViewModel;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace arXiv_Crawller
{
    /// <summary>
    /// arXivCardWindow.xaml 的交互逻辑
    /// </summary>
    public partial class arXivCardWindow : UserControl
    {
        public arXivCardWindow()
        {
            DataContext = arXivViewModel.Instance;
            
            InitializeComponent();

            Loaded += ArXivCardWindow_Loaded;
        }

        private async void ArXivCardWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            try
            {
                arXivViewModel.Instance.Suggestion = "生成AI建议中… \n可双击查看详情";
                await arXivViewModel.Instance.RefreshArticle();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            arXivViewModel.Instance.Suggestion = "生成AI建议中… \n可双击查看详情";
            await arXivViewModel.Instance.RefreshArticle();
        }

        private void ShowSuggestion(object sender, MouseEventArgs e)
        {
            TitleBox.Effect = new BlurEffect { Radius = 50 };
            SuggestionBox.Visibility = Visibility.Visible;
        }

        private void ShowTitle(object sender, MouseEventArgs e)
        {
            TitleBox.Effect = null;
            SuggestionBox.Visibility = Visibility.Hidden;
        }
    }
}
