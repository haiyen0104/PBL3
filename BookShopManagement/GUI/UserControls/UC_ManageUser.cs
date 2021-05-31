using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopManagement.UserControls
{
    public partial class UC_ManageUser : UserControl
    {
        public UC_ManageUser()
        {
            InitializeComponent();
            txtName.Enabled = false;
            txtEmail.Enabled = false;
            groupBox1.Enabled = false;
            txtPhone.Enabled = false;
            txtUserNm.Enabled = false;
            txtPass.Enabled = false;
            dateTimePicker1.Enabled = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtName.Enabled = true;
            txtEmail.Enabled = true;
            groupBox1.Enabled = true;
            txtPhone.Enabled = true;
            txtUserNm.Enabled = true;
            txtPass.Enabled = true;
            dateTimePicker1.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
