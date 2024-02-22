using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HakunaMatata_Bookstore.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
using System.Web.Security;

namespace HakunaMatata_Bookstore.Controllers
{
    public class HomeController : Controller
    {
        QuanlynhasachEntities8 db = new QuanlynhasachEntities8();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Xemchitiet(int? id)
        {
            //Kiểm tra tham số truyền vào có rỗng hay không, nếu rỗng thì dẫn đến trang 404
            //if(id== null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //nếu không thì truy xuất csdl lấy ra sp tương ứng
            SACH s = db.SACHes.SingleOrDefault(N => N.MaSP == id);
            //if (s == null) 
            //{
            //    //Thông báo nếu như không có csdl đó
            //    return HttpNotFound(); 
            //}
            return View(s);
        }
        public ActionResult Trangsanpham(int? MaloaiSP)
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
        public ActionResult Tintuc()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Even()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Giohang()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dangki()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangki(THANHVIEN tv)
        {
            //Kiểm tra capcha hợp lệ
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                ViewBag.Thongbao = "Thêm thành công";
                //Thêm thành viên vào csdl
                db.THANHVIENs.Add(tv); //add
                db.SaveChanges(); //lưu
                return View();
            }
            ViewBag.Thongbao = "**Bạn đã nhập sai mã Captcha!";
            return View();

        }

        [HttpGet]
        public ActionResult Dangki1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangki1(FormCollection f)
        {
            return View();
        }

        public ActionResult MenuPartial()
        {
            var lstSP = db.SACHes;
            return PartialView(lstSP);
        }

        public ActionResult Textss()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dangnhap(FormCollection f)
        {

            //kiểm tra tên đăng nhập và mật khẩu
            string Taikhoan = f["Taikhoan"].ToString();
            string Matkhau = f["Matkhau"].ToString();
            THANHVIEN tv = db.THANHVIENs.SingleOrDefault(n => n.Taikhoan == Taikhoan && n.Matkhau == Matkhau);
            if (tv != null)
            {
                //Láy ra List quyền của thành viên tương ứng với loại thành viên
                var lstQuyen = db.LOAITHANHVIEN_QUYEN.Where(n => n.Maloaithanhvien == tv.Maloaithanhvien);
                //Duyệt list quyền
                string Quyen = "";
                foreach (var item in lstQuyen)
                {
                    Quyen += item.QUYEN.Maquyen + ",";
                }
                // Cắt dấu ","
                Quyen = Quyen.Substring(0, Quyen.Length - 1);
                PhanQuyen(tv.Taikhoan, Quyen);
                Session["TaiKhoan"] = tv;
                return Content("<script>window.location.href = '/Home/Index/'</script>");
            }
            return Content("Tài khoản hoặc mật khẩu không đúng!");

        }

        public void PhanQuyen(string TaiKhoan, string Quyen)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1,
                                          TaiKhoan, //user
                                          DateTime.Now, //Thời gian bắt đầu
                                          DateTime.Now.AddHours(3), //Thời gian kết thúc
                                          false, //Ghi nhớ?
                                          Quyen, // "DangKy,QuanLyDonHang,QuanLySanPham"
                                          FormsAuthentication.FormsCookiePath);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
        }

        // Tạo trang ngăn chặn truy cập
        public ActionResult LoiPhanQuyen()
        {
            return View();
        }



        public ActionResult LogOut()
        {
            Session["Taikhoan"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}