using GTMDweixinManagement.BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            UserBLL userBll = new UserBLL();
            string phoneNumber = value["mobilePhone"].Value<string>();
            string password = value["password"].Value<string>();
            if (userBll.Authentication(phoneNumber, password))
            {
                result["status"] = 1;
                result["url"] = "/WeiXin/Home/Index";
            }
            result["info"] = "用户名或者密码错误！";
            return result;
        }

      
    }
}
