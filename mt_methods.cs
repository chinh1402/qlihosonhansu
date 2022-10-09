using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace muontra_newdesign
{
    class muontraMethod
    {
        
        public static bool checkTonKho(string id)
        {
            // tra ve true neu con hang, false neu tonkho = 0

            DataTable table = new DataTable();
            string sql = "Select tonKho from sach where maSach = '" + id + "' and " +
                "tonKho > 0";
            table = KetNoi.getData(sql);
            if (table.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        
        public static DataTable updatedSachData(List<string> idlist)
        {
            DataTable dt = new DataTable();
            if (idlist.Count > 0)
                {
                // list có item
                string sql = "Select sach.maSach, theLoai.ten AS theloaiTen, tacGia.ten AS tacgiaTen," + "nhaXB.ten AS tenNXB" +
                ", sach.namXuatBan, sach.tenSach, sach.donGia, sach.tonKho " +
                "from sach, tacGia, theLoai, nhaXB where tacGia.id = sach.idTacGia AND theLoai.id = sach.idTheLoai " +
                "AND nhaXB.id = sach.idNXB ";

                for (int i=0;i<idlist.Count;i++)
                {
                    sql += "AND sach.maSach <>'" + idlist[i] + "' ";
                }
                dt = KetNoi.getData(sql);
            } else {
                // list 0 có item
                string sql = "Select sach.maSach, theLoai.ten AS theloaiTen, tacGia.ten AS tacgiaTen," + "nhaXB.ten AS tenNXB" +
                ", sach.namXuatBan, sach.tenSach, sach.donGia, sach.tonKho " +
                "from sach, tacGia, theLoai, nhaXB where tacGia.id = sach.idTacGia AND theLoai.id = sach.idTheLoai " +
                "AND nhaXB.id = sach.idNXB";
                dt = KetNoi.getData(sql);
            }
            return dt;
        }
        public static DataTable updatedMuonData(List<string> idlist)
        {
            DataTable dt = new DataTable();

            if (idlist.Count > 0)
            {


                string sql = "select maSach,tenSach from sach where maSach='" + idlist[0] +
                "' ";

                for (int i = 1; i < idlist.Count; i++)
                {
                    sql += "OR maSach ='" + idlist[i] + "' ";
                }
                dt = KetNoi.getData(sql);

            } else { string sql = "select maSach,tenSach from sach where maSach='-1'";
                dt = KetNoi.getData(sql);
            }
            return dt;
        }

        public static void updateTonKho(List<string> idlist)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string sql;
            for (int i=0; i<idlist.Count;i++)
            {
                sql = "Select tonKho from sach where maSach = '" + idlist[i] +"'";
                dt = KetNoi.getData(sql);

                // 1 ng mượn sách -1 trong kho
                int tonKho = Convert.ToInt32(dt.Rows[0][0]) -1;

                sql = "update sach set tonKho = '" + tonKho + "' where maSach = '" + idlist[i] + "'";

                cmd = KetNoi.sqlconfig(cmd, sql);
                cmd.ExecuteNonQuery();
            }
        }

        public static string layIdPhieuMuon()
        {
            // lấy id cuối cùng của bảng (id vừa mới query xong)
            string sql = "select id from phieuMuon";
            DataTable dt = new DataTable();
            dt = KetNoi.getData(sql);
            return dt.Rows[dt.Rows.Count - 1][0].ToString();
        }
        public static void updateSachMuon(List<string> idlist, string idMuon)
        {
            string sql;
            SqlCommand cmd = new SqlCommand();

            for (int i=0;i<idlist.Count;i++)
            {
                sql = "insert into sachMuon (idSach, idPhieuMuon) values ('" + idlist[i] +
                    "','" + idMuon +
                    "')";

                cmd = KetNoi.sqlconfig(cmd, sql);
                cmd.ExecuteNonQuery();
            }
        }
        public static void checkForPhieuPhat()
        {
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string sql, masv, idThuThu, idPhieuMuon;
            int tongTien = 0;
            sql = "select id from phieuMuon where hetHan = 'true'";
            dt = KetNoi.getData(sql);

            for (int i=0;i<dt.Rows.Count;i++)
            {
                idPhieuMuon = dt.Rows[i][0].ToString();
                sql = "select id from phieuPhat where idPhieuMuon =" + idPhieuMuon;
                dt2 = KetNoi.getData(sql);
                // nếu đã có phiếu phạt rồi thì không tạo thêm phiếu (tức là row.count > 0);
                // còn nếu chưa có phiếu phạt thì tạo thêm phiếu (row.count = 0)

                if (dt2.Rows.Count ==0)
                {
                    sql = "select idSinhVien, idThuThu from phieuMuon where id = " + idPhieuMuon;
                    dt2 = KetNoi.getData(sql);
                    masv = dt2.Rows[0][0].ToString();
                    idThuThu = dt2.Rows[0][1].ToString();

                    sql = "select sach.donGia from sachMuon,sach where sachMuon.idPhieuMuon = '" + idPhieuMuon +
                        "' AND sach.maSach = sachMuon.idSach";
                    dt3 = KetNoi.getData(sql);

                    for (int j=0;j< dt3.Rows.Count;j++)
                    {
                        int soTien = Convert.ToInt32(dt3.Rows[j][0]);
                        tongTien += soTien;
                    }

                    sql = "insert into phieuPhat (idSinhVien, idThuThu, idPhieuMuon, tienPhat, lyDo) " +
                        "values ('" + masv +
                        "','" + idThuThu +
                        "','" + idPhieuMuon +
                        "','" + tongTien.ToString() +
                        "','Quá hạn trả sách')";

                    cmd = KetNoi.sqlconfig(cmd, sql);
                    cmd.ExecuteNonQuery();
                }
                
            }

        }
        public static void nopphat_xoasachchon(string idPhieuMuon)
        {
            // update daMuon = false;
            SqlCommand cmd = new SqlCommand();
            string sql = "select sach.id from phieuPhat, sachMuon, sach " +
                "where phieuPhat.idPhieuMuon = sachMuon.idPhieuMuon AND sachMuon.idSach = sach.id " +
                "AND phieuPhat.idPhieuMuon = '" + idPhieuMuon +
                "'";
            DataTable dt = KetNoi.getData(sql);
            //dt cầm id của 2 sách r
            
            for (int i=0;i<dt.Rows.Count;i++)
            {
                string idSach = dt.Rows[i][0].ToString();
                sql = "UPDATE sach set daMuon = 'False' " +
                    "Where id = '" + idSach +"'" ;

                cmd = KetNoi.sqlconfig(cmd, sql);
                cmd.ExecuteNonQuery();
            }

            sql = "Delete from sachMuon Where idPhieuMuon = '" + idPhieuMuon + "'";
            cmd = KetNoi.sqlconfig(cmd, sql);
            cmd.ExecuteNonQuery();
        }
        public static void nopphat_xoaphieumuon(string idPhieuMuon)
        {
            string sql = "Delete from phieuMuon Where id = '" + idPhieuMuon + "'";
            SqlCommand cmd = new SqlCommand();
            cmd = KetNoi.sqlconfig(cmd, sql);
            cmd.ExecuteNonQuery();
        }
        public static void nopphat_xoaphieuphat(string idPhieuPhat)
        {
            SqlCommand cmd = new SqlCommand();
            string sql = "Delete from phieuPhat where id = '" + idPhieuPhat +
                           "'";
            cmd = KetNoi.sqlconfig(cmd, sql);
            cmd.ExecuteNonQuery();
        }

        public static bool checkforHetHan(string idPhieuMuon)
        {
            string sql = "select hetHan from phieuMuon where id='" + idPhieuMuon + "'";
            DataTable dt = KetNoi.getData(sql);
            string hetHanstate = dt.Rows[0][0].ToString();
            if (hetHanstate == "True")
            {
                return true;
            } return false;
        }

    }
}
