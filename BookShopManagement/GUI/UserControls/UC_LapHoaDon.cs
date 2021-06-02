using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookShopManagement.Forms;
using BookShopManagement.DTO;
using BookShopManagement.BLL;
namespace BookShopManagement.UserControls
{
    public partial class UC_LapHoaDon : UserControl
    {
        public UC_LapHoaDon()
        {
            InitializeComponent();
            show();
        }
        List<TTSach> l = new List<TTSach>();
        public void show()
        {
            txtMaHD.Text = (BLL_BookShop.Instance.GetMaHDcuoi()+1).ToString();
            txtTongTien.Text = "0";
            datGdTenSach.DataSource = BLL_BookShop.Instance.GetAllSach_BLL();
            datGdTenSach.Columns["DonGia"].Visible = false;
            datGdTenSach.Columns["TenLoaiSach"].Visible = false;
            datGdTenSach.Columns["TenTacGia"].Visible = false;
            datGdTenSach.Columns["TenLinhVuc"].Visible = false;
            datGdTenSach.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            datGdTenSach.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            datGdTenSach.Columns[0].HeaderText = "Book ID";
            datGdTenSach.Columns[1].HeaderText = "Book title";
        }
        private void btnThemSach_Click(object sender, EventArgs e)
        {
            if (KtraAdd())
            {
                DataGridViewSelectedRowCollection rows = datGdTenSach.SelectedRows;
                TTSach m = new TTSach();
                foreach (DataGridViewRow i in rows)
                {
                    m = new TTSach
                    {
                        MaSach = Convert.ToInt32(i.Cells["MaSach"].Value),
                        TenSach = i.Cells["TenSach"].Value.ToString(),
                        SoLuong = Convert.ToInt32(txtSoLuong.Text),
                        DonGia = Convert.ToInt32(i.Cells["DonGia"].Value),
                        MucGiamGia = BLL_BookShop.Instance.GetMucGiamGia_ByMaSach(Convert.ToInt32(i.Cells["MaSach"].Value)),
                        ThanhTien = Convert.ToDecimal(Convert.ToInt32(txtSoLuong.Text) * float.Parse(i.Cells["DonGia"].Value.ToString()) - float.Parse(i.Cells["DonGia"].Value.ToString()) * BLL_BookShop.Instance.GetMucGiamGia_ByMaSach(Convert.ToInt32(i.Cells["MaSach"].Value)) / 100)
                    };
                }
                int dem = 0;
                foreach (TTSach z in l)
                {
                    if (z.MaSach == m.MaSach)
                    {
                        z.SoLuong += m.SoLuong;
                        z.ThanhTien += m.ThanhTien;
                        dem = 1; break;
                    }
                }
                if (dem == 0) l.Add(m);
                datGdSachMua.DataSource = null;
                datGdSachMua.DataSource = l;
                datGdSachMua.Columns[0].HeaderText = "Book ID";
                datGdSachMua.Columns[1].HeaderText = "Book title";
                datGdSachMua.Columns[2].HeaderText = "Quantity";
                datGdSachMua.Columns[3].HeaderText = "Purchase price(VNĐ)";
                datGdSachMua.Columns[4].HeaderText = "Discount rate(%)";
                datGdSachMua.Columns[5].HeaderText = "The price paid(VNĐ)";
                decimal Tongtien = l.Sum(x => x.ThanhTien);
                txtTongTien.Text = Tongtien.ToString();
                txtSoLuong.Clear();
            }
        }
        private bool KtraSave()
        {
            if (txtTenKH.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Enter Client Name,Please!");
                return false;
            }
            if(txtIDStaff.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Enter ID_Staff,Please!");
                return false;
            }
            if (l != null && l.Count == 0)
            {
                MessageBox.Show("Please choose a book!");
                return false;
            }
            return true;
        }
        private bool KtraAdd()
        {
            
            if (txtSoLuong.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Enter Quatity,Please!");
                txtSoLuong.Focus();
                return false;
            }
            else
            {
                int kq;
                bool formatSL = int.TryParse(txtSoLuong.Text.Trim(), out kq);
                if (!formatSL)
                {
                    MessageBox.Show("Quanity should be interger value!");
                    txtSoLuong.Clear();
                    txtSoLuong.Focus();
                    return false;
                }
            }
            DataGridViewSelectedRowCollection r = datGdTenSach.SelectedRows;
            foreach (DataGridViewRow i in r)
            {
                if (Convert.ToInt32(txtSoLuong.Text) > BLL_BookShop.Instance.GetKho_ByMaSach(Convert.ToInt32(i.Cells["MaSach"].Value)).SLcon)
                {
                    MessageBox.Show("Insufficient quantity in stock, please choose the quantity no more than " + BLL_BookShop.Instance.GetKho_ByMaSach(Convert.ToInt32(i.Cells["MaSach"].Value)).SLcon);
                    return false;
                }
            }
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnNewOder.Enabled = true;
            btnCancel.Enabled = false;
            groupBox1.Enabled = false;
            txtTenKH.Clear();
            txtSoLuong.Clear();
            txtTongTien.Text = "0";
            datGdSachMua.DataSource = null;
            l.Clear();

        }

        private void datGdSachMua_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var a = datGdSachMua.HitTest(e.X, e.Y);
                datGdSachMua.Rows[a.RowIndex].Selected = true;
                contextMenuStrip1.Show(datGdSachMua, e.X, e.Y);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = datGdSachMua.CurrentCell.RowIndex;
            l.RemoveAt(index);
            datGdSachMua.DataSource = null;
            datGdSachMua.DataSource = l;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (KtraSave())
            {
            HoaDon s = new HoaDon
            {
                MaHoaDon = Convert.ToInt32(txtMaHD.Text),
                TenKhachHang = txtTenKH.Text,
                NgayLap = dtNgayNhap.Value,
                TongTien = Convert.ToInt32(txtTongTien.Text),
                ID_Staff = Convert.ToInt32(txtIDStaff.Text),
            };
            BLL_BookShop.Instance.AddHD_BLL(s);
            foreach (TTSach i in l)
            {
                ChiTietHD ct = new ChiTietHD
                {
                    MaHoaDon = s.MaHoaDon,
                    MaSach = i.MaSach,
                    SoLuong = i.SoLuong,
                    MucGiamGia = i.MucGiamGia
                };
                BLL_BookShop.Instance.AddCTHD_BLL(ct);
                Kho k = new Kho()
                {
                    MaSach = i.MaSach,
                    TongSL = Convert.ToInt32(BLL_BookShop.Instance.GetKho_ByMaSach(i.MaSach).TongSL),
                    SLcon = Convert.ToInt32(BLL_BookShop.Instance.GetKho_ByMaSach(i.MaSach).SLcon - i.SoLuong)
                };
                BLL_BookShop.Instance.UpdateKho_BLL(k);
                }
                MessageBox.Show("Susscess!");
            }
        }

        private void btnNewOder_Click(object sender, EventArgs e)
        {
            btnNewOder.Enabled = false;
            btnCancel.Enabled = true;
            groupBox1.Enabled = true;
            txtTenKH.Focus();
            txtSoLuong.Clear();
            txtTongTien.Text = "0";
            datGdSachMua.DataSource = null;
            l.Clear();
        }
    }
}
