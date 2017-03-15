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
    public partial class FrmApartment : RibbonForm
    {
        //Khai báo biến
        Database db = new Database();
        DataRow r;
        DataTable tb;
        int checkAdd = 0;
        public FrmApartment()
        {
            InitializeComponent();
        }

        //Tạo cột cho table
        void CreateColumns()
        {
            tb = new DataTable();
            tb.Columns.Add("STT");
            tb.Columns.Add("Mã");
            tb.Columns.Add("Tên căn hộ");
            tb.Columns.Add("Loại");
            tb.Columns.Add("Diện tích (m2)");
            tb.Columns.Add("Mô tả");
            tb.Columns.Add("Tầng");
            tb.Columns.Add("Khu");
            tb.Columns.Add("Chung cư");
            dtgridApart.DataSource = tb;
        }

        //Set size table
        void SetSizeTable()
        {
            for (int i = 1; i < dtgridApart.ColumnCount; i++)
                dtgridApart.Columns[i].ReadOnly = true;

            dtgridApart.Columns[1].FillWeight = 27;
            dtgridApart.Columns[2].FillWeight = 30;
        }

        //Cài đặt các lable (*)
        void SetLabel(bool val)
        {
            if (val)
            {
                lbName.Text = "";
                lbBuilding.Text = "";
                lbBlock.Text = "";
                lbFloor.Text = "";
                lbType.Text = "";
            }
            else
            {
                lbName.Text = "(*)";
                lbBuilding.Text = "(*)";
                lbBlock.Text = "(*)";
                lbFloor.Text = "(*)";
                lbType.Text = "(*)";
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
            txtAcreage.Enabled = val;
            txtDes.Enabled = val;
            cbType.Enabled = val;
            cbFloor.Enabled = val;
            cbBuilding.Enabled = val;
            cbBlock.Enabled = val;
            txtSearch.Enabled = !val;
            dtgridApart.Enabled = !val;
        }

        //Xóa trắng các textbox
        void SetNull()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtAcreage.Text = "";
            txtDes.Text = "";
            cbType.Text = "";
            cbBlock.Text = "";
            cbFloor.Text = "";
            cbBuilding.Text = "";
        }

        //Load dữ liệu chung cư
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

        //Load khu
        void LoadBlock(string BuildingName)
        {
            cbBlock.Items.Clear();
            string NameProc = "ViewAllBlock";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][2].ToString() == BuildingName)
                    cbBlock.Items.Add(dt.Rows[i][1].ToString());
            }
        }

        //Load tầng
        void LoadFloor(string BlockName, string BuildingName)
        {
            cbFloor.Items.Clear();
            string NameProc = "ViewAllFloor";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][2].ToString() == BlockName && dt.Rows[i][3].ToString() == BuildingName)
                    cbFloor.Items.Add(dt.Rows[i][1].ToString());
            }
        }

        //Load loại
        void LoadType()
        {
            cbType.Items.Clear();
            cbType.Items.Add("Bình thường");
            cbType.Items.Add("Kinh doanh");
        }

        //Load dữ liệu vào bảng
        void LoadData()
        {
            LoadCombobox();
            LoadType();
            tb.Rows.Clear();
            string NameProc = "ViewAllApart";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                r = tb.NewRow();
                r[0] = i + 1;
                r[1] = dt.Rows[i][0].ToString();
                r[2] = dt.Rows[i][1].ToString();
                if (dt.Rows[i][2].ToString() == "False")
                    r[3] = "Bình thường";
                else
                    r[3] = "Kinh doanh";
                r[4] = dt.Rows[i][3].ToString();
                r[5] = dt.Rows[i][4].ToString();
                r[6] = dt.Rows[i][5].ToString();
                r[7] = dt.Rows[i][6].ToString();
                r[8] = dt.Rows[i][7].ToString();
                tb.Rows.Add(r);
                dtgridApart.DataSource = tb;
            }

        }

        private void FrmApartment_Load(object sender, EventArgs e)
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
            txtID.Text = dtgridApart.Rows[posTable].Cells[2].Value.ToString();
            txtName.Text = dtgridApart.Rows[posTable].Cells[3].Value.ToString();
            cbType.Text = dtgridApart.Rows[posTable].Cells[4].Value.ToString();
            txtAcreage.Text = dtgridApart.Rows[posTable].Cells[5].Value.ToString();
            txtDes.Text = dtgridApart.Rows[posTable].Cells[6].Value.ToString();
            cbFloor.Text = dtgridApart.Rows[posTable].Cells[7].Value.ToString();
            cbBlock.Text = dtgridApart.Rows[posTable].Cells[8].Value.ToString();
            cbBuilding.Text = dtgridApart.Rows[posTable].Cells[9].Value.ToString();
        }

        //Chọn nút thêm
        private void btAdd_Click(object sender, EventArgs e)
        {
            checkAdd = 1;
            SetBtn(false);
            SetTxt(true);
            SetNull();
            SetLabel(false);
            
            string sql = "Select IDENT_CURRENT('Apartment')";
            DataTable dt = db.LoadData(sql);
            txtID.Text = (int.Parse(dt.Rows[0][0].ToString())+1).ToString();
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
                MessageBox.Show("Vui lòng chọn tầng để sửa !", "Thông báo");
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

        //Click chọn vào table
        private void dtgridApart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tb.Rows.Count > 0)
            {
                SetLabel(true);
                int posTable = dtgridApart.CurrentRow.Index;
                ShowData(posTable);
            }
        }

        //Load khu khi có chung cư
        private void cbBuilding_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBlock(cbBuilding.Text);
            if (cbBuilding.Enabled == false)
                cbBlock.Text = dtgridApart.Rows[dtgridApart.CurrentRow.Index].Cells[8].Value.ToString();
        }

        //Load tầng khi có khu
        private void cbBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFloor(cbBlock.Text, cbBuilding.Text);
            if (cbBlock.Enabled == false)
                cbFloor.Text = dtgridApart.Rows[dtgridApart.CurrentRow.Index].Cells[7].Value.ToString();
        }

        //Xóa thông tin tìm kiếm khi click vào textbox tìm kiếm
        private void txtSearch_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtSearch.Text == "Nhập tên căn hộ ....")
                txtSearch.Text = "";
        }

        //Set lại thông tin tìm kiếm khi bỏ chọn
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
                txtSearch.Text = "Nhập tên căn hộ ....";
        }

        //Hàm xóa
        void DeleteApart(string ID)
        {
            string sql = "execute DeleteApart'" + ID + "'";
            if (db.Execute(sql) == true)
                SetNull();
            else
                MessageBox.Show("Không thể xóa ! Xin vui lòng kiểm tra lại các thông tin liên quan !", "Thông báo");
        }

        //Chọn nút xóa
        private void btDelete_Click(object sender, EventArgs e)
        {
            string t = "";
            string kd = "";
            int CheckKD = 0;
            int countCheck = 0;
            for (int i = 0; i < dtgridApart.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtgridApart.Rows[i].Cells[0].Value) == true)
                {
                    if (dtgridApart.Rows[i].Cells[4].Value.ToString() == "Kinh doanh")
                    {
                        kd += "- " + dtgridApart.Rows[i].Cells[3].Value.ToString() + "\n";
                        CheckKD = 1;
                    }

                    t += "- " + dtgridApart.Rows[i].Cells[3].Value.ToString() + "\n";
                    countCheck++;
                }
            }
            if (countCheck == 0)
                MessageBox.Show("Vui lòng chọn căn hộ cần xóa !", "Xóa");
            else
            {
                string tb = "Bạn đang xóa " + countCheck + " căn hộ: \n" + t + "Bạn có chắc xóa không ?";
                if (CheckKD == 1)
                    tb = tb + "\nTrong đó: những căn hộ sau là đang kinh doanh nên không thể xóa: \n" + kd;
                DialogResult dr = MessageBox.Show(tb, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    for (int i = 0; i < dtgridApart.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dtgridApart.Rows[i].Cells[0].Value) == true)
                        {
                            if (dtgridApart.Rows[i].Cells[4].Value.ToString() != "Kinh doanh")
                                DeleteApart(dtgridApart.Rows[i].Cells[2].Value.ToString());
                        }
                    }
                }
            }
            
            
            LoadData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SetNull();
            if (txtSearch.Text == "Nhập tên căn hộ ...." || txtSearch.Text.Trim() == "")
                LoadData();
            else
            if (txtSearch.Text != "")
            {
                tb.Rows.Clear();
                string procName = "ViewAllApart";
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
                                if (dt.Rows[i][2].ToString() == "False")
                                    r[3] = "Bình thường";
                                else
                                    r[3] = "Kinh doanh";
                                r[4] = dt.Rows[i][3].ToString();
                                r[5] = dt.Rows[i][4].ToString();
                                r[6] = dt.Rows[i][5].ToString();
                                r[7] = dt.Rows[i][6].ToString();
                                r[8] = dt.Rows[i][7].ToString();
                                tb.Rows.Add(r);
                                dtgridApart.DataSource = tb;

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

        //Chuyển tên thành mã 
        int Change(string nameFloor, string nameBlock, string nameBuilding)
        {
            string NameProc = "ViewAllFloor";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString() == nameFloor && dt.Rows[i][2].ToString() == nameBlock && dt.Rows[i][3].ToString() == nameBuilding)
                    return int.Parse(dt.Rows[i][0].ToString());
            }
            return -1;
        }

        //Chuyển loại thành bit
        int ChangeType(string Type)
        {
            if (Type.Trim() == "Bình thường")
                return 0;
            else
                return 1;
        }

        //Hàm kiểm tra tên
        bool CheckName(string Name, string ID, string BuildingName, string BlockName, string FloorName, int checkAdd)
        {
            string NameProc = "ViewAllApart";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString().ToLower() == Name.ToLower() && dt.Rows[i][7].ToString().ToLower() == BuildingName.ToLower())
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

        //Kiểm tra nhập số thực trong diện tích
        bool CheckAcreageFloat(string Acreage)
        {
            try
            {
                if (float.Parse(Acreage) > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        //Chọn nút lưu
        private void btSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
                MessageBox.Show("Vui lòng nhập [Tên căn hộ]", "Thông báo");
            else
            if (cbBuilding.Text.Trim() == "")
                MessageBox.Show("Vui lòng chọn [Chung cư]", "Thông báo");
            else
            if (cbBlock.Text.Trim() == "")
                MessageBox.Show("Vui lòng chọn [Khu]", "Thông báo");
            else
            if (cbFloor.Text.Trim() == "")
                MessageBox.Show("Vui lòng chọn [Tầng]", "Thông báo");
            else
            if (cbType.Text.Trim() == "")
                MessageBox.Show("Vui lòng chọn [Loại]", "Thông báo");
            else
            if (txtName.Text.Trim().Length > 30)
                MessageBox.Show("[Tên căn hộ] không quá 30 ký tự !", "Thông báo");
            else
            if (txtDes.Text.Trim().Length > 1000)
                MessageBox.Show("[Mô tả] không quá 1000 ký tự !", "Thông báo");
            else
            {
                if (CheckName(txtName.Text.Trim(), txtID.Text.Trim(), cbBuilding.Text.Trim(), cbBlock.Text.Trim(), cbFloor.Text.Trim(), checkAdd) == true)
                    MessageBox.Show("Tên trùng lặp !", "Thông báo");
                else
                if (CheckAcreageFloat(txtAcreage.Text.Trim()) == false && txtAcreage.Text.Trim() != "")
                    MessageBox.Show("Diện tích sai !", "Thông báo");
                else
                {
                    if (checkAdd == 1)
                    {
                        string sql = string.Format("execute InsertApart N'{0}','{1}','{2}',N'{3}','{4}'", txtName.Text.Trim(), ChangeType(cbType.Text.Trim()), txtAcreage.Text.Trim(), txtDes.Text.Trim(), Change(cbFloor.Text.Trim(), cbBlock.Text.Trim(), cbBuilding.Text.Trim()));
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
                        string sql = string.Format("execute UpdateApart '{0}',N'{1}','{2}','{3}',N'{4}','{5}'", txtID.Text.Trim(), txtName.Text.Trim(),ChangeType(cbType.Text.Trim()), txtAcreage.Text.Trim(), txtDes.Text.Trim(), Change(cbFloor.Text.Trim(), cbBlock.Text.Trim(), cbBuilding.Text.Trim()));
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

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    SendKeys.Send("{BACKSPACE}");
        }

    }
}
