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
    public partial class QuenMatKhau : Form
    {
        public QuenMatKhau()
        {
            InitializeComponent();
            label2.Text = "";
        }

        Modify modify = new Modify();

        private void btnLayLaiPass_Click(object sender, EventArgs e)
        {
            string SDT = txtSDT_2.Text;
            if(SDT.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại đã đăng ký!");
            }
            else
            {
                string query = "select * from sinhVien where sdt = '" + SDT + "'";
                if (modify.Taikhoans(query).Count != 0)
                {
                    label2.ForeColor = Color.Green;
                    label2.Text = "Mật Khẩu :" + modify.Taikhoans(query)[0].MatKhau;
                }
                else
                {
                    label2.ForeColor = Color.Red;
                    label2.Text = "Số điện thoại này chưa được đăng ký!";
                }
            }
        }
    }
}
