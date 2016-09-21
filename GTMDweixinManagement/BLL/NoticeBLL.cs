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
    public class NoticeBLL
    {
       private  GTMDweixinEntities db = null;
       public NoticeBLL()
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

            var predicate = PredicateBuilder.True<NoticeInfo>();
            predicate = predicate.And(p => p.ID >-1);
            //if (queryName != null && queryName.ToString() != null)
            //{
            //    predicate = predicate.And(item => item.Name.Contains(queryName.ToString()));
            //}
            //if (queryNunmber != null && queryNunmber.ToString() != null)
            //{
            //    predicate= predicate.And(item => item.MobileTelphoneNumber.Contains(queryNunmber.ToString()));
            //}
            var total = db.NoticeInfoes.Where(predicate.Compile()).ToList().Count;
            var userInfos = db.NoticeInfoes.Where(predicate.Compile()).ToList().Skip((PageIndex - 1) * PageSize).Take(PageSize);
            var rows= JArray.Parse(JsonConvert.SerializeObject(userInfos));
            json["rows"] = rows;
            json["total"] = total;
            return json;
        }
        //新增
        public int Add(JObject json)
        {
            NoticeInfo noticeInfo = JsonConvert.DeserializeObject<NoticeInfo>(json.ToString());
            db.Insert(noticeInfo);
            return 1;
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Updata(JObject json,int id)
        {
            NoticeInfo noticeInfo = JsonConvert.DeserializeObject<NoticeInfo>(json.ToString());
            noticeInfo.ID = id;
            //var userInfo = db.UserInfoes.FirstOrDefault(item => item.ID == id);
            if (noticeInfo != null)
            {
                db.Updata(noticeInfo);
            }
            return 1;
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