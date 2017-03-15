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
    public partial class FrmItemName : RibbonForm
    {
        //Khai báo biến
        Database db = new Database();
        DataRow r;
        DataTable tb;
        int checkAdd = 0;

        public FrmItemName()
        {
            InitializeComponent();
        }

        //Tạo cột cho table
        void CreateColumns()
        {
            tb = new DataTable();
            tb.Columns.Add("STT");
            tb.Columns.Add("Mã");
            tb.Columns.Add("Tên tài sản");
            dtgridItemName.DataSource = tb;

        }

        //Set size table
        void SetSizeTable()
        {
            for (int i = 1; i < dtgridItemName.ColumnCount; i++)
                dtgridItemName.Columns[i].ReadOnly = true;

            dtgridItemName.Columns[0].Width = 30;
            dtgridItemName.Columns[1].Width = 32;
            dtgridItemName.Columns[2].Width = 32;
        }

        //Cài đặt các lable (*)
        void SetLabel(bool val)
        {
            if (val)
            {
                lbName.Text = "";
            }
            else
            {
                lbName.Text = "(*)";
            }
        }

        //Cài đặt trạng thái các button
        void SetBtn(bool val)
        {
            btAdd.Enabled = val;
            btDelete.Enabled = val;
            btEdit.Enabled = val;
            btSave.Enabled = !val;
            btCancel.Enabled = !val;
        }

        //Cài đặt trạng thái các textbox
        void SetTxt(bool val)
        {
            txtName.Enabled = val;
            txtSearch.Enabled = !val;
        }

        //Xóa trắng các textbox
        void SetNull()
        {
            txtName.Text = "";
        }

        //Load dữ liệu vào bảng
        void LoadData()
        {
            tb.Rows.Clear();
            string NameProc = "ViewItemName";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                r = tb.NewRow();
                r[0] = i + 1;
                r[1] = dt.Rows[i][0].ToString();
                r[2] = dt.Rows[i][1].ToString();
                tb.Rows.Add(r);
                dtgridItemName.DataSource = tb;
            }

        }

        private void FrmItemName_Load(object sender, EventArgs e)
        {
            CreateColumns();
            SetSizeTable();
            LoadData();
            SetBtn(true);
            SetTxt(false);
        }

        //Đổ dữ liệu vào các textboxx
        void ShowData(int posTable)
        {
            txtName.Text = dtgridItemName.Rows[posTable].Cells[3].Value.ToString();
        }

        //Click chọn vào table
        private void dtgridItemName_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tb.Rows.Count > 0)
            {
                SetLabel(true);
                int posTable = dtgridItemName.CurrentRow.Index;
                ShowData(posTable);
            }
        }

        //Nút thêm
        private void btAdd_Click(object sender, EventArgs e)
        {
            checkAdd = 1;
            SetBtn(false);
            SetTxt(true);
            SetNull();
            SetLabel(false);

        }

        //Nút sửa
        private void btEdit_Click(object sender, EventArgs e)
        {
            if(txtName.Text.Trim() == "")
                MessageBox.Show("Vui lòng chọn tài sản để sửa !", "Thông báo");
            else
            {
                checkAdd = 2;
                SetBtn(false);
                SetTxt(true);
                SetLabel(false);
            }
        }

        //Nút hủy
        private void btCancel_Click(object sender, EventArgs e)
        {
            SetBtn(true);
            SetTxt(false);
            SetNull();
            SetLabel(false);
            checkAdd = 0;
        }

        //Hàm xóa
        void DeleteItemName(string ID)
        {
            string sql = "execute DeleteItemName'" + ID + "'";
            if (db.Execute(sql) == true)
                SetNull();
            else
                MessageBox.Show("Không thể xóa ! Xin vui lòng kiểm tra lại các thông tin liên quan !", "Thông báo");
        }

        //Nút xóa
        private void btDelete_Click(object sender, EventArgs e)
        {
            string t = "";
            int countCheck = 0;
            for (int i = 0; i < dtgridItemName.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtgridItemName.Rows[i].Cells[0].Value) == true)
                {
                    t += "- " + dtgridItemName.Rows[i].Cells[3].Value.ToString() + "\n";
                    countCheck++;
                }
            }
            if (countCheck == 0)
                MessageBox.Show("Vui lòng chọn tài sản cần xóa !", "Xóa");
            else
            {
                string tb = "Bạn đang xóa " + countCheck + " tài sản: \n" + t + "Bạn có chắc xóa không ?";
                DialogResult dr = MessageBox.Show(tb, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    for (int i = 0; i < dtgridItemName.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dtgridItemName.Rows[i].Cells[0].Value) == true)
                        {
                            DeleteItemName(dtgridItemName.Rows[i].Cells[2].Value.ToString());
                        }
                    }
                }
            }


            LoadData();

        }

        //Hàm kiểm tra tên
        bool CheckName(string ID, string Name, int checkAdd)
        {
            string NameProc = "ViewItemName";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString().ToLower() == Name.ToLower())
                {
                    if (checkAdd == 2)
                    {
                        if (dt.Rows[i][0].ToString().ToLower() != ID.ToLower())
                            return true;
                    }
                    else
                        return true;
                }
            }
            return false;
        }

        //Nút lưu
        private void btSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng nhập [Tên tài sản] !", "Thông báo");
            else
            if (txtName.Text.Trim().Length > 20)
                MessageBox.Show("[Tên tài sản] không quá 20 ký tự !", "Thông báo");
            else
            {
                if (CheckName(dtgridItemName.Rows[dtgridItemName.CurrentRow.Index].Cells[2].Value.ToString().Trim(), txtName.Text.Trim(), checkAdd) == true)
                    MessageBox.Show("Tên trùng lặp !", "Thông báo");
                else
                {
                    if (checkAdd == 1)
                    {
                        string sql = string.Format("execute InsertItemName N'{0}'", txtName.Text.Trim());
                        db.Execute(sql);
                        LoadData();
                        SetBtn(true);
                        SetTxt(false);
                        SetNull();
                        SetLabel(false);
                        checkAdd = 0;
                    }
                    else
                        if (checkAdd == 2)
                    {
                        string sql = string.Format("execute UpdateItemName '{0}',N'{1}'", dtgridItemName.Rows[dtgridItemName.CurrentRow.Index].Cells[2].Value.ToString().Trim(), txtName.Text.Trim());
                        db.Execute(sql);
                        LoadData();
                        SetBtn(true);
                        SetTxt(false);
                        SetNull();
                        SetLabel(false);
                        checkAdd = 0;
                    }


                }
            }
        }

        //Xóa thông tin tìm kiếm khi click vào textbox tìm kiếm
        private void txtSearch_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtSearch.Text == "Nhập tên tài sản ....")
                txtSearch.Text = "";
        }

        //Set lại thông tin tìm kiếm khi bỏ chọn
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
                txtSearch.Text = "Nhập tên tài sản ....";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SetNull();
            if (txtSearch.Text == "Nhập tên tài sản ...." || txtSearch.Text.Trim() == "")
                LoadData();
            else
            if (txtSearch.Text != "")
            {
                tb.Rows.Clear();
                string procName = "ViewItemName";
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
                                tb.Rows.Add(r);
                                dtgridItemName.DataSource = tb;

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
