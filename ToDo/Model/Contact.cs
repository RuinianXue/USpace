using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UIDisplay.Utils;

namespace UIDisplay.Model
{
    public class Contact
    {
        public string CID { get; set; } // 联系人唯一标识符
        public string Name { get; set; } // 联系人姓名
        public string Phone { get; set; } // 联系人电话
        public string Email { get; set; } // 联系人邮箱
        public string ImgPath { get; set; } // 联系人头像文件路径
        public string UID { get; set; } // 所属用户唯一标识符

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Contact()
        {
        }

        /// <summary>
        /// 构造函数，初始化联系人对象
        /// </summary>
        /// <param name="cid">联系人唯一标识符</param>
        /// <param name="name">联系人姓名</param>
        /// <param name="phone">联系人电话</param>
        /// <param name="email">联系人邮箱</param>
        /// <param name="uid">所属用户唯一标识符</param>
        /// <param name="imgPath">联系人头像文件路径</param>
        public Contact(string cid, string name, string phone, string email, string uid, string imgPath = "")
        {
            CID = cid;
            Name = name;
            Phone = phone;
            Email = email;
            UID = uid;
            ImgPath = imgPath;
        }

        /// <summary>
        /// 获取完整的联系人头像路径（从七牛云获取）
        /// </summary>
        /// <returns>联系人头像的完整路径</returns>
        public string GetCompleteImgPath()
        {
            return QiniuBase.GetUrl(ImgPath);
        }

        /// <summary>
        /// 获取联系人头像的BitmapImage
        /// </summary>
        /// <returns>联系人头像的BitmapImage</returns>
        public BitmapImage GetImg()
        {
            return new BitmapImage(new Uri(GetCompleteImgPath()));
        }

        /// <summary>
        /// 加载本地图片文件并返回BitmapImage对象
        /// </summary>
        /// <param name="fileName">本地图片文件路径</param>
        /// <returns>BitmapImage对象</returns>
        public static BitmapImage LoadImage(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            int byteLength = (int)fileStream.Length;
            byte[] fileBytes = new byte[byteLength];
            fileStream.Read(fileBytes, 0, byteLength);
            fileStream.Close();

            return ByteArrayToBitmapImage(fileBytes);
        }

        /// <summary>
        /// 将字节数组转换为BitmapImage对象
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <returns>BitmapImage对象</returns>
        public static BitmapImage ByteArrayToBitmapImage(byte[] array)
        {
            using (var ms = new MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                image.Freeze();
                return image;
            }
        }
    }
}
