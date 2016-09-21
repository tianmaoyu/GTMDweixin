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
    public class FeedbackController : ApiController
    {
        //public object NoticeBll { get; private set; }

        // Post: api/User/GetList
        [HttpPost]
        public JObject GetList(JObject pagerParas)
        {
            FeedbackBLL feedbackBll = new FeedbackBLL();
            return feedbackBll.GetList(pagerParas);
        }

       

        // DELETE: api/User/Delete/5
        [HttpPost]
        public JObject Delete(JObject json)
        {
            var ids= JsonConvert.DeserializeObject<List<int>>(json["ids"].ToString());
            FeedbackBLL feedbackBll = new FeedbackBLL();
            var deleteSuccess= feedbackBll.Delete(ids);
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
        public JArray GetInfo()
        {
            JArray result = new JArray();
            FeedbackBLL feedbackBll = new FeedbackBLL();

            var infos = JsonConvert.SerializeObject(feedbackBll.GetAll());
            result = JArray.Parse(infos);
            return result;
        }
       
    }
}
