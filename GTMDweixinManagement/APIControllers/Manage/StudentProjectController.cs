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
    public class StudentProjectController : ApiController
    {
        public object StudentProjectBLL { get; private set; }

        // Post: api/User/GetList
        [HttpPost]
        public JObject GetList(JObject pagerParas)
        {
            StudentProjectBLL studentProjectBLL = new StudentProjectBLL();
            return studentProjectBLL.GetList(pagerParas);
        }

        // Post: api/User/Add
        [HttpPost]
        public string Add(JObject pagerParas)
        {
            var id = HttpContext.Current.Request.QueryString["id"];
            //var form = HttpContext.Current.Request.Form;

            StudentProjectBLL studentProjectBLL = new StudentProjectBLL();
            if (id != null && Int32.Parse(id) > 0)
            {
                studentProjectBLL.Updata(pagerParas, Int32.Parse(id));
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
                studentProjectBLL.Add(pagerParas);
            }
            return "1";
        }

        // DELETE: api/User/Delete/5
        [HttpPost]
        public JObject Delete(JObject json)
        {
            var ids = JsonConvert.DeserializeObject<List<int>>(json["ids"].ToString());
            StudentProjectBLL studentProjectBLL = new StudentProjectBLL();
            var deleteSuccess = studentProjectBLL.Delete(ids);
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

        public JArray GetProject()
        {
            //new StudentProjectBLL()
            return new JArray();
        }

        public JObject Edit(JObject pagerParas)
        {
            StudentProjectBLL studentProjectBLL = new StudentProjectBLL();
            studentProjectBLL.Add(pagerParas);
            return new JObject();
        }

        /// <summary>
        /// 微信端报名
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JObject Enter(JObject value)
        {
            JObject reslut = new JObject();
            String projectID = value["projectID"].Value<String>();
            if (!String.IsNullOrEmpty(projectID))
            {
                UserBLL userBll = new UserBLL();
                var user= userBll.GetCurrentUser(this.Request);
                if (user != null)
                {
                    var studentInfo = user.StudentInfo.FirstOrDefault();
                    if (studentInfo != null)
                    {
                        StudentProjectBLL studentProjectBLL = new StudentProjectBLL();
                        EF.StudentProjectInfo sp = new EF.StudentProjectInfo();
                        sp.ProjectID= Int32.Parse(projectID);
                        sp.StudentID= studentInfo.ID;
                        sp.EnteredDate = DateTime.Now;
                        if (studentProjectBLL.Add(sp) == 1)
                        {
                            reslut["status"] = true;
                            return reslut;
                        }
                    }
                }
            }
            reslut["msg"] = "报名出现错误！";
            return reslut;
        }
    }
}
