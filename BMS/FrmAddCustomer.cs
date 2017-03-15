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
    public partial class FrmAddCustomer : RibbonForm
    {
        Database db = new Database();
        //Lấy mã Detail
        private string _mesID;
        public String MesID
        {
            get { return _mesID; }
            set { _mesID = value; }
        }

        //Lấy chức năng chọn
        private int _mesFunc;
        public int MesFunc
        {
            get { return _mesFunc; }
            set { _mesFunc = value; }
        }
        public FrmAddCustomer()
        {
            InitializeComponent();
        }

        //Cài đặt trang thái nút
        void SetBtn(bool val)
        {
            btSave.Enabled = val;
            btCancel.Enabled = !val;
        }
        //Cài đặt trang thái lable Liv
        void SetLBLive()
        {
            
            lbBuilding.Text = "";
            lbApart.Text = "";
            lbStart.Text = "";
            lbStatus.Text = "";
            lbHost.Text = "";
        }

        //Cài đặt trang thái lable Cus
        void SetLBCus()
        {
            lbID.Text = "";
            lbName.Text = "";
            lbSex.Text = "";
            lbPhone.Text = "";
        }

        //Cài đặt text Cus
        void SetTxtCus()
        {
            txtID.Enabled = false;
            txtName.Enabled = false;
            dtimeDoB.Enabled = false;
            txtTown.Enabled = false;
            txtEmail.Enabled = false;
            pnSex.Enabled = false;
            txtPhone.Enabled = false;
            txtIDNumber.Enabled = false;
        }

        //Cài đặt text Live
        void SetTxtLive()
        {
            cbID.Enabled = false;
            cbBuilding.Enabled = false;
            cbApart.Enabled = false;
            dtimeStart.Enabled = false;
            dtimeEnd.Enabled = false;
            pnStatus.Enabled = false;
            pnHost.Enabled = false;
            txtRelation.Enabled = false;
        }

        //Load Mã nhân khẩu
        void LoadCusID()
        {
            cbID.Items.Clear();
            string NameProc = "ViewCustomer";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbID.Items.Add(dt.Rows[i][0].ToString());
            }
        }

        //Load chung cư
        void LoadBuilding()
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

        //Xem customer
        void ViewCus()
        {
            string NameProc = "ViewCustomer";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == _mesID)
                {
                    txtID.Text = dt.Rows[i][0].ToString();
                    txtName.Text = dt.Rows[i][1].ToString();
                    dtimeDoB.Text = dt.Rows[i][2].ToString();
                    if (dt.Rows[i][3].ToString() == "True")
                        rdoMale.Checked = true;
                    else
                        rdiFemale.Checked = true;
                    txtTown.Text = dt.Rows[i][4].ToString();
                    txtPhone.Text = dt.Rows[i][5].ToString();
                    txtEmail.Text = dt.Rows[i][6].ToString();
                    txtIDNumber.Text = dt.Rows[i][7].ToString();
                }
            }
        }

        //Xem living
        void View()
        {
            string NameProc = "ViewAllCustomer";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == _mesID)
                {
                    txtID.Text = dt.Rows[i][1].ToString();
                    txtName.Text = dt.Rows[i][2].ToString();
                    dtimeDoB.Text = dt.Rows[i][3].ToString();
                    txtTown.Text = dt.Rows[i][4].ToString();
                    txtEmail.Text = dt.Rows[i][5].ToString();
                    if (dt.Rows[i][6].ToString() == "True")
                        rdoMale.Checked = true;
                    else
                        rdiFemale.Checked = true;
                    txtPhone.Text = dt.Rows[i][7].ToString();
                    txtIDNumber.Text = dt.Rows[i][8].ToString();
                    cbBuilding.Text = dt.Rows[i][9].ToString();
                    cbApart.Text = dt.Rows[i][12].ToString();
                    dtimeStart.Text = dt.Rows[i][13].ToString();
                    dtimeEnd.Text = dt.Rows[i][14].ToString();
                    if (dt.Rows[i][15].ToString() == "True")
                        rdoResident.Checked = true;
                    else
                        rdoStaying.Checked = true;
                    if (dt.Rows[i][16].ToString() == "True")
                        rdoYes.Checked = true;
                    else
                        rdoNo.Checked = true;
                    txtRelation.Text = dt.Rows[i][17].ToString();
                }
            }
        }
        
        //Load khu
        private void cbBuilding_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApart(cbBuilding.Text);
        }

        //Chọn nút thoát
        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                Close();
        }

        //Hàm kiểm tra CMND
        bool CheckIDNumber(string IDNumber, string StaffID)
        {
            string NameProc = "ViewCustomer";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][7].ToString().ToLower() == IDNumber.ToLower())
                {
                    if (_mesFunc == 5)
                    {
                        if (dt.Rows[i][0].ToString().ToLower() != StaffID.ToLower())
                            return true;
                    }
                    else
                        return true;
                }
            }
            return false;
        }

        //Hàm kiểm tra mã nhân khẩu
        bool CheckID(string StaffID)
        {
            string NameProc = "ViewCustomer";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString().ToLower() == StaffID.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        //Kiểm tra nhập số
        bool CheckNumber(string Number)
        {
            try
            {
                long.Parse(Number);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Hàm kiểm tra căn hộ có chủ hộ chưa
        bool CheckHost(string nameApart, string nameBuilding)
        {
            string NameProc = "ViewLiving";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][2].ToString().ToLower() == Change(nameApart, nameBuilding).ToString().ToLower())
                {
                    if (dt.Rows[i][6].ToString() == "True")
                    {
                        if (dt.Rows[i][4].ToString() == "")
                            return true;
                        else
                        if ((DateTime.Compare(DateTime.Parse(dt.Rows[i][4].ToString()), DateTime.Today) >= 0) || (DateTime.Compare(DateTime.Parse(dt.Rows[i][4].ToString()), DateTime.Parse("1/1/1900")) == 0))
                            return true;
                    }
                }
            }
            return false;
        }

        //Hàm kiểm tra căn chủ hộ
        string GetHostCus(string nameApart, string nameBuilding)
        {
            string NameProc = "ViewLiving";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][2].ToString().ToLower() == Change(nameApart, nameBuilding).ToString().ToLower())
                {
                    if (dt.Rows[i][6].ToString() == "True")
                    {
                        if (dt.Rows[i][4].ToString() == "")
                            return dt.Rows[i][1].ToString();
                        else
                        if ((DateTime.Compare(DateTime.Parse(dt.Rows[i][4].ToString()), DateTime.Today) >= 0) || (DateTime.Compare(DateTime.Parse(dt.Rows[i][4].ToString()), DateTime.Parse("1/1/1900")) == 0))
                            return dt.Rows[i][1].ToString();
                    }
                }
            }
            return "";
        }

        //Hàm lấy ngày bắt đầu chủ hộ
        string GetStartDateHost(string nameApart, string nameBuilding)
        {
            string NameProc = "ViewLiving";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][2].ToString().ToLower() == Change(nameApart, nameBuilding).ToString().ToLower())
                {
                    if (dt.Rows[i][6].ToString() == "True")
                    {
                        if (dt.Rows[i][4].ToString() == "")
                            return dt.Rows[i][3].ToString();
                        else
                        if ((DateTime.Compare(DateTime.Parse(dt.Rows[i][4].ToString()), DateTime.Today) > 0) || (DateTime.Compare(DateTime.Parse(dt.Rows[i][4].ToString()), DateTime.Parse("1/1/1900")) == 0))
                            return dt.Rows[i][3].ToString();
                    }
                }
            }
            return "1/1/1900";
        }

        //Hàm lấy ngày kết thúc chủ hộ
        string GetEndDateHost(string nameApart, string nameBuilding)
        {
            string NameProc = "ViewLiving";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][2].ToString().ToLower() == Change(nameApart, nameBuilding).ToString().ToLower())
                {
                    if (dt.Rows[i][6].ToString() == "True")
                    {
                        if (dt.Rows[i][4].ToString() == "")
                            return "1/1/1900";
                        else
                        if ((DateTime.Compare(DateTime.Parse(dt.Rows[i][4].ToString()), DateTime.Today) >= 0) || (DateTime.Compare(DateTime.Parse(dt.Rows[i][4].ToString()), DateTime.Parse("1/1/1900")) == 0))
                            if(dt.Rows[i][6].ToString() != "1")
                                return dt.Rows[i][4].ToString();
                    }
                }
            }
            return "1/1/1900";
        }

        //Hàm kiểm tra ngày kết thúc người ở chung căn hộ
        bool checkEndDate(string date,string nameApart, string nameBuilding)
        {
            string NameProc = "ViewLiving";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][2].ToString().ToLower() == Change(nameApart, nameBuilding).ToString().ToLower())
                {
                    if (dt.Rows[i][6].ToString() == "False")
                    {
                        if (dt.Rows[i][4].ToString() == "")
                            return true;
                        else
                        if ((DateTime.Compare(DateTime.Parse(dt.Rows[i][4].ToString()), DateTime.Parse(date)) > 0) || (DateTime.Compare(DateTime.Parse(dt.Rows[i][4].ToString()), DateTime.Parse("1/1/1900")) == 0))
                            return true;
                    }
                }
            }
            return false;
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

        //=============================================================================================================================================//
        private void FrmCustomer_Load_1(object sender, EventArgs e)
        {
            LoadBuilding();
            LoadCusID();
            if (_mesFunc == 1)
            {
                View();
                btSave.Enabled = false;
                SetLBCus();
                SetLBLive();
                if (DateTime.Compare(dtimeDoB.Value, DateTime.Parse("1/1/1900")) == 0)
                    dtimeDoB.Text = "";
                if (DateTime.Compare(dtimeEnd.Value, DateTime.Parse("1/1/1900")) == 0)
                    dtimeEnd.Text = "";
                cbID.Enabled = false;
                SetTxtCus();
                SetTxtLive();
                lbCBID.Text = "";
            }
            else
            if (_mesFunc == 2)
            {
                SetTxtCus();
                SetLBCus();
            }
            else
            if (_mesFunc == 3)
            {
                SetTxtCus();
                SetTxtLive();
                View();
                if (DateTime.Compare(dtimeDoB.Value, DateTime.Parse("1/1/1900")) == 0)
                    dtimeDoB.Text = "";
                if (DateTime.Compare(dtimeEnd.Value, DateTime.Parse("1/1/1900")) == 0)
                    dtimeEnd.Text = "";
                dtimeEnd.Enabled = true;
                pnStatus.Enabled = true;
                if (rdoNo.Checked == true)
                    txtRelation.Enabled = true;
            }
            else
            if (_mesFunc == 4)
            {
                lbCBID.Text = "";
                cbID.Enabled = false;
            }
            else
            if (_mesFunc == 5)
            {
                SetTxtLive();
                ViewCus();
                if (DateTime.Compare(dtimeDoB.Value, DateTime.Parse("1/1/1900")) == 0)
                    dtimeDoB.Text = "";
                SetLBLive();
                txtID.Enabled = false;
                lbRela.Text = "";
            }
        }

        //Kiểm tra chọn check
        int CheckChoose(bool x, bool y)
        {
            if (x == true)
                return 1;
            else
            if (y == true)
                return 0;
            return -1;
        }

        //Kiểm tra ngày
        DateTime CheckDate (string Date)
        {
            if (Date == "")
                return DateTime.Parse("1/1/1900");
            else
                return DateTime.Parse(Date);
        }

        //Chọn nút lưu
        private void btSave_Click(object sender, EventArgs e)
        {
            if (txtID.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng nhập [Mã nhân khẩu] !", "Thông báo");
            else
            if (txtName.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng nhập [Tên nhân khẩu] !", "Thông báo");
            else
            if (DateTime.Compare(dtimeDoB.Value, DateTime.Today) > 0)
                MessageBox.Show("[Ngày sinh] phải lớn hơn hoặc bằng ngày hiện tại !", "Thông báo");
            else
            if (rdoMale.Checked == false && rdiFemale.Checked == false)
                MessageBox.Show("Vui lòng chọn [Giới tính] !", "Thông báo");
            else
            if (txtPhone.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng nhập [Số điện thoại] !", "Thông báo");
            else      
            if (txtID.Text.Trim().Length > 10)
                MessageBox.Show("[Mã nhân khẩu] không quá 10 ký tự !", "Thông báo");
            else
            if (txtName.Text.Trim().Length > 50)
                MessageBox.Show("[Tên nhân khẩu] không quá 50 ký tự !", "Thông báo");
            else
            if (txtTown.Text.Trim().Length > 100)
                MessageBox.Show("[Quê quán] không quá 100 ký tự !", "Thông báo");
            else
            if (txtPhone.Text.Trim().Length > 15)
                MessageBox.Show("[Só điện thoại] không quá 15 ký tự !", "Thông báo");
            else
            if (txtEmail.Text.Trim().Length > 50)
                MessageBox.Show("[Email] không quá 50 ký tự !", "Thông báo");
            else
            if (txtIDNumber.Text.Trim().Length > 12)
                MessageBox.Show("[Số CMND] không quá 12 ký tự !", "Thông báo");
            else
            {
                if (_mesFunc == 2 || _mesFunc == 4)
                {
                    int ktthem = 0;
                    string sql;
                    if (cbBuilding.Text.Trim().Length == 0)
                        MessageBox.Show("Vui lòng chọn [Chung cư] !", "Thông báo");
                    else
                    if (cbApart.Text.Trim().Length == 0)
                        MessageBox.Show("Vui lòng chọn [Căn hộ] !", "Thông báo");
                    else
                    if (dtimeStart.Text.Trim().Length == 0)
                        MessageBox.Show("Vui lòng chọn [Ngày bắt đầu] !", "Thông báo");
                    else
                    if (DateTime.Compare(dtimeStart.Value, dtimeEnd.Value) > 0 && dtimeEnd.Text != "")
                        MessageBox.Show("[Ngày bắt đầu] phải nhỏ hơn [Ngày kết thúc] !", "Thông báo");
                    else
                    if (DateTime.Compare(dtimeStart.Value, DateTime.Today) < 0)
                        MessageBox.Show("[Ngày bắt đầu] phải lớn hơn hoặc bằng ngày hiện tại !", "Thông báo");
                    else
                    if (rdoStaying.Checked == false && rdoResident.Checked == false)
                        MessageBox.Show("Vui lòng chọn [Trạng thái] !", "Thông báo");
                    else
                    if (CheckHost(cbApart.Text, cbBuilding.Text) == false && rdoYes.Checked == false && rdoNo.Checked == false)
                        MessageBox.Show("Vui lòng chọn [Chủ hộ] !", "Thông báo");
                    else
                    if (txtRelation.Text.Trim() == "")
                        MessageBox.Show("Vui lòng nhập [Mối quan hệ với chủ hộ] !", "Thông báo");
                    else
                    {
                        string dateStart = GetStartDateHost(cbApart.Text.Trim(), cbBuilding.Text.Trim());
                        string dateEnd = GetEndDateHost(cbApart.Text.Trim(), cbBuilding.Text.Trim());
                        if (DateTime.Compare(DateTime.Parse(dateStart), dtimeStart.Value) > 0)
                            MessageBox.Show("[Ngày bắt đầu] phải lớn hơn hoặc bằng [Ngày bắt đầu] của chủ hộ: " + DateTime.Parse(dateStart).ToShortDateString() + " !", "Thông báo");
                        else
                        if (DateTime.Compare(DateTime.Parse(dateEnd), dtimeEnd.Value) < 0 && DateTime.Compare(DateTime.Parse(dateEnd), DateTime.Parse("1/1/1900")) != 0 && GetHostCus(cbApart.Text.Trim(), cbBuilding.Text.Trim()) != txtID.Text)
                            MessageBox.Show("[Ngày kết thúc] phải nhỏ hơn hoặc bằng [Ngày kết thúc] của chủ hộ: " + DateTime.Parse(dateEnd).ToShortDateString() + " !", "Thông báo");
                        else
                        if (DateTime.Compare(DateTime.Parse(dateEnd), DateTime.Parse("1/1/1900")) != 0 && dtimeEnd.Text == "" && rdoYes.Checked == false)
                            MessageBox.Show("Phải nhập [Ngày kết thúc] do chủ hộ đã có [Ngày kết thúc]: " + DateTime.Parse(dateEnd).ToShortDateString() + " !", "Thông báo");
                        else
                        if (checkEndDate(dtimeEnd.Text, cbApart.Text.Trim(), cbBuilding.Text.Trim()) == true && GetHostCus(cbApart.Text.Trim(), cbBuilding.Text.Trim()) == txtID.Text)
                            MessageBox.Show("[Ngày kết thúc] của chủ hộ không được nhỏ hơn [gày kết thúc] của người ở chung ! Vui lòng thay đổi [Ngày kết thúc] của những người ở chung trước !", "Thông báo");
                        else
                        {
                            if (_mesFunc == 4)
                            {
                                if (CheckIDNumber(txtIDNumber.Text.Trim(), txtID.Text.Trim()) == true && txtIDNumber.Text.Trim() != "")
                                    MessageBox.Show("Số CMND trùng lặp !", "Thông báo");
                                else
                                if (CheckID(txtID.Text.Trim()) == true)
                                    MessageBox.Show("Mã trùng lặp !", "Thông báo");
                                else
                                {
                                    sql = string.Format("execute InsertCustomer '{0}',N'{1}','{2}','{3}',N'{4}','{5}','{6}','{7}','{8}'", txtID.Text.Trim(), txtName.Text.Trim(), CheckDate(dtimeDoB.Text), CheckChoose(rdoMale.Checked, rdiFemale.Checked), txtTown.Text.Trim(), txtPhone.Text.Trim(), txtEmail.Text.Trim(), txtIDNumber.Text.Trim(), DateTime.Today);
                                    db.Execute(sql);
                                    ktthem = 1;
                                }
                            }

                            if (ktthem == 1 || _mesFunc == 2)
                            {
                                string NameProc = "ViewLiving";
                                System.Data.DataTable dt = db.LoadProcedure(NameProc);

                                sql = string.Format("execute InsertLiving '{0}','{1}','{2}','{3}','{4}','{5}',N'{6}'", txtID.Text.Trim(), Change(cbApart.Text.Trim(), cbBuilding.Text.Trim()), dtimeStart.Value, CheckDate(dtimeEnd.Text), CheckChoose(rdoResident.Checked, rdoStaying.Checked), CheckChoose(rdoYes.Checked, rdoNo.Checked), txtRelation.Text.Trim());
                                db.Execute(sql);

                                NameProc = "ViewApart";
                                dt = db.LoadProcedure(NameProc);
                                float Acreage;
                                string Des;
                                int FloorID;
                                int ApartID;
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i][0].ToString() == Change(cbApart.Text.Trim(), cbBuilding.Text.Trim()).ToString())
                                    {
                                        ApartID = int.Parse(dt.Rows[i][0].ToString());
                                        Acreage = float.Parse(dt.Rows[i][3].ToString());
                                        Des = dt.Rows[i][4].ToString();
                                        FloorID = int.Parse(dt.Rows[i][5].ToString());

                                        sql = string.Format("execute UpdateApart '{0}',N'{1}','{2}','{3}',N'{4}','{5}'", ApartID, cbApart.Text.Trim(), 1, Acreage, Des, FloorID);
                                        db.Execute(sql);
                                        break;
                                    }
                                }
                                Close();
                            }

                            
                        }
                    }
                }
                else
                if (_mesFunc == 3)
                {
                    string dateEnd = GetEndDateHost(cbApart.Text.Trim(), cbBuilding.Text.Trim());
                    if (DateTime.Compare(dtimeStart.Value, dtimeEnd.Value) > 0 && dtimeEnd.Text != "")
                        MessageBox.Show("[Ngày bắt đầu] phải nhỏ hơn [Ngày kết thúc] !", "Thông báo");
                    else
                    if (txtRelation.Text.Trim() == "")
                        MessageBox.Show("Vui lòng nhập [Mối quan hệ với chủ hộ] !", "Thông báo");
                    else
                    if (DateTime.Compare(DateTime.Parse(dateEnd), dtimeEnd.Value) < 0 && DateTime.Compare(DateTime.Parse(dateEnd), DateTime.Parse("1/1/1900")) != 0 && GetHostCus(cbApart.Text.Trim(), cbBuilding.Text.Trim()) != txtID.Text)
                        MessageBox.Show("[Ngày kết thúc] phải nhỏ hơn hoặc bằng [Ngày kết thúc] của chủ hộ: " + DateTime.Parse(dateEnd).ToShortDateString() + " !", "Thông báo");
                    else
                    if (DateTime.Compare(DateTime.Parse(dateEnd), DateTime.Parse("1/1/1900")) != 0 && dtimeEnd.Text == "" && rdoYes.Checked == false)
                        MessageBox.Show("Phải nhập [Ngày kết thúc] do chủ hộ đã có [Ngày kết thúc]: " + DateTime.Parse(dateEnd).ToShortDateString() + " !", "Thông báo");
                    else
                    if (checkEndDate(dtimeEnd.Text, cbApart.Text.Trim(), cbBuilding.Text.Trim()) == true && GetHostCus(cbApart.Text.Trim(), cbBuilding.Text.Trim()) == txtID.Text)
                        MessageBox.Show("[Ngày kết thúc] của chủ hộ không được nhỏ hơn [gày kết thúc] của người ở chung ! Vui lòng thay đổi [Ngày kết thúc] của những người ở chung trước !", "Thông báo");
                    else
                    {
                        string sql = string.Format("execute UpdateLiving '{0}','{1}','{2}','{3}','{4}','{5}','{6}',N'{7}'", _mesID, txtID.Text.Trim(), Change(cbApart.Text.Trim(), cbBuilding.Text.Trim()), dtimeStart.Value, CheckDate(dtimeEnd.Text), CheckChoose(rdoResident.Checked, rdoStaying.Checked), CheckChoose(rdoYes.Checked, rdoNo.Checked), txtRelation.Text.Trim());
                        db.Execute(sql);
                        Close();
                    }
                }
                else
                if (_mesFunc == 5)
                {
                    if (CheckIDNumber(txtIDNumber.Text.Trim(), txtID.Text.Trim()) == true && txtIDNumber.Text.Trim() != "")
                        MessageBox.Show("Số CMND trùng lặp !", "Thông báo");
                    else
                    {
                        string sql = string.Format("execute UpdateCustomer '{0}',N'{1}','{2}','{3}',N'{4}','{5}','{6}','{7}','{8}'", txtID.Text.Trim(), txtName.Text.Trim(), CheckDate(dtimeDoB.Text), CheckChoose(rdoMale.Checked, rdiFemale.Checked), txtTown.Text.Trim(), txtPhone.Text.Trim(), txtEmail.Text.Trim(), txtIDNumber.Text.Trim(), DateTime.Today);
                        db.Execute(sql);
                        Close();
                    }
                    
                }
            }
        }

        private void cbApart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CheckHost(cbApart.Text, cbBuilding.Text) == true)
            {
                rdoNo.Checked = true;
                rdoYes.Checked = false;
                rdoNo.Enabled = false;
                rdoYes.Enabled = false;
            }
            else
            {
                rdoNo.Checked = false;
                rdoYes.Checked = false;
                rdoNo.Enabled = true;
                rdoYes.Enabled = true;
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtIDNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void rdoYes_CheckedChanged(object sender, EventArgs e)
        {
            txtRelation.Text = "Chủ hộ";
            txtRelation.Enabled = false;
            lbRela.Text = "";
        }

        private void rdoNo_CheckedChanged(object sender, EventArgs e)
        {
            if (_mesFunc == 2 || _mesFunc == 4)
            {
                if(cbApart.Text.Trim() != "" && rdoNo.Checked == true)
                    if (CheckHost(cbApart.Text.Trim(), cbBuilding.Text.Trim()) == false)
                    {
                        MessageBox.Show("Chưa có chủ hộ trong căn hộ này !", "Thông báo");
                        rdoNo.Checked = false;
                    }
                    else
                    {
                        txtRelation.Enabled = true;
                        txtRelation.Text = "";
                        lbRela.Text = "(*)";
                    }
            }
        }

        //Hàm kiểm tra nhân khẩu đang ở
        bool CheckCustomLive(string CustomerID)
        {
            string NameProc = "ViewLiving";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString() == CustomerID)
                {
                    if (dt.Rows[i][4].ToString() == "" || DateTime.Compare(DateTime.Parse(dt.Rows[i][4].ToString()), DateTime.Parse("1/1/1900")) == 0)
                        return true;
                    else
                    if (DateTime.Compare(DateTime.Parse(dt.Rows[i][4].ToString()), DateTime.Today) >= 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        //Chọn combo mã nhân khẩu
        private void cbID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NameProc = "ViewCustomer";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(CheckCustomLive(cbID.Text.Trim()) == true)
                {
                    MessageBox.Show("Nhân khẩu chưa hết thời hạn ở trong căn hộ, không thể thêm vào căn hộ mới !", "Thông báo");
                    txtID.Text = "";
                    txtName.Text = "";
                    dtimeDoB.Text = "";
                    txtEmail.Text = "";
                    rdoMale.Checked = false;
                    rdiFemale.Checked = false;
                    txtPhone.Text = "";
                    txtIDNumber.Text = "";
                    return;
                }
                else
                if (dt.Rows[i][0].ToString() == cbID.Text.Trim())
                {
                    txtID.Text = dt.Rows[i][0].ToString();
                    txtName.Text = dt.Rows[i][1].ToString();
                    dtimeDoB.Text = dt.Rows[i][2].ToString();
                    if (DateTime.Compare(dtimeDoB.Value, DateTime.Parse("1/1/1900")) == 0)
                        dtimeDoB.Text = "";
                    if (dt.Rows[i][3].ToString() == "True")
                        rdoMale.Checked = true;
                    else
                        rdiFemale.Checked = true;
                    txtTown.Text = dt.Rows[i][4].ToString();
                    txtPhone.Text = dt.Rows[i][5].ToString();
                    txtEmail.Text = dt.Rows[i][6].ToString();
                    txtIDNumber.Text = dt.Rows[i][7].ToString();
                }
            }
        }


    }
}