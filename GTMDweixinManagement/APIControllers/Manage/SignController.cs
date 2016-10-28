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
    public class SignController : ApiController
    {
        public object ProjectBLL { get; private set; }

        // Post: api/Project/GetList
        [HttpPost]
        public JArray GetList()
        {
            SignBLL signBLL = new SignBLL();
            return signBLL.GetTree();
        }

        // Post: api/Project/Add
        [HttpPost]
        public string Add(JObject pagerParas)
        {
            var id = HttpContext.Current.Request.QueryString["id"];
            //var form = HttpContext.Current.Request.Form;

            SignBLL signBLL = new SignBLL();
            if (id != null && Int32.Parse(id) > 0)
            {
                signBLL.Updata(pagerParas, Int32.Parse(id));
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
                signBLL.Add(pagerParas);
            }
            return "1";
        }

        // DELETE: api/Project/Delete/5
        [HttpPost]
        public JObject Delete(JObject param)
        {
            var id = param["ID"].Value<int>();
            SignBLL signBLL = new SignBLL();
            var deleteSuccess = signBLL.Delete(id);
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
            SignBLL signBLL = new SignBLL();
            signBLL.Add(pagerParas);
            return new JObject();
        }
        
        public JObject GetSignInfo(int id)
        {
            SignBLL signBLL = new SignBLL();
            return signBLL.GetInfo(id); ;
        }

        [HttpPost]
        public JArray GetAll()
        {
            var signs = new SignBLL().GetAll();
            return  JArray.Parse(JsonConvert.SerializeObject(signs));
        }
    }
}
