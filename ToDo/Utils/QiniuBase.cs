using Microsoft.Win32;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HandyControl.Controls;

namespace UIDisplay.Utils
{
    internal class QiniuBase
    {
        /// <summary>
        /// 上传图片到七牛云存储
        /// </summary>
        /// <param name="img_local_path">本地图片路径</param>
        /// <param name="target_img_name">目标图片名称</param>
        public static void UploadImg(string img_local_path, string target_img_name)
        {
            string AccessKey = Settings.QiniuAccessKey;
            string SecretKey = Settings.QiniuSecretKey;
            Mac mac = new Mac(AccessKey, SecretKey);

            string key = target_img_name;
            string filePath = img_local_path;
            string Bucket = "uspace1";

            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = Bucket;
            putPolicy.SetExpires(3600);
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());

            Config config = new Config();
            config.Zone = Zone.ZONE_CN_South;
            config.UseHttps = true;
            config.UseCdnDomains = true;
            config.ChunkSize = ChunkUnit.U512K;

            FormUploader target = new FormUploader(config);
            HttpResult result = target.UploadFile(filePath, key, token, null);
            Console.WriteLine("form upload result: " + result.ToString());
            Growl.Info("form upload result: " + result.ToString());
        }

        /// <summary>
        /// 获取七牛云存储中图片的访问链接
        /// </summary>
        /// <param name="target_img_name">目标图片名称</param>
        /// <returns>图片的访问链接</returns>
        public static string GetUrl(string target_img_name)
        {
            string AccessKey = Settings.QiniuAccessKey;
            string SecretKey = Settings.QiniuSecretKey;
            Mac mac = new Mac(AccessKey, SecretKey);

            string domain = "http://s4u6u3ckk.hn-bkt.clouddn.com";
            string key = target_img_name;
            string privateUrl = DownloadManager.CreatePrivateUrl(mac, domain, key, 3600);
            return privateUrl;
        }

        /// <summary>
        /// 删除七牛云存储中的图片
        /// </summary>
        /// <param name="target_img_name">目标图片名称</param>
        public static void DeleteImg(string target_img_name)
        {
            string AccessKey = Settings.QiniuAccessKey;
            string SecretKey = Settings.QiniuSecretKey;
            Mac mac = new Mac(AccessKey, SecretKey);

            string key = target_img_name;
            Console.WriteLine(key);
            string Bucket = "uspace1";

            Config config = new Config();
            config.Zone = Zone.ZONE_CN_South;
            BucketManager bucketManager = new BucketManager(mac, config);
            HttpResult deleteRet = bucketManager.Delete(Bucket, key);
            Console.WriteLine("delete error: " + deleteRet.ToString());
            if (deleteRet.Code != (int)HttpCode.OK)
            {
                Console.WriteLine("delete error: " + deleteRet.ToString());
            }
        }
    }
}
