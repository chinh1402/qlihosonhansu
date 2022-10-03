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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private string ten, tacGia, namXuatBan, theLoai, daMuon,anh;

        public Form2(string Ten,string TacGia,string NamXuatBan,string TheLoai,string DaMuon,string Anh): this(){
            ten= Ten;
            tacGia= TacGia;
            namXuatBan= NamXuatBan;
            theLoai= TheLoai;
            daMuon = DaMuon;
            anh= Anh;
        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

            textBox1.Text = ten;
            textBox2.Text = tacGia;
            textBox3.Text = namXuatBan;
            textBox4.Text = theLoai;
            textBox5.Text = daMuon;
            pictureBox1.Image = Image.FromFile(anh);
            //pictureBox1.ImageLocation = anh;
        }
    }
}
