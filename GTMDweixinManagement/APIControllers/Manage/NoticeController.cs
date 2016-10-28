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
    public class NoticeController : ApiController
    {
        //public object NoticeBll { get; private set; }

        // Post: api/User/GetList
        [HttpPost]
        public JObject GetList(JObject pagerParas)
        {
            NoticeBLL noticeBll = new NoticeBLL();
            return noticeBll.GetList(pagerParas);
        }

        // Post: api/User/Add
        [HttpPost]
        public string Add(JObject pagerParas)
        {
            var id= HttpContext.Current.Request.QueryString["id"];
            //var form = HttpContext.Current.Request.Form;

            NoticeBLL noticeBll = new NoticeBLL();
            if (id != null&& Int32.Parse(id)>0)
            {
                noticeBll.Updata(pagerParas, Int32.Parse(id));
            }
            else
            {
                pagerParas.Remove("ID");
                noticeBll.Add(pagerParas);
            }
            return "1";
        }

        // DELETE: api/User/Delete/5
        [HttpPost]
        public JObject Delete(JObject json)
        {
            var ids= JsonConvert.DeserializeObject<List<int>>(json["ids"].ToString());
            NoticeBLL noticeBll = new NoticeBLL();
            var deleteSuccess= noticeBll.Delete(ids);
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
            NoticeBLL noticeBll = new NoticeBLL();
            noticeBll.Add(pagerParas);
            return new JObject();
        }
       
        [HttpPost]
        public JArray GetInfo()
        {
            JArray result = new JArray();
            NoticeBLL noticeBll = new NoticeBLL();

            var noticeInfos = JsonConvert.SerializeObject(noticeBll.GetAll());
            result = JArray.Parse(noticeInfos);
            return result;
        }
       
    }
}
