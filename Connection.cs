using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace quanLyThuVien
{
    internal class Connection
    {
        private static string stringConnection = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLTV_db3;Integrated Security=True";
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(stringConnection);
        }
    }
}
