using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopManagement.DTO
{
    class HoaDonView
    {
        public int MaHoaDon { get; set; }
        public string TenKhachHang { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
        //public string nameStaff { get; set; }
        public HoaDonView(int ma, string name, DateTime ngl, decimal tien)
        {
            this.MaHoaDon = ma;
            this.TenKhachHang = name;
            this.NgayLap = ngl;
            this.TongTien = tien;
        }
    }
}
