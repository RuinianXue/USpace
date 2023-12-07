using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shell;
using CefSharp;
using CefSharp.Wpf;
using Markdig;

namespace UIDisplay.Cards
{
    public class MarkdownCard : BigSquareCard
    {
        public string MarkdownContent { get; set; }

        private ChromiumWebBrowser webBrowser;

        public MarkdownCard(string markdownContent) : base()
        {
            RenderMarkdownToHtml(markdownContent);
            InitializeWebBrowser();
        }

        private void InitializeWebBrowser()
        {
            webBrowser = new ChromiumWebBrowser();

            // string htmlContent = "<html><body><h1>Hello, Markdown!</h1></body></html>";
            string htmlFilePath = tempPath;

            LoadLocalHtmlFile(htmlFilePath);

            // Set the web browser as the content of the card
            Content = webBrowser;
        }

        private void LoadLocalHtmlFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                // Convert the file path to a file:// URL
                string fileUrl = new Uri(filePath).AbsoluteUri;

                // Load the HTML file into the web browser
                webBrowser.Load(fileUrl);
            }
            else
            {
                MessageBox.Show("HTML file not found: " + filePath);
            }
        }

        public string tempPath = Path.GetTempPath() + "markdowneditor_temp_0.html";

        private void RenderMarkdownToHtml(string markdownContent)
        {
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            string font = "<link rel=\"preconnect\" href=\"https://fonts.googleapis.com\">\r\n<link rel=\"preconnect\" href=\"https://fonts.gstatic.com\" crossorigin>\r\n<link href=\"https://fonts.googleapis.com/css2?family=Roboto+Slab:wght@400;700&display=swap\" rel=\"stylesheet\">";
            string style = @"<style>
                            body {
                                font-family: 'Roboto Slab', serif; 
                                background-color: #FFFFFF; 
                                border-radius: 5px; 
                                color: #000000;
                                margin: 20px; /* Adjust the body margin as needed */
                            } 
                            /* Add margin to other elements as needed */
                            p {
                                margin-bottom: 10px;
                            }
                            h1, h2, h3, h4, h5, h6 {
                                margin-top: 20px;
                                margin-bottom: 10px;
                            }
                            blockquote {
                                margin: 20px 0 30px; 
                                padding-left: 20px;
                                border-left: 3px solid #FFFFFF
                            }
                            /* Add more styles as needed */
                        </style>";
            var result = Markdown.ToHtml(markdownContent, pipeline);
            Console.WriteLine(font + style + result);

            try
            {
                File.WriteAllText(tempPath, font + style + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }
        }
    }
}
