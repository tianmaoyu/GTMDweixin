using GTMDweixinManagement.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace GTMDweixinManagement.APIControllers.WeiXin
{
    public class WX_StudentController : ApiController
    {
        // GET: api/WX_Student
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/WX_Student/5
        public string Get(int id)
        {
            return "value";
        }

        public JObject GetInfo()
        {
            JObject result = new JObject();
            UserBLL userBLL = new UserBLL();
            var info = userBLL.GetCurrentUser(Request);
            result= JObject.Parse(JsonConvert.SerializeObject(info));
            return result;
        }

        // POST: api/WX_Student
        public void Edit(JObject value)
        {
            UserBLL userBLL = new UserBLL();
            //当前的Request
            var info = userBLL.GetCurrentUser(Request);
            if (info != null)
            {
                info.IDCardNumber = value["IDCardNumber"].Value<string>();
                info.LoginName = value["Name"].Value<string>();
                info.Major = value["Major"].Value<string>();
                info.School = value["School"].Value<string>();
                info.SchoolNumber = value["SchoolNumber"].Value<string>();
                info.QQNumber = value["QQNumber"].Value<string>();
                info.Email = value["Email"].Value<string>();
                info.Name = value["Name"].Value<string>();
            }
            int i = 0;
        }

        // PUT: api/WX_Student/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/WX_Student/5
        public void Delete(int id)
        {
        }
    }
}
