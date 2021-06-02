using BookShopManagement.UserControls;
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
    public partial class Form_Dashboard : Form
    {
        int PanelWidth;
        bool isCollapsed;

        public Form_Dashboard()
        {
            InitializeComponent();
            timerTime.Start();
            PanelWidth = panelLeft.Width;
            isCollapsed = false;
            //UC_ManageUser uch = new UC_ManageUser();
            //AddControlsToPanel(uch);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Form_Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                panelLeft.Width = panelLeft.Width + 10;
                if (panelLeft.Width >= PanelWidth)
                {
                    timer1.Stop();
                    isCollapsed = false;
                    this.Refresh();
                }
            }
            else
            {
                panelLeft.Width = panelLeft.Width - 10;
                if (panelLeft.Width <= 59)
                {
                    timer1.Stop();
                    isCollapsed = true;
                    this.Refresh();
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void moveSidePanel(Control btn)
        {
            panelSide.Top = btn.Top;
            panelSide.Height = btn.Height;
        }

        private void AddControlsToPanel(Control c)
        {
            c.Dock = DockStyle.Fill;
            panelControls.Controls.Clear();
            panelControls.Controls.Add(c);
        }
        private void btnPurchase_Click(object sender, EventArgs e)
        {
            moveSidePanel(btn_LapHoaDon);
            UC_LapHoaDon up = new UC_LapHoaDon();
            AddControlsToPanel(up);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnExpense);
            UC_QuanliHoaDon ea = new UC_QuanliHoaDon();
            AddControlsToPanel(ea);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnUsers);
            //UC_ManageUser um = new UC_ManageUser();
            //AddControlsToPanel(um);
        }

        private void btnViewSales_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnViewSales);
            UC_DoanhThu vs = new UC_DoanhThu();
            AddControlsToPanel(vs);
        }

        private void timerTime_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            labelTime.Text = dt.ToString("HH:MM:ss");
        }
    }
}
