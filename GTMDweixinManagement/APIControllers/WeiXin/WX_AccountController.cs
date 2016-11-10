using GTMDweixinManagement.BLL;
using GTMDweixinManagement.EF;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace GTMDweixinManagement.APIControllers.WeiXin
{
    public class WX_AccountController : ApiController
    {

        /// <summary>
        /// 微信登录认证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/WX_Account
        public JObject Login(JObject value)
        {
            JObject result = new JObject();
            string phoneNumber = value["mobilePhone"].Value<string>();
            string password = value["password"].Value<string>();
            UserBLL userBll = new UserBLL();
            if (userBll.Authentication(phoneNumber, password))
            {
                result["status"] = 1;
                result["url"] = "/WeiXin/Home/Index?phoneNumber="+ phoneNumber+ "&password="+ password;
                
            }
            result["info"] = "用户名或者密码错误！";
            return result;
        }

        /// <summary>
        /// 微信用户注册
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JObject Register(JObject value)
        {
            JObject result = new JObject();
            string mobilePhone = value["mobilePhone"].Value<string>();
            string passsword = value["password"].Value<string>();
            string userName = value["userName"].Value<string>();
            UserBLL userBll = new UserBLL();
            //用户名，手机号码是否北占用
            if (userBll.IsUserdPhone(mobilePhone))
            {
                result["errorMsg"] = "手机号码已经被使用：" + mobilePhone;
                return result;
            }
            if (userBll.IsUserdUserName(userName))
            {
                result["errorMsg"] = "用户名已经北使用：" + userName;
                return result;
            }
            UserInfo user = new UserInfo();
            user.GreateDate = DateTime.Now;
            user.LoginName = userName;
            user.Password = passsword;
            user.MobileTelphoneNumber = mobilePhone;
            if (userBll.Add(user) == 1)
            {
                result["status"] = 1;
                result["url"] = "/WeiXin/Home/Index?phoneNumber=" + mobilePhone + "&password=" + passsword;
                return result;
            }
            return result;
        }
        /// <summary>
        ///验证手机
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JObject ValidatePhone(JObject value)
        {
            JObject result = new JObject();
            string mobilePhone = value["mobilePhone"].Value<string>();
            UserBLL userBll = new UserBLL();
            if (mobilePhone.Length > 12 || mobilePhone.Length < 11)
            {
                result["status"] = true;
                return result;
            }
            if (userBll.IsUserdPhone(mobilePhone))
            {
                result["status"] = true;
                return result;
            }
            result["status"] = false;
            return result;
        }

        /// <summary>
        ///验证用户名
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JObject ValidateUserName(JObject value)
        {
            JObject result = new JObject();
            string userName = value["userName"].Value<string>();
            UserBLL userBll = new UserBLL();
            if (userBll.IsUserdUserName(userName))
            {
                result["status"] = true;
                return result;
            }
            else
            {
                result["status"] = false;

            }
            return result;
        }

        public JObject ModifyPassword(JObject value)
        {
            JObject reslut = new JObject();
            UserBLL userBll = new UserBLL();
            var user= userBll.GetCurrentUser(this.Request);
            var oldPassword= value["oldPassword"].Value<string>();
            if (user.Password.Equals(oldPassword))
            {
                user.Password = value["newpassword"].Value<string>();
                if (userBll.Updata(user) == 1)
                {
                    reslut["status"] = true;
                    return reslut;
                }
                else
                {
                    reslut["status"] = false;
                    reslut["msg"] = "出现未知错误！";
                    return reslut;
                }
            }
            reslut["status"] = false;
            reslut["msg"] = "当前密码错误！";
            return reslut;
        }
    }
}
