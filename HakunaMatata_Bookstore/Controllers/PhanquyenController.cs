using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HakunaMatata_Bookstore.Models;

namespace HakunaMatata_Bookstore.Controllers
{
    public class PhanquyenController : Controller
    {
        // GET: Phanquyen
        QuanlynhasachEntities8 db = new QuanlynhasachEntities8();
        public ActionResult Index()
        {
            return View(db.LOAITHANHVIENs.OrderBy(n => n.Maloaithanhvien));
        }

        [HttpGet]
        public ActionResult Phanquyen(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            LOAITHANHVIEN ltv = db.LOAITHANHVIENs.SingleOrDefault(n => n.Maloaithanhvien == id);
            if (ltv == null)
            {
                return HttpNotFound();
            }
            // Lấy danh sách quyền để load ra checkbox
            ViewBag.MaQuyen = db.QUYENs;
            //Lấy ra danh sách quyền của loại thành viên được chọn..dựa vào bảng LoaiTV_Quyen
            ViewBag.LoaiTVQuyen = db.LOAITHANHVIEN_QUYEN.Where(n => n.Maloaithanhvien == id);

            return View(ltv);
        }

        [HttpPost]
        public ActionResult Phanquyen(int? MaLoaiTV, IEnumerable<LOAITHANHVIEN_QUYEN> lstPhanQuyen)
        {
            //Trường hợp : Nếu đã đã tiến hành phân quyền rồi nhưng muốn phân quyền lại
            //Bước 1: Xóa những quyền cũa thuộc loại TV đó
            var lstDaPhanQuyen = db.LOAITHANHVIEN_QUYEN.Where(n => n.Maloaithanhvien== MaLoaiTV);
            if (lstDaPhanQuyen.Count() != 0)
            {
                db.LOAITHANHVIEN_QUYEN.RemoveRange(lstDaPhanQuyen);
                db.SaveChanges();
            }
            if (lstPhanQuyen != null)
            {
                //Kiểm tra list danh sách quyền được check
                foreach (var item in lstPhanQuyen)
                {
                    item.Maloaithanhvien = int.Parse(MaLoaiTV.ToString());
                    // Nếu được check thì insert dữ liệu vào bảng phân quyền
                    db.LOAITHANHVIEN_QUYEN.Add(item);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
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