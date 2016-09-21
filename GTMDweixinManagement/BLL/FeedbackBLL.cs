using EntityFramework.Extensions;
using GTMDweixinManagement.EF;
using LinqKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace GTMDweixinManagement.BLL
{
    public class FeedbackBLL
    {
       private  GTMDweixinEntities db = null;
       public FeedbackBLL()
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

            var predicate = PredicateBuilder.True<FeedbackInfo>();
            var total = db.FeedbackInfoes.Where(predicate.Compile()).ToList().Count;
            var userInfos = db.FeedbackInfoes.Where(predicate.Compile()).ToList().Skip((PageIndex - 1) * PageSize).Take(PageSize);
            var rows= JArray.Parse(JsonConvert.SerializeObject(userInfos));
            json["rows"] = rows;
            json["total"] = total;
            return json;
        }
       
        public int Delete(int id)
        {
            var noticeInfo = db.NoticeInfoes.FirstOrDefault(item => item.ID == id);
            if (noticeInfo != null)
            {
                db.Delete(noticeInfo);
            }
            return 1;
        }
        public int Delete(List<int> ids)
        {
            
            Expression<Func<NoticeInfo, bool>> conditions = item=> ids.Contains(item.ID);
            db.DeleteBulk<NoticeInfo>(conditions);
            return 1;
        }

        /// <summary>
        /// 得到所有的用户信息
        /// </summary>
        /// <returns></returns>
        public List<NoticeInfo> GetAll()
        {
            List<NoticeInfo> noticeInfoes = new List<NoticeInfo>();
            noticeInfoes = db.NoticeInfoes.ToList();
            return noticeInfoes;
        }
    }
}