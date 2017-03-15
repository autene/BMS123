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
    public partial class FrmBlock : RibbonForm
    {
        //Khai báo biến
        Database db = new Database();
        DataRow r;
        DataTable tb;
        int checkAdd = 0;
        public FrmBlock()
        {
            InitializeComponent();
        }

        //Tạo cột cho table
        void CreateColumns()
        {
            tb = new DataTable();
            tb.Columns.Add("STT");
            tb.Columns.Add("Mã khu");
            tb.Columns.Add("Tên khu");
            tb.Columns.Add("Chung cư");
            dtgridBlock.DataSource = tb;
            
        }

        //Set size table
        void SetSizeTable()
        {
            for (int i = 1; i < dtgridBlock.ColumnCount; i++)
                dtgridBlock.Columns[i].ReadOnly = true;

            dtgridBlock.Columns[1].FillWeight = 15;
            dtgridBlock.Columns[2].FillWeight = 25;
        }

        //Cài đặt các lable (*)
        void SetLabel(bool val)
        {
            if (val)
            {
                lbName.Text = "";
                lbBuilding.Text = "";
            }
            else
            {
                lbName.Text = "(*)";
                lbBuilding.Text = "(*)";
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
            cbBuilding.Enabled = val;
            txtSearch.Enabled = !val;
            dtgridBlock.Enabled = !val;
        }

        //Xóa trắng các textbox
        void SetNull()
        {
            txtID.Text = "";
            txtName.Text = "";
            cbBuilding.Text = null;
        }

        //Load dữ liệu vào combobox
        void LoadCombobox()
        {
            cbBuilding.Items.Clear();
            string NameProc = "ViewAllBuilding";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbBuilding.Items.Add(dt.Rows[i][1].ToString());
            }
        }

        //Load dữ liệu vào bảng
        void LoadData()
        {
            LoadCombobox();
            tb.Rows.Clear();
            string NameProc = "ViewAllBlock";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                r = tb.NewRow();
                r[0] = i + 1;
                r[1] = dt.Rows[i][0].ToString();
                r[2] = dt.Rows[i][1].ToString();
                r[3] = dt.Rows[i][2].ToString();
                tb.Rows.Add(r);
                dtgridBlock.DataSource = tb;
            }

        }

        private void FrmBlock_Load(object sender, EventArgs e)
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
            txtID.Text = dtgridBlock.Rows[posTable].Cells[2].Value.ToString();
            txtName.Text = dtgridBlock.Rows[posTable].Cells[3].Value.ToString();
            cbBuilding.Text = dtgridBlock.Rows[posTable].Cells[4].Value.ToString();
        }

        //Click chọn vào table
        private void dtgridBlock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tb.Rows.Count > 0)
            {
                SetLabel(true);
                int posTable = dtgridBlock.CurrentRow.Index;
                ShowData(posTable);
            }
        }

        //Xóa thông tin tìm kiếm khi click vào textbox tìm kiếm
        private void txtSearch_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtSearch.Text == "Nhập tên khu ....")
                txtSearch.Text = "";
        }

        //Set lại thông tin tìm kiếm khi bỏ chọn
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
                txtSearch.Text = "Nhập tên khu ....";
        }

        //Chọn nút thêm
        private void btAdd_Click(object sender, EventArgs e)
        {
            checkAdd = 1;
            SetBtn(false);
            SetTxt(true);
            SetNull();
            SetLabel(false);

            string sql = "Select IDENT_CURRENT('Block')";
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
            if (txtID.Text != "")
            {
                checkAdd = 2;
                SetBtn(false);
                SetTxt(true);
                SetLabel(false);
                txtID.Enabled = false;
            }
            else
                MessageBox.Show("Vui lòng chọn khu để sửa !", "Thông báo");
        }

        //Hàm xóa
        void DeleteBlock(string ID)
        {
            string sql = "execute DeleteBlock'" + ID + "'";
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
            for (int i = 0; i < dtgridBlock.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtgridBlock.Rows[i].Cells[0].Value) == true)
                {
                    t += "- " + dtgridBlock.Rows[i].Cells[3].Value.ToString() +" - " + dtgridBlock.Rows[i].Cells[4].Value.ToString() + "\n";
                    countCheck++;
                }
            }
            if (countCheck == 0)
                MessageBox.Show("Vui lòng chọn khu cần xóa !", "Xóa");
            else
            {
                string tb = "Bạn đang xóa " + countCheck + " khu: \n" + t + "Bạn có chắc xóa không ?";
                DialogResult dr = MessageBox.Show(tb, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    for (int i = 0; i < dtgridBlock.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dtgridBlock.Rows[i].Cells[0].Value) == true)
                        {
                                DeleteBlock(dtgridBlock.Rows[i].Cells[2].Value.ToString());
                        }
                    }
                }
            }


            LoadData();
        }

        //Chuyển tên thành mã
        int Change(string BuildingID)
        {
            string NameProc = "ViewAllBuilding";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString() == cbBuilding.Text)
                    return int.Parse(dt.Rows[i][0].ToString());
            }
            return -1;
        }

        //Hàm kiểm tra tên
        bool CheckName(string Name, string ID, string BuildingName, int checkAdd)
        {
            string NameProc = "ViewAllBlock";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString().ToLower() == Name.ToLower() && dt.Rows[i][2].ToString().ToLower() == BuildingName.ToLower())
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
                MessageBox.Show("Vui lòng nhập [Tên khu] !", "Thông báo");
            else
            if (cbBuilding.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng chọn [Chung cư] !", "Thông báo");
            else
            if (txtName.Text.Trim().Length > 30)
                MessageBox.Show("[Tên khu] không quá 30 ký tự !", "Thông báo");
            else
            {
                if (CheckName(txtName.Text.Trim(), txtID.Text.Trim(), cbBuilding.Text.Trim(), checkAdd) == true)
                    MessageBox.Show("Tên trùng lặp !", "Thông báo");
                else
                {
                    if (checkAdd == 1)
                    {
                        string sql = string.Format("execute InsertBlock N'{0}','{1}'", txtName.Text.Trim(), Change(cbBuilding.Text.Trim()));
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
                        string sql = string.Format("execute UpdateBlock '{0}',N'{1}','{2}'", txtID.Text.Trim(), txtName.Text.Trim(), Change(cbBuilding.Text.Trim()));
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
            if (txtSearch.Text == "Nhập tên khu ...." || txtSearch.Text.Trim() == "")
                LoadData();
            else
            if (txtSearch.Text != "")
            {
                tb.Rows.Clear();
                string procName = "ViewAllBlock";
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
                                dtgridBlock.DataSource = tb;

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

        //Xử lý nút Enter trong search
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    SendKeys.Send("{BACKSPACE}");
        }
    }
}
