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
    public class StudentSignController : ApiController
    {
        public object StudentSignBLL { get; private set; }

        // Post: api/User/GetList
        [HttpPost]
        public JObject GetList(JObject pagerParas)
        {
            StudentSignBLL studentSignBLL = new StudentSignBLL();
            return studentSignBLL.GetList(pagerParas);
        }

        // Post: api/User/Add
        [HttpPost]
        public string Add(JObject pagerParas)
        {
            var id = HttpContext.Current.Request.QueryString["id"];
            //var form = HttpContext.Current.Request.Form;

            StudentSignBLL studentSignBLL = new StudentSignBLL();
            if (id != null && Int32.Parse(id) > 0)
            {
                studentSignBLL.Updata(pagerParas, Int32.Parse(id));
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
                studentSignBLL.Add(pagerParas);
            }
            return "1";
        }

        // DELETE: api/User/Delete/5
        [HttpPost]
        public JObject Delete(JObject json)
        {
            var ids = JsonConvert.DeserializeObject<List<int>>(json["ids"].ToString());
            StudentSignBLL studentSignBLL = new StudentSignBLL();
            var deleteSuccess = studentSignBLL.Delete(ids);
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
            StudentSignBLL studentSignBLL = new StudentSignBLL();
            studentSignBLL.Add(pagerParas);
            return new JObject();
        }
    }
}
