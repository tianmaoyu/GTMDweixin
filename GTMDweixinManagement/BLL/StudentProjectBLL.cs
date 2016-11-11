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
    public class StudentProjectBLL
    {
        private GTMDweixinEntities db = null;
        public StudentProjectBLL()
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
            var projectID = pagerParas["projectID"];
            var predicate = PredicateBuilder.True<StudentProjectInfo>();

            if(projectID != null && projectID.Value<string>() != "")
            {
                predicate = predicate.And(item => item.ProjectID== projectID.Value<int>());
            }
            if (queryName!=null&&queryName.Value<string>()!="")
            {
                predicate = predicate.And(item => item.StudentInfo.UserInfo.Name.Contains(queryName.ToString()));
            }
            if (queryNunmber!=null&& queryNunmber.Value<string>()!="")
            {
                predicate = predicate.And(item => item.StudentInfo.UserInfo.MobileTelphoneNumber.Contains(queryNunmber.ToString()));
            }
            var total = db.StudentProjectInfoes.Where(predicate.Compile()).ToList().Count;

            //重新组装stuentInfo
            var StudentInfos = db.StudentProjectInfoes.Where(predicate.Compile()).ToList().Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select( item=>new {
                    ID=item.ID,
                    ProjectID=item.ProjectID,
                    EnteredDate = item.EnteredDate,
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
            StudentProjectInfo studentProjectInfo = JsonConvert.DeserializeObject<StudentProjectInfo>(json.ToString());
            db.Insert(studentProjectInfo);
            return 1;
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Updata(JObject json, int id)
        {
            StudentProjectInfo studentProjectInfo = JsonConvert.DeserializeObject<StudentProjectInfo>(json.ToString());
            studentProjectInfo.ID = id;
            //var studentProjectInfo = db.StudentInfoes.FirstOrDefault(item => item.ID == id);
            if (studentProjectInfo != null)
            {
                db.Updata(studentProjectInfo);
            }
            return 1;
        }
       
        public int Delete(int id)
        {
            var studentProjectInfo = db.StudentProjectInfoes.FirstOrDefault(item => item.ID == id);
            if (studentProjectInfo != null)
            {
                db.Delete(studentProjectInfo);
            }
            return 1;
        }
        public int Delete(List<int> ids)
        {

            Expression<Func<StudentProjectInfo, bool>> conditions = item => ids.Contains(item.ID);
            db.DeleteBulk<StudentProjectInfo>(conditions);
            return 1;
        }

        public IQueryable<StudentProjectInfo> GetAll()
        {
            return db.StudentProjectInfoes;
        }
    }
}