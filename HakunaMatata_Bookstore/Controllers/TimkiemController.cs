using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HakunaMatata_Bookstore.Models;

namespace HakunaMatata_Bookstore.Controllers
{
    public class TimkiemController : Controller
    {
        // GET: Timkiem
        QuanlynhasachEntities8 db= new QuanlynhasachEntities8();
        public ActionResult KQTimkiem(string sTukhoa)
        {
            //Tìm kiếm theo tên sản phẩm
            var lstSP = db.SACHes.Where(n=>n.TenSP.Contains(sTukhoa));
            return View(lstSP.OrderBy(n=>n.TenSP));
        }
    }
}