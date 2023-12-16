using HandyControl.Controls;
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
    /// 联系人列表页面，负责显示和管理联系人信息。
    /// </summary>
    public partial class ContactListPage : Page
    {
        /// <summary>
        /// 获取或设置一个值，表示页面是否已加载。
        /// </summary>
        public new bool IsLoaded { get; set; } = false;

        /// <summary>
        /// 初始化 <see cref="ContactListPage"/> 类的新实例。
        /// </summary>
        public ContactListPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 刷新联系人列表。
        /// </summary>
        public void Refresh()
        {
            DataTable dt;
            bool success = ContactManager.QueryAllContacts(LoginManager.CurrentUserID, out dt);

            if (success)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    wrapPanel.Children.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        Contact contact = new Contact(
                            row[0].ToString(),
                            row[1].ToString(),
                            row[2].ToString(),
                            row[3].ToString(),
                            row[4].ToString(),
                            row[5].ToString()
                        );

                        AddressUnit addressUnit = new AddressUnit(contact);
                        wrapPanel.Children.Add(addressUnit);
                    }
                }));
                Growl.Success("联系人列表拉取成功！");
            }
            else
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    wrapPanel.Children.Clear();
                    Growl.Info("联系人列表为空！");
                }));
            }
        }

        /// <summary>
        /// 页面加载完成时触发的事件，用于播放加载动画和初始化页面数据。
        /// </summary>
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadInAnimation(sender);
            if (!IsLoaded)
            {
                IsLoaded = true;
                await Task.Run(Refresh);
            }
        }

        /// <summary>
        /// 播放加载动画。
        /// </summary>
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

        /// <summary>
        /// 刷新按钮点击事件，触发刷新联系人列表。
        /// </summary>
        private async void Btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(Refresh);
        }

        /// <summary>
        /// 插入联系人按钮点击事件，打开联系人编辑页面以添加新联系人。
        /// </summary>
        private void Btn_InsertContact_Click(object sender, RoutedEventArgs e)
        {
            // 创建一个新的联系人实例
            Contact newContact = new Contact(IDGenerator.GenUUID(), "", "", "", LoginManager.CurrentUserID, "default.jpg");
            // 打开联系人编辑页面
            ContactEditUnit addressUnitEdit = new ContactEditUnit(this, newContact);
            NavigationService.GetNavigationService(this).Navigate(addressUnitEdit);
            // 监听联系人保存事件
            addressUnitEdit.ContactSaved += (s, args) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (args.Success)
                    {
                        Growl.Success("联系人插入成功");
                    }
                    else
                    {
                        Growl.Error("联系人插入失败");
                    }
                });
            };
        }

        /// <summary>
        /// 删除联系人按钮点击事件，删除选中的联系人。
        /// </summary>
        private void Btn_DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    bool success = true;
                    List<AddressUnit> li = new List<AddressUnit>();
                    foreach (AddressUnit addressUnit in wrapPanel.Children)
                    {
                        if (addressUnit.IsChecked)
                        {
                            // 删除联系人对应的图片
                            if (addressUnit.ContactInfo.ImgPath != "default.jpg")
                            {
                                QiniuBase.DeleteImg(addressUnit.ContactInfo.ImgPath);
                            }
                            // 删除联系人
                            if (!ContactManager.DeleteContact(addressUnit.ContactInfo.CID))
                            {
                                Growl.Error($"删除联系人{addressUnit.ContactInfo.Name}时出错");
                                success = false;
                            }
                            li.Add(addressUnit);
                        }
                    }
                    // 移除页面上对应的联系人显示单元
                    foreach (AddressUnit addressUnit1 in li)
                    {
                        wrapPanel.Children.Remove(addressUnit1);
                    }
                    // 显示删除结果
                    if (success)
                    {
                        Growl.Success("联系人删除成功！");
                    }
                }));
            });
        }

        /// <summary>
        /// 更新联系人按钮点击事件，打开联系人编辑页面以修改选中的联系人信息。
        /// </summary>
        private void Btn_UpdateContact_Click(object sender, RoutedEventArgs e)
        {
            foreach (AddressUnit addressUnit in wrapPanel.Children)
            {
                if (addressUnit.IsChecked)
                {
                    // 打开联系人编辑页面
                    ContactEditUnit addressUnitEdit = new ContactEditUnit(this, addressUnit.ContactInfo, 1);
                    NavigationService.GetNavigationService(this).Navigate(addressUnitEdit);
                    // 监听联系人保存事件
                    addressUnitEdit.ContactSaved += (s, args) =>
                    {
                        Dispatcher.Invoke(() =>
                        {
                            if (args.Success)
                            {
                                Growl.Success("联系人更新成功");
                            }
                            else
                            {
                                Growl.Error("联系人更新失败");
                            }
                        });
                    };
                }
            }
        }
    }
}
