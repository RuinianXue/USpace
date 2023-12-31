﻿using System;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using OpenAIGPT.GPTHelper;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;

namespace arXiv_Crawller.ViewModel
{
    internal class arXivViewModel : INotifyPropertyChanged
    {
        // 单例实例
        private static arXivViewModel instance;

        /// <summary>
        /// 获取 arXivViewModel 的单例实例。
        /// </summary>
        public static arXivViewModel Instance
        {
            get
            {
                if (instance == null) instance = new arXivViewModel();
                return instance;
            }
        }

        // arXiv 文章
        private ArxivArticle article;

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        private string author;
        public string Author
        {
            get { return author; }
            set
            {
                if (author != value)
                {
                    author = value;
                    OnPropertyChanged(nameof(Author));
                }
            }
        }

        private string category;
        public string Category
        {
            get { return category; }
            set
            {
                if (category != value)
                {
                    category = value;
                    OnPropertyChanged(nameof(Category));
                }
            }
        }

        private string summary = "haha";
        public string Summary
        {
            get { return summary; }
            set
            {
                if (summary != value)
                {
                    summary = value;
                    OnPropertyChanged(nameof(Summary));
                }
            }
        }

        private string date;
        public string Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        private string suggestion;
        public string Suggestion
        {
            get { return suggestion; }
            set
            {
                if (suggestion != value)
                {
                    suggestion = value;
                    OnPropertyChanged(nameof(Suggestion));
                }
            }
        }
        
        private string pdf;
        public string Pdf
        {
            get { return pdf; }
            set
            {
                if (pdf != value)
                {
                    pdf = value;
                    OnPropertyChanged(nameof(Pdf));
                }
            }
        }
        
        private string home;
        public string Home
        {
            get { return home; }
            set
            {
                if (home != value)
                {
                    home = value;
                    OnPropertyChanged(nameof(Home));
                }
            }
        }private string doi;
        public string Doi
        {
            get { return doi; }
            set
            {
                if (doi != value)
                {
                    doi = value;
                    OnPropertyChanged(nameof(Doi));
                }
            }
        }

        /// <summary>
        /// INotifyPropertyChanged 接口的 PropertyChanged 事件。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 触发 PropertyChanged 事件。
        /// </summary>
        /// <param name="propertyName">属性名称。</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 私有构造函数，用于初始化 arXivViewModel。
        /// </summary>
        private arXivViewModel()
        {
        }

        /// <summary>
        /// 刷新文章。
        /// </summary>
        public async Task RefreshArticle()
        {
            article = arXivCrawl.GetOneRandomArticle();
            try
            {
                await RefreshArticleData();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 刷新文章数据。
        /// </summary>
        public async Task RefreshArticleData()
        {
            Title = article.Title;
            Author = string.Join("; ", article.Authors);
            Category = string.Join("; ", article.Categories);
            Summary = article.Summary;
            Date = article.PublishDate;
            Pdf = article.Pdfpage;
            Home = article.Homepage;
            Doi = article.Doipage;
            try
            {
                await RefreshSuggestion();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 刷新AI建议。
        /// </summary>
        public async Task RefreshSuggestion()
        {
            GPTRequest req = new GPTRequest();
            try
            {
                await req.SendTurboRequest("以下是一片论文的摘要，用三句话说出你对它的感想，用中文回答。" + Summary);
            }catch(Exception error)
            {
                Console.WriteLine(error.Message);
                Suggestion = "生成AI建议中…";
            }
            Console.WriteLine(req.LastResponse);
            Suggestion = req.LastResponse;
        }

    }
}
