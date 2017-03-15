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
    public partial class FrmCustomer : RibbonForm
    {
        //Khai báo biến
        Database db = new Database();
        DataRow r;
        DataTable tb;
        public FrmCustomer()
        {
            InitializeComponent();
        }

        //Tạo cột cho table
        void CreateColumns()
        {
            tb = new DataTable();
            tb.Columns.Add("STT");
            tb.Columns.Add("Mã nhân khẩu");
            tb.Columns.Add("Tên nhân khẩu");
            tb.Columns.Add("Ngày sinh");
            tb.Columns.Add("Giới tính");
            tb.Columns.Add("Quên quán");
            tb.Columns.Add("Số điện thoại");
            tb.Columns.Add("Email");
            tb.Columns.Add("CMND");
            tb.Columns.Add("Ngày tạo");
            dtgridCus.DataSource = tb;

        }

        //Set size table
        void SetSizeTable()
        {
            for (int i = 1; i < dtgridCus.ColumnCount; i++)
                dtgridCus.Columns[i].ReadOnly = true;

            dtgridCus.Columns[1].Width = 30;
            dtgridCus.Columns[2].Width = 150;
            dtgridCus.Columns[3].Width = 150;
            dtgridCus.Columns[8].Width = 200;
        }

        //Load dữ liệu vào bảng
        void LoadData()
        {
            tb.Rows.Clear();
            string NameProc = "ViewCustomer";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                r = tb.NewRow();
                r[0] = i + 1;
                r[1] = dt.Rows[i][0].ToString();
                r[2] = dt.Rows[i][1].ToString();
                if (dt.Rows[i][2].ToString() != "")
                {
                    if (DateTime.Compare(DateTime.Parse(dt.Rows[i][2].ToString()), DateTime.Parse("1/1/1900")) == 0)
                        r[3] = "";
                    else
                        r[3] = DateTime.Parse(dt.Rows[i][2].ToString()).ToShortDateString();
                }
                else
                    r[3] = "";
                if (dt.Rows[i][3].ToString() == "True")
                    r[4] = "Nam";
                else
                    r[4] = "Nữ";
                r[5] = dt.Rows[i][4].ToString();
                r[6] = dt.Rows[i][5].ToString();
                r[7] = dt.Rows[i][6].ToString();
                r[8] = dt.Rows[i][7].ToString();
                r[9] = DateTime.Parse(dt.Rows[i][8].ToString()).ToShortDateString();
                tb.Rows.Add(r);
                dtgridCus.DataSource = tb;
            }

        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            CreateColumns();
            SetSizeTable();
            LoadData();
        }

        //Hàm xóa
        void DeleteCustomer(string ID)
        {
            string sql = "execute DeleteCustomer'" + ID + "'";
            if (db.Execute(sql) == false)
                MessageBox.Show("Không thể xóa! Xin vui lòng kiểm tra lại các thông tin liên quan !", "Thông báo");
        }
        private void btDelete_Click(object sender, EventArgs e)
        {
            string t = "";
            int countCheck = 0;
            for (int i = 0; i < dtgridCus.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtgridCus.Rows[i].Cells[0].Value) == true)
                {
                    t += "- " + dtgridCus.Rows[i].Cells[3].Value.ToString() + "\n";
                    countCheck++;
                }
            }
            if (countCheck == 0)
                MessageBox.Show("Vui lòng chọn nhân khẩu cần xóa !", "Xóa");
            else
            {
                string tb = "Bạn đang xóa " + countCheck + " nhân khẩu: \n" + t + "Bạn có chắc xóa không ?";
                DialogResult dr = MessageBox.Show(tb, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    for (int i = 0; i < dtgridCus.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dtgridCus.Rows[i].Cells[0].Value) == true)
                        {
                            DeleteCustomer(dtgridCus.Rows[i].Cells[2].Value.ToString());
                        }
                    }
                }
            }


            LoadData();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            FrmAddCustomer f = new FrmAddCustomer();
            f.MesFunc = 4;
            f.ShowDialog();
            LoadData();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (dtgridCus.CurrentRow.Index < 0)
                MessageBox.Show("Vui lòng chọn 1 nhân khẩu cần sửa !", "Thông báo");
            else
            {
                FrmAddCustomer f = new FrmAddCustomer();
                f.MesID = dtgridCus.Rows[dtgridCus.CurrentRow.Index].Cells[2].Value.ToString();
                f.MesFunc = 5;
                f.Width=430;
                f.FormBorderStyle = FormBorderStyle.FixedSingle;
                f.ShowDialog();
                LoadData();
            }
        }

        private void txtSearch_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtSearch.Text == "Nhập tên nhân khẩu ....")
                txtSearch.Text = "";
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
                txtSearch.Text = "Nhập tên nhân khẩu ....";
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    SendKeys.Send("{BACKSPACE}");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Nhập tên nhân khẩu ...." || txtSearch.Text.Trim() == "")
                LoadData();
            else
            if (txtSearch.Text.Trim() != "")
            {
                tb.Rows.Clear();
                string procName = "ViewCustomer";
                DataTable dt = db.LoadProcedure(procName);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int check = 0;
                    for (int a = 0; a < dt.Rows[i][1].ToString().Length; a++)
                    {
                        string getName = "";
                        for (int b = a; b < dt.Rows[i][1].ToString().Length; b++)
                        {
                            char getChar = dt.Rows[i][1].ToString()[b];
                            getName = getName + getChar;
                            getName = getName.ToLower();
                            string getSearch = txtSearch.Text.ToLower();
                            if (getName.Trim() == getSearch.Trim())
                            {
                                r = tb.NewRow();
                                r[0] = i + 1;
                                r[1] = dt.Rows[i][0].ToString();
                                r[2] = dt.Rows[i][1].ToString();
                                if (dt.Rows[i][2].ToString() != "")
                                {
                                    if (DateTime.Compare(DateTime.Parse(dt.Rows[i][2].ToString()), DateTime.Parse("1/1/1900")) == 0)
                                        r[3] = "";
                                    else
                                        r[3] = DateTime.Parse(dt.Rows[i][2].ToString()).ToShortDateString();
                                }
                                else
                                    r[3] = "";
                                if (dt.Rows[i][3].ToString() == "True")
                                    r[4] = "Nam";
                                else
                                    r[4] = "Nữ";
                                r[5] = dt.Rows[i][4].ToString();
                                r[6] = dt.Rows[i][5].ToString();
                                r[7] = dt.Rows[i][6].ToString();
                                r[8] = dt.Rows[i][7].ToString();
                                r[9] = DateTime.Parse(dt.Rows[i][8].ToString()).ToShortDateString();
                                tb.Rows.Add(r);
                                dtgridCus.DataSource = tb;

                                check = 1;
                                break;
                            }
                        }
                        if (check == 1)
                            break;
                    }
                }
            }
        }


    }
}
