using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace quanLyThuVien
{
    public partial class Dang_Ky : Form
    {
        public Dang_Ky()
        {
            InitializeComponent();
        }

        public bool CheckAccount(string acc)
        {
            return Regex.IsMatch(acc, "^[a-zA-Z0-9]{6,24}$");
        }
        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        Modify modify = new Modify();

        private void btnDangKy_2_Click(object sender, EventArgs e)
        {
            string tk = txtTenDangNhap.Text;
            string mk = txtMatKhau_2.Text;
            string xnmatkhau = txtNhapLaiPass.Text;
            string phone = txtSDT.Text;
            if (!CheckAccount(tk))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập dài 6-24 ký tự, với các ký tự chữ và số, chữ hoa và chữ thường !", "Thông Báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!CheckAccount(mk))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu dài 6-24 ký tự!", "Thông Báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(xnmatkhau != mk)
            {
                MessageBox.Show("Bạn chưa xác nhận mật khẩu chính xác!", "Thông Báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (modify.Taikhoans("select * from sinhVien where sdt = '"+phone+"'").Count!=0)
            {
                MessageBox.Show("Số điện thoại này đã được đăng ký!", "Thông Báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string query = "Insert into sinhVien(TenTaiKhoan,MatKhau,sdt) values ('"+tk+"', '"+mk+"', '"+phone+"')";
                modify.Command(query);
                if(MessageBox.Show("Đăng ký thành công! Bạn có muốn đăng nhập luôn không?", "Thông báo",MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    mdi Mdi = new mdi();
                    this.Close();
                    Mdi.Show();
                }
                else
                {
                    DangNhap frm = new DangNhap();
                    this.Close();
                    frm.Show();
                }
            }
            catch
            {
                MessageBox.Show("Tên đăng nhập đã được đăng ký!");
            }

        }

        private void btnHuy_2_Click(object sender, EventArgs e)
        {
            DangNhap frm = new DangNhap();
            this.Close();
            frm.Show();
        }
    }
}
