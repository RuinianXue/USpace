using arXiv_Crawller;
using arXiv_Crawller.ViewModel;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

//使用例
class Program
{


    static async Task Main(string[] args)
    {
        await arXivViewModel.Instance.RefreshArticle();
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();

    }
}