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
    public partial class ChiTietSach : Form
    {
        public ChiTietSach()
        {
            InitializeComponent();
        }

        private string ten, tacGia, namXuatBan, theLoai, tonKho,anh,nhaXB;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public ChiTietSach(string Ten,string TacGia,string NamXuatBan,string TheLoai,string NhaXB,string TonKho,string Anh): this(){
            ten= Ten;
            tacGia= TacGia;
            namXuatBan= NamXuatBan;
            theLoai= TheLoai;
            tonKho= TonKho;
            nhaXB= NhaXB;
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
            textBox5.Text = nhaXB;
            textBox6.Text = tonKho;

            pictureBox1.Image = Image.FromFile(anh);
        }
    }
}
