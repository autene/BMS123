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
    public partial class FrmDetailLiving : RibbonForm
    {
        //Khai báo biến
        Database db = new Database();
        DataRow r;
        DataTable tb;
        public FrmDetailLiving()
        {
            InitializeComponent();
        }

        //Tạo cột cho table
        void CreateColumns()
        {
            tb = new DataTable();
            tb.Columns.Add("STT");
            tb.Columns.Add("Mã Detail");
            tb.Columns.Add("Mã nhân khẩu");
            tb.Columns.Add("Tên nhân khẩu");
            tb.Columns.Add("Căn hộ");
            tb.Columns.Add("Chung cư");
            tb.Columns.Add("Ngày bắt đầu");
            tb.Columns.Add("Ngày kết thúc");
            tb.Columns.Add("Tình trạng");
            tb.Columns.Add("Chủ hộ");
            tb.Columns.Add("Mối quan hệ với chủ hộ");
            dtgridLiving.DataSource = tb;

        }

        //Set size table
        void SetSizeTable()
        {

            for (int i = 1; i < dtgridLiving.ColumnCount; i++)
                dtgridLiving.Columns[i].ReadOnly = true;

            dtgridLiving.Columns[1].Width = 30;
            dtgridLiving.Columns[2].Visible = false;
            dtgridLiving.Columns[3].Width = 150;
            dtgridLiving.Columns[4].Width = 150;
            dtgridLiving.Columns[6].Width = 200;
            dtgridLiving.Columns[11].Width = 200;
        }

        //Load dữ liệu vào bảng
        void LoadData()
        {
            tb.Rows.Clear();
            string NameProc = "ViewAllLiving";
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
                r[6] = DateTime.Parse(dt.Rows[i][5].ToString()).ToShortDateString();
                if (dt.Rows[i][6].ToString() != "")
                {
                    if (DateTime.Compare(DateTime.Parse(dt.Rows[i][6].ToString()), DateTime.Parse("1/1/1900")) == 0)
                        r[7] = "";
                    else
                        r[7] = DateTime.Parse(dt.Rows[i][6].ToString()).ToShortDateString();
                }
                else
                    r[7] = "";
                if (dt.Rows[i][7].ToString() == "True")
                    r[8] = "Thường trú";
                else
                    r[8] = "Tạm trú";
                if (dt.Rows[i][8].ToString() == "True")
                    r[9] = "Có";
                else
                    r[9] = "Không";
                r[10] = dt.Rows[i][9].ToString();
                tb.Rows.Add(r);
                dtgridLiving.DataSource = tb;
            }

        }

        //Chọn nút thêm
        private void btAdd_Click(object sender, EventArgs e)
        {
            FrmAddCustomer f = new FrmAddCustomer();
            f.MesFunc = 2;
            f.ShowDialog();
            LoadData();
        }

        private void FrmDetailLiving_Load(object sender, EventArgs e)
        {
            CreateColumns();
            SetSizeTable();
            LoadData();
        }

        //chọn nút xem
        private void btView_Click(object sender, EventArgs e)
        {
            if (dtgridLiving.CurrentRow.Index < 0)
                MessageBox.Show("Vui lòng chọn 1 nhân khẩu cần xem !", "Thông báo");
            else
            {
                FrmAddCustomer f = new FrmAddCustomer();
                f.MesID = dtgridLiving.Rows[dtgridLiving.CurrentRow.Index].Cells[2].Value.ToString();
                f.MesFunc = 1;
                f.ShowDialog();
            }
        }

        //Chọn nút sửa
        private void btEdit_Click(object sender, EventArgs e)
        {
            if (dtgridLiving.CurrentRow.Index < 0)
                MessageBox.Show("Vui lòng chọn 1 nhân khẩu cần sửa !", "Thông báo");
            else
            {
                FrmAddCustomer f = new FrmAddCustomer();
                f.MesID = dtgridLiving.Rows[dtgridLiving.CurrentRow.Index].Cells[2].Value.ToString();
                f.MesFunc = 3;
                f.ShowDialog();
                LoadData();
            }
        }

        //Hàm xóa
        void DeleteLiving(string ID, string CustomerID)
        {
            string sql = "execute DeleteLiving'" + ID + "'";
            if (db.Execute(sql) == false)
                MessageBox.Show("Không thể xóa ! Xin vui lòng kiểm tra lại các thông tin liên quan !", "Thông báo");
        }

        //chọn nút xóa
        private void btDelete_Click(object sender, EventArgs e)
        {
            string t = "";
            string kt = "";
            string th = "";
            int checkkt = 0;
            int checkth = 0;
            int countCheck = 0;
            int temp;
            for (int i = 0; i < dtgridLiving.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtgridLiving.Rows[i].Cells[0].Value) == true)
                {
                    if (dtgridLiving.Rows[i].Cells[8].Value.ToString() == "")
                    {
                        kt += "- " + dtgridLiving.Rows[i].Cells[4].Value.ToString() + " - ở căn hộ - " + dtgridLiving.Rows[i].Cells[5].Value.ToString() + "\n";
                        checkkt = 1;
                    }
                    else
                    if (DateTime.Compare(DateTime.Parse(dtgridLiving.Rows[i].Cells[8].Value.ToString()), DateTime.Today) >= 0)
                    {
                        th += "- " + dtgridLiving.Rows[i].Cells[4].Value.ToString() + " - ở căn hộ - " + dtgridLiving.Rows[i].Cells[5].Value.ToString() + "\n";
                        checkth = 1;
                    }

                    t += "- " + dtgridLiving.Rows[i].Cells[4].Value.ToString() + " - ở căn hộ - " + dtgridLiving.Rows[i].Cells[5].Value.ToString() + "\n";
                    countCheck++;
                }
            }
            if (countCheck == 0)
                MessageBox.Show("Vui lòng chọn nhân khẩu cần xóa !", "Xóa");
            else
            {
                string tb = "Bạn đang xóa " + countCheck + " nhân khẩu: \n" + t + "Bạn có chắc xóa không ?";
                if (checkkt == 1 || checkth == 1)
                    tb += "\n\nNhưng những nhân khẩu sau đây không thể xóa:\n";
                if (checkkt == 1)
                    tb += "\n Chưa có ngày kết thúc ở căn hộ :\n" + kt;
                if (checkth == 1)
                    tb += "\n Chưa hết thời hạn ở căn hộ :\n" + th;
                DialogResult dr = MessageBox.Show(tb, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    for (int i = 0; i < dtgridLiving.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dtgridLiving.Rows[i].Cells[0].Value) == true)
                        {
                            if (dtgridLiving.Rows[i].Cells[8].Value.ToString() == "") temp = 0;
                            else
                                if (DateTime.Compare(DateTime.Parse(dtgridLiving.Rows[i].Cells[8].Value.ToString()), DateTime.Today) >= 0) temp = 0;
                            else
                                DeleteLiving(dtgridLiving.Rows[i].Cells[2].Value.ToString(), dtgridLiving.Rows[i].Cells[3].Value.ToString());
                        }
                    }
                }
            }


            LoadData();
            
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
                string procName = "ViewAllLiving";
                DataTable dt = db.LoadProcedure(procName);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int check = 0;
                    for (int a = 0; a < dt.Rows[i][2].ToString().Length; a++)
                    {
                        string getName = "";
                        for (int b = a; b < dt.Rows[i][2].ToString().Length; b++)
                        {
                            char getChar = dt.Rows[i][2].ToString()[b];
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
                                r[6] = DateTime.Parse(dt.Rows[i][5].ToString()).ToShortDateString();
                                if (dt.Rows[i][6].ToString() != "")
                                {
                                    if (DateTime.Compare(DateTime.Parse(dt.Rows[i][6].ToString()), DateTime.Parse("1/1/1900")) == 0)
                                        r[7] = "";
                                    else
                                        r[7] = DateTime.Parse(dt.Rows[i][6].ToString()).ToShortDateString();
                                }
                                else
                                    r[7] = "";
                                if (dt.Rows[i][7].ToString() == "True")
                                    r[8] = "Thường trú";
                                else
                                    r[8] = "Tạm trú";
                                if (dt.Rows[i][8].ToString() == "True")
                                    r[9] = "Có";
                                else
                                    r[9] = "Không";
                                r[10] = dt.Rows[i][9].ToString();
                                tb.Rows.Add(r);
                                dtgridLiving.DataSource = tb;

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

        private void btChangeHost_Click(object sender, EventArgs e)
        {

            if (dtgridLiving.Rows[dtgridLiving.CurrentRow.Index].Cells[10].Value.ToString().Trim() != "Có")
                MessageBox.Show("Không phải chủ hộ. Chỉ thay đổi được nếu nhân khẩu đang là chủ hộ của căn hộ này !", "Thông báo");
            else
            if ((DateTime.Compare(DateTime.Parse(dtgridLiving.Rows[dtgridLiving.CurrentRow.Index].Cells[8].Value.ToString().Trim()), DateTime.Today) < 0 && DateTime.Compare(DateTime.Parse(dtgridLiving.Rows[dtgridLiving.CurrentRow.Index].Cells[8].Value.ToString().Trim()), DateTime.Parse("1/1/1900")) != 0) && (dtgridLiving.Rows[dtgridLiving.CurrentRow.Index].Cells[8].Value.ToString().Trim() != ""))
                MessageBox.Show("Nhân khẩu đã không còn ở trong căn hộ này .Chỉ thay đổi khi nhân khẩu hiện đang ở và là chủ hộ của căn hộ này !", "Thông báo");
            else
            {
                FrmChangeHost f = new FrmChangeHost();
                f.MesID = dtgridLiving.Rows[dtgridLiving.CurrentRow.Index].Cells[2].Value.ToString().Trim();
                f.MesName = dtgridLiving.Rows[dtgridLiving.CurrentRow.Index].Cells[4].Value.ToString().Trim();
                f.ShowDialog();
                LoadData();
            }


        }



    }
}
