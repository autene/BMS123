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
    public partial class FrmEvent : RibbonForm
    {
        //Khai báo biến
        Database db = new Database();
        DataRow r;
        DataTable tb;
        int checkAdd = 0;
        public FrmEvent()
        {
            InitializeComponent();
        }

        //Tạo cột cho table
        void CreateColumns()
        {
            tb = new DataTable();
            tb.Columns.Add("STT");
            tb.Columns.Add("Mã");
            tb.Columns.Add("Tên sự kiện");
            tb.Columns.Add("Ngày diễn ra");
            tb.Columns.Add("Chung cư");
            tb.Columns.Add("Nội dung");
            tb.Columns.Add("Nhà tổ chức");
            tb.Columns.Add("Liên lạc");
            dtgridEvent.DataSource = tb;
        }

        //Set size table
        void SetSizeTable()
        {
            for (int i = 1; i < dtgridEvent.ColumnCount; i++)
                dtgridEvent.Columns[i].ReadOnly = true;

            dtgridEvent.Columns[1].FillWeight = 25;
            dtgridEvent.Columns[2].FillWeight = 25;
        }

        //Cài đặt các lable (*)
        void SetLabel(bool val)
        {
            if (val)
            {
                lbName.Text = "";
                lbBuilding.Text = "";
                lbDate.Text = "";
                lbDes.Text = "";
            }
            else
            {
                lbName.Text = "(*)";
                lbBuilding.Text = "(*)";
                lbDate.Text = "(*)";
                lbDes.Text = "(*)";
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
            txtDes.Enabled = val;
            txtOrgan.Enabled = val;
            txtContact.Enabled = val;
            cbBuilding.Enabled = val;
            dtimeDate.Enabled = val;
            txtSearch.Enabled = !val;
            dtgridEvent.Enabled = !val;
        }

        //Xóa trắng các textbox
        void SetNull()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtDes.Text = "";
            txtOrgan.Text = "";
            txtContact.Text = "";
            cbBuilding.Text = "";
            dtimeDate.Text = "";
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
            string NameProc = "ViewAllEvent";
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
                r[7] = dt.Rows[i][6].ToString();
                tb.Rows.Add(r);
                dtgridEvent.DataSource = tb;
            }

        }

        private void FrmEvent_Load(object sender, EventArgs e)
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
            txtID.Text = dtgridEvent.Rows[posTable].Cells[2].Value.ToString();
            txtName.Text = dtgridEvent.Rows[posTable].Cells[3].Value.ToString();
            dtimeDate.Text = dtgridEvent.Rows[posTable].Cells[4].Value.ToString();
            cbBuilding.Text = dtgridEvent.Rows[posTable].Cells[5].Value.ToString();
            txtDes.Text = dtgridEvent.Rows[posTable].Cells[6].Value.ToString();
            txtOrgan.Text = dtgridEvent.Rows[posTable].Cells[7].Value.ToString();
            txtContact.Text = dtgridEvent.Rows[posTable].Cells[8].Value.ToString();
        }

        //Click chọn vào table
        private void dtgridEvent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tb.Rows.Count > 0)
            {
                SetLabel(true);
                int posTable = dtgridEvent.CurrentRow.Index;
                ShowData(posTable);
            }
        }

        //Xóa thông tin tìm kiếm khi click vào textbox tìm kiếm
        private void txtSearch_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtSearch.Text == "Nhập tên sự kiện ....")
                txtSearch.Text = "";
        }

        //Set lại thông tin tìm kiếm khi bỏ chọn
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
                txtSearch.Text = "Nhập tên sự kiện ....";
        }

        //Chọn nút thêm
        private void btAdd_Click(object sender, EventArgs e)
        {
            checkAdd = 1;
            SetBtn(false);
            SetTxt(true);
            SetNull();
            SetLabel(false);

            string sql = "Select IDENT_CURRENT('Event')";
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
                MessageBox.Show("Vui lòng chọn sự kiện để sửa !", "Thông báo");
        }

        //Hàm xóa
        void DeleteBlock(string ID)
        {
            string sql = "execute DeleteEvent'" + ID + "'";
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
            for (int i = 0; i < dtgridEvent.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtgridEvent.Rows[i].Cells[0].Value) == true)
                {
                    t += "- " + dtgridEvent.Rows[i].Cells[3].Value.ToString() + " - " + DateTime.Parse(dtgridEvent.Rows[i].Cells[4].Value.ToString()).ToShortDateString() + " - " + dtgridEvent.Rows[i].Cells[5].Value.ToString() + "\n";
                    countCheck++;
                }
            }
            if (countCheck == 0)
                MessageBox.Show("Vui lòng chọn sự kiện cần xóa !", "Xóa");
            else
            {
                string tb = "Bạn đang xóa " + countCheck + " sự kiện: \n" + t + "Bạn có chắc xóa không ?";
                DialogResult dr = MessageBox.Show(tb, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    for (int i = 0; i < dtgridEvent.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dtgridEvent.Rows[i].Cells[0].Value) == true)
                        {
                            DeleteBlock(dtgridEvent.Rows[i].Cells[2].Value.ToString());
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

        //Chọn nút lưu
        private void btSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng nhập [Tên sự kiện] !", "Thông báo");
            else
            if (cbBuilding.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng chọn [Chung cư] !", "Thông báo");
            else
            if (dtimeDate.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng chọn [Ngày] !", "Thông báo");
            else
            if (txtDes.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng nhập [Nội dung] !", "Thông báo");
            else
            if (txtName.Text.Trim().Length > 50)
                MessageBox.Show("[Tên sự kiện] không quá 50 ký tự !", "Thông báo");
            else
            if (txtDes.Text.Trim().Length > 1000)
                MessageBox.Show("[Nội dung] không quá 1000 ký tự !", "Thông báo");
            else
            if (txtOrgan.Text.Trim().Length > 50)
                MessageBox.Show("[Bộ phận tổ chức] không quá 50 ký tự !", "Thông báo");
            else
            if (txtContact.Text.Trim().Length > 50)
                MessageBox.Show("[Thông tin liên hệ] không quá 50 ký tự !", "Thông báo");
            else
            {
                {
                    if (checkAdd == 1)
                        if (DateTime.Compare(dtimeDate.Value, DateTime.Today) < 0)
                            MessageBox.Show("[Ngày diễn ra] phải bằng hoặc lớn hơn ngày hiện tại !", "Thông báo");
                        else
                        {
                        string sql = string.Format("execute InsertEvent '{0}',N'{1}','{2}',N'{3}',N'{4}',N'{5}'", Change(cbBuilding.Text.Trim()), txtName.Text.Trim(), dtimeDate.Value, txtDes.Text.Trim(), txtOrgan.Text.Trim(), txtContact.Text.Trim());
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
                        string sql = string.Format("execute UpdateEvent '{0}','{1}',N'{2}','{3}',N'{4}',N'{5}',N'{6}'", txtID.Text.Trim(), Change(cbBuilding.Text.Trim()), txtName.Text.Trim(), dtimeDate.Value, txtDes.Text.Trim(), txtOrgan.Text.Trim(), txtContact.Text.Trim());
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
            if (txtSearch.Text == "Nhập tên sự kiện ...." || txtSearch.Text.Trim() == "")
                LoadData();
            else
            if (txtSearch.Text != "")
            {
                tb.Rows.Clear();
                string procName = "ViewAllEvent";
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
                                r[5] = dt.Rows[i][4].ToString();
                                r[6] = dt.Rows[i][5].ToString();
                                r[7] = dt.Rows[i][6].ToString();
                                tb.Rows.Add(r);
                                dtgridEvent.DataSource = tb;

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
