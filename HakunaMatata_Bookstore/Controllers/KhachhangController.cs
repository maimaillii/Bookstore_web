using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HakunaMatata_Bookstore.Models;

namespace HakunaMatata_Bookstore.Controllers
{
    public class KhachhangController : Controller
    {
        // GET: Khachhang
        QuanlynhasachEntities8 db = new QuanlynhasachEntities8();
        public ActionResult Index()
        {
            // Truy vấn dữ liệu thông qua câu lệnh
            // Đối tượng lstKH sẽ lấy toàn bộ dữ liệu từ bảng KHACHHANG
            // Cách 1: Lấy dữ liệu là một danh sách khách hàng
            // var lstKH = from KH in db.KHACHHANGs select KH;
            // Cách 2: Dùng phương thức hỗ trợ sẵn
            var lstKH = db.KHACHHANGs;
            return View(lstKH); //Trả về View là lstKH
        }

        public ActionResult Index1()
        {
            //Truy vấn dữ liệu thông qua câu lệnh
            // Đối tượng lstKH sẽ lấy toàn bộ dữ liệu từ bảng KHACHHANG
            var lstKH = from KH in db.KHACHHANGs select KH;
            return View(lstKH); //Trả về View là lstKH
        }

        public ActionResult Truyvan1doituong()
        {
            //Truy vấn một đối tượng bằng câu lệnh truy vấn
            // Giả sử cần lấy ra đối tượng Mai,
            // Bước 1: lấy ra đối tượng khách hàng
            //var lstKH = from KH in db.KHACHHANGs where KH.MaKH == 1 select KH;
            // Bước 2:
            KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n=>n.MaKH == 1); //lấy dòng đầu tiên
            return View(kh); //Trả về View là lstKH
        }
        public ActionResult Textss()
        {
            return View();
        }

        //Xây dựng trang xem chi tiết
        public ActionResult Xemchitiet(int? id)
        {
            //Kiểm tra tham số truyền vào có rỗng hay không, nếu rỗng thì dẫn đến trang 404
            //if(id== null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //nếu không thì truy xuất csdl lấy ra sp tương ứng
            if (id == null) { return View(); }
            SACH s = db.SACHes.SingleOrDefault(N => N.MaSP == id);
            //if (s == null) 
            //{
            //    //Thông báo nếu như không có csdl đó
            //    return HttpNotFound(); 
            //}
            return View(s);
        }
        public ActionResult Sanpham(int? MaloaiSP)
        {
            //Xây dựng action load sp theo mã loại sp
            //if (MaloaiSP == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var lstSP = db.SACHes.Where(n => n.Matheloaii == MaloaiSP);
            //if (lstSP.Count() == 0)
            //{
            //    return HttpNotFound();
            //}
            return View(lstSP);
        }
    }
}