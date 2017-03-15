using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace BMS
{
    public partial class FrmMain : RibbonForm
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void ribbonBar2_ItemClick(object sender, EventArgs e)
        {

        }

        static int KiemTraTonTai(System.Windows.Forms.TabControl TabControlName, string TabName)
        {
            int temp = -1;
            for (int i = 0; i < TabControlName.TabPages.Count; i++)
            {
                if (TabControlName.TabPages[i].Text == TabName)
                {
                    temp = i;
                    break;
                }
            }
            return temp;
        }
        //Tạo tabpage
        public void TabCreating(System.Windows.Forms.TabControl tabControl, string Text, Form Form)
        {
            int Index = KiemTraTonTai(tabControl, Text);
            if (Index >= 0)
            {
                tabControl.SelectedTab = tabControl.TabPages[Index];
                tabControl.SelectedTab.Text = Text;

            }
            else
            {
                TabPage TabPage = new TabPage { Text = Text };
                tabControl.TabPages.Add(TabPage);
                tabControl.SelectedTab = TabPage;

                Form.TopLevel = false;

                Form.Parent = TabPage;
                Form.Show();
                Form.Dock = DockStyle.Fill;

            }
        }

        //Hàm vẽ dấu X
        private void tabcontrol_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font closefont = new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold);
            Font titlefont = new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Italic);
            if (e.Index > 0)
            {
                e.Graphics.DrawString("X", closefont, Brushes.Blue, e.Bounds.Right - 15, e.Bounds.Top + 5);
            }
            e.Graphics.DrawString(this.tabcontrol.TabPages[e.Index].Text, titlefont, Brushes.Black, e.Bounds.Left, e.Bounds.Top + 5);
        }

        //Xử lý khi click vào dấu X
        private void tabcontrol_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.tabcontrol.TabPages.Count; i++)
            {
                Rectangle rPage = tabcontrol.GetTabRect(i);
                Rectangle closeButton = new Rectangle(rPage.Right - 15, rPage.Top + 5, 10, 10);
                if (closeButton.Contains(e.Location))
                {

                    if (MessageBox.Show("Bạn Có Muốn Tắt Tab Này?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.tabcontrol.TabPages.RemoveAt(i);
                        break;
                    }

                }
            }
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            FrmSection f = new FrmSection();
            TabCreating(tabcontrol, "Phòng Ban  [X]", f);
        }

        private void ribbonBar7_ItemClick(object sender, EventArgs e)
        {

        }

        private void buttonItem3_Click_1(object sender, EventArgs e)
        {
            FrmBuilding f = new FrmBuilding();
            TabCreating(tabcontrol, "Chung Cư  [X]", f);
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            FrmBlock f = new FrmBlock();
            TabCreating(tabcontrol, "Khu  [X]", f);
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            FrmFloor f = new FrmFloor();
            TabCreating(tabcontrol, "Tầng  [X]", f);
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            FrmApartment f = new FrmApartment();
            TabCreating(tabcontrol, "Căn hộ  [X]", f);
        }

        private void buttonItem9_Click(object sender, EventArgs e)
        {
            FrmEvent f = new FrmEvent();
            TabCreating(tabcontrol, "Sự kiện  [X]", f);
        }

        private void buttonItem10_Click(object sender, EventArgs e)
        {
            FrmDetailLiving f = new FrmDetailLiving();
            TabCreating(tabcontrol, "Living  [X]", f);
        }

        private void buttonItem11_Click(object sender, EventArgs e)
        {
            FrmCustomer f = new FrmCustomer();
            TabCreating(tabcontrol, "Nhân khẩu  [X]", f);
        }

        private void buttonItem12_Click(object sender, EventArgs e)
        {
            FrmElevator f = new FrmElevator();
            TabCreating(tabcontrol, "Thẻ thang máy  [X]", f);
        }

        private void buttonItem13_Click(object sender, EventArgs e)
        {
            FrmTransport f = new FrmTransport();
            TabCreating(tabcontrol, "Thẻ xe tháng  [X]", f);
        }

        private void buttonItem14_Click(object sender, EventArgs e)
        {
            FrmItem f = new FrmItem();
            TabCreating(tabcontrol, "Tài sản  [X]", f);
        }
    }
}
