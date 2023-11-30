using System;

namespace UIDisplay.Myscripts
{
    internal class Settings
    {
        // 数据库名
        public static string DatbaseName { get; } = "todo";
        // 数据库所在服务器域名或ip
        public static string DatabaseHost { get; } = "localhost";
        // 数据库端口号
        public static string DatabasePort { get; } = "3306";
        // 数据库用户名
        public static string DatabaseUsername { get; } = "root";
        // 数据库密码
        public static string DatabasePassword { get; } = "271744";

        // 七牛云密钥对
        public static string QiniuAccessKey { get; } = "fUWIthaPBIja3BGrphwZ0guP0lcd3pvDJWMwLbeK";
        public static string QiniuSecretKey { get; } = "c-jGS1qHO3tbY7B8yR-z4pHFs2_KZMxSfYjIZOk4";

        // 邮件服务发送方
        public static string EmailFrom { get; } = "2066892296@qq.com";
        // 邮件服务发送方授权码
        public static string EmailPwd { get; } = "nrzayyqhvyiccjae";
    }
}