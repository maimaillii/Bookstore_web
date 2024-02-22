using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HakunaMatata_Bookstore.Models;
using PagedList;

namespace HakunaMatata_Bookstore.Controllers
{
    public class DanhmucsanphamController : Controller
    {
        
        QuanlynhasachEntities8 db = new QuanlynhasachEntities8();
        // GET: Danhmucsanpham
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Danhmucsanpham()
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
            if(id == null) { return View(); }
            SACH s= db.SACHes.SingleOrDefault(N=>N.MaSP == id);
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
        public ActionResult Dangki()
        {
            return View();
        }
        public ActionResult Sanpham(int? MaloaiSP, int? page)
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

            //Thực hiện chức năng phân trang
            // tạo biến số sp trên trang
            if(Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int Pagesize = 20;
            // Số trang hiện tại
            int Pagenumber = (page ?? 1);
            ViewBag.MaloaiSP= MaloaiSP;
            return View(lstSP.OrderBy(n=>n.MaSP).ToPagedList(Pagenumber,Pagesize));
        }


        // Load sp theo mã nhà xuất bản

        public ActionResult Sanphamtheonhaxuatban(int? MaNXB)
        {
            var lstSP = db.SACHes.Where(n => n.MaNXB == MaNXB);
            return View(lstSP);
        }
    }
}