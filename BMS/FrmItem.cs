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
    public partial class FrmItem : RibbonForm
    {
        //Khai báo biến
        Database db = new Database();
        DataRow r;
        DataTable tb;
        int checkAdd = 0;

        public FrmItem()
        {
            InitializeComponent();
        }

        //Tạo cột cho table
        void CreateColumns()
        {
            tb = new DataTable();
            tb.Columns.Add("STT");
            tb.Columns.Add("Mã");
            tb.Columns.Add("Tài sản");
            tb.Columns.Add("Loại");
            tb.Columns.Add("Số lượng");
            tb.Columns.Add("Ngày");
            tb.Columns.Add("Chung cư");
            tb.Columns.Add("ID phân bố");
            dtgridItem.DataSource = tb;
        }

        //Set size table
        void SetSizeTable()
        {
            for (int i = 1; i < dtgridItem.ColumnCount; i++)
                dtgridItem.Columns[i].ReadOnly = true;

            dtgridItem.Columns[1].FillWeight = 30;
            dtgridItem.Columns[2].FillWeight = 25;
            dtgridItem.Columns[8].Visible = false;
        }

        //Cài đặt các lable (*)
        void SetLabel(bool val)
        {
            if (val)
            {
                lbName.Text = "";
                lbBuilding.Text = "";
                lbAmount.Text = "";
                lbDate.Text = "";
            }
            else
            {
                lbName.Text = "(*)";
                lbBuilding.Text = "(*)";
                lbAmount.Text = "(*)";
                lbDate.Text = "(*)";
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
            cbName.Enabled = val;
            txtAmount.Enabled = val;
            cbBuilding.Enabled = val;
            txtSearch.Enabled = !val;
            dtimeDate.Enabled = val;
            dtgridItem.Enabled = !val;
            pnInOut.Enabled = val;
        }

        //Xóa trắng các textbox
        void SetNull()
        {
            cbName.Text = null;
            txtAmount.Text = "";
            cbBuilding.Text = null;
            dtimeDate.Text = "";
            rdoIn.Checked = false;
            rdoOut.Checked = false;
            
        }

        //Load dữ liệu vào combobox chung cư
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

        //Load dữ liệu vào combobox tên tài sản
        void LoadName()
        {
            cbName.Items.Clear();
            string NameProc = "ViewItemName";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbName.Items.Add(dt.Rows[i][1].ToString());
            }
        }

        //Chuyển mã thành tên chung cư
        string ChangeIDToName(int ID)
        {
            string NameProc = "ViewAllBuilding";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
                if (int.Parse(dt.Rows[i][0].ToString()) == ID)
                    return dt.Rows[i][1].ToString();
            return "";
        }

        //Load dữ liệu vào bảng
        void LoadData()
        {
            LoadName();
            LoadCombobox();
            tb.Rows.Clear();
            string NameProc = "ViewAllItem";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                r = tb.NewRow();
                r[0] = i + 1;
                r[1] = dt.Rows[i][0].ToString();
                r[2] = dt.Rows[i][1].ToString();
                if(dt.Rows[i][2].ToString() == "True")
                    r[3] = "Xuất kho";
                else
                    r[3] = "Nhập kho";
                r[4] = dt.Rows[i][3].ToString();
                r[5] = DateTime.Parse(dt.Rows[i][4].ToString()).ToShortDateString();
                if (dt.Rows[i][5].ToString() != "")
                    r[6] = ChangeIDToName(int.Parse(dt.Rows[i][5].ToString()));
                else
                    r[6] = "";
                r[7] = dt.Rows[i][6].ToString();
                tb.Rows.Add(r);
                dtgridItem.DataSource = tb;
            }

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            FrmItemName f = new FrmItemName();
            f.ShowDialog();
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
                string procName = "ViewAllItem";
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
                                if (dt.Rows[i][2].ToString() == "True")
                                    r[3] = "Xuất kho";
                                else
                                    r[3] = "Nhập kho";
                                r[4] = dt.Rows[i][3].ToString();
                                r[5] = DateTime.Parse(dt.Rows[i][4].ToString()).ToShortDateString();
                                if (dt.Rows[i][5].ToString() != "")
                                    r[6] = ChangeIDToName(int.Parse(dt.Rows[i][5].ToString()));
                                else
                                    r[6] = "";
                                r[7] = dt.Rows[i][6].ToString();
                                tb.Rows.Add(r);
                                dtgridItem.DataSource = tb;

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

        //Chuyển tên thành mã chung cư
        int ChangeNameToID(string Name)
        {
            string NameProc = "ViewAllBuilding";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
                if (dt.Rows[i][1].ToString() == Name)
                    return int.Parse(dt.Rows[i][0].ToString());
            return -1;
        }

        //Chuyển tên thành mã tài sản
        int Change(string Name)
        {
            string NameProc = "ViewItemName";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
                if (dt.Rows[i][1].ToString() == Name)
                    return int.Parse(dt.Rows[i][0].ToString());
            return -1;
        }

        //Chuyển tên thành mã xuất tài sản
        int ChangeOut(string Name)
        {
            string NameProc = "ViewItemName";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
                if (dt.Rows[i][1].ToString() == Name)
                    return int.Parse(dt.Rows[i][0].ToString());
            return -1;
        }

        //Kiểm tra tài sản đã nhập chưa
        bool CheckIn(string Name)
        {
            string NameProc = "ViewAllItem";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString() == Name)
                    if (dt.Rows[i][2].ToString() == "False")
                        return false;
            }
            return true;
        }

        //Kiểm tra nhập số
        bool CheckNumber(string number)
        {
            try
            {
                if(int.Parse(number) > 0)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        //Nút lưu
        private void btSave_Click(object sender, EventArgs e)
        {
            if (cbName.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng chọn [Tên tài sản] !", "Thông báo");
            else
            if (rdoIn.Checked == false && rdoOut.Checked == false)
                MessageBox.Show("Vui lòng chọn [Trạng thái nhập/ xuất] !", "Thông báo");
            else
            if (cbBuilding.Text.Trim().Length == 0 && rdoIn.Checked == false)
                MessageBox.Show("Vui lòng chọn [Chung cư] !", "Thông báo");
            else
            if (txtAmount.Text.Trim().Length ==0 )
                MessageBox.Show("Vui lòng nhập [Số lượng] !", "Thông báo");
            else
            if (CheckNumber(txtAmount.Text.Trim()) == false)
                MessageBox.Show("[Số lượng] sai ! Chỉ được nhập số và lớn hơn 0", "Thông báo");
            else
            if (dtimeDate.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng nhập [Ngày] !", "Thông báo");
            else
            {
                int type = -1;
                if (rdoIn.Checked == true)
                    type = 0;
                else
                    type = 1;

                if (checkAdd == 1)
                {
                    if (DateTime.Compare(dtimeDate.Value, DateTime.Today) < 0)
                        MessageBox.Show("[Ngày] phải lớn hơn hoặc bằng ngày hiện tại !", "Thông báo");
                    else
                    {

                        string sql = string.Format("execute InsertItem '{0}','{1}','{2}','{3}'", Change(cbName.Text.Trim()), type, int.Parse(txtAmount.Text.Trim()), dtimeDate.Value);
                        db.Execute(sql);

                        if (type == 1)
                        {
                            sql = "Select IDENT_CURRENT('Item')";
                            DataTable dt = db.LoadData(sql);
                            int ID = int.Parse(dt.Rows[0][0].ToString());

                            sql = string.Format("execute InsertItemMana '{0}','{1}'", ID, ChangeNameToID(cbBuilding.Text.Trim()));
                            db.Execute(sql);
                        }

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
                    int ID = int.Parse(dtgridItem.Rows[dtgridItem.CurrentRow.Index].Cells[2].Value.ToString());
                    string sql = string.Format("execute UpdateItem '{0}','{1}','{2}','{3}','{4}'", ID, Change(cbName.Text.Trim()), type, int.Parse(txtAmount.Text.Trim()), dtimeDate.Value);
                    db.Execute(sql);

                    if (type == 1)
                    {
                        sql = "Select IDENT_CURRENT('ItemManagement')";
                        DataTable dt = db.LoadData(sql);
                        int IDs = int.Parse(dt.Rows[0][0].ToString());

                        sql = string.Format("execute InsertItemMana '{0}','{1}'", IDs, ChangeNameToID(cbBuilding.Text.Trim()));
                        db.Execute(sql);
                    }

                    LoadData();
                    SetBtn(true);
                    SetTxt(false);
                    SetNull();
                    SetLabel(false);
                    checkAdd = 0;
                }

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

        private void FrmItem_Load(object sender, EventArgs e)
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
            cbName.Text = dtgridItem.Rows[posTable].Cells[3].Value.ToString();
            if (dtgridItem.Rows[posTable].Cells[4].Value.ToString() == "Nhập kho")
                rdoIn.Checked = true;
            else
                rdoOut.Checked = true;
            txtAmount.Text = dtgridItem.Rows[posTable].Cells[5].Value.ToString();
            dtimeDate.Text = dtgridItem.Rows[posTable].Cells[6].Value.ToString();
            LoadCombobox();
            cbBuilding.Text = dtgridItem.Rows[posTable].Cells[7].Value.ToString();
        }

        //Click vào table
        private void dtgridItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tb.Rows.Count > 0)
            {
                SetLabel(true);
                int posTable = dtgridItem.CurrentRow.Index;
                ShowData(posTable);
            }
        }

        //Nút sửa
        private void btEdit_Click(object sender, EventArgs e)
        {
            if (cbName.Text != "")
            {
                checkAdd = 2;
                SetBtn(false);
                SetTxt(true);
                SetLabel(false);
                cbName.Enabled = false;
                pnInOut.Enabled = false;
                lbName.Text = "";
                if(dtgridItem.Rows[dtgridItem.CurrentRow.Index].Cells[4].Value.ToString() == "Nhập kho")
                {
                    cbBuilding.Enabled = false;
                    lbBuilding.Text = "";
                }
            }
            else
                MessageBox.Show("Vui lòng chọn dòng để sửa !", "Thông báo");
        }

        //Hủy
        private void btCancel_Click(object sender, EventArgs e)
        {
            SetBtn(true);
            SetTxt(false);
            SetNull();
            SetLabel(false);
            checkAdd = 0;
        }

        //Chọn nhập kho
        private void rdoIn_CheckedChanged(object sender, EventArgs e)
        {
            LoadCombobox();
            cbBuilding.Text = null;
            cbBuilding.Enabled = false;
            lbBuilding.Text = "";
        }

        //Chọn xuất kho
        private void rdoOut_CheckedChanged(object sender, EventArgs e)
        {
            cbBuilding.Enabled = true;
            lbBuilding.Text = "(*)";
            if(checkAdd == 1)
            {
                if (cbName.Text == "")
                {
                    if (rdoOut.Checked == true)
                    {
                        MessageBox.Show("Vui lòng chọn tài sản trước", "Thông báo");
                        rdoOut.Checked = false;
                    }
                }
                else
                    if (rdoOut.Checked == true)
                    if (CheckIn(cbName.Text) == true)
                    {
                        MessageBox.Show("Tài sản chưa nhập kho ! Vui lòng nhập trước", "Thông báo");
                        rdoIn.Checked = true;
                    }
            }
        }

        //Chọn combo tài sản
        private void cbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CheckIn(cbName.Text) == true)
                rdoIn.Checked = true;
        }

        //Không cho nhập chữ vào số lượng
        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Hàm xóa Item
        void DeleteItem(int ID)
        {
            string sql = "execute DeleteItem'" + ID + "'";
            if (db.Execute(sql) == true)
                SetNull();
            else
                MessageBox.Show("Không thể xóa ! Xin vui lòng kiểm tra lại các thông tin liên quan !", "Thông báo");
        }

        //Hàm xóa ItemManagement
        void DeleteItemMana(int ID)
        {
            string sql = "execute DeleteItemMana'" + ID + "'";
            if (db.Execute(sql) == true)
                SetNull();
            else
                MessageBox.Show("Không thể xóa ! Xin vui lòng kiểm tra lại các thông tin liên quan !", "Thông báo");
        }

        //Kiểm tra xóa tài sản nhập
        bool CheckDelete(string name)
        {
            string NameProc = "ViewAllItem";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString() == name && dt.Rows[i][2].ToString() == "True")
                    return true;
            }
            return false;
        }

        //Xóa
        private void btDelete_Click(object sender, EventArgs e)
        {
            string t = "";
            int countCheck = 0;
            for (int i = 0; i < dtgridItem.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtgridItem.Rows[i].Cells[0].Value) == true)
                {
                    t += "- " + dtgridItem.Rows[i].Cells[3].Value.ToString() + " - " + dtgridItem.Rows[i].Cells[4].Value.ToString() + " - " + DateTime.Parse(dtgridItem.Rows[i].Cells[6].Value.ToString()).ToShortDateString() + " - " + dtgridItem.Rows[i].Cells[7].Value.ToString() + "\n";
                    countCheck++;
                }
            }
            if (countCheck == 0)
                MessageBox.Show("Vui lòng chọn khu cần xóa !", "Xóa");
            else
            {
                string tb = "Bạn đang xóa " + countCheck + " dòng: \n" + t + "Bạn có chắc xóa không ?";
                DialogResult dr = MessageBox.Show(tb, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    for (int i = 0; i < dtgridItem.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dtgridItem.Rows[i].Cells[0].Value) == true)
                        {
                            int type = -1;
                            if (dtgridItem.Rows[i].Cells[4].Value.ToString() == "Nhập kho")
                                type = 0;
                            else
                                type = 1;

                            if (type == 1)
                            {
                                DeleteItemMana(int.Parse(dtgridItem.Rows[i].Cells[8].Value.ToString()));
                                DeleteItem(int.Parse(dtgridItem.Rows[i].Cells[2].Value.ToString()));
                            }
                            else
                            if (type == 0)
                                if (CheckDelete(dtgridItem.Rows[i].Cells[3].Value.ToString()) == true)
                                    MessageBox.Show("Tài sản còn trạng thái xuất trong dữ liệu. Vui lòng xóa hết tài sản này đã xuất mới có thể xóa trạng thái nhập !", "Thông báo");
                                else
                                {
                                    DeleteItem(int.Parse(dtgridItem.Rows[i].Cells[2].Value.ToString()));
                                }
                            
                        }
                    }
                }
            }

            LoadData();
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
    }
}
