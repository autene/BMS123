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
    public partial class FrmSection : RibbonForm
    {
        //Khai báo biến
        Database db = new Database();
        DataRow r;
        DataTable tb;
        int checkAdd = 0;
        public FrmSection()
        {
            InitializeComponent();
        }

        //Tạo cột cho table
        void CreateColumns()
        {
            tb = new DataTable();
            tb.Columns.Add("STT");
            tb.Columns.Add("Mã phòng ban");
            tb.Columns.Add("Tên phòng ban");
            tb.Columns.Add("Số điện thoại");
            tb.Columns.Add("Mô tả");
            dtgridSection.DataSource = tb;
        }

        //Set size table
        void SetSizeTable()
        {
            for(int i=1;i<dtgridSection.ColumnCount;i++)
                dtgridSection.Columns[i].ReadOnly = true;

            dtgridSection.Columns[1].FillWeight = 15;
            dtgridSection.Columns[2].FillWeight = 50;
        }

        //Cài đặt các lable (*)
        void SetLabel(bool val)
        {
            if(val)
            {
                lbID.Text = "";
                lbName.Text = "";
            }
            else
            {
                lbID.Text = "(*)";
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
            txtID.Enabled = val;
            txtName.Enabled = val;
            txtPhone.Enabled = val;
            txtDes.Enabled = val;
            txtSearch.Enabled = !val;
            dtgridSection.Enabled = !val;
        }

        //Xóa trắng các textbox
        void SetNull()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            txtDes.Text = "";
        }

        //Load dữ liệu vào bảng
        void LoadData()
        {
            tb.Rows.Clear();
            string NameProc = "ViewAllSection";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                r = tb.NewRow();
                r[0] = i + 1;
                r[1] = dt.Rows[i][0].ToString();
                r[2] = dt.Rows[i][1].ToString();
                r[3] = dt.Rows[i][2].ToString();
                r[4] = dt.Rows[i][3].ToString();
                tb.Rows.Add(r);
                dtgridSection.DataSource = tb;
            }
   
        }

        private void FrmSection_Load(object sender, EventArgs e)
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
            txtID.Text = dtgridSection.Rows[posTable].Cells[2].Value.ToString();
            txtName.Text = dtgridSection.Rows[posTable].Cells[3].Value.ToString();
            txtPhone.Text = dtgridSection.Rows[posTable].Cells[4].Value.ToString();
            txtDes.Text = dtgridSection.Rows[posTable].Cells[5].Value.ToString();
        }

        //Xóa thông tin tìm kiếm khi click vào textbox tìm kiếm
        private void textBoxX4_MouseClick(object sender, MouseEventArgs e)
        {
            if(txtSearch.Text == "Nhập tên phòng ban ....")
                txtSearch.Text = "";
        }

        //Set lại thông tin tìm kiếm khi bỏ chọn
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
                txtSearch.Text = "Nhập tên phòng ban ....";
        }

        //Chọn nút thêm
        private void btAdd_Click(object sender, EventArgs e)
        {
            checkAdd = 1;
            SetBtn(false);
            SetTxt(true);
            SetNull();
            SetLabel(false);
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
                lbID.Text = "";
                txtID.Enabled = false;
            }
            else
                MessageBox.Show("Vui lòng chọn phòng ban để sửa !", "Thông báo");
        }

        //Hàm xóa phòng ban
        void DeleteSection(string ID)
        {
            string sql = "execute DeleteSection'" + ID + "'";
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
            for (int i = 0; i < dtgridSection.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtgridSection.Rows[i].Cells[0].Value) == true)
                {
                    t += "- " + dtgridSection.Rows[i].Cells[3].Value.ToString() + "\n";
                    countCheck++;
                }
            }
            if (countCheck == 0)
                MessageBox.Show("Vui lòng chọn phòng ban cần xóa !", "Xóa");
            else
            {
                string tb = "Bạn đang xóa " + countCheck + " phòng ban: \n" + t + "Bạn có chắc xóa không ?";
                DialogResult dr = MessageBox.Show(tb, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    for (int i = 0; i < dtgridSection.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dtgridSection.Rows[i].Cells[0].Value) == true)
                        {
                            DeleteSection(dtgridSection.Rows[i].Cells[2].Value.ToString());
                        }
                    }
                }
            }


            LoadData();
        }

        //Hàm kiểm tra mã
        bool CheckID(string ID)
        {
            string NameProc = "ViewAllSection";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == ID)
                    return true;
            }
            return false;
        }

        //Hàm kiểm tra tên
        bool CheckName(string Name, string ID, int checkAdd)
        {
            string NameProc = "ViewAllSection";
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

        //Hàm kiểm tra sđt
        bool CheckPhone(string Phone, string ID, int checkAdd)
        {
            string NameProc = "ViewAllSection";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][2].ToString() == Phone)
                {
                    if (checkAdd == 2)
                    {
                        if (dt.Rows[i][0].ToString() != ID)
                            return true;
                    }
                    else
                        return true;
                }
            }
            return false;
        }

        //Kiểm tra nhập số trong sđt
        bool CheckPhoneNumber(string Phone)
        {
            try
            {
                long.Parse(Phone);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Chọn nút lưu
        private void btSave_Click(object sender, EventArgs e)
        {
            if (txtID.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng nhập [Mã phòng ban] !", "Thông báo");
            else
            if (txtName.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng nhập [Tên phòng ban] !", "Thông báo");
            else
            if(txtID.Text.Trim().Length > 10)
                MessageBox.Show("[Mã phòng ban] không quá 10 ký tự !", "Thông báo");
            else
            if (txtName.Text.Trim().Length > 50)
                MessageBox.Show("[Tên phòng ban] không quá 50 ký tự !", "Thông báo");
            else
            if (txtPhone.Text.Trim().Length > 15)
                MessageBox.Show("[Số điện thoại] không quá 15 số !", "Thông báo");
            else
            if (txtDes.Text.Trim().Length > 1000)
                MessageBox.Show("[Mô tả] không quá 1000 ký tự !", "Thông báo");
            else
            {
                if (CheckName(txtName.Text.Trim(), txtID.Text.Trim(), checkAdd) == true)
                    MessageBox.Show("Tên trùng lặp !", "Thông báo");
                else
                    if (CheckPhone(txtPhone.Text.Trim(), txtID.Text.Trim(), checkAdd) == true)
                    MessageBox.Show("Số điện thoại trùng lặp !", "Thông báo");
                else
                {
                    if (checkAdd == 1)
                    {
                        if (CheckID(txtID.Text.Trim()) == true)
                            MessageBox.Show("Mã trùng lặp !", "Thông báo");
                        else
                        if (CheckPhoneNumber(txtPhone.Text.Trim()) == false && txtPhone.Text.Trim() != "")
                            MessageBox.Show("Số điện thoại sai !", "Thông báo");
                        else

                        {

                            string sql = string.Format("execute InsertSection '{0}',N'{1}','{2}',N'{3}'", txtID.Text.Trim(), txtName.Text.Trim(), txtPhone.Text.Trim(), txtDes.Text.Trim());
                            db.Execute(sql);
                            LoadData();
                            SetBtn(true);
                            SetTxt(false);
                            SetNull();
                            SetLabel(false);
                            checkAdd = 0;
                        }

                    }
                    else
                        if (checkAdd == 2)
                    {
                        string sql = string.Format("execute UpdateSection '{0}',N'{1}','{2}',N'{3}'", txtID.Text.Trim(), txtName.Text.Trim(), txtPhone.Text.Trim(), txtDes.Text.Trim());
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
            if (txtSearch.Text == "Nhập tên phòng ban ...." || txtSearch.Text.Trim() == "")
                LoadData();
            else
            if(txtSearch.Text.Trim() != "")
            {
                tb.Rows.Clear();
                string procName = "ViewAllSection";
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
                                r[4] = dt.Rows[i][3].ToString();
                                tb.Rows.Add(r);
                                dtgridSection.DataSource = tb;

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

        //Ràng buộc nhập chữ trong Phone
        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Click chọn vào table
        private void dtgridSection_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tb.Rows.Count > 0)
            {
                SetLabel(true);
                int posTable = dtgridSection.CurrentRow.Index;
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
