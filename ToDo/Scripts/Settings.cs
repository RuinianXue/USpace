using System;

namespace LinkToDo.Myscripts
{
    internal class Settings
    {
        // 数据库名
        public static string DatbaseName { get; } = "mydatabase";
        // 数据库所在服务器域名或ip
        public static string DatabaseHost { get; } = "localhost";
        // 数据库端口号
        public static string DatabasePort { get; } = "3306";
        // 数据库用户名
        public static string DatabaseUsername { get; } = "root";
        // 数据库密码
        public static string DatabasePassword { get; } = "271744";

        // 七牛云密钥对
        public static string QiniuAccessKey { get; } = "";
        public static string QiniuSecretKey { get; } = "";


        // 邮件服务发送方
        public static string EmailFrom { get; } = "xxxxxxxxx@qq.com";
        // 邮件服务发送方授权码
        public static string EmailPwd { get; } = "ajrtpsdsdsncfdgz";

        //public static string EmailFrom { get; set; }
        //public static string EmailPwd { get; set; }

        //public static string DatbaseName = "mydatabase";

        //public static string DatabaseHost = "localhost";

        //public static string DatabaseUsername = "root";

        //public static string DatabasePassword = "271744";

        //public static string DatabasePort = "3306";

        //public static string QiniuAccessKey { get; set; }
        //public static string QiniuSecretKey { get; set; }

        //// 新增的属性
        //public static string DefaultUserProfileImg { get; set; }
    }
}