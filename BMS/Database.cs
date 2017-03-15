using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BMS
{
    class Database
    {
        SqlConnection sqlConn;
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        string tenmay = System.Environment.MachineName; //==============TÊN MÁY KẾT NỐI====================
        public Database()
        {
            string sql = "Data Source=" + tenmay + ";Initial Catalog=BMS;Integrated Security=True"; //chuỗi kết nối 
            //string sql = @"Data Source =" + tenmay + "; AttachDbFilename =|DataDirectory|\\BMS.mdf;Integrated Security=True";
            sqlConn = new SqlConnection(sql);
        }
        public DataTable LoadProcedure(string procName)
        {
            ds = new DataSet();
            da = new SqlDataAdapter(procName, sqlConn);
            da.Fill(ds);
            return ds.Tables[0];
        }
        public bool Execute(string sql)
        {
            
                SqlCommand sqlcmd = new SqlCommand(sql, sqlConn);
                sqlConn.Open();
            try
            {
                sqlcmd.ExecuteNonQuery();
                sqlConn.Close();
                return true;
            }
            catch
            {
                sqlConn.Close();
                return false;
            }
        }

        public DataTable LoadData(string sql)
        {
            ds = new DataSet();
            da = new SqlDataAdapter(sql, sqlConn);
            da.Fill(ds);
            return ds.Tables[0];
        }
    }
}
