using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookShopManagement.BLL;
using BookShopManagement.DTO;

namespace BookShopManagement.UserControls
{
    public partial class UC_DoanhThu : UserControl
    {
        List<TTSach> l = new List<TTSach>();
        public UC_DoanhThu()
        {
            InitializeComponent();
            l = BLL_BookShop.Instance.GetAllSachBan(dateTimePicker1.Value);
            dataGridView1.DataSource = l;
            text_DoanhThu.Text = l.Sum(x => x.ThanhTien).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            l = BLL_BookShop.Instance.GetAllSachBan(dateTimePicker1.Value);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = l;
            text_DoanhThu.Clear();
            text_DoanhThu.Text = l.Sum(x => x.ThanhTien).ToString();
        }
    }
}
