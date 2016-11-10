using EntityFramework.Extensions;
using GTMDweixinManagement.EF;
using LinqKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace GTMDweixinManagement.BLL
{
    public class UserBLL
    {
       private  GTMDweixinEntities db = null;
       public UserBLL()
       {
            db = new GTMDweixinEntities();
       }

        public JObject GetList(JObject pagerParas)
        {
            JObject json = new JObject();
            var PageIndex = Int32.Parse(pagerParas["page"].ToString());
            var PageSize = Int32.Parse(pagerParas["rows"].ToString());
            var queryName = pagerParas["name"];
            var queryNunmber= pagerParas["phoneNumber"];

            var predicate = PredicateBuilder.True<UserInfo>();
            predicate = predicate.And(p => p.ID >-1);
            if (queryName != null && queryName.ToString() != null)
            {
                predicate = predicate.And(item => item.Name.Contains(queryName.ToString()));
            }
            if (queryNunmber != null && queryNunmber.ToString() != null)
            {
                predicate= predicate.And(item => item.MobileTelphoneNumber.Contains(queryNunmber.ToString()));
            }
            var total = db.UserInfoes.Where(predicate.Compile()).ToList().Count;
            var userInfos = db.UserInfoes.Where(predicate.Compile()).ToList().Skip((PageIndex - 1) * PageSize).Take(PageSize);
            var rows= JArray.Parse(JsonConvert.SerializeObject(userInfos));
            json["rows"] = rows;
            json["total"] = total;
            return json;
        }
        //新增
        public int Add(JObject json)
        {
            UserInfo userInfo = JsonConvert.DeserializeObject<UserInfo>(json.ToString());
            db.Insert(userInfo);
            return 1;
        }

        public int Add(UserInfo info)
        {
            try
            {
                db.Insert(info);
             }
            catch (Exception ex)
            {
                
            }
            return 1;
        }

        public int Updata(UserInfo userInfo)
        {
            if (userInfo != null)
            {
                db.Updata(userInfo);
            }
            return 1;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Updata(JObject json,int id)
        {
            UserInfo userInfo = JsonConvert.DeserializeObject<UserInfo>(json.ToString());
            userInfo.ID = id;
            //var userInfo = db.UserInfoes.FirstOrDefault(item => item.ID == id);
            if (userInfo != null)
            {
                db.Updata(userInfo);
            }
            return 1;
        }

        public int Delete(int id)
        {
            var userInfo = db.UserInfoes.FirstOrDefault(item => item.ID == id);
            if (userInfo != null)
            {
                db.Delete(userInfo);
            }
            return 1;
        }
        public int Delete(List<int> ids)
        {
            
            Expression<Func<UserInfo,bool>> conditions = item=> ids.Contains(item.ID);
            db.DeleteBulk<UserInfo>(conditions);
            return 1;
        }

        /// <summary>
        /// 得到所有的用户信息
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> GetAll()
        {
            List<UserInfo> userInfos = new List<UserInfo>();
            userInfos = db.UserInfoes.ToList();
            return userInfos;
        }

        /// <summary>
        /// 微信端登录认证
        /// </summary>
        /// <param name="mobilePhone">电话号码</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public bool Authentication(string mobilePhone ,string password)
        {
           return db.UserInfoes.Any(item => item.MobileTelphoneNumber == mobilePhone && item.Password == password);
        }

        public bool IsUserdPhone(string phone)
        {
            return db.UserInfoes.Any(item => item.MobileTelphoneNumber == phone);
        }
        public bool IsUserdUserName(string name)
        {
            return db.UserInfoes.Any(item => item.LoginName == name);
        }

        /// <summary>
        /// 得到 mvc 中的cookie
        /// </summary>
        /// <returns></returns>
        public UserInfo GetCurrentUser()
        {
            UserInfo userInfo = new UserInfo();
            var cookie = HttpContext.Current.Request.Cookies.Get("session");
            var mobilePhone = cookie["mobilePhone"];
            var password = cookie["password"];
            if (!string.IsNullOrEmpty(mobilePhone))
            {
                userInfo = db.UserInfoes.Where(item => item.MobileTelphoneNumber.Equals(mobilePhone)).FirstOrDefault();
            }
            return userInfo;
        }
  
        /// <summary>
        /// 的到web api 中的cookie
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public UserInfo GetCurrentUser(System.Net.Http.HttpRequestMessage Request)
        {
            UserInfo userInfo = new UserInfo();
            CookieHeaderValue cookie =Request.Headers.GetCookies("session").FirstOrDefault();
            if (cookie != null)
            {
                CookieState cookieState = cookie["session"];
                var password = cookieState["password"];
               var mobilePhone = cookieState["mobilePhone"];
                //得到用户的
                if (!string.IsNullOrEmpty(mobilePhone))
                {
                    userInfo = db.UserInfoes.Where(item => item.MobileTelphoneNumber.Equals(mobilePhone)).FirstOrDefault();
                }
            }
            return userInfo;
        }
    }
}