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
    public class StudentSignBLL
    {
        private GTMDweixinEntities db = null;
        public StudentSignBLL()
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
            var signID = pagerParas["projectID"];
            var predicate = PredicateBuilder.True<StudentSignInfo>();

            if(signID != null && signID.Value<string>() != "")
            {
                predicate = predicate.And(item => item.SignID== signID.Value<int>());
            }
            if (queryName!=null&&queryName.Value<string>()!="")
            {
                predicate = predicate.And(item => item.StudentInfo.UserInfo.Name.Contains(queryName.ToString()));
            }
            if (queryNunmber!=null&& queryNunmber.Value<string>()!="")
            {
                predicate = predicate.And(item => item.StudentInfo.UserInfo.MobileTelphoneNumber.Contains(queryNunmber.ToString()));
            }
            var total = db.StudentSignInfoes.Where(predicate.Compile()).ToList().Count;

            //重新组装stuentInfo
            var StudentInfos = db.StudentSignInfoes.Where(predicate.Compile()).ToList().Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select( item=>new {
                    ID=item.ID,
                    ProjectID=item.CreateTime,
                    EnteredDate = item.FifthDay_Afternoon,
                    //学生信息
                    UserID =item.StudentInfo.UserID,
                    IsSupervisor=item.StudentInfo.IsSupervisor,
                    SupervisorUserID=item. StudentInfo.SupervisorUserID,
                    SuccessfulTotalNumber=item.StudentInfo.SuccessfulTotalNumber,
                    //用户信息
                    Name =item.StudentInfo.UserInfo.Name,
                    LoginName=item.StudentInfo.UserInfo.LoginName,
                    MobileTelphoneNumber=item.StudentInfo.UserInfo.MobileTelphoneNumber,
                    QQNumber=item.StudentInfo.UserInfo.QQNumber,
                    Email= item.StudentInfo.UserInfo.Email,
                    Grade=item.StudentInfo.UserInfo.Grade,

                });
            var rows = JArray.Parse(JsonConvert.SerializeObject(StudentInfos));
            json["rows"] = rows;
            json["total"] = total;
            return json;
        }
        //新增
        public int Add(JObject json)
        {
            StudentSignInfo studentSignInfo = JsonConvert.DeserializeObject<StudentSignInfo>(json.ToString());
            db.Insert(studentSignInfo);
            return 1;
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Updata(JObject json, int id)
        {
            StudentSignInfo studentSignInfo = JsonConvert.DeserializeObject<StudentSignInfo>(json.ToString());
            studentSignInfo.ID = id;
            //var studentProjectInfo = db.StudentInfoes.FirstOrDefault(item => item.ID == id);
            if (studentSignInfo != null)
            {
                db.Updata(studentSignInfo);
            }
            return 1;
        }
       
        public int Delete(int id)
        {
            var studentSignInfo = db.StudentSignInfoes.FirstOrDefault(item => item.ID == id);
            if (studentSignInfo != null)
            {
                db.Delete(studentSignInfo);
            }
            return 1;
        }
        public int Delete(List<int> ids)
        {

            Expression<Func<StudentSignInfo, bool>> conditions = item => ids.Contains(item.ID);
            db.DeleteBulk<StudentSignInfo>(conditions);
            return 1;
        }
    }
}