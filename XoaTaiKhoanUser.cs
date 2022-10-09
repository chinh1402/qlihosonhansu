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
    public partial class XoaTaiKhoanUser : Form
    {
        public XoaTaiKhoanUser()
        {
            InitializeComponent();
        }

        Modify modify = new Modify();

        private void btnXoaTaiKhoan_Click(object sender, EventArgs e)
        {
            string tenLogin = txtTenDangNhap_4.Text;
            string query_3 = "select * from sinhVien where TenTaiKhoan = '"+tenLogin+"'";
            DialogResult thongbao;
            thongbao = MessageBox.Show("Bạn có chắc chắn muốn xoá tài khoản này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (modify.Taikhoans(query_3).Count!= 0 && (thongbao==DialogResult.Yes))
            {
                string query_4 = "delete from sinhVien where TenTaiKhoan = '"+tenLogin+"'";
                modify.Command(query_4);
                MessageBox.Show("Xoá thành công!", "Thông Báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên tài khoản không tồn tại!", "Thông Báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
