using System;

namespace UIDisplay.Utils
{
    internal class IDGenerator
    {
        /// <summary>
        /// 生成UUID（通用唯一标识符）
        /// </summary>
        /// <returns>生成的UUID字符串</returns>
        public static string GenUUID()
        {
            Guid myUUId = Guid.NewGuid();
            string convertedUUID = myUUId.ToString();
            Console.WriteLine("Current TID is: " + convertedUUID);
            return convertedUUID;
        }
    }
}
