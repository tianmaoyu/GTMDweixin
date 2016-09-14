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
    public class StudentBLL
    {
        private GTMDweixinEntities db = null;
        public StudentBLL()
        {
            db = new GTMDweixinEntities();
        }

        public JObject GetList(JObject pagerParas)
        {
            JObject json = new JObject();
            var pageIndex = pagerParas["page"].Value<int>();
            var pageSize = pagerParas["rows"].Value<int>();
            var queryName = pagerParas["name"];
            var queryNunmber = pagerParas["phoneNumber"];

            var predicate = PredicateBuilder.True<StudentInfo>();

            if (queryName!=null&&queryName.Value<string>()!="")
            {
                predicate = predicate.And(item => item.UserInfo.Name.Contains(queryName.ToString()));
            }
            if (queryNunmber!=null&& queryNunmber.Value<string>()!="")
            {
                predicate = predicate.And(item => item.UserInfo.MobileTelphoneNumber.Contains(queryNunmber.ToString()));
            }
            var total = db.StudentInfoes.Where(predicate.Compile()).ToList().Count;

            //重新组装stuentInfo
            var StudentInfos = db.StudentInfoes.Where(predicate.Compile()).ToList().Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select( item=>new {
                    ID=item.ID,
                    UserID=item.UserID,
                    IsSupervisor=item.IsSupervisor,
                    SupervisorUserID=item.SupervisorUserID,
                    EnteredDate=item.EnteredDate,
                    SuccessfulTotalNumber=item.SuccessfulTotalNumber,
                    Name =item.UserInfo.Name,
                    LoginName=item.UserInfo.LoginName,
                    MobileTelphoneNumber=item.UserInfo.MobileTelphoneNumber,
                    QQNumber=item.UserInfo.QQNumber,
                    Email= item.UserInfo.Email,
                    Grade=item.UserInfo.Grade,

                });
            var rows = JArray.Parse(JsonConvert.SerializeObject(StudentInfos));
            json["rows"] = rows;
            json["total"] = total;
            return json;
        }
        //新增
        public int Add(JObject json)
        {
            StudentInfo StudentInfo = JsonConvert.DeserializeObject<StudentInfo>(json.ToString());
            db.Insert(StudentInfo);
            return 1;
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Updata(JObject json, int id)
        {
            StudentInfo StudentInfo = JsonConvert.DeserializeObject<StudentInfo>(json.ToString());
            StudentInfo.ID = id;
            //var StudentInfo = db.StudentInfoes.FirstOrDefault(item => item.ID == id);
            if (StudentInfo != null)
            {
                db.Updata(StudentInfo);
            }
            return 1;
        }

        public int Delete(int id)
        {
            var StudentInfo = db.StudentInfoes.FirstOrDefault(item => item.ID == id);
            if (StudentInfo != null)
            {
                db.Delete(StudentInfo);
            }
            return 1;
        }
        public int Delete(List<int> ids)
        {

            Expression<Func<StudentInfo, bool>> conditions = item => ids.Contains(item.ID);
            db.DeleteBulk<StudentInfo>(conditions);
            return 1;
        }
        public int Add(string str)
        {
            StudentInfo StudentInfo = JsonConvert.DeserializeObject<StudentInfo>(str);

            return 1;
        }
    }
}