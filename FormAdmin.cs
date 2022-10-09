using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanLyThuVien
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void xoáTàiKhoảnNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XoaTaiKhoanUser frm = new XoaTaiKhoanUser();
            frm.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                this.Hide();
                DangNhap frm = new DangNhap();
                frm.ShowDialog();
            }
        }

        private void trangChínhToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
