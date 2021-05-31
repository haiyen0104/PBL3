using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopManagement.DTO
{
    class HoaDon
    {
        public int MaHoaDon { get; set; }
        public string TenKhachHang { get; set; }
        public DateTime NgayLap { get; set; }
        public Decimal TongTien { get; set; }
        public int ID_Staff { get; set; }
    }
}
