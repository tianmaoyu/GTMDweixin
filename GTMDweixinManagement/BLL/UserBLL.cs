using EntityFramework.Extensions;
using GTMDweixinManagement.EF;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            
            var total = db.UserInfoes.ToList().Count;
            var userInfos = db.UserInfoes.ToList().Skip((PageIndex - 1) * PageSize).Take(PageSize);
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
        public int Add(string str)
        {
            UserInfo userInfo = JsonConvert.DeserializeObject<UserInfo>(str);

            return 1;
        }
    }
}