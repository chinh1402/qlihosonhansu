using muontra_newdesign;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fromtacgia
{
    public partial class xemTacGia : Form
    {
        DataTable dt = new DataTable();
        //SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=QLTV_db3;Integrated Security=True");
        public xemTacGia()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string sql = "select * from tacGia";
            dt = KetNoi.getData(sql);
            dataGridView1.DataSource = dt;         
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string k=textBox1.Text;
            string sql = "select * from tacGia where ten like '%"+k+"%' or queQuan like '%"+k+"%' or namSinh like '%" + k+"%'";
            dataGridView1.DataSource = new DataTable();
            dt = KetNoi.getData(sql);
            dataGridView1.DataSource = dt;
        }
    }
}
