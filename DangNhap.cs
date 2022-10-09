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

namespace quanLyThuVien
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        USER user=new USER();

        private bool isSV = true;

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        Modify modify = new Modify();

        private void btnDangNhap_Click_1(object sender, EventArgs e)
        {
            string tentk = txtTaiKhoan.Text;
            string matkhau = txtMatKhau.Text;
            if (tentk.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản!", "Thông Báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (matkhau.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông Báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string query = "select * from sinhVien where TenTaiKhoan = '" + tentk + "' and MatKhau = '" + matkhau + "'";
                string query_2 = "select * from Admin where TenTaiKhoan = '" + tentk + "' and MatKhau = '" + matkhau + "'";
                if (modify.Taikhoans(query).Count != 0 && (ckbSinhVien.CheckState == CheckState.Checked))
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông Báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    user.isSV=true;
                    mdi Mdi = new mdi();
                    this.Hide();
                    Mdi.Show();
                }
                else if ((ckbSinhVien.CheckState == CheckState.Unchecked) && (ckbAdmin.CheckState == CheckState.Unchecked))
                {
                    MessageBox.Show("Vui lòng chọn sinh viên hoặc thủ thư!", "Thông Báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if((ckbSinhVien.CheckState == CheckState.Checked) && (ckbAdmin.CheckState == CheckState.Checked))
                {
                    MessageBox.Show("Bạn chỉ được chọn sinh viên hoặc thủ thư!", "Thông Báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(modify.Taikhoans(query_2).Count != 0 && (ckbAdmin.CheckState == CheckState.Checked))
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông Báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    user.isSV = false;
                    mdi Mdi = new mdi();
                    this.Hide();
                    Mdi.Show();
                }
                else
                {
                    MessageBox.Show("Tên tài khoản hoặc mật khẩu không chính xác!", "Thông Báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnDangKy_Click(object sender, EventArgs e)
        {
                this.Hide();
                Dang_Ky dangky = new Dang_Ky();
                dangky.ShowDialog();   
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QuenMatKhau frm = new QuenMatKhau();
            frm.ShowDialog();
        }
    }
}
