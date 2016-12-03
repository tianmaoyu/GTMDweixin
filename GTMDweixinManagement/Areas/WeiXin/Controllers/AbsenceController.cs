using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GTMDweixinManagement.Areas.WeiXin.Controllers
{
    public class AbsenceController : Controller
    {
        // GET: WeiXin/Absence
        public ActionResult Index()
        {
            return View();
        }

        // GET: WeiXin/Absence/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WeiXin/Absence/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WeiXin/Absence/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: WeiXin/Absence/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WeiXin/Absence/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: WeiXin/Absence/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WeiXin/Absence/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
