using muontra_newdesign;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanLyThuVien
{
    internal class editing
    {
        public static bool checkSachMuon(string maSach)
        {
            // nếu có sách mượn thì không được xóa, còn nếu ko có thì đc xóa
            string sql = "select idSach from sachMuon where idSach = '" + maSach + "'";
            DataTable dt = new DataTable();
            dt = KetNoi.getData(sql);
            if (dt.Rows.Count>0)
            {
                return true;
            }
            return false;
        }
    }
}
