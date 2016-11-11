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
    public class ProjectController : ApiController
    {
        public object ProjectBLL { get; private set; }

        // Post: api/Project/GetList
        [HttpPost]
        public JArray GetList()
        {
            ProjectBLL ProjectBLL = new ProjectBLL();
            return ProjectBLL.GetTree();
        }

        // Post: api/Project/Add
        [HttpPost]
        public string Add(JObject pagerParas)
        {
            var id = HttpContext.Current.Request.QueryString["id"];
            //var form = HttpContext.Current.Request.Form;

            ProjectBLL ProjectBLL = new ProjectBLL();
            if (id != null && Int32.Parse(id) > 0)
            {
                ProjectBLL.Updata(pagerParas, Int32.Parse(id));
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
                ProjectBLL.Add(pagerParas);
            }
            return "1";
        }

        // DELETE: api/Project/Delete/5
        [HttpPost]
        public JObject Delete(JObject param)
        {
            var id = param["ID"].Value<int>();
            ProjectBLL ProjectBLL = new ProjectBLL();
            var deleteSuccess = ProjectBLL.Delete(id);
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


        public JObject Edit(JObject pagerParas)
        {
            ProjectBLL ProjectBLL = new ProjectBLL();
            ProjectBLL.Add(pagerParas);
            return new JObject();
        }
        
        public JObject GetProjectInfo(int id)
        {
            ProjectBLL ProjectBLL = new ProjectBLL();
            return ProjectBLL.GetInfo(id); ;
        }

        [HttpPost]
        public JArray GetAll()
        {
            var projects = new ProjectBLL().GetAll();
            return  JArray.Parse(JsonConvert.SerializeObject(projects));
        }

        /// <summary>
        /// 得到当前正在执行的项目
        /// </summary>
        /// <returns></returns>
        public JArray GetActive()
        {
            var projects = new ProjectBLL().GetAll().Where(item => item.IsActive == true);
            return JArray.Parse(JsonConvert.SerializeObject(projects));
        }

        /// <summary>
        /// 得到当前这个人之前参加过的项目列表
        /// </summary>
        /// <returns></returns>
        public JArray GetAttended()
        {
            UserBLL userBll = new UserBLL();
            //当前用户信息
            var currentUser = userBll.GetCurrentUser(this.Request);
            if (currentUser != null)
            {
                //当前学生信息
                var currentstudent = currentUser.StudentInfo.FirstOrDefault();
                if (currentstudent != null)
                {
                    StudentProjectBLL studentProjectBLL = new StudentProjectBLL();
                    //得到学生加入的项目
                    var studentProjectInfos = studentProjectBLL.GetAll().Where(item => item.StudentID == currentstudent.ID);
                    if (studentProjectInfos.Any())
                    {
                        List<int> ids = studentProjectInfos.Select(item => (int)item.ProjectID).ToList();
                        var projectinfos = new ProjectBLL().GetAll().Where(item => ids.Contains(item.ID)).Where(item => item.IsActive == false);
                        if (projectinfos.Any())
                        {
                            return JArray.Parse(JsonConvert.SerializeObject(projectinfos));
                        }
                    }
                }
            }
            return new JArray();
        }

    }
}
