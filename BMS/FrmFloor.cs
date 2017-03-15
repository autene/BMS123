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
    public partial class FrmFloor : RibbonForm
    {
        //Khai báo biến
        Database db = new Database();
        DataRow r;
        DataTable tb;
        int checkAdd = 0;
        public FrmFloor()
        {
            InitializeComponent();
        }

        //Tạo cột cho table
        void CreateColumns()
        {
            tb = new DataTable();
            tb.Columns.Add("STT");
            tb.Columns.Add("Mã tầng");
            tb.Columns.Add("Tên tầng");
            tb.Columns.Add("Khu");
            tb.Columns.Add("Chung cư");
            dtgridFloor.DataSource = tb;
        }

        //Set size table
        void SetSizeTable()
        {
            for (int i = 1; i < dtgridFloor.ColumnCount; i++)
                dtgridFloor.Columns[i].ReadOnly = true;

            dtgridFloor.Columns[1].FillWeight = 15;
            dtgridFloor.Columns[2].FillWeight = 25;
        }

        //Cài đặt các lable (*)
        void SetLabel(bool val)
        {
            if (val)
            {
                lbName.Text = "";
                lbBuilding.Text = "";
                lbBlock.Text = "";
            }
            else
            {
                lbName.Text = "(*)";
                lbBuilding.Text = "(*)";
                lbBlock.Text = "(*)";
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
            cbBlock.Enabled = val;
            txtSearch.Enabled = !val;
            dtgridFloor.Enabled = !val;
        }

        //Xóa trắng các textbox
        void SetNull()
        {
            txtID.Text = "";
            txtName.Text = "";
            cbBuilding.Text = "";
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

        //Lấy tên chung cư
        string GetNameBuilding(int FloorID)
        {
            int BlockID = -1;
            string NameProc = "ViewFloor";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (FloorID == int.Parse(dt.Rows[i][0].ToString()))
                {
                    BlockID = int.Parse(dt.Rows[i][2].ToString());
                    break;
                }
            }
            NameProc = "ViewAllBlock";
            System.Data.DataTable dt2 = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                if (BlockID == int.Parse(dt2.Rows[i][0].ToString()))
                    return dt2.Rows[i][2].ToString();
            }
            return null;
        }

        //Load khu
        void LoadBlock (string BuildingName)
        {
            cbBlock.Items.Clear();
            string NameProc = "ViewAllBlock";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(dt.Rows[i][2].ToString() == BuildingName)
                    cbBlock.Items.Add(dt.Rows[i][1].ToString());
            }
        }

        //Load dữ liệu vào bảng
        void LoadData()
        {
            LoadCombobox();
            tb.Rows.Clear();
            string NameProc = "ViewAllFloor";
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
                dtgridFloor.DataSource = tb;
            }

        }

        private void FrmFloor_Load(object sender, EventArgs e)
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
            txtID.Text = dtgridFloor.Rows[posTable].Cells[2].Value.ToString();
            txtName.Text = dtgridFloor.Rows[posTable].Cells[3].Value.ToString();
            cbBuilding.Text = dtgridFloor.Rows[posTable].Cells[5].Value.ToString();
        }

        //Click chọn vào table
        private void dtgridFloor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tb.Rows.Count > 0)
            {
                SetLabel(true);
                int posTable = dtgridFloor.CurrentRow.Index;
                ShowData(posTable);
            }
        }

        //Xóa thông tin tìm kiếm khi click vào textbox tìm kiếm
        private void txtSearch_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtSearch.Text == "Nhập tên tầng ....")
                txtSearch.Text = "";
        }

        //Set lại thông tin tìm kiếm khi bỏ chọn
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
                txtSearch.Text = "Nhập tên tầng ....";
        }

        //Chọn nút thêm
        private void btAdd_Click(object sender, EventArgs e)
        {
            checkAdd = 1;
            SetBtn(false);
            SetTxt(true);
            SetNull();
            SetLabel(false);

            string sql = "Select IDENT_CURRENT('Floor')";
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

        //chọn nút sửa
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
                MessageBox.Show("Vui lòng chọn tầng để sửa !", "Thông báo");
        }

        //Hàm xóa
        void DeleteFloor(string ID)
        {
            string sql = "execute DeleteFloor'" + ID + "'";
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
            for (int i = 0; i < dtgridFloor.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtgridFloor.Rows[i].Cells[0].Value) == true)
                {
                    t += "- " + dtgridFloor.Rows[i].Cells[3].Value.ToString() + " - " + dtgridFloor.Rows[i].Cells[4].Value.ToString() + " - " + dtgridFloor.Rows[i].Cells[5].Value.ToString() + "\n";
                    countCheck++;
                }
            }
            if (countCheck == 0)
                MessageBox.Show("Vui lòng chọn tầng cần xóa !", "Xóa");
            else
            {
                string tb = "Bạn đang xóa " + countCheck + " tầng: \n" + t + "Bạn có chắc xóa không ?";
                DialogResult dr = MessageBox.Show(tb, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    for (int i = 0; i < dtgridFloor.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dtgridFloor.Rows[i].Cells[0].Value) == true)
                        {
                            DeleteFloor(dtgridFloor.Rows[i].Cells[2].Value.ToString());
                        }
                    }
                }
            }


            LoadData();

        }

        //Chuyển tên thành mã 
        int Change(string nameBlock, string nameBuilding)
        {
            string NameProc = "ViewAllBlock";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString() == nameBlock && dt.Rows[i][2].ToString() == nameBuilding)
                    return int.Parse(dt.Rows[i][0].ToString());
            }
            return -1;
        }

        //Hàm kiểm tra tên
        bool CheckName(string Name, string ID, string BuildingName, string BlockName, int checkAdd)
        {
            string NameProc = "ViewAllFloor";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString().ToLower() == Name.ToLower() && dt.Rows[i][2].ToString().ToLower() == BlockName.ToLower() && dt.Rows[i][3].ToString().ToLower() == BuildingName.ToLower())
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
            if (txtName.Text.Trim() == "")
                MessageBox.Show("Vui lòng nhập [Tên tầng]", "Thông báo");
            else
            if (cbBuilding.Text.Trim() == "")
                MessageBox.Show("Vui lòng chọn [Chung cư]", "Thông báo");
            else
            if (cbBlock.Text.Trim() == "")
                MessageBox.Show("Vui lòng chọn [Khu]", "Thông báo");
            else
            if (txtName.Text.Trim().Length > 30)
                MessageBox.Show("[Tên tầng] không quá 30 ký tự !", "Thông báo");
            else
            {
                if (CheckName(txtName.Text.Trim(), txtID.Text.Trim(), cbBuilding.Text.Trim(), cbBlock.Text.Trim(), checkAdd) == true)
                    MessageBox.Show("Tên trùng lặp !", "Thông báo");
                else
                {
                    if (checkAdd == 1)
                    {
                        string sql = string.Format("execute InsertFloor N'{0}','{1}'", txtName.Text.Trim(), Change(cbBlock.Text.Trim(), cbBuilding.Text.Trim()));
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
                        string sql = string.Format("execute UpdateFloor '{0}',N'{1}','{2}'", txtID.Text.Trim(), txtName.Text.Trim(), Change(cbBlock.Text.Trim(), cbBuilding.Text.Trim()));
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
            if (txtSearch.Text == "Nhập tên tầng ...." || txtSearch.Text.Trim() == "")
                LoadData();
            else
            if (txtSearch.Text != "")
            {
                tb.Rows.Clear();
                string procName = "ViewAllFloor";
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
                                dtgridFloor.DataSource = tb;

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

        //Load khu khi có chung cư
        private void cbBuilding_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBlock(cbBuilding.Text);
            if(cbBuilding.Enabled == false)
                cbBlock.Text = dtgridFloor.Rows[dtgridFloor.CurrentRow.Index].Cells[4].Value.ToString();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    SendKeys.Send("{BACKSPACE}");
        }
    }
}
