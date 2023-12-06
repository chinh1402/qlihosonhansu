using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quanLyThuVien;

namespace muontra_newdesign
{
    class KetNoi
    {
        public static SqlConnection conn;

        public static SqlConnection connectDB()
        {
            string connString = @"Data Source=pc;Initial Catalog=finaldemo_upd1;Integrated Security=True";
            conn = new SqlConnection(connString);
            return conn;
        }

        public static DataTable getData(string sql)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, connectDB());
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static SqlCommand sqlconfig(SqlCommand cmd, string sql)
        {
            conn = Connection.GetSqlConnection();
            conn.Open();
            cmd = new SqlCommand(sql, conn);
            return cmd;
        }
    }
}
