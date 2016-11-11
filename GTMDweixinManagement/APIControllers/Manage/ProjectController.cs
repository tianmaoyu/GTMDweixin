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
            //所有的可报名的
            var projects = new ProjectBLL().GetAll().Where(item => item.IsActive == true);

            //得到 当前此人参加的项目，并且是激活状态的项目id ,(可报名的)
            ProjectBLL projectBll = new ProjectBLL();
            var projectInfos = projectBll.GetEnterProject(this.Request);
            if (projectInfos.Any())
            {
                //所有的可报名的。并且已经报名的
                var infos = projectInfos.Where(item => item.IsActive == true);
                if (infos.Any())
                {
                 //已经参加的项目，并且在可报名利表中的
                  var ids= infos.Select(item => item.ID);
                  var _projects=  projects.Select(item =>new
                    {
                        Entered = ids.Contains(item.ID),
                        ID=item.ID,
                        DisplayName = item.DisplayName,
                        Remark = item.Remark,
                        CreateTime = item.CreateTime,
                        IsActive= item.IsActive,
                        IsDisplay=item.IsDisplay
                    });
                    return JArray.Parse(JsonConvert.SerializeObject(_projects));
                }
            }
            return JArray.Parse(JsonConvert.SerializeObject(projects));
        }

        /// <summary>
        /// 得到当前这个人之前参加过的项目列表,并且已经关闭的
        /// </summary>
        /// <returns></returns>
        public JArray GetAttended()
        {
           ProjectBLL projectBll = new ProjectBLL();
           var projectInfos=  projectBll.GetEnterProject(this.Request);
            if (projectInfos.Any())
            {
                var infos = projectInfos.Where(item => item.IsActive == false);
                if (infos.Any())
                {
                    return JArray.Parse(JsonConvert.SerializeObject(infos));
                }
            }
            return new JArray();
        }


    }
}
