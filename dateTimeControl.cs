using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace muontra_newdesign
{
    class dateTimeControl
    {
        //logic: ngày trả lấy từ db; Có thể trừ 2 Datetime sử dụng Timespan :Đ; 
        // trước khi load data thì chạy hàm này để trả về dữ liệu hết hạn hay chưa!
        public static void run()
        {
            DateTime ngayTra;
            TimeSpan dateDiff;
            int due;
            SqlCommand cmd = new SqlCommand();
            DateTime today = DateTime.Now;
            DataTable dt = new DataTable();
            string date = null;

            string sql = "select id, ngayTra, hetHan from phieuMuon where hethan = 'False'";
            dt = KetNoi.getData(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                date = dt.Rows[i][1].ToString();
                if (date != "" || date != null)
                {
                    ngayTra = Convert.ToDateTime(date);
                    // format Bắt buộc phải để là M/DD/YYYY

                    dateDiff = ngayTra - today;
                    due = dateDiff.Days;
                    if (due < 0)
                    {
                        // Debug.WriteLine("het han");
                        sql = "UPDATE phieuMuon SET hetHan = 'True' where " +
                            "id = '" + dt.Rows[i][0] + "'";

                        cmd = KetNoi.sqlconfig(cmd, sql);
                        cmd.ExecuteNonQuery();
                    }
                }
                else { continue; }
            }
        }
    }
}
