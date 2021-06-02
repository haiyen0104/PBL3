using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopManagement.DAL
{
    class DBHelper
    {
        private static DBHelper _Instance;
        private SqlConnection cnn;
        private DBHelper (string s)
        {
            cnn = new SqlConnection(s);
        }
        public static DBHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    string cnnstr = @"Data Source=DESKTOP-OARJDVA\SQLEXPRESS;Initial Catalog=PBL3;User ID=sa;Password=#Hthy01042001";
                    _Instance = new DBHelper(cnnstr);
                }
                return _Instance;
            }
            private set { }
        }
        public DataTable GetRecord(string sql)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(sql, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            cnn.Open();
            da.Fill(dt);
            cnn.Close();
            return dt;
        }
        public int GetRecord1(string s)
        {
            SqlCommand cmd = new SqlCommand(s, cnn);
            cnn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            int i = Convert.ToInt16(dr[0].ToString());
            cnn.Close();
            return i;

        }
        public void ExcuteDB(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
    }
}
