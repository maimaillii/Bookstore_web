using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using HakunaMatata_Bookstore.Models;
    

namespace HakunaMatata_Bookstore.Controllers
{
    public class QuanlydonhangController : Controller
    {
        QuanlynhasachEntities8 db = new QuanlynhasachEntities8();
        // GET: Quanlydonhang
        public ActionResult ChuaThanhToan()
        {
            // Ds đơn hàng chưa thanh toán
            var lst = db.DONDATHANGs.Where(n => n.Dathanhtoan == false).OrderBy(n => n.Ngaydat);
            return View(lst);
        }

        public ActionResult ChuaGiao()
        {
            var lst = db.DONDATHANGs.Where(n => n.Dathanhtoan == true && n.Tinhtranggiaohang == false).OrderByDescending(n => n.Ngaydat);
            return View(lst);
        }

        public ActionResult DaGiaoDathanhtoan()
        {
            var lst = db.DONDATHANGs.Where(n => n.Dathanhtoan == true && n.Tinhtranggiaohang == true).OrderByDescending(n => n.Ngaydat);
            return View(lst);
        }

        [HttpGet]
        public ActionResult DuyetDonHang(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONDATHANG model = db.DONDATHANGs.SingleOrDefault(n => n.MaDDH == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            // Lấy ds chi tiết đơn hàng để hiển thị cho người dùng thấy
            var lstChiTietDH = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == id);
            ViewBag.ListChiTietDH = lstChiTietDH;
            return View(model);
        }

        [HttpPost]
        public ActionResult DuyetDonHang(DONDATHANG ddh)
        {
            // Lấy dữ liệu của đơn hàng đó
            DONDATHANG ddhUpdate = db.DONDATHANGs.Single(n => n.MaDDH == ddh.MaDDH);
            ddhUpdate.Dathanhtoan = ddh.Dathanhtoan;
            ddhUpdate.Tinhtranggiaohang = ddh.Tinhtranggiaohang;
            db.SaveChanges();

            // Lấy ds chi tiết đơn hàng để hiển thị cho người dùng thấy
            var lstChiTietDH = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == ddh.MaDDH);
            ViewBag.ListChiTietDH = lstChiTietDH;
            GuiEmail("HakunaMakata - Xác nhận đơn hàng", "maikin2525@gmail.com", "hakunamakataabookstore@gmail.com", "Mai1022001@", "Đơn hàng của bạn đã được đặt thành công, cảm ơn bạn đã mua sản phẩm của chúng tôi");
            return View(ddhUpdate);
        }

        public void GuiEmail(string Title, string ToEmail, string FromEmail, string PassWord, string Content)
        {
            // goi email
            MailMessage mail = new MailMessage();
            mail.To.Add(ToEmail); // Địa chỉ nhận
            mail.From = new MailAddress(ToEmail); // Địa chửi gửi
            mail.Subject = Title;  // tiêu đề gửi
            mail.Body = Content;                 // Nội dung
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; // host gửi của Gmail
            smtp.Port = 587;               //port của Gmail
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            (FromEmail, PassWord);//Tài khoản password người gửi
            smtp.EnableSsl = true;   //kích hoạt giao tiếp an toàn SSL
            smtp.Send(mail);   //Gửi mail đi
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