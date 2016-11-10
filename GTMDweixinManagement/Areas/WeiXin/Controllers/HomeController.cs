using GTMDweixinManagement.BLL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace GTMDweixinManagement.Areas.WeiXin.Controllers
{
    public class HomeController : Controller
    {
        // GET: WeiXin/Home

        public ActionResult Index()
        {
            //设置cookie
            String phoneNumber = Request.QueryString["phoneNumber"];
            string password = Request.QueryString["password"];
            HttpCookie cookie = new HttpCookie("session");
            cookie["mobilePhone"] = phoneNumber;
            cookie["password"] = password;
            cookie.Expires =DateTime.Now.AddDays(30);
            HttpContext.Response.Cookies.Add(cookie);
            return View();
        }
       
    }
}
