using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HakunaMatata_Bookstore.Models;

namespace HakunaMatata_Bookstore.Controllers
{
    public class QuanlyphieunhapController : Controller
    {
        QuanlynhasachEntities8 db = new QuanlynhasachEntities8();
        // GET: QuanLyPhieuNhap
        [HttpGet]
        public ActionResult NhapHang()
        {
            ViewBag.MaNXB = db.NHAXUATBANs;
            ViewBag.ListSanPham = db.SACHes;
            return View();
        }

        [HttpPost]
        public ActionResult NhapHang(PHIEUNHAP model, IEnumerable<CHITIETPHIEUNHAP> lstModel)
        {

            ViewBag.MaNXB = db.NHAXUATBANs;
            ViewBag.ListSanPham = db.SACHes;
            // Kiểm tra dữ liệu đầu vào bằng javascript hay bên metadata đều được
            // Phải ktra để khớp với kiểu dữ liệu của database

            //Gán đã xóa = false
            model.Daxoa = false;
            db.PHIEUNHAPs.Add(model);
            db.SaveChanges();
            // SaveChanges lần đầu để  sinh ra mã phiếu nhập gán cho lstChiTietPhieuNhap
            SACH sp;
            foreach (var item in lstModel)
            {
                // Cập nhật số lượng tồn
                // vì sản phẩm trong lstModel chắc chắn có nên k tạo new SanPham
                sp = db.SACHes.Single(n => n.MaSP == item.MaSP);
                sp.Soluongton += item.Soluongnhap;
                // Gán mã phiếu nhập cho từng chi tiết phiếu nhập
                item.MaPN = model.Maphieunhap;
            }
            // lệnh gán theo list
            db.CHITIETPHIEUNHAPs.AddRange(lstModel);
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult DSSPHetHang()
        {
            // đk số lượng tồn < = 5 .. tình trạng đã xóa false
            var lstSP = db.SACHes.Where(n=> n.Soluongton <= 20);
            return View(lstSP);
        }

        // tạo view phục vụ cho việc nhập hàng đơn
        [HttpGet]
        public ActionResult NhapHangDon(int? id)
        {
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.OrderBy(n => n.TenNXB),"MaNXB", "TenNXB");

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

            return View(sp);
        }
        // Xử lý nhập hàng từng sản phẩm
        [HttpPost]
        public ActionResult NhapHangDon(PHIEUNHAP model, CHITIETPHIEUNHAP ctpn)
        {
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.OrderBy(n => n.TenNXB),"MaNXB","TenNXB");

            model.Daxoa = false;
            model.Ngaynhap = DateTime.Now;
            db.PHIEUNHAPs.Add(model);
            // SAve lấy mã phiếu nhập
            db.SaveChanges();
            ctpn.MaPN = model.Maphieunhap;
            // Cập nhật tồn
            SACH sp = db.SACHes.Single(n => n.MaSP == ctpn.MaSP);
            sp.Soluongton += ctpn.Soluongnhap;
            db.CHITIETPHIEUNHAPs.Add(ctpn);
            db.SaveChanges();
            return RedirectToAction("NhapHangDon", sp.MaSP);
        }


        // Giải phóng vùng nhớ
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                }
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
