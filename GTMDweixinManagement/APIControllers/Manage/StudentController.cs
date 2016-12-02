using GTMDweixinManagement.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GTMDweixinManagement.APIControllers
{
    public class StudentController : ApiController
    {
        public object StudentBLL { get; private set; }

        // Post: api/Student/GetList
        [HttpPost]
        public JObject GetList(JObject pagerParas)
        {
            StudentBLL studentBLL = new StudentBLL();
            return studentBLL.GetList(pagerParas);
        }

        // Post: api/User/Add
        [HttpPost]
        public string Add(JObject pagerParas)
        {
            var id = HttpContext.Current.Request.QueryString["id"];
            //var form = HttpContext.Current.Request.Form;

            StudentBLL studentBLL = new StudentBLL();
            if (id != null && Int32.Parse(id) > 0)
            {
                studentBLL.Updata(pagerParas, Int32.Parse(id));
            }
            else
            {
                try
                {
                    pagerParas.Remove("ID");
                }
                catch (Exception ex)
                {

                }
               
                studentBLL.Add(pagerParas);
            }
            return "1";
        }

        // DELETE: api/User/Delete/5
        [HttpPost]
        public JObject Delete(JObject json)
        {
            var ids = JsonConvert.DeserializeObject<List<int>>(json["ids"].ToString());
            StudentBLL studentBLL = new StudentBLL();
            var deleteSuccess = studentBLL.Delete(ids);
            JObject result = new JObject();
            if (deleteSuccess == 1)
            {
                result["success"] = true;
            }
            else
            {
                result["errorMsg"] = "删除失败！";
            }
            return result;
        }
        [HttpPost]
        public JArray GetStudent()
        {
            var students = new StudentBLL().GetAll().Select(item=> new{
                ID=item.ID,
                Name=item.UserInfo.Name,
            }).ToList();
            var json =JArray.Parse(JsonConvert.SerializeObject(students));
            return json;
        }

        [HttpPost]
        public JObject UpdateSupervisor(JObject pagerParas)
        {
            var id = pagerParas["supervisor"].Value<int>();
            StudentBLL studentBLL = new StudentBLL();
            var info= studentBLL.GetCurrentStudent(Request);
            try
            {
                studentBLL.Updata(info.ID, id);
            }
            catch(Exception ex)
            {
               
            }
            return null;
        }

        public JObject Edit(JObject pagerParas)
        {
            StudentBLL studentBLL = new StudentBLL();
            studentBLL.Add(pagerParas);
            return new JObject();
        }

        /// <summary>
        /// 得到所有的督导
        /// </summary>
        /// <returns></returns>
        ///  api/Student/GetAllSupervisor
        [HttpGet]
        public JArray GetAllSupervisor()
        {
            StudentBLL studentBLL = new StudentBLL();
            var infos= studentBLL.GetAll().Where(item => item.IsSupervisor == 1);
            if (infos.Any())
            {
               var _infos= infos.Select(item => new
                {
                    title = item.UserInfo.LoginName,
                    value = item.UserID
                });
                return JArray.Parse(JsonConvert.SerializeObject(_infos));
            }
            return new JArray();
        }
    }
}
