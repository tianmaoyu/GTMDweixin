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
            //var properties = pagerParas.Properties();
            //var values = pagerParas.PropertyValues();
           
            
            var pageIndex = pagerParas["page"].Value<int>();
            var pageSize = pagerParas["rows"].Value<int>();
            var queryName = pagerParas["name"];
            var queryNunmber = pagerParas["phoneNumber"];
            var signID = pagerParas["signID"];
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
                    SignID=item.SignID,
                    StudentID=item.StudentID,
                    FirstDay_Morning=item.FirstDay_Morning,
                    FirstDay_Afternoon=item.FirstDay_Afternoon,
                    FirstDay_Evening=item.FirstDay_Evening,

                    SecondDay_Morning = item.SecondDay_Morning,
                    SecondDay_Afternoon = item.SecondDay_Afternoon,
                    SecondDay_Evening = item.SecondDay_Evening,

                    ThirdDay_Morning = item.ThirdDay_Morning,
                    ThirdDay_Afternoon = item.ThirdDay_Afternoon,
                    ThirdDay_Evening = item.ThirdDay_Evening,

                    FourthDay_Morning = item.FourthDay_Morning,
                    FourthDay_Afternoon = item.FourthDay_Afternoon,
                    FourthDay_Evening = item.FourthDay_Evening,

                    FifthDay_Morning = item.FifthDay_Morning,
                    FifthDay_Afternoon = item.FifthDay_Afternoon,
                    FifthDay_Evening = item.FifthDay_Evening,

                    SixthDay_Morning = item.SixthDay_Morning,
                    SixthDay_Afternoon = item.SixthDay_Afternoon,
                    SixthDay_Evening = item.SixthDay_Evening,

                    SeventhDay_Morning = item.SeventhDay_Morning,
                    SeventhDay_Afternoon = item.SeventhDay_Afternoon,
                    SeventhDay_Evening = item.SeventhDay_Evening,

                    Name = item.StudentInfo.UserInfo.Name,
                    Total=item.Total
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

        public int Add(StudentSignInfo studentSignInfo)
        {
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

        public List<StudentSignInfo> Find(Expression<Func<StudentSignInfo,bool>> condition)
        {
            return db.StudentSignInfoes.Where(condition).ToList();
        }
       
    }
}