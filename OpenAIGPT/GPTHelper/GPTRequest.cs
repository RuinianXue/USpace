using System;
using System.Windows;

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static OpenAIGPT.GPTHelper.JsonRespond;
using Newtonsoft.Json;
using System.Net;



namespace OpenAIGPT.GPTHelper
{
    /// <summary>
    /// 处理与 OpenAI GPT 进行交互的类。
    /// </summary>
    public class GPTRequest
    {
        // API 访问密钥
        private static string apiKey;

        // GPT 模型和请求的相关信息
        private static readonly string Model = "gpt-3.5-turbo";
        private static readonly string Temperature = "0.7";
        private static readonly string TurboApiUrl = "https://api.openai.com/v1/chat/completions";
        private static readonly string DavinciApiUrl = "https://api.openai.com/v1/engines/text-davinci-003/completions";

        // 请求内容
        private string reqContent;

        /// <summary>
        /// 获取或设置上一次请求的响应内容。
        /// </summary>
        public string LastResponse { get; set; }

        /// <summary>
        /// 初始化 GPTRequest 类的新实例。
        /// </summary>
        public GPTRequest()
        {
            apiKey = "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";
        }

        /// <summary>
        /// 创建并配置带有身份验证头的 HttpClient。
        /// </summary>
        /// <returns>配置好的 HttpClient 实例。</returns>
        private HttpClient CreateHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            return client;
        }

        /// <summary>
        /// 异步发送 HTTP 请求，并返回响应内容。
        /// </summary>
        /// <param name="apiUrl">API 地址。</param>
        /// <param name="requestBody">HTTP 请求的主体内容。</param>
        /// <returns>HTTP 响应内容。</returns>
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
                        throw new HttpStatusException(response.StatusCode, response.ReasonPhrase);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"异常：{ex.Message}");
                    return null;
                }
            }
        }

        /// <summary>
        /// 发送 Turbo 模型的 GPT 请求。
        /// </summary>
        /// <param name="message">用户输入的消息。</param>
        public async Task SendTurboRequest(string message)
        {
            message = EscapeStringForJson(message);
            message = message.Replace("\"", "");
            reqContent = $"{{\"model\": \"{Model}\", \"messages\": [{{\"role\": \"user\", \"content\": \"{message}\"}}], \"temperature\": {Temperature}}}";
            string responseString = await SendRequestAsync(TurboApiUrl, reqContent);
            Console.WriteLine(responseString);
            LastResponse =JsonToContent(responseString);
        }

        /// <summary>
        /// 发送 Davinci 模型的 GPT 请求。
        /// </summary>
        /// <param name="message">用户输入的消息。</param>
        public async Task SendDavinciRequest(string message)
        {
            message = EscapeStringForJson(message);
            message = message.Replace("\"", "");
            int maxTokens = 60;
            string requestBody = $"{{\"prompt\": \"{message}\", \"max_tokens\": {maxTokens}}}";
            string responseString = await SendRequestAsync(DavinciApiUrl, requestBody);
            LastResponse= JsonToContent (responseString);
        }

        /// <summary>
        /// 将 JSON 字符串转换为 GPT 响应的内容。
        /// </summary>
        /// <param name="jsonString">包含 GPT 响应的 JSON 字符串。</param>
        /// <returns>GPT 响应的内容。</returns>
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

        /// <summary>
        /// 将输入字符串进行 JSON 转义。
        /// </summary>
        /// <param name="input">需要进行转义的输入字符串。</param>
        /// <returns>转义后的 JSON 字符串。</returns>
        static string EscapeStringForJson(string input)
        {
            return JsonConvert.ToString(input);
        }

    }
}

    /// <summary>
    /// 表示 HTTP 状态异常的类。
    /// </summary>
    public class HttpStatusException : Exception
    {
        /// <summary>
        /// 获取 HTTP 状态码。
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// 获取与 HTTP 异常相关的原因。
        /// </summary>
        public string Reason { get; }

        /// <summary>
        /// 初始化 HttpStatusException 类的新实例。
        /// </summary>
        /// <param name="statusCode">HTTP 状态码。</param>
        public HttpStatusException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// 初始化 HttpStatusException 类的新实例。
        /// </summary>
        /// <param name="statusCode">HTTP 状态码。</param>
        /// <param name="message">异常消息。</param>
        public HttpStatusException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// 初始化 HttpStatusException 类的新实例。
        /// </summary>
        /// <param name="statusCode">HTTP 状态码。</param>
        /// <param name="message">异常消息。</param>
        /// <param name="innerException">内部异常。</param>
        public HttpStatusException(HttpStatusCode statusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
}
