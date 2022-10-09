using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace muontra_newdesign
{
    public partial class pp_user : Form
    {
        DataTable dt = new DataTable();
        DataTable phieuPhatData = dgv_data.phieuPhatTable();
        SqlCommand cmd = new SqlCommand();
        string mainsql;
        int selectedRowIndex = -1;
        public pp_user()
        {
            InitializeComponent();
        }

        private void pp_user_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pp_user_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            mainsql = "select id, idPhieuMuon, tienPhat, lyDo from phieuPhat where " +
                "maSinhVien = '72DCTT32'"; // Đổi mã sinh viên ở đây
            dt = KetNoi.getData(mainsql);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRowIndex = e.RowIndex;
        }

        private void cmd_nopphat_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex >=0)
            {
                if (MessageBox.Show("Bạn có muốn nộp phạt cho phiếu phạt id: " + dt.Rows[selectedRowIndex][0] + "?"
                    ,"Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Trước khi đấy check xem phieuPhat này là của thuThu hay ko
                    // rồi:
                    string idPhieuMuonvalue = dt.Rows[selectedRowIndex][1].ToString();
                    if (idPhieuMuonvalue == "0")
                    {
                        // là phiếu phạt của ThuThu
                        string idPhieuPhatvalue = dt.Rows[selectedRowIndex][0].ToString();
                        selectedRowIndex = -1; // reset index để prevent bug

                        string sql = "Delete from phieuPhat where id = '" + idPhieuPhatvalue +
                            "'";

                        cmd = KetNoi.sqlconfig(cmd, sql);
                        cmd.ExecuteNonQuery();

                        dt = KetNoi.getData(mainsql);
                        dataGridView1.DataSource = dt;

                        MessageBox.Show("Nộp phạt thành công");
                    }  else
                    {
                        string idPhieuPhatvalue = dt.Rows[selectedRowIndex][0].ToString();
                        // giả sử nộp phạt có trả sách :)
                        // Thực hiện 3 thứ
                        // b1: xóa ở sachMuon (lấy idSach trong đấy từ idphieuMuon rồi trả daMuon = false, rồi xóa)
                        // b2: xóa ở phieuMuon (để tránh recurve về từ phieuPhat) 
                        // b3: xóa phieuPhat (Xóa như bth)
                        muontraMethod.nopphat_xoasachchon(idPhieuMuonvalue);
                        muontraMethod.nopphat_xoaphieumuon(idPhieuMuonvalue);
                        muontraMethod.nopphat_xoaphieuphat(idPhieuPhatvalue);

                        dt = KetNoi.getData(mainsql);
                        dataGridView1.DataSource = dt;

                        MessageBox.Show("Nộp phạt thành công");
                    }
                }
            }
        }
    }
}
