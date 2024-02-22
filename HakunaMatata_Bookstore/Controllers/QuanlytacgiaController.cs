using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HakunaMatata_Bookstore.Models;

namespace HakunaMatata_Bookstore.Controllers
{
    public class QuanlytacgiaController : Controller
    {
        // GET: Quanlytacgia
        QuanlynhasachEntities8 db = new QuanlynhasachEntities8();
        // GET: Admin_quanlysanpham
        public ActionResult Index()
        {
            return View(db.TACGIAs);
        }

        [HttpGet]
        public ActionResult Taomoitacgia()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Taomoitacgia(TACGIA tacgia)
        {
            db.TACGIAs.Add(tacgia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}