using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HakunaMatata_Bookstore.Models;

namespace HakunaMatata_Bookstore.Controllers
{
    public class GiohangController : Controller
    {
        QuanlynhasachEntities8 db = new QuanlynhasachEntities8();

        //Lấy giỏ hàng
        public List<ItemGioHang> Laygiohang()
        {
            //Giỏ hàng đã tồn tại
            List<ItemGioHang> lstGiohang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGiohang == null)
            {
                //Nếu session giỏ hàng chưa tồn tại thì khởi tạo giỏ hàng
                lstGiohang = new List<ItemGioHang>();
                Session["GioHang"] = lstGiohang;
            }
            return lstGiohang;
        }

        //Thêm giỏ hàng thông thường (Load lại trang)
        public ActionResult Themgiohang(int MaSP, string strURL)
        {
            //Kiểm tra sản phẩm có tồn tại trong CSDL hay không
            SACH sp = db.SACHes.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                //404
                Response.StatusCode = 404;
                return null;
            }
            //Tạo list giỏ hàng(Lấy giỏ hàng)
            List<ItemGioHang> lstGioHang = Laygiohang();
            //Trường hNếu sản phẩm đã tồn tại trong giỏ hàng
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck != null)
            {
                //Kiểm tra số lượng tồn trước khi cho khách hàng đặt hàng
                if (sp.Soluongton < spCheck.Soluong)
                {
                    return View("Thongbao");
                }
                spCheck.Soluong++;
                spCheck.Thanhtien = spCheck.Soluong * spCheck.DonGia;
                return Redirect(strURL);
            }
            ItemGioHang itemGH = new ItemGioHang(MaSP);
            if (sp.Soluongton < itemGH.Soluong)
            {
                return View("Thongbao");
            }
            lstGioHang.Add(itemGH);
            return Redirect(strURL);

        }

        //Tính tổng số lượng
        public double TinhTongSoLuong()
        {
            //Lấy giỏ hàng
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.Soluong);
        }

        //Tính tổng tiền
        public decimal TinhTongTien()
        {
            //Lấy giỏ hàng
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.Thanhtien);
        }

        public ActionResult GiohangPartial()
        {
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();

            return PartialView();
        }

        // GET: Giohang
        public ActionResult Xemgiohang()
        {
            //Lấy giỏ hàng
            List<ItemGioHang> lstGioHang = Laygiohang();
            return View(lstGioHang);
        }



        //--------------------------------

        //Chỉnh sửa giỏ hàng
        [HttpGet]
        public ActionResult Suagiohang(int MaSP)
        {
            //Kiểm tra session giỏ hàng tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Kiểm tra sản phẩm có tồn tại trong CSDL hay không
            SACH sp = db.SACHes.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                //404
                Response.StatusCode = 404;
                return null;
            }
            //Lấy list giỏ hàng từ session
            List<ItemGioHang> lstGioHang = Laygiohang();
            //Kiểm tra xem sản phẩm đó tồn tại trong giỏ hàng hay không
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Lấy list giỏ hàng tạo giao diện
            ViewBag.GioHang = lstGioHang;
            //Nếu tồn tại rồi
            return View(spCheck);
        }

        //Xử lý cập nhật giỏ hàng
        [HttpPost]
        public ActionResult Capnhatgiohang(ItemGioHang itemGH)
        {
            //Kiểm tra số lượng tồn
            SACH spCheck = db.SACHes.SingleOrDefault(n => n.MaSP == itemGH.MaSP);
            if (spCheck.Soluongton < itemGH.Soluong)
            {
                return View("ThongBao");
            }
            // Cập nhật số lượng giỏ hàng trong session giỏ hàng
            List<ItemGioHang> lstGH = Laygiohang();
            //Bước 2: Lấy sản phẩm cần cập nhật từ list<Giohang> ra
            ItemGioHang itemGHUpdate = lstGH.Find(n => n.MaSP == itemGH.MaSP);
            //Bước 3: Tiến hành cập nhật lại số lượng
            itemGHUpdate.Soluong = itemGH.Soluong;
            itemGHUpdate.Thanhtien = itemGHUpdate.Soluong * itemGHUpdate.DonGia;
            return RedirectToAction("Xemgiohang");
        }

        public ActionResult Xoagiohang(int MaSP)
        {
            //Kiểm tra session giỏ hàng tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Kiểm tra sản phẩm có tồn tại trong CSDL hay không
            SACH sp = db.SACHes.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                //404
                Response.StatusCode = 404;
                return null;
            }
            //Lấy list giỏ hàng từ session
            List<ItemGioHang> lstGioHang = Laygiohang();
            //Kiểm tra xem sản phẩm đó tồn tại trong giỏ hàng hay không
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Xóa item trong giỏ hàng
            lstGioHang.Remove(spCheck);
            return RedirectToAction("Xemgiohang");
        }



        //Xây dựng chức năng đặt hàng
        public ActionResult DatHang(KHACHHANG kh)
        {
            //Kiểm tra session giỏ hàng tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            KHACHHANG khang = new KHACHHANG();
            if (Session["Taikhoan"] == null)
            {
                //Thêm khách hàng vào bảng khách hàng với khách hàng chưa có tài khoản
                khang = kh;
                db.KHACHHANGs.Add(khang);
                db.SaveChanges();
            }
            else
            {
                //Đối với khách hàng là thành viên
                THANHVIEN tv = Session["Taikhoan"] as THANHVIEN;
                khang.Diachi = tv.Diachi;
                khang.SĐT = tv.Sodienthoai;
                khang.Email = tv.Email;
                db.KHACHHANGs.Add(khang);
                db.SaveChanges();

            }

            //Thêm đơn hàng
            DONDATHANG ddh = new DONDATHANG();
            ddh.MaKH = khang.MaKH;
            ddh.Ngaydat = DateTime.Now;
            ddh.Tinhtranggiaohang = false;
            ddh.Dathanhtoan = false;
            ddh.Dahuy = false;
            ddh.Daxoa = false;
            db.DONDATHANGs.Add(ddh);
            db.SaveChanges();
            //Them chi tiet don dat hang
            List<ItemGioHang> lstGH = Laygiohang();
            foreach (var item in lstGH)
            {
                CHITIETDONDATHANG ctdh = new CHITIETDONDATHANG();
                ctdh.MaDDH = ddh.MaDDH;
                ctdh.MaSP = item.MaSP;
                ctdh.TenSP = item.TenSP;
                ctdh.Soluong = item.Soluong;
                ctdh.Dongia = item.DonGia;
                db.CHITIETDONDATHANGs.Add(ctdh);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("Xemgiohang");
        }


    }
}