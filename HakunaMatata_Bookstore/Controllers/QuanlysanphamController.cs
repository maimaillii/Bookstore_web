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
    [Authorize(Roles = "Quantri")]
    public class QuanlysanphamController : Controller
    {
        QuanlynhasachEntities8 db = new QuanlynhasachEntities8();
        // GET: Admin_quanlysanpham
        public ActionResult Index()
        {
            return View(db.SACHes);
        }


        public ActionResult Taomoi()
        {
            //load dropdownlist nhà cung cấp
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            ViewBag.MaTheloaii = new SelectList(db.THELOAIs.OrderBy(n => n.Matheloaii), "MaTheloaii", "Tentheloai");
            ViewBag.MaTG = new SelectList(db.TACGIAs.OrderBy(n => n.MaTG), "MaTG", "Tentacgia");
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Taomoi(SACH sach, HttpPostedFileBase Hinhanh)
        {

            //load dropdownlist nhà cung cấp
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            ViewBag.MaTheloaii = new SelectList(db.THELOAIs.OrderBy(n => n.Matheloaii), "MaTheloaii", "Tentheloai");
            ViewBag.MaTG = new SelectList(db.TACGIAs.OrderBy(n => n.MaTG), "MaTG", "Tentacgia");

            //Kiểm tra trùng hình ảnh
            if (Hinhanh.ContentLength > 0)
            {
                //Lấy tên hình ảnh
                var fileName = Path.GetFileName(Hinhanh.FileName);
                //Lấy hình ảnh chuyển vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/Content/Sach"), fileName);
                //nếu thư mục chứa hình ảnh đó rồi thì xuất ra thông báo
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình ảnh đã tồn tại";
                    return View();
                }
                else
                {
                    //Lấy hình ảnh đưa vào thư mục HinhanhSP
                    Hinhanh.SaveAs(path);
                    sach.Hinhanh = fileName;
                }
            }
            db.SACHes.Add(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        [HttpGet]
        public ActionResult ChinhSua(int? id)
        {
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


            //Load dropdownlist nhà cung cấp và loại sản phẩm
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            ViewBag.MaTheloaii = new SelectList(db.THELOAIs.OrderBy(n => n.Matheloaii), "MaTheloaii", "Tentheloai");
            ViewBag.MaTG = new SelectList(db.TACGIAs.OrderBy(n => n.MaTG), "MaTG", "Tentacgia");

            return View(sp);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(SACH model)
        {
            //Load dropdownlist nhà cung cấp và loại sản phẩm
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            ViewBag.MaTheloaii = new SelectList(db.THELOAIs.OrderBy(n => n.Matheloaii), "MaTheloaii", "Tentheloai");
            ViewBag.MaTG = new SelectList(db.TACGIAs.OrderBy(n => n.MaTG), "MaTG", "Tentacgia");

            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Xoa(int? id)
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
        public ActionResult Xoa(int? id, FormCollection f)
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
            return RedirectToAction("Index");
        }
    }
}