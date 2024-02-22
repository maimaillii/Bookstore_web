using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HakunaMatata_Bookstore.Models;

namespace HakunaMatata_Bookstore.Controllers
{
    public class QuanlynhaxuatbanController : Controller
    {
        // GET: Quanlynhaxuatban
        QuanlynhasachEntities8 db = new QuanlynhasachEntities8();
        public ActionResult Indexnhaxuatban()
        {
            return View(db.NHAXUATBANs);
        }

        [HttpGet]
        public ActionResult TaomoiNXB()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaomoiNXB(NHAXUATBAN nhaxuatban)
        {
            db.NHAXUATBANs.Add(nhaxuatban);
            db.SaveChanges();
            return RedirectToAction("Indexnhaxuatban");
        }
    }
}