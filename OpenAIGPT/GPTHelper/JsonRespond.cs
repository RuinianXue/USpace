using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIGPT.GPTHelper
{
    /// <summary>
    /// 定义OpenAI返回的完整 JSON 响应的类。
    /// </summary>
    internal class JsonRespond
    {
        public class Message
        {
            /// <summary>
            /// 表示消息的类。
            /// </summary>
            public string Role { get; set; }
            public string Content { get; set; }
        }

        /// <summary>
        /// 表示选择信息的类。
        /// </summary>
        public class Choice
        {
            public int Index { get; set; }
            public Message Message { get; set; }
            public string FinishReason { get; set; }
        }

        /// <summary>
        /// 表示 API 使用情况的类。
        /// </summary>
        public class Usage
        {
            public int PromptTokens { get; set; }
            public int CompletionTokens { get; set; }
            public int TotalTokens { get; set; }
        }

        /// <summary>
        /// 表示 OpenAI 响应的类。
        /// </summary>
        public class OpenAIResponse
        {
            public string Id { get; set; }
            public string Object { get; set; }
            public long Created { get; set; }
            public string Model { get; set; }
            public List<Choice> Choices { get; set; }
            public Usage Usage { get; set; }
        }

    }
}
