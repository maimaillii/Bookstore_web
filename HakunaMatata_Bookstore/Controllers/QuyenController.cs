using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HakunaMatata_Bookstore.Models;

namespace HakunaMatata_Bookstore.Controllers
{
    public class QuyenController : Controller
    {
        // GET: Quyen
        QuanlynhasachEntities8 db = new QuanlynhasachEntities8();
        public ActionResult Index()
        {
            return View(db.QUYENs.OrderBy(n => n.Tenquyen));
        }

        [HttpGet]
        public ActionResult ThemQuyen()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemQuyen(QUYEN quyen)
        {
            if (ModelState.IsValid)
            {
                db.QUYENs.Add(quyen);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}