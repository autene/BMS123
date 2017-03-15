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
    public partial class FrmChangeHost : RibbonForm
    {
        Database db = new Database();
        //Lấy mã Detail
        private string _mesID;
        public String MesID
        {
            get { return _mesID; }
            set { _mesID = value; }
        }

        //Lấy mã Tên chủ hộ cũ
        private string _mesName;
        public String MesName
        {
            get { return _mesName; }
            set { _mesName = value; }
        }

        public FrmChangeHost()
        {
            InitializeComponent();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmChangeHost_Load(object sender, EventArgs e)
        {
            txtNameOld.Text = _mesName;
            LoadCombo();
        }

        //Lấy vị trí của ID Detail
        int GetPosID(string ID)
        {
            string NameProc = "ViewLiving";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == ID)
                    return i;
            }
            return -1;
        }

        //Load combobox
        void LoadCombo()
        {
            cbID.Items.Clear();
            string NameProc = "ViewLiving";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                if (dt.Rows[i][2].ToString().ToLower() == dt.Rows[GetPosID(_mesID)][2].ToString().ToLower())
                {
                    if (dt.Rows[i][6].ToString() == "False")
                    {
                        if (dt.Rows[i][4].ToString() == "")
                            cbID.Items.Add(dt.Rows[i][1].ToString());
                        else
                        if ((DateTime.Compare(DateTime.Parse(dt.Rows[i][4].ToString()), DateTime.Parse(dt.Rows[GetPosID(_mesID)][4].ToString())) <= 0) || (DateTime.Compare(DateTime.Parse(dt.Rows[i][4].ToString()), DateTime.Parse("1/1/1900")) == 0))
                            if (DateTime.Compare(DateTime.Parse(dt.Rows[i][4].ToString()), DateTime.Today) >= 0)
                                cbID.Items.Add(dt.Rows[i][1].ToString());

                    }
                }
            }

        }

        //Hàm lấy tên nhân khẩu
        string getNameCus(string ID)
        {
            string NameProc = "ViewCustomer";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == ID)
                    return dt.Rows[i][1].ToString();
            }
            return "";
        }

        private void cbID_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNameNew.Text = getNameCus(cbID.Text);
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            string NameProc = "ViewLiving";
            System.Data.DataTable dt = db.LoadProcedure(NameProc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                if (dt.Rows[i][1].ToString().ToLower() == cbID.Text.Trim().ToLower() && dt.Rows[i][2].ToString().ToLower() == dt.Rows[GetPosID(_mesID)][2].ToString().ToLower())
                {
                    //MessageBox.Show(dt.Rows[GetPosID(_mesID)][2].ToString().ToLower(), "");
                    if (dt.Rows[i][6].ToString() == "False")
                    {
                        if (dt.Rows[i][4].ToString() == "")
                        {
                            int stt = -1;
                            string CusID = dt.Rows[GetPosID(_mesID)][1].ToString();
                            string ApartID = dt.Rows[GetPosID(_mesID)][2].ToString();
                            string StartDate = dt.Rows[GetPosID(_mesID)][3].ToString();
                            string EndtDate = dt.Rows[GetPosID(_mesID)][4].ToString();
                            string Status = dt.Rows[GetPosID(_mesID)][5].ToString();
                            if (Status == "True")
                                stt = 1;
                            else
                                stt = 0;
                            string Relationship = "Chỉnh sửa lại sau";
                            string sql = string.Format("execute UpdateLiving '{0}','{1}','{2}','{3}','{4}','{5}','{6}',N'{7}'", _mesID, CusID, ApartID, DateTime.Parse(StartDate), DateTime.Parse(EndtDate), stt, 0, Relationship);
                            db.Execute(sql);

                            string ID = dt.Rows[i][0].ToString();
                            CusID = dt.Rows[i][1].ToString();
                            ApartID = dt.Rows[i][2].ToString();
                            StartDate = dt.Rows[i][3].ToString();
                            EndtDate = dt.Rows[i][4].ToString();
                            Status = dt.Rows[i][5].ToString();
                            if (Status == "True")
                                stt = 1;
                            else
                                stt = 0;
                            Relationship = "Chủ hộ";
                            sql = string.Format("execute UpdateLiving '{0}','{1}','{2}','{3}','{4}','{5}','{6}',N'{7}'", ID, CusID, ApartID, DateTime.Parse(StartDate), DateTime.Parse(EndtDate), stt, 1, Relationship);
                            db.Execute(sql);

                            Close();
                        }
                        else
                        if ((DateTime.Compare(DateTime.Parse(dt.Rows[i][4].ToString()), DateTime.Parse(dt.Rows[GetPosID(_mesID)][4].ToString())) <= 0) || (DateTime.Compare(DateTime.Parse(dt.Rows[i][4].ToString()), DateTime.Parse("1/1/1900")) == 0))
                            if (DateTime.Compare(DateTime.Parse(dt.Rows[i][4].ToString()), DateTime.Today) >= 0)
                            {
                                int stt = -1;
                                string CusID = dt.Rows[GetPosID(_mesID)][1].ToString();
                                string ApartID = dt.Rows[GetPosID(_mesID)][2].ToString();
                                string StartDate = dt.Rows[GetPosID(_mesID)][3].ToString();
                                string EndtDate = dt.Rows[GetPosID(_mesID)][4].ToString();
                                string Status = dt.Rows[GetPosID(_mesID)][5].ToString();
                                if (Status == "True")
                                    stt = 1;
                                else
                                    stt = 0;
                                string Relationship = "Chỉnh sửa lại sau";
                                string sql = string.Format("execute UpdateLiving '{0}','{1}','{2}','{3}','{4}','{5}','{6}',N'{7}'", _mesID, CusID, ApartID, DateTime.Parse(StartDate), DateTime.Parse(EndtDate), stt, 0, Relationship);
                                db.Execute(sql);

                                string ID = dt.Rows[i][0].ToString();
                                CusID = dt.Rows[i][1].ToString();
                                ApartID = dt.Rows[i][2].ToString();
                                StartDate = dt.Rows[i][3].ToString();
                                EndtDate = dt.Rows[i][4].ToString();
                                Status = dt.Rows[i][5].ToString();
                                if (Status == "True")
                                    stt = 1;
                                else
                                    stt = 0;
                                Relationship = "Chủ hộ";
                                sql = string.Format("execute UpdateLiving '{0}','{1}','{2}','{3}','{4}','{5}','{6}',N'{7}'", ID, CusID, ApartID, DateTime.Parse(StartDate), DateTime.Parse(EndtDate), stt, 1, Relationship);
                                db.Execute(sql);

                                Close();
                            }

                    }
                }
            }

        }
    }
}
