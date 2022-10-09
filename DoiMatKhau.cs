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
    public partial class DoiMatKhau : Form
    {
        public DoiMatKhau()
        {
            InitializeComponent();
        }

        private void DoiMatKhau_Load(object sender, EventArgs e)
        {
            
        }

        public bool CheckNewPass(string newpass)
        {
            return Regex.IsMatch(newpass, "^[a-zA-Z0-9]{6,24}$");
        }
    

        Modify modify = new Modify();

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string tkhoan = txtTenLogin.Text;
            string mk_cu = txtMatKhauCu.Text;
            string mk_moi = txtMatKhauMoi.Text;
            string xn_mk_moi = txtXacNhanPassMoi.Text;
            if (!CheckNewPass(mk_cu))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu dài 6-24 ký tự!", "Thông Báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!CheckNewPass(mk_moi))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu dài 6-24 ký tự!", "Thông Báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (mk_cu == mk_moi)
            {
                MessageBox.Show("Mật khẩu mới và mật khẩu cũ không được trùng nhau!", "Thông Báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (xn_mk_moi != mk_moi)
            {
                MessageBox.Show("Bạn chưa xác nhận mật khẩu chính xác!", "Thông Báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (modify.Taikhoans("select * from sinhVien where TenTaiKhoan = '"+tkhoan+"'").Count<=0)
            {
                MessageBox.Show("Tên đăng nhập này chưa được đăng ký!", "Thông Báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                SqlConnection cn = Connection.GetSqlConnection();
                SqlDataAdapter da = new SqlDataAdapter("Select count (*) from sinhVien where TenTaiKhoan = N'"+tkhoan +"' and MatKhau = N'"+mk_cu+"'",cn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    SqlDataAdapter da1 = new SqlDataAdapter("update sinhVien set MatKhau = N'"+mk_moi+"' where TenTaiKhoan = N'"+tkhoan+"' and MatKhau = N'"+mk_cu+"'",cn);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông Báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
