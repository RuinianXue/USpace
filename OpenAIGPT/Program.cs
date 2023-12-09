using OpenAIGPT.GPTHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenAIGPT
{
    //使用例
    internal class Program
    {
        static async Task Main(string[] args)
        {
            GPTRequest req01 = new GPTRequest();
           
            await req01.SendTurboRequest("say this is a test");
            Console.WriteLine(req01.LastResponse);

            //为啥我await都没结束主线程先退出了，以下措施用来保险
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
