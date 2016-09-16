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
    }
}
