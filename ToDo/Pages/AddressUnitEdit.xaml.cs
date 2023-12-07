using UIDisplay.Utils;
using Microsoft.Win32;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using UIDisplay.Model;
using UIDisplay.BLL;

namespace UIDisplay.Pages
{
    /// <summary>
    /// AddressUnitEdit.xaml 的交互逻辑
    /// </summary>
    public partial class AddressUnitEdit : Page
    {
        private readonly Contact contact;
        private readonly int mode;
        private string tmp_img_path { get; set; }
        private AddressbookPage _adbp;
        public AddressUnitEdit()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        /// <param name="contact">contact信息</param>
        /// <param name="mode">模式，默认0（添加新联系人），1（修改信息）</param>
        public AddressUnitEdit(AddressbookPage adbp, Contact contact, int mode = 0)
        {
            InitializeComponent();
            _adbp = adbp;
            this.contact = contact;
            this.mode = mode;
            Init();
        }
        private void Init()
        {
            Refresh();
            if (mode == 0)
            {
                updateBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                insertBtn.Visibility = Visibility.Collapsed;
            }
        }
        private void Refresh()
        {
            nameTextBox.Text = contact.Name;
            phoneTextBox.Text = contact.Phone;
            emailTextBox.Text = contact.Email;
            img.Source = contact.getImg();
        }
        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).GoBack();
        }

        private async void Btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            contact.Name = nameTextBox.Text;
            contact.Phone = phoneTextBox.Text;
            contact.Email = emailTextBox.Text;
            if (tmp_img_path != null && tmp_img_path.Length > 0)
            {
                if (contact.ImgPath != "default.jpg")
                {
                    QiniuBase.DeleteImg(contact.ImgPath);
                }
                contact.ImgPath = IDManager.genUUID() + tmp_img_path.Substring(tmp_img_path.Length - 4);
                Console.WriteLine("Now contact imgpath is: " + contact.ImgPath);
                QiniuBase.UploadImg(tmp_img_path, contact.ImgPath);
            }
            await Task.Run(UploadContactInfo);
            _adbp.IsLoaded = false;
            NavigationService.GetNavigationService(this).GoBack();
        }

        private async void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
            contact.Name = nameTextBox.Text;
            contact.Phone = phoneTextBox.Text;
            contact.Email = emailTextBox.Text;
            if (tmp_img_path != null && tmp_img_path.Length > 0)
            {
                if (contact.ImgPath != "default.jpg")
                {
                    QiniuBase.DeleteImg(contact.ImgPath);
                }
                contact.ImgPath = IDManager.genUUID() + tmp_img_path.Substring(tmp_img_path.Length - 4);
                Console.WriteLine("Now contact imgpath is: " + contact.ImgPath);
                QiniuBase.UploadImg(tmp_img_path, contact.ImgPath);
            }
            await Task.Run(UploadContactInfo);
            _adbp.IsLoaded = false;
            NavigationService.GetNavigationService(this).GoBack();
        }

        private void UploadContactInfo()
        {
            if (mode == 0)
            {
                ContactManager.InsertContact(contact);
            }
            else
            {
                ContactManager.UpdateContact(contact);
            }
        }

        private void Btn_UploadImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "jpg图像|*.jpg|png图像|*.png";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                tmp_img_path = openFileDialog.FileName;
                Console.WriteLine(tmp_img_path);
                img.Source = Contact.LoadImage(tmp_img_path);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadInAnimation(sender);
        }
        private void LoadInAnimation(object sender)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                From = 0.4,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.6),
                DecelerationRatio = 0.6
            };
            DoubleAnimation doubleAnimation2 = new DoubleAnimation()
            {
                From = 50,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.8),
                DecelerationRatio = 0.6
            };
            Storyboard.SetTarget(doubleAnimation, (Page)sender);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(doubleAnimation);
            Storyboard.SetTarget(doubleAnimation2, (Page)sender);
            Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("RenderTransform.(TranslateTransform.Y)"));
            storyboard.Children.Add(doubleAnimation2);
            storyboard.Begin();
        }
    }
}
