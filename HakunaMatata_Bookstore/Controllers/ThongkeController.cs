using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HakunaMatata_Bookstore.Models;

namespace HakunaMatata_Bookstore.Controllers
{
    public class ThongkeController : Controller
    {
        QuanlynhasachEntities8 db = new QuanlynhasachEntities8();
        // GET: Thongke
        public ActionResult Index()
        {
            ViewBag.TongDoanhThu = ThongKeDoanhThu();
            ViewBag.TongDDH = ThongKeDonHang();
            ViewBag.TongKH = ThongKeKhachHang();
            ViewBag.TongThanhVien = TongThanhVien();
            ViewBag.TongDoanhThuTheoThang = ThongKeDoanhThuTheoThang(01, 2022);
            return View();
        }

        public decimal? ThongKeDoanhThu()
        {
            decimal? TongDoanhThu = db.CHITIETDONDATHANGs.Sum(n => n.Dongia * n.Soluong);
            return TongDoanhThu;
        }

        public double ThongKeDonHang()
        {
            //Đếm đơn đặt hàng
            double sl = db.DONDATHANGs.Count();
            return sl;
        }

        public double ThongKeKhachHang()
        {
            //Đếm đơn đặt hàng
            double sl = db.KHACHHANGs.Count();
            return sl;
        }

        public double TongThanhVien()
        {
            double slTV = db.THANHVIENs.Count();
            return slTV;
        }

        public decimal? ThongKeDoanhThuTheoThang(int Thang, int Nam)
        {
            var lstDDH = db.DONDATHANGs.Where(n => n.Ngaydat.Value.Month == Thang && n.Ngaydat.Value.Year == Nam);
            decimal? TongTien = 0;
            //Duyệt chi tiết đơn hàng theo điều kiện
            foreach (var item in lstDDH)
            {
                TongTien += item.CHITIETDONDATHANGs.Sum(n => n.Dongia * n.Soluong);
            }
            return TongTien;
        }
        [HttpPost]
        public ActionResult Index(FormCollection f)
        {
            int Thang = Convert.ToInt32(f["txtThang"].ToString());
            int Nam = Convert.ToInt32(f["txtNam"].ToString());
            decimal? tongtien = ThongKeDoanhThuTheoThang(Thang, Nam);
            return Content(tongtien.ToString());
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}