using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace muontra_newdesign
{
    public partial class mt_user : Form
    {
        public mt_user()
        {
            InitializeComponent();
        }

        private void mt_user_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void mt_user_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            DataTable dt = new DataTable();
            string sql = "select sachMuon.idPhieuMuon, sachMuon.idSach, sach.ten from sachMuon, phieuMuon, sach " +
                "where phieuMuon.maSinhVien = " +
                "'72DCTT32'" + // Đổi mã sv ở đây
                " AND sachMuon.idPhieuMuon=phieuMuon.id AND sachMuon.idSach=sach.id";
            dt = KetNoi.getData(sql);
            dataGridView1.DataSource = dt;
        }
    }
}
