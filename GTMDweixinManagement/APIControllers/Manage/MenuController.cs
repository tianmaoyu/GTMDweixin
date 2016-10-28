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
    public class MenuController : ApiController
    {
        [HttpPost]
        public JArray GetTree(int? selectedID)
        {
            MenuBLL menuBll = new MenuBLL();
            return menuBll.GetTree(selectedID);
        }

        // Post: api/User/Add
        [HttpPost]
        public string Add(JObject pagerParas)
        {
            var id = HttpContext.Current.Request.QueryString["id"];
            //var form = HttpContext.Current.Request.Form;

            MenuBLL menuBll = new MenuBLL();
            if (id != null && Int32.Parse(id) > 0)
            {

                menuBll.Updata(pagerParas, Int32.Parse(id));
            }
            else
            {
                menuBll.Add(pagerParas);
            }
            return "1";
        }

        // DELETE: api/User/Delete/5
        [HttpPost]
        public JObject Delete(JObject josn)
        {
            var id = josn["ID"].Value<int>();
            MenuBLL menuBll = new MenuBLL();
            var deleteSuccess = menuBll.Delete(id);
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

        public JObject GetMenuInfo(int id)
        {
            MenuBLL menuBll = new MenuBLL();
            //var id = json["ID"].Value<int>();
            return menuBll.GetInfo(id);
        }

        public JObject Edit(JObject pagerParas)
        {
            MenuBLL menuBll = new MenuBLL();
            menuBll.Add(pagerParas);
            return new JObject();
        }
    }
}
