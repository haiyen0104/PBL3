using BookShopManagement.BLL;
using BookShopManagement.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopManagement.Forms
{
    public partial class Form_DetailHoaDon : Form
    {
        //public int MaHD { get; set; }
        public Form_DetailHoaDon(int MaHD)
        {
            InitializeComponent();
            //this.MaHD = MaHD;
            SetGUI(MaHD);
        }
        public void SetGUI(int maHD)
        {
            HoaDon s = new HoaDon();
            s = BLL_BookShop.Instance.GetHoaDon_ByMaHD(maHD);
            txtMaHD.Text = (s.MaHoaDon).ToString();
            txtTenKH.Text = (s.TenKhachHang).ToString();
            txtNgayLap.Text = (s.NgayLap).ToString();
            txtTongTien.Text = (s.TongTien).ToString();
            txtStaff.Text = (s.ID_Staff).ToString();
            dataGridView1.DataSource = BLL_BookShop.Instance.GetTTSach_ByMaHD(maHD);
            //dataGridView1.Columns["MaHD"].Visible = false;
            dataGridView1.Columns["ThanhTien"].Visible = false;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
