//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HakunaMatata_Bookstore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class THANHVIEN
    {
        public int Mathanhvien { get; set; }
        public string Taikhoan { get; set; }
        public string Matkhau { get; set; }
        public string Diachi { get; set; }
        public string Email { get; set; }
        public string Sodienthoai { get; set; }
        public Nullable<int> Maloaithanhvien { get; set; }
    
        public virtual LOAITHANHVIEN LOAITHANHVIEN { get; set; }
    }
}
