using GTMDweixinManagement.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GTMDweixinManagement.Areas.WeiXin.Controllers
{
    public class StudentSignController : Controller
    {
        // GET: WeiXin/StudentSign
        public ActionResult Index()
        {
            return View();
        }

        // GET: WeiXin/StudentSign/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WeiXin/StudentSign/Create
        public ActionResult Create(int signID=-1)
        {
            if (signID != -1)
            {
                var info = new SignBLL().GetInfoByID(signID);
                if (info != null)
                {
                    return View(info);
                }
            }
            return View();
        }

        // GET: WeiXin/StudentSign/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

    }
}
