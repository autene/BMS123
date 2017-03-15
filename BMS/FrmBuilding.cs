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
using System.Data.SqlClient;

namespace BMS
{
    public partial class FrmBuilding : RibbonForm
    {
        //Khai báo biến
        Database db = new Database();
        DataRow r;
        DataTable tb;
        int checkAdd = 0;
        public FrmBuilding()
        {
            InitializeComponent();
        }

        //Tạo cột cho table
        void CreateColumns()
        {
            tb = new DataTable();
            tb.Columns.Add("STT");
            tb.Columns.Add("Mã chung cư");
            tb.Columns.Add("Tên chung cư");
            tb.Columns.Add("Địa chỉ");
            dtgridBuilding.DataSource = tb;
        }

        //Set size table
        void SetSizeTable()
        {
            for (int i = 1; i < dtgridBuilding.ColumnCount; i++)
                dtgridBuilding.Columns[i].ReadOnly = true;

            dtgridBuilding.Columns[1].FillWeight = 15;
            dtgridBuilding.Columns[2].FillWeight = 25;
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
            //txtID.Enabled = val;
            txtName.Enabled = val;
            txtAddress.Enabled = val;
            txtSearch.Enabled = !val;
            dtgridBuilding.Enabled = !val;
        }

        //Xóa trắng các textbox
        void SetNull()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
        }

        //Load dữ liệu vào bảng
        void LoadData()
        {
            tb.Rows.Clear();
            string NameProc = "ViewAllBuilding";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                r = tb.NewRow();
                r[0] = i + 1;
                r[1] = dt.Rows[i][0].ToString();
                r[2] = dt.Rows[i][1].ToString();
                r[3] = dt.Rows[i][2].ToString();
                tb.Rows.Add(r);
                dtgridBuilding.DataSource = tb;
            }

        }

        private void FrmBuilding_Load(object sender, EventArgs e)
        {
            CreateColumns();
            SetSizeTable();
            LoadData();
            SetBtn(true);
            SetTxt(false);
            txtID.Enabled = false;
        }

        //Đổ dữ liệu vào các textboxx
        void ShowData(int posTable)
        {
            txtID.Text = dtgridBuilding.Rows[posTable].Cells[2].Value.ToString();
            txtName.Text = dtgridBuilding.Rows[posTable].Cells[3].Value.ToString();
            txtAddress.Text = dtgridBuilding.Rows[posTable].Cells[4].Value.ToString();
        }

        //Xóa thông tin tìm kiếm khi click vào textbox tìm kiếm
        private void txtSearch_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtSearch.Text == "Nhập tên chung cư ....")
                txtSearch.Text = "";
        }

        //Set lại thông tin tìm kiếm khi bỏ chọn
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
                txtSearch.Text = "Nhập tên chung cư ....";
        }

        //Chọn nút thêm
        private void btAdd_Click(object sender, EventArgs e)
        {
            checkAdd = 1;
            SetBtn(false);
            SetTxt(true);
            SetNull();
            SetLabel(false);

            string sql = "Select IDENT_CURRENT('Building')";
            DataTable dt = db.LoadData(sql);
            txtID.Text = (int.Parse(dt.Rows[0][0].ToString()) + 1).ToString();
        }

        //Chọn nút hủy
        private void btCancel_Click(object sender, EventArgs e)
        {
            SetBtn(true);
            SetTxt(false);
            SetNull();
            SetLabel(false);
            checkAdd = 0;
        }

        //Chọn nút sửa
        private void btEdit_Click(object sender, EventArgs e)
        {
            if (txtID.Text.Trim() != "")
            {
                checkAdd = 2;
                SetBtn(false);
                SetTxt(true);
                SetLabel(false);
                txtID.Enabled = false;
            }
            else
                MessageBox.Show("Vui lòng chỉ chọn chung cư để sửa !", "Thông báo");
        }

        //Hàm xóa
        void DeleteBuilding(string ID)
        {
            string sql = "execute DeleteBuilding'" + ID + "'";
            if (db.Execute(sql) == true)
                SetNull();
            else
                MessageBox.Show("Không thể xóa ! Xin vui lòng kiểm tra lại các thông tin liên quan !", "Thông báo");
        }

        //Chọn nút xóa
        private void btDelete_Click(object sender, EventArgs e)
        {
            string t = "";
            int countCheck = 0;
            for (int i = 0; i < dtgridBuilding.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtgridBuilding.Rows[i].Cells[0].Value) == true)
                {
                    t += "- " + dtgridBuilding.Rows[i].Cells[3].Value.ToString() + "\n";
                    countCheck++;
                }
            }
            if (countCheck == 0)
                MessageBox.Show("Vui lòng chọn chung cư cần xóa !", "Xóa");
            else
            {
                string tb = "Bạn đang xóa " + countCheck + " chung cư: \n" + t + "Bạn có chắc xóa không ?";
                DialogResult dr = MessageBox.Show(tb, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    for (int i = 0; i < dtgridBuilding.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dtgridBuilding.Rows[i].Cells[0].Value) == true)
                        {
                            DeleteBuilding(dtgridBuilding.Rows[i].Cells[2].Value.ToString());
                        }
                    }
                }
            }


            LoadData();
        }

        //Hàm kiểm tra tên
        bool CheckName(string Name, string ID, int checkAdd)
        {
            string NameProc = "ViewAllBuilding";
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

        //Chọn nút lưu
        private void btSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng nhập [Tên chung cư] !", "Thông báo");
            else
            if (txtName.Text.Trim().Length > 50)
                MessageBox.Show("[Tên chung cư] không quá 50 ký tự !", "Thông báo");
            else
            if (txtAddress.Text.Trim().Length > 100)
                MessageBox.Show("[Địa chỉ] không quá 100 ký tự !", "Thông báo");
            else
            {
                if (CheckName(txtName.Text.Trim(), txtID.Text.Trim(), checkAdd) == true)
                    MessageBox.Show("Tên trùng lặp !", "Thông báo");
                else
                {
                    if (checkAdd == 1)
                    {
                        string sql = string.Format("execute InsertBuilding N'{0}',N'{1}'", txtName.Text.Trim(), txtAddress.Text.Trim());
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
                        string sql = string.Format("execute UpdateBuilding '{0}',N'{1}',N'{2}'", txtID.Text.Trim(), txtName.Text.Trim(), txtAddress.Text.Trim());
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

        //Tìm kiếm
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SetNull();
            if (txtSearch.Text == "Nhập tên chung cư ...." || txtSearch.Text.Trim() == "")
                LoadData();
            else
            if (txtSearch.Text.Trim() != "")
            {
                tb.Rows.Clear();
                string procName = "ViewAllBuilding";
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
                                r[3] = dt.Rows[i][2].ToString();
                                tb.Rows.Add(r);
                                dtgridBuilding.DataSource = tb;

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

        //Click vào table
        private void dtgridBuilding_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tb.Rows.Count > 0)
            {
                SetLabel(true);
                int posTable = dtgridBuilding.CurrentRow.Index;
                ShowData(posTable);
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    SendKeys.Send("{BACKSPACE}");
        }
    }
}
