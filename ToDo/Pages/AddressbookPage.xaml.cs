using UIDisplay.Components;
using UIDisplay.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using UIDisplay.Model;
using UIDisplay.BLL;

namespace UIDisplay.Pages
{
    /// <summary>
    /// AddressbookPage.xaml 的交互逻辑
    /// </summary>
    public partial class AddressbookPage : Page
    {
        public bool isLoaded { get; set; } = false;
        public AddressbookPage()
        {
            InitializeComponent();
        }
        public void Refresh()
        {
            DataTable dt;
            bool success = ContactManager.SearchAllContact(out dt);

            if (success)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    wrapPanel.Children.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        Contact userInfo = new Contact(
                            row["uuid"].ToString(),
                            row["name"].ToString(),
                            row["phone"].ToString(),
                            row["email"].ToString(),
                            row["imgpath"].ToString()
                        );

                        AddressUnit addressUnit = new AddressUnit(userInfo);
                        wrapPanel.Children.Add(addressUnit);
                    }
                }));
            }
            else
            {
                // 处理搜索失败的情况，可能记录日志或者显示错误消息
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadInAnimation(sender);
            if (!isLoaded)
            {
                isLoaded = true;
                await Task.Run(Refresh);
            }
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

        private void insertpersonBtn_Click(object sender, RoutedEventArgs e)
        {
            Contact userInfo = new Contact(Contact.genUUID(), "", "", "", "default.jpg");
            AddressUnitEdit addressUnitEdit = new AddressUnitEdit(this, userInfo);
            NavigationService.GetNavigationService(this).Navigate(addressUnitEdit);
        }

        private async void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(Refresh);
        }

        private void deletepersonBtn_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    List<AddressUnit> li = new List<AddressUnit>();
                    foreach (AddressUnit addressUnit in wrapPanel.Children)
                    {
                        if (addressUnit.IsChecked)
                        {
                            if (addressUnit.ContactInfo.ImgPath != "default.jpg")
                            {
                                QiniuBase.DeleteImg(addressUnit.ContactInfo.ImgPath);
                            }
                            ContactManager.DeleteContact(addressUnit.ContactInfo.Email);
                            li.Add(addressUnit);
                        }
                    }
                    foreach (AddressUnit addressUnit1 in li)
                    {
                        wrapPanel.Children.Remove(addressUnit1);
                    }
                }));
            });

        }

        private void updatepersonBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (AddressUnit addressUnit in wrapPanel.Children)
            {
                if (addressUnit.IsChecked)
                {
                    AddressUnitEdit addressUnitEdit = new AddressUnitEdit(this, addressUnit.ContactInfo, 1);
                    NavigationService.GetNavigationService(this).Navigate(addressUnitEdit);
                    break;
                }
            }
        }
    }
}
