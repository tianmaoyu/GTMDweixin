using GTMDweixinManagement.BLL;
using GTMDweixinManagement.EF;
using LinqKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GTMDweixinManagement.APIControllers
{
    public class UserController : ApiController
    {
        public object UserBll { get; private set; }

        // Post: api/User/GetList
        [HttpPost]
        public JObject GetList(JObject pagerParas)
        {
            UserBLL userBll = new UserBLL();
            return userBll.GetList(pagerParas);
        }

        // Post: api/User/Add
        [HttpPost]
        public string Add(JObject pagerParas)
        {
            var id= HttpContext.Current.Request.QueryString["id"];
            //var form = HttpContext.Current.Request.Form;

            UserBLL userBll = new UserBLL();
            if (id != null&& Int32.Parse(id)>0)
            {
                userBll.Updata(pagerParas, Int32.Parse(id));
            }
            else
            {
                userBll.Add(pagerParas);
            }
            return "1";
        }

        // DELETE: api/User/Delete/5
        [HttpPost]
        public JObject Delete(JObject json)
        {
            var ids= JsonConvert.DeserializeObject<List<int>>(json["ids"].ToString());
            UserBLL userBll = new UserBLL();
            var deleteSuccess=  userBll.Delete(ids);
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

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="pagerParas"></param>
        /// <returns></returns>
        public JObject Edit(JObject pagerParas)
        {
            UserBLL userBll = new UserBLL();
            userBll.Add(pagerParas);
            return new JObject();
        }
       
        [HttpPost]
        public JArray GetInfo()
        {
            JArray result = new JArray();
            UserBLL userBll = new UserBLL();

            var userInfos = JsonConvert.SerializeObject(userBll.GetAll());
            result = JArray.Parse(userInfos);
            return result;
        }
    }
}
