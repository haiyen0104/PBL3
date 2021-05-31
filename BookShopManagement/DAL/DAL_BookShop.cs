using BookShopManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopManagement.DAL
{
    class DAL_BookShop
    {
        private static DAL_BookShop _Instance;
        public static DAL_BookShop Instance
        {
            get
            {
                if (_Instance == null)
                {

                    _Instance = new DAL_BookShop();
                }
                return _Instance;
            }
            private set { }
        }
        public DAL_BookShop() { }
        public List<HoaDon> GetAllHoaDon_DAL()
        {
            List<HoaDon> data = new List<HoaDon>();
            string query = "select * from HoaDon";
            foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
                data.Add(GetHD(i));
            }
            return data;
        }
        public HoaDon GetHD(DataRow i)
        {
            return new HoaDon
            {
                MaHoaDon = Convert.ToInt32(i["MaHoaDon"]),
                TenKhachHang = i["TenKhachHang"].ToString(),
                NgayLap = Convert.ToDateTime(i["NgayLap"]),
                TongTien = Convert.ToDecimal(i["TongTien"]),
                ID_Staff = Convert.ToInt32(i["ID_Staff"])
            };
        }
        //public List<Staff> GetAllStaff_DAL()
        //{
        //    List<Staff> data = new List<Staff>();
        //    string query = "select * from Staff";
        //    foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
        //    {
        //        data.Add(GetStaff(i));
        //    }
        //    return data;
        //}
        //public Staff GetStaff(DataRow i)
        //{
        //    return new Staff
        //    {
        //        ID_Staff = Convert.ToInt32(i["ID_Staff"]),
        //        NameStaff= i["Name_Staff"].ToString(),
        //        Gender = Convert.ToBoolean(i["Gender"]),
        //        Birth = Convert.ToDateTime(i["DateOfBirth"]),
        //        IdUser = Convert.ToInt32(i["ID_User"]),
        //        Address = i["Address"].ToString()
        //    };
        //}

        public List<ChiTietHD> GetAllChiTietHD_DAL()
        {
            List<ChiTietHD> cthd = new List<ChiTietHD>();
            string query = "select * from ChiTietHoaDon";
            foreach(DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
                cthd.Add(GetChiTietHD(i));
            }
            return cthd;
        }
        public ChiTietHD GetChiTietHD(DataRow i)
        {
            return new ChiTietHD
            {
                MaHoaDon = Convert.ToInt32(i["MaHoaDon"].ToString()),
                MaSach = Convert.ToInt32(i["MaSach"].ToString()),
                SoLuong = Convert.ToInt32(i["SoLuong"].ToString()),
                MucGiamGia = float.Parse((string)i["MucGiamGia"].ToString())
            };
        }
        public List<Sach> GetAllSach_DAL()
        {
            List<Sach> s = new List<Sach>();
            string query = "select * from Sach";
            foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
                s.Add(GetSach(i));
            }
            return s;
        }
        public Sach GetSach(DataRow i)
        {
            return new Sach
            {
                MaSach = Convert.ToInt32(i["MaSach"]),
                TenSach = i["TenSach"].ToString(),
                DonGia = Convert.ToInt32(i["GiaMua"]),
                TenLoaiSach = i["TenLoaiSach"].ToString(),
                TenTacGia = i["TenTacGia"].ToString(),
                TenLinhVuc = i["TenLinhVuc"].ToString(),
            };
        }
        public List<SachKhuyenMai> GetAllSachKM_DAL()
        {
            List<SachKhuyenMai> data = new List<SachKhuyenMai>();
            string query = "select * from SachKhuyenMai";
            foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
                data.Add(GetSKM(i));
            }
            return data;
        }
        public SachKhuyenMai GetSKM(DataRow i)
        {
            return new SachKhuyenMai
            {
                MaSach = Convert.ToInt32(i["MaSach"]),
                MucGiamGia = float.Parse(i["MucGiamGia"].ToString())
            };
        }
            public void AddHD_DAL(HoaDon hd)
        {
            string query = "insert into HoaDon values ('" + hd.TenKhachHang + "','" + hd.NgayLap + "','" + hd.TongTien + "','" + hd.ID_Staff + "')";
            DBHelper.Instance.ExcuteDB(query);
        }
        public void AddCTHD_DAL(ChiTietHD dt)
        {
            string query = "insert into ChiTietHoaDon values ('" + dt.MaHoaDon + "','" + dt.MaSach + "','" + dt.SoLuong + "','" + dt.MucGiamGia + "')";
            DBHelper.Instance.ExcuteDB(query);
        }
        public void EditHD(HoaDon dt)
        {
            string query = "update HoaDon set TenKhachHang = '" + dt.TenKhachHang + "', NgayLap = '" + dt.NgayLap + "', TongTien = '" + dt.TongTien + "', ID_Staff = '" + dt.ID_Staff + "' where MaHoaDon = '" + dt.MaHoaDon + "'";
            DBHelper.Instance.ExcuteDB(query);
        }
        public void EditCTHD(ChiTietHD dt)
        {
            string query = "update ChiTietHoaDon set MaSach = '" + dt.MaSach + "', SoLuong = " + dt.SoLuong + "', MucGiamGia = " + dt.MucGiamGia + " 'where MaHoaDon = '" + dt.MaHoaDon + "'";
            DBHelper.Instance.ExcuteDB(query);
        }
        public void DelHD_DAL(List<int> Mahd)
        {
            foreach(int i in Mahd)
            {
                string query1 = "delete from HoaDon where MaHoaDon = '" + i + "'";
                string query2 = "delete from ChiTietHoaDon where MaHoaDon = '" + i + "'";
                DBHelper.Instance.ExcuteDB(query2);
                DBHelper.Instance.ExcuteDB(query1);
            }
        }
        public void DelCTHD_DAL(int Ma)
        {
                string query1 = "delete from ChiTietHoaDon where MaHoaDon = '" + Ma + "'";
                DBHelper.Instance.ExcuteDB(query1);
        }
        //public void EditStaff(Staff dt)
        //{
        //    string query = "update Staff set Name_Staff = '" + dt.NameStaff + "', Gender = '" + dt.Birth + "', Address = '" + dt.Address + "', ID_User = '" + dt.IdUser + "' where ID_Staff = '" + dt.ID_Staff + "'";
        //    DBHelper.Instance.ExcuteDB(query);
        //}
    }
}
