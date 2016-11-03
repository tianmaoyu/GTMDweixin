using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GTMDweixinManagement.Areas.WeiXin.Controllers
{
    public class AccountController : Controller
    {
        // GET: WeiXin/Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: WeiXin/Account/Login/5
        public ActionResult Login()
        {
            return View();
        }

        // GET: WeiXin/Account/Login/5
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {

            return View();
        }
        // GET: WeiXin/Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // GET: WeiXin/Account/Register
        public ActionResult ModifyPassword()
        {
            return View();
        }
    }
}
