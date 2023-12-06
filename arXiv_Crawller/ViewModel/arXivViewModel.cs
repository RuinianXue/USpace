using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAIGPT.GPTHelper;

namespace arXivCrawller.ViewModel
{
    internal class arXivViewModel
    {
        private static arXivViewModel instance;

        public static arXivViewModel Instance { get { if (instance == null) instance = new arXivViewModel();  return instance; } } 

        public arXivViewModel() {
            //初始化viewmodel操作，比如刷新文章，然后赋值给自己的属性
            article = arXivCrawl.GetOneRandomArticle();
        }

        private static ArxivArticle article;

        public static string Title { get; set; }
        public static string Author { get; set; }
        public static string Category { get; set; }
        public static string Summary { get; set;  }
        public static string Date { get; set; }

        public static string Suggestion { get; set; }

        public void RefreshArticle()
        {
            article = arXivCrawl.GetOneRandomArticle();
            RefreshArticleData();
        }

        public void RefreshArticleData()
        {
            Title = article.Title;
            foreach(string a in article.Authors)
            {
                Author += a + "; ";
            }
            foreach(string c in article.Categories)
            {
                Category += c + "; ";  
            }
            Summary = article.Summary;
            Date = article.PublishDate;
            //RefreshSuggestion();
        }

        public void RefreshSuggestion()
        {
            GPTRequest req = new GPTRequest("apikey");
            req.SendTurboRequest("这是一篇论文的摘要，对其进行解释,提取重要的关键词" + Summary);
            Suggestion = req.LastResponse;
        }
    }
}
