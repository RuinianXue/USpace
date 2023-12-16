using System;

namespace UIDisplay.Model
{
    public class User
    {
        public string UID { get; set; } // 用户唯一标识符
        public string Nickname { get; set; } // 用户昵称
        public DateTime DateOfBirth { get; set; } // 用户出生日期
        public string Email { get; set; } // 用户电子邮箱
        public string Password { get; set; } // 用户密码
        public string JsonFilePath { get; set; } // 用户JSON文件路径
    }
}
