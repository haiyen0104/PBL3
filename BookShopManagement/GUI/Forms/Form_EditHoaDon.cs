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
    public partial class Form_EditHoaDon : Form
    {
        public int MaHD { get; set; }
        public Form_EditHoaDon(int mahd)
        {
            InitializeComponent();
            MaHD = mahd;
            SetGUI(mahd);
            SetList();
        }
        List<TTSach> l = new List<TTSach>();
        public void SetGUI(int maHD)
        {
            HoaDon s = new HoaDon();
            s = BLL_BookShop.Instance.GetHoaDon_ByMaHD(maHD);
            txtMaHD.Text = (s.MaHoaDon).ToString();
            txtTenKH.Text = (s.TenKhachHang).ToString();
            dateTimePicker1.Value = (s.NgayLap);
            txtTongTien.Text = (s.TongTien).ToString();
            txt_IDStaff.Text = s.ID_Staff.ToString();
            dataGridView_Sachmua.DataSource = BLL_BookShop.Instance.GetTTSach_ByMaHD(maHD);
            dataGridView_Sachmua.Columns[0].HeaderText = "Book ID";
            dataGridView_Sachmua.Columns[1].HeaderText = "Book title";
            dataGridView_Sachmua.Columns[2].HeaderText = "Quantity";
            dataGridView_Sachmua.Columns[3].HeaderText = "Purchase price(VNĐ)";
            dataGridView_Sachmua.Columns[4].HeaderText = "Discount rate(%)";
            dataGridView_Sachmua.Columns[5].HeaderText = "The price paid(VNĐ)";
            dataGridView_Sachmua.Columns["ThanhTien"].Visible = false;
            dataGridView_dsSach.DataSource = BLL_BookShop.Instance.GetAllSach_BLL();
            dataGridView_dsSach.Columns["DonGia"].Visible = false;
            dataGridView_dsSach.Columns["TenLoaiSach"].Visible = false;
            dataGridView_dsSach.Columns["TenTacGia"].Visible = false;
            dataGridView_dsSach.Columns["TenLinhVuc"].Visible = false;
            dataGridView_dsSach.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView_dsSach.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_dsSach.Columns[0].HeaderText = "Book ID";
            dataGridView_dsSach.Columns[1].HeaderText = "Book title";
        }
        private void SetList()
        {
            for (int i = 0; i < dataGridView_Sachmua.Rows.Count; ++i)
            {
                TTSach s = new TTSach();
                s.MaSach = Convert.ToInt32(dataGridView_Sachmua.Rows[i].Cells["MaSach"].Value);
                s.TenSach = dataGridView_Sachmua.Rows[i].Cells["TenSach"].Value.ToString();
                s.DonGia = Convert.ToInt32(dataGridView_Sachmua.Rows[i].Cells["DonGia"].Value);
                s.SoLuong = Convert.ToInt32(dataGridView_Sachmua.Rows[i].Cells["SoLuong"].Value);
                s.MucGiamGia = float.Parse(dataGridView_Sachmua.Rows[i].Cells["MucGiamGia"].Value.ToString());
                s.ThanhTien = Convert.ToDecimal(dataGridView_Sachmua.Rows[i].Cells["ThanhTien"].Value);
                l.Add(s);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BLL_BookShop.Instance.DelCTHD_BLL(MaHD);
            foreach (TTSach i in l)
            {
                ChiTietHD ct = new ChiTietHD
                {
                    MaHoaDon = MaHD,
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
            
            HoaDon s = new HoaDon
            {
                MaHoaDon = Convert.ToInt32(txtMaHD.Text),
                TenKhachHang = txtTenKH.Text,
                NgayLap = dateTimePicker1.Value,
                TongTien = Convert.ToDecimal(txtTongTien.Text),
                ID_Staff = Convert.ToInt32(txt_IDStaff.Text)
            };
            BLL_BookShop.Instance.UpdateHD_BLL(s);
            MessageBox.Show("Susscess!");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (KtraGt())
            {
                DataGridViewSelectedRowCollection rows = dataGridView_dsSach.SelectedRows;
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
                dataGridView_Sachmua.DataSource = null;
                dataGridView_Sachmua.DataSource = l;
                decimal Tongtien = l.Sum(x => x.ThanhTien);
                txtTongTien.Text = Tongtien.ToString();
                txtSoLuong.Clear();
            }
        }
        private bool KtraGt()
        {
            if (txtTenKH.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập Client Name!");
                return false;
            }
            if (txtSoLuong.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập Quantity!");
                txtSoLuong.Focus();
                return false;
            }
            else
            {
                int kq;
                bool formatSL = int.TryParse(txtSoLuong.Text.Trim(), out kq);
                if (!formatSL)
                {
                    MessageBox.Show("Quantity không đúng định dạng!");
                    txtSoLuong.Clear();
                    txtSoLuong.Focus();
                    return false;
                }
            }

            return true;
        }

        private void dataGridView_Sachmua_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var a = dataGridView_Sachmua.HitTest(e.X, e.Y);
                dataGridView_Sachmua.Rows[a.RowIndex].Selected = true;
                contextMenuStrip1.Show(dataGridView_Sachmua, e.X, e.Y);
            }
        }
        private void deleteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            int index = dataGridView_Sachmua.CurrentCell.RowIndex;
            l.RemoveAt(index);
            dataGridView_Sachmua.DataSource = null;
            dataGridView_Sachmua.DataSource = l;
        }
    }
}
