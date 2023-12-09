using System;
using System.Windows;

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static OpenAIGPT.GPTHelper.JsonRespond;
using Newtonsoft.Json;



namespace OpenAIGPT.GPTHelper
{
    public class GPTRequest
    {
        private static string apiKey;
        private static readonly string Model = "gpt-3.5-turbo";
        private static readonly string Temperature = "0.7";
        private static readonly string TurboApiUrl = "https://api.openai.com/v1/chat/completions";
        private static readonly string DavinciApiUrl = "https://api.openai.com/v1/engines/text-davinci-003/completions";

        private string reqContent;

        public string LastResponse { get; set; }

        public GPTRequest()
        {
            apiKey = "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";
        }

        private HttpClient CreateHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            return client;
        }

        private async Task<string> SendRequestAsync(string apiUrl, string requestBody)
        {
            using (HttpClient client = CreateHttpClient())
            {
                try
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(apiUrl),
                        Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
                    };

                    HttpResponseMessage response = await client.SendAsync(request);
                    Console.WriteLine(response.StatusCode);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        Console.WriteLine($"错误：{response.StatusCode} - {response.ReasonPhrase}");
                        Console.WriteLine(await response.Content.ReadAsStringAsync()); // 为调试打印响应内容
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"异常：{ex.Message}");
                    return null;
                }
            }
        }

        public async Task SendTurboRequest(string message)
        {
            message = EscapeStringForJson(message);
            message = message.Replace("\"", "");
            reqContent = $"{{\"model\": \"{Model}\", \"messages\": [{{\"role\": \"user\", \"content\": \"{message}\"}}], \"temperature\": {Temperature}}}";
            string responseString = await SendRequestAsync(TurboApiUrl, reqContent);
            Console.WriteLine(responseString);
            LastResponse =JsonToContent(responseString);
        }

        //可以使用davinci但没必要
        public async Task SendDavinciRequest(string message)
        {
            message = EscapeStringForJson(message);
            message = message.Replace("\"", "");
            int maxTokens = 60;
            string requestBody = $"{{\"prompt\": \"{message}\", \"max_tokens\": {maxTokens}}}";
            string responseString = await SendRequestAsync(DavinciApiUrl, requestBody);
            LastResponse= JsonToContent (responseString);
        }

        public string JsonToContent(string jsonString)
        {
            try
            {
                OpenAIResponse myApiResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<OpenAIResponse>(jsonString);

                return myApiResponse.Choices[0].Message.Content;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"ArgumentNullException: {ex.Message}"); return "生成AI建议中…";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}"); return "生成AI建议中…";
            }
        }

        static string EscapeStringForJson(string input)
        {
            return JsonConvert.ToString(input);
        }

    }
}
