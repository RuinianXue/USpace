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
        public static void UploadImg(string img_local_path, string target_img_name)
        {
            string AccessKey = Settings.QiniuAccessKey;
            string SecretKey = Settings.QiniuSecretKey;
            Mac mac = new Mac(AccessKey, SecretKey);
            // 上传文件名
            //string key = "hznu/class-img-bed/" + target_img_name;
            string key = target_img_name;
            //string key = "1.jpg";
            // 本地文件路径
            string filePath = img_local_path;
            //string filePath = "D:\\Desktop\\1.jpg";
            // 存储空间名
            string Bucket = "uspace1";
            // 设置上传策略
            PutPolicy putPolicy = new PutPolicy();
            // 设置要上传的目标空间
            putPolicy.Scope = Bucket;
            // 上传策略的过期时间(单位:秒)
            putPolicy.SetExpires(3600);
            //// 文件上传完毕后，在多少天后自动被删除
            //putPolicy.DeleteAfterDays = 1;
            // 生成上传token
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
            Config config = new Config();
            // 设置上传区域
            config.Zone = Zone.ZONE_CN_South;
            // 设置 http 或者 https 上传
            config.UseHttps = true;
            config.UseCdnDomains = true;
            config.ChunkSize = ChunkUnit.U512K;
            // 表单上传
            FormUploader target = new FormUploader(config);
            HttpResult result = target.UploadFile(filePath, key, token, null);
            Console.WriteLine("form upload result: " + result.ToString());
            Growl.Info("form upload result: " + result.ToString());
        }
        public static void DeleteImg(string target_img_name)
        {
            string AccessKey = Settings.QiniuAccessKey;
            string SecretKey = Settings.QiniuSecretKey;
            Mac mac = new Mac(AccessKey, SecretKey);
            // 删除文件名
            //string key = "hznu/class-img-bed/" + target_img_name;
            string key = target_img_name;
            Console.WriteLine(key);
            // 存储空间名
            string Bucket = "uspace1";
            // 设置存储区域
            Config config = new Config();
            config.Zone = Zone.ZONE_CN_South;
            BucketManager bucketManager = new BucketManager(mac, config);
            HttpResult deleteRet = bucketManager.Delete(Bucket, key);
            Console.WriteLine("delete error: " + deleteRet.ToString());
            if (deleteRet.Code != (int)HttpCode.OK)
            {
                Console.WriteLine("delete error: " + deleteRet.ToString());
                //Growl.Error("delete error: " + deleteRet.ToString());
            }
        }
    }
}
