using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UIDisplay.Model
{
    public class Contact
    {
        private readonly string IMG_PATH_PREFIX = "http://s4u6u3ckk.hn-bkt.clouddn.com/";
        public string CID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImgPath { get; set; }
        public string UID { get; set; }

        public Contact()
        {
        }

        public Contact(string cid, string name, string phone, string email, string uid, string imgPath = "")
        {
            CID = cid;
            Name = name;
            Phone = phone;
            Email = email;
            UID = uid;
            ImgPath = imgPath;
        }

        public string GetCompeleteImgPath()
        {
            string temp = IMG_PATH_PREFIX + ImgPath + ".jpg";
            return temp;
        }

        public BitmapImage GetImg()
        {
            return new BitmapImage(new Uri(GetCompeleteImgPath()));
        }
        public static BitmapImage LoadImage(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            int byteLength = (int)fileStream.Length;
            byte[] fileBytes = new byte[byteLength];
            fileStream.Read(fileBytes, 0, byteLength);
            fileStream.Close();

            return ByteArrayToBitmapImage(fileBytes);
        }

        // byte[] --> BitmapImage
        public static BitmapImage ByteArrayToBitmapImage(byte[] array)
        {
            using (var ms = new MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                image.Freeze();
                return image;
            }
        }
    }
}
