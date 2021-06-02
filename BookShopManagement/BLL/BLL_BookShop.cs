using BookShopManagement.DAL;
using BookShopManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopManagement.BLL
{
    class BLL_BookShop
    {
        
        private static BLL_BookShop _Instance;

        public static BLL_BookShop Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_BookShop();
                }
                return _Instance;
            }
            private set { }
        }

        private BLL_BookShop() { }
        public List<HoaDon> GetAllHoaDon_BLL()
        {
            DAL_BookShop dal = new DAL_BookShop();
            return dal.GetAllHoaDon_DAL();
        }
        public HoaDon GetHoaDon_ByMaHD(int ma)
        {
            HoaDon s = new HoaDon();
            foreach (HoaDon i in GetAllHoaDon_BLL())
            {
                if (i.MaHoaDon == ma) s = i;
            }
            return s;
        }
        public List<HoaDonView> ListAllHDView_BLL(int maHD,string nameKH)
        {
            List<HoaDonView> l = new List<HoaDonView>();
            foreach (HoaDon i in GetAllHoaDon_BLL())
            {
                HoaDonView s = new HoaDonView(i.MaHoaDon, i.TenKhachHang, i.NgayLap, i.TongTien);
                l.Add(s);
            }

            List<HoaDonView> result = new List<HoaDonView>();
            if (maHD != 0)
            {
                foreach (HoaDonView i in l)
                {
                    if (i.MaHoaDon == maHD && i.TenKhachHang.Contains(nameKH))
                    {
                        result.Add(i);
                    }
                }
            }
            else
            {
                if (nameKH == null) result = l;
                else
                {
                    foreach (HoaDonView i in l)
                    {
                        if (i.TenKhachHang.Contains(nameKH))
                        {
                            result.Add(i);
                        }
                    }
                }
            }
            return result;
        }
        public List<ChiTietHD> GetAllChiTietHD_BLL()
        {
            DAL_BookShop dal = new DAL_BookShop();
            return dal.GetAllChiTietHD_DAL();
        }
        public List<Sach> GetAllSach_BLL()
        {
            DAL_BookShop dal = new DAL_BookShop();
            return dal.GetAllSach_DAL();
        }
        public List<HoaDonView> GetAllHoaDonView()
        {
            List<HoaDonView> hd = new List<HoaDonView>();
            foreach(HoaDon i in GetAllHoaDon_BLL())
            {
                HoaDonView s = new HoaDonView(i.MaHoaDon, i.TenKhachHang, i.NgayLap, i.TongTien);
                hd.Add(s);
            }
            return hd;
        }
        public Sach GetSach_ByMaSach(int ms)
        {
            Sach s = new Sach();
            foreach (Sach i in GetAllSach_BLL())
            {
                if (i.MaSach == ms)
                {
                    s = i;
                }
            }
            return s;
        }
        public List<TTSach> GetTTSach_ByMaHD(int Ma)
        {
            List<TTSach> l = new List<TTSach>();
            foreach (ChiTietHD i in GetAllChiTietHD_BLL())
            {
                if(i.MaHoaDon == Ma)
                {
                    TTSach s = new TTSach();
                    //s.MaHD = Ma;
                    s.MaSach = i.MaSach;
                    s.TenSach = GetSach_ByMaSach(i.MaSach).TenSach;
                    s.SoLuong = i.SoLuong;
                    s.DonGia = GetSach_ByMaSach(i.MaSach).DonGia;
                    s.MucGiamGia = i.MucGiamGia;
                    s.ThanhTien = Convert.ToDecimal(s.SoLuong * s.DonGia - s.DonGia * (s.MucGiamGia / 100));
                    l.Add(s);
                }
            }
            return l;
        }
        public List<SachKhuyenMai> GetAllSachKM_DAL()
        {
            return DAL_BookShop.Instance.GetAllSachKM_DAL();
        }
        public float GetMucGiamGia_ByMaSach(int MaSach)
        {
            float a = 0;
            foreach(SachKhuyenMai i in DAL_BookShop.Instance.GetAllSachKM_DAL())
            {
                if (i.MaSach == MaSach) a = i.MucGiamGia;
            }
            return a;
        }
        public int GetMaHDcuoi()
        {
            return DAL_BookShop.Instance.getmaTT();
        }
        public List<int> GetAllMaHD_ByNgaylap(string s)
        {
            List<int> ds = new List<int>();
            foreach(HoaDon i in DAL_BookShop.Instance.GetAllHoaDon_DAL())
            {

                if (i.NgayLap == Convert.ToDateTime(s))
                {
                    ds.Add(i.MaHoaDon);
                }
            }
            return ds;
        }
        public List<TTSach> GetAllSachBan(DateTime s)
        {
            List<TTSach> l = new List<TTSach>();
            string date = string.Format("{0}-{1}-{2}", s.Year, s.Month, s.Day);
            foreach (int i in GetAllMaHD_ByNgaylap(date))
            {
                foreach (TTSach n in GetTTSach_ByMaHD(i))
                {
                    if (l != null && l.Count == 0)
                    {
                        TTSach a = new TTSach();
                        a.MaSach = n.MaSach;
                        a.TenSach = n.TenSach;
                        a.SoLuong = n.SoLuong;
                        a.DonGia = n.DonGia;
                        a.MucGiamGia = n.MucGiamGia;
                        a.ThanhTien = n.ThanhTien;
                        decimal ce = a.ThanhTien;
                        l.Add(a);
                    }
                    else
                    {
                        int dem = 0;
                        foreach (TTSach z in l)
                        {
                            if (z.MaSach == n.MaSach)
                            {
                                z.SoLuong += n.SoLuong;
                                z.ThanhTien += n.ThanhTien;
                                dem = 1;break;
                            }
                        }
                        if (dem == 0) l.Add(n);
                        
                    }
                }
            }
            return l;
        }
        public List<Kho> GetAllKho_BLL()
        {
            return DAL_BookShop.Instance.GetAllKho_DAL();
        }
        public Kho GetKho_ByMaSach(int ma)
        {
            Kho s = new Kho();
            foreach(Kho i in DAL_BookShop.Instance.GetAllKho_DAL())
            {
                if (i.MaSach == ma) s=i;
            }
            return s;
        }
        public void AddHD_BLL(HoaDon s)
        {
            DAL_BookShop.Instance.AddHD_DAL(s);
        }
        public void AddCTHD_BLL(ChiTietHD s)
        {
            DAL_BookShop.Instance.AddCTHD_DAL(s);
        }
        public void UpdateHD_BLL(HoaDon s)
        {
            DAL_BookShop.Instance.EditHD(s);
        }
        public void UpdateCTHD_BLL(ChiTietHD s)
        {
            DAL_BookShop.Instance.EditCTHD(s);
        }
        public void UpdateKho_BLL(Kho s)
        {
            DAL_BookShop.Instance.EditKho(s);
        }
        public void DelHD_BLL(List<int> Ma)
        {
            DAL_BookShop.Instance.DelHD_DAL(Ma);
        }
        public void DelCTHD_BLL(int Ma)
        {
            DAL_BookShop.Instance.DelCTHD_DAL(Ma);
        }
    }
}
