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
    public partial class FrmTransport : RibbonForm
    {
        //Khai báo biến
        Database db = new Database();
        DataRow r;
        DataTable tb;
        int checkAdd = 0;

        public FrmTransport()
        {
            InitializeComponent();
        }

        //Tạo cột cho table
        void CreateColumns()
        {
            tb = new DataTable();
            tb.Columns.Add("STT");
            tb.Columns.Add("Mã thẻ");
            tb.Columns.Add("Căn hộ");
            tb.Columns.Add("Tầng");
            tb.Columns.Add("Khu");
            tb.Columns.Add("Chung cư");
            tb.Columns.Add("Trạng thái");
            tb.Columns.Add("Ngày tạo");
            dtgridTransport.DataSource = tb;
        }

        //Set size table
        void SetSizeTable()
        {
            for (int i = 1; i < dtgridTransport.ColumnCount; i++)
                dtgridTransport.Columns[i].ReadOnly = true;

            dtgridTransport.Columns[1].FillWeight = 27;
            dtgridTransport.Columns[2].FillWeight = 60;
        }

        //Cài đặt các lable (*)
        void SetLabel(bool val)
        {
            if (val)
            {
                lbID.Text = "";
                lbApart.Text = "";
                lbBuilding.Text = "";
                lbStatus.Text = "";
            }
            else
            {
                lbID.Text = "(*)";
                lbApart.Text = "(*)";
                lbBuilding.Text = "(*)";
                lbStatus.Text = "(*)";
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
            cbApart.Enabled = val;
            cbStatus.Enabled = val;
            cbBuilding.Enabled = val;
            txtSearch.Enabled = !val;
            dtgridTransport.Enabled = !val;
        }

        //Xóa trắng các textbox
        void SetNull()
        {
            txtID.Text = "";
            cbApart.Text = null;
            cbStatus.Text = null;
            cbBuilding.Text = null;
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

        //Load căn hộ
        void LoadApart( string BuildingName)
        {
            cbApart.Items.Clear();
            string NameProc = "ViewAllApart";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if ( dt.Rows[i][7].ToString() == BuildingName)
                    cbApart.Items.Add(dt.Rows[i][1].ToString());
            }
        }

        //Load trạng thái
        void LoadStatus()
        {
            cbStatus.Items.Clear();
            cbStatus.Items.Add("Đã giao");
            cbStatus.Items.Add("Mất");
            cbStatus.Items.Add("Hư");
        }

        //Load dữ liệu vào bảng
        void LoadData()
        {
            LoadCombobox();
            LoadStatus();
            tb.Rows.Clear();
            string NameProc = "ViewAllTransport";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                r = tb.NewRow();
                r[0] = i + 1;
                r[1] = dt.Rows[i][0].ToString();
                r[2] = dt.Rows[i][1].ToString();
                r[3] = dt.Rows[i][2].ToString();
                r[4] = dt.Rows[i][3].ToString();
                r[5] = dt.Rows[i][4].ToString();
                r[6] = dt.Rows[i][5].ToString();
                r[7] = DateTime.Parse(dt.Rows[i][6].ToString()).ToShortDateString();
                tb.Rows.Add(r);
                dtgridTransport.DataSource = tb;
            }

        }

        private void FrmTransport_Load(object sender, EventArgs e)
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
            txtID.Text = dtgridTransport.Rows[posTable].Cells[2].Value.ToString();
            cbApart.Text = dtgridTransport.Rows[posTable].Cells[3].Value.ToString();
            cbBuilding.Text = dtgridTransport.Rows[posTable].Cells[6].Value.ToString();
            cbStatus.Text = dtgridTransport.Rows[posTable].Cells[7].Value.ToString();
        }

        //Click vào table
        private void dtgridTransport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tb.Rows.Count > 0)
            {
                SetLabel(true);
                int posTable = dtgridTransport.CurrentRow.Index;
                ShowData(posTable);
            }
        }

        //Load căn hộ khi chọn chung cư
        private void cbBuilding_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApart(cbBuilding.Text);
            if (cbBuilding.Enabled == false)
                cbApart.Text = dtgridTransport.Rows[dtgridTransport.CurrentRow.Index].Cells[3].Value.ToString();
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
            if (txtID.Text.Trim() != "")
            {
                checkAdd = 2;
                SetBtn(false);
                //SetTxt(true);
                SetLabel(false);
                cbStatus.Enabled = true;
                lbStatus.Text = "(*)";
                txtID.Enabled = false;
            }
            else
                MessageBox.Show("Vui lòng chọn thẻ để sửa !", "Thông báo");
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
        void DeleteTransport(string TranID, int ApartID)
        {
            string sql = "execute DeleteDetailTransport'" + TranID + "','" + ApartID + "'";
            if (db.Execute(sql) == true)
                SetNull();
            else
                MessageBox.Show("Không thể xóa ! Xin vui lòng kiểm tra lại các thông tin liên quan !", "Thông báo");

            sql = "execute DeleteTransport'" + TranID + "'";
            if (db.Execute(sql) == true)
                SetNull();
            else
                MessageBox.Show("Không thể xóa ! Xin vui lòng kiểm tra lại các thông tin liên quan !", "Thông báo");
        }

        //Chuyển tên thành mã
        int Change(string nameApart, string nameBuilding)
        {
            string NameProc = "ViewAllApart";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString() == nameApart && dt.Rows[i][7].ToString() == nameBuilding)
                    return int.Parse(dt.Rows[i][0].ToString());
            }
            return -1;
        }

        //Nút xóa
        private void btDelete_Click(object sender, EventArgs e)
        {
            string t = "";
            int countCheck = 0;
            for (int i = 0; i < dtgridTransport.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtgridTransport.Rows[i].Cells[0].Value) == true)
                {
                    t += "- " + dtgridTransport.Rows[i].Cells[2].Value.ToString() + "\n";
                    countCheck++;
                }
            }
            if (countCheck == 0)
                MessageBox.Show("Vui lòng chọn thẻ cần xóa !", "Xóa");
            else
            {
                string tb = "Bạn đang xóa " + countCheck + " thẻ: \n" + t + "Bạn có chắc xóa không ?";
                DialogResult dr = MessageBox.Show(tb, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    for (int i = 0; i < dtgridTransport.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dtgridTransport.Rows[i].Cells[0].Value) == true)
                        {
                            DeleteTransport(dtgridTransport.Rows[i].Cells[2].Value.ToString(), Change(dtgridTransport.Rows[i].Cells[3].Value.ToString(), dtgridTransport.Rows[i].Cells[6].Value.ToString()));
                        }
                    }
                }
            }


            LoadData();
        }

        //Hàm kiểm tra mã
        bool CheckID(string ID, int checkAdd)
        {
            string NameProc = "ViewTransport";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString().ToLower() == ID.ToLower())
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

        //Hàm kiểm tra căn hộ đang kinh doanh
        bool CheckApart(int ID)
        {
            string NameProc = "ViewApart";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == ID.ToString())
                    if (dt.Rows[i][2].ToString() == "False")
                        return true;
            }
            return false;
        }

        //Nút lưu
        private void btSave_Click(object sender, EventArgs e)
        {
            if (txtID.Text.Trim() == "")
                MessageBox.Show("Vui lòng nhập [Mã thẻ]", "Thông báo");
            else
            if (cbBuilding.Text.Trim() == "")
                MessageBox.Show("Vui lòng chọn [Chung cư]", "Thông báo");
            else
            if (cbApart.Text.Trim() == "")
                MessageBox.Show("Vui lòng chọn [Căn hộ]", "Thông báo");
            else
            if (cbStatus.Text.Trim() == "")
                MessageBox.Show("Vui lòng chọn [Trạng thái]", "Thông báo");
            else
            if (txtID.Text.Trim().Length > 10)
                MessageBox.Show("[Tên căn hộ] không quá 10 ký tự !", "Thông báo");
            else
            {
                if (CheckID(txtID.Text.Trim(), checkAdd) == true)
                    MessageBox.Show("Mã trùng lặp !", "Thông báo");
                else
                if (CheckApart(Change(cbApart.Text.Trim(), cbBuilding.Text.Trim())) == true)
                    MessageBox.Show("Căn hộ chưa có người ở !", "Thông báo");
                else
                {
                    if (checkAdd == 1)
                    {
                        string sql = string.Format("execute InsertTransport '{0}',N'{1}'", txtID.Text.Trim(), cbStatus.Text.Trim());
                        db.Execute(sql);

                        sql = string.Format("execute InsertDetailTransport '{0}','{1}','{2}'", txtID.Text.Trim(), Change(cbApart.Text.Trim(), cbBuilding.Text.Trim()), DateTime.Today);
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
                        string sql = string.Format("execute UpdateTransport '{0}',N'{1}'", txtID.Text.Trim(), cbStatus.Text.Trim());
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
            if (txtSearch.Text == "Nhập mã thẻ ....")
                txtSearch.Text = "";
        }

        //Set lại thông tin tìm kiếm khi bỏ chọn
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
                txtSearch.Text = "Nhập mã thẻ ....";
        }

        //Loại bỏ enter
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    SendKeys.Send("{BACKSPACE}");
        }

        //Tìm kiếm
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SetNull();
            if (txtSearch.Text == "Nhập mã thẻ ...." || txtSearch.Text.Trim() == "")
                LoadData();
            else
            if (txtSearch.Text != "")
            {
                tb.Rows.Clear();
                string procName = "ViewAllTransport";
                DataTable dt = db.LoadProcedure(procName);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int check = 0;
                    for (int a = 0; a < dt.Rows[i][0].ToString().Length; a++)
                    {
                        string getName = "";
                        for (int b = a; b < dt.Rows[i][0].ToString().Length; b++)
                        {
                            char getChar = dt.Rows[i][0].ToString()[b];
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
                                r[5] = dt.Rows[i][4].ToString();
                                r[6] = dt.Rows[i][5].ToString();
                                r[7] = DateTime.Parse(dt.Rows[i][6].ToString()).ToShortDateString();
                                tb.Rows.Add(r);
                                dtgridTransport.DataSource = tb;

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
