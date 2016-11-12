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

        // Post: api/Sign/GetList
        [HttpPost]
        public JArray GetList()
        {
            SignBLL signBLL = new SignBLL();
            return signBLL.GetTree();
        }

        // Post: api/Sign/Add
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

        // DELETE: api/Sign/Delete/5
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

        /// <summary>
        /// 得到可以勾班的班次
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JArray GetUseableSign(JObject value)
        {
            var infos=   new SignBLL().GetUseableSign(this.Request);
            if (infos.Any())
            {
                return JArray.Parse(JsonConvert.SerializeObject(infos));
            }
            return null;
        }

        /// <summary>
        /// 得到勾过的勾班列表
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JArray GetExpireSign(JObject value)
        {
            var infos = new SignBLL().GetExpireSign(this.Request);
            if (infos.Any())
            {
                return JArray.Parse(JsonConvert.SerializeObject(infos));
            }
            return null;
        }
    }
}
