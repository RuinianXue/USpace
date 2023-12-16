using System;
using System.Data;
using System.Text.RegularExpressions;
using UIDisplay.DAL;
using UIDisplay.Model;

namespace UIDisplay.BLL
{
    public class ContactManager
    {
        /// <summary>
        /// 插入新联系人
        /// </summary>
        /// <param name="newContact">新联系人信息</param>
        /// <returns>操作是否成功</returns>
        public static bool InsertContact(Contact newContact)
        {
            return ContactRepository.InsertContact(newContact);
        }

        /// <summary>
        /// 更新联系人信息
        /// </summary>
        /// <param name="updatedContact">更新后的联系人信息</param>
        /// <returns>操作是否成功</returns>
        public static bool UpdateContact(Contact updatedContact)
        {
            return ContactRepository.UpdateContact(updatedContact);
        }

        /// <summary>
        /// 删除联系人
        /// </summary>
        /// <param name="contactID">联系人ID</param>
        /// <returns>操作是否成功</returns>
        public static bool DeleteContact(string contactID)
        {
            return ContactRepository.DeleteContactByID(contactID);
        }

        /// <summary>
        /// 查询用户所有联系人
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="result">查询结果</param>
        /// <returns>是否存在联系人</returns>
        public static bool QueryAllContacts(string userID, out DataTable result)
        {
            return ContactRepository.QueryAllContacts(userID, out result);
        }

        /// <summary>
        /// 通过联系人姓名获取邮箱
        /// </summary>
        /// <param name="contactName">联系人姓名</param>
        /// <param name="email">查询结果</param>
        /// <returns>是否存在联系人</returns>
        public static bool GetEmailByName(string contactName, out string email)
        {
            return ContactRepository.QueryEmailByName(contactName, out email);
        }

        /// <summary>
        /// 验证邮箱格式
        /// </summary>
        /// <param name="email">待验证的邮箱</param>
        /// <returns>是否为有效邮箱格式</returns>
        public static bool ValidateEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        /// <summary>
        /// 验证电话号码格式
        /// </summary>
        /// <param name="phoneNumber">待验证的电话号码</param>
        /// <returns>是否为有效电话号码格式</returns>
        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            string pattern = @"^\+?[0-9]{1,4}?[-.\\s]?(\(\d{1,}\)|\d{1,})[-.\\s]?\d{1,}[-.\\s]?\d{1,}[-.\\s]?\d{1,}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
