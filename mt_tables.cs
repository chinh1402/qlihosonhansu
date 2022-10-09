using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace muontra_newdesign
{
    internal class dgv_data
    {
        public static DataTable sachTable()
        {
            DataTable dt = new DataTable();
            string sql = "Select * from sach";
            dt = KetNoi.getData(sql);
            return dt;
        }
        public static DataTable phieumuonTable()
        {
            DataTable dt = new DataTable();
            string sql = "Select * from phieuMuon";
            dt = KetNoi.getData(sql);
            return dt;
        }

        public static DataTable phieuPhatTable()
        {
            DataTable dt = new DataTable();
            string sql = "Select * from phieuPhat";
            dt = KetNoi.getData(sql);
            return dt;
        }
        public static DataTable phieuPhatTableThuThu()
        {
            DataTable dt = new DataTable();
            string sql = "Select * from phieuPhat where idPhieuMuon = '0'";
            dt = KetNoi.getData(sql);
            return dt;
        }
    }
}
