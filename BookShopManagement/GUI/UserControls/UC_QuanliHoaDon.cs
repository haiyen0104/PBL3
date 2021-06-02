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
using BookShopManagement.BLL;
using BookShopManagement.DTO;

namespace BookShopManagement.UserControls
{
    public partial class UC_QuanliHoaDon : UserControl
    {
        public UC_QuanliHoaDon()
        {
            InitializeComponent();
            show(0,null);
        }
        public void show(int mahd, string nameKH)
        {
            dataGridView1.DataSource = BLL_BookShop.Instance.ListAllHDView_BLL(mahd,nameKH);
            dataGridView1.Columns[0].HeaderText = "Oder ID";
            dataGridView1.Columns[1].HeaderText = "Client Name";
            dataGridView1.Columns[2].HeaderText = "Date";
            dataGridView1.Columns[3].HeaderText = "Total Amount(VNĐ)";
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) MessageBox.Show("Vui lòng chọn đối tượng hoa don ");
            else
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    using (Form_EditHoaDon ae = new Form_EditHoaDon(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["MaHoaDon"].Value)))
                    {
                        ae.ShowDialog();
                    }
                }
                else MessageBox.Show("Vui lòng chọn 1 sinh viên ");
            }
        }

        private void btnDetailHoaDon_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) MessageBox.Show("Vui lòng chọn đối tượng hóa đơn");
            else
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {

                    using (Form_DetailHoaDon ae = new Form_DetailHoaDon(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["MaHoaDon"].Value)))
                    {

                        ae.ShowDialog();
                    }
                }
                else MessageBox.Show("Vui lòng chọn 1 hóa đơn ");
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            List<int> s = new List<int>();
            if (dataGridView1.SelectedRows.Count < 1)
            {
                MessageBox.Show("Choose rows");
            }

            DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;

            foreach (DataGridViewRow i in rows)
            {
                s.Add(Convert.ToInt32(i.Cells["MaHoaDon"].Value));
                foreach(TTSach a in BLL_BookShop.Instance.GetTTSach_ByMaHD(Convert.ToInt32(i.Cells["MaHoaDon"].Value)))
                {
                    Kho k = new Kho()
                    {
                        MaSach = a.MaSach,
                        TongSL = Convert.ToInt32(BLL_BookShop.Instance.GetKho_ByMaSach(a.MaSach).TongSL),
                        SLcon = Convert.ToInt32(BLL_BookShop.Instance.GetKho_ByMaSach(a.MaSach).SLcon + a.SoLuong)
                    };
                    BLL_BookShop.Instance.UpdateKho_BLL(k);
                }
            }
            BLL_BookShop.Instance.DelHD_BLL(s);

            show(0,null);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") MessageBox.Show("Nhập tên khách hàng cần tìm!");
            else show(0,textBox1.Text);
        }
    }
}
