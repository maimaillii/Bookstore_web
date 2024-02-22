using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HakunaMatata_Bookstore.Models;

namespace HakunaMatata_Bookstore.Controllers
{
    public class TimkiemsachdathemController : Controller
    {
        // GET: Timkiemsachdathem
        QuanlynhasachEntities8 db = new QuanlynhasachEntities8();
        public ActionResult KQTimkiemm(string Tukhoa)
        {
            //Tìm kiếm theo tên sản phẩm
            var lstSP = db.SACHes.Where(n => n.TenSP.Contains(Tukhoa));
            return View(lstSP.OrderBy(n => n.TenSP));
        }

        [HttpGet]
        public ActionResult Xoaa(int? id)
        {

            //Lấy sản phẩm cần chỉnh sửa dựa vào id
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SACH sp = db.SACHes.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            //Load dropdownlist nhà cung cấp và dropdownlist loại sp, mã nhà sản xuất
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            ViewBag.MaTheloaii = new SelectList(db.THELOAIs.OrderBy(n => n.Matheloaii), "MaTheloaii", "Tentheloai");
            ViewBag.MaTG = new SelectList(db.TACGIAs.OrderBy(n => n.MaTG), "MaTG", "Tentacgia");
            return View(sp);
        }

        [HttpPost]
        public ActionResult Xoaa(int? id, FormCollection f)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH model = db.SACHes.SingleOrDefault(n => n.MaSP == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.SACHes.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Xoathanhcong");
        }
        public ActionResult Xoathanhcong()
        {
            return View();
        }
    }
}