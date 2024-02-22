using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HakunaMatata_Bookstore.Models
{
    public class ItemGioHang
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int Soluong { get; set; }
        public decimal DonGia { get; set; }
        public string Hinhanh { get; set; }
        public decimal Thanhtien { get; set; }


        public ItemGioHang(int imaSP)
        {
            using (QuanlynhasachEntities8 db = new QuanlynhasachEntities8())
            {
                this.MaSP = imaSP;
                SACH s = db.SACHes.Single(n => n.MaSP == imaSP);
                this.TenSP = s.TenSP;
                this.Hinhanh = s.Hinhanh;
                this.DonGia = s.Dongia.Value;
                this.Soluong = 1;
                this.Thanhtien = DonGia * Soluong;
            }
        }

        public ItemGioHang()
        {

        }


        public ItemGioHang(int imaSP, int sluong)
        {
            using(QuanlynhasachEntities8 db= new QuanlynhasachEntities8()){
                this.MaSP = imaSP;
                SACH s = db.SACHes.Single(n => n.MaSP == imaSP);
                this.TenSP = s.TenSP;
                this.Soluong = 1;
                this.Hinhanh = s.Hinhanh;
                this. DonGia = s.Dongia.Value;
                this.Thanhtien = DonGia * Soluong;

            }
        }


    }
    
}