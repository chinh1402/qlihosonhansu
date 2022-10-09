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

namespace formsuaxoa
{
    public partial class suaxoatg : Form
    {
        int selectedRowIndex = -1;
        SqlCommand cmd;
        DataTable dt = new DataTable();
        //SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=""quanLyThuVien2 (2)"";Integrated Security=True");
        public suaxoatg()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string sql = "select * from tacGia";
            dt = KetNoi.getData(sql);
            dataGridView1.DataSource = dt;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
           
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "insert into tacGia (ten ,queQuan, namSinh) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";
           cmd = KetNoi.sqlconfig(cmd, sql);
            cmd.ExecuteNonQuery();
            sql = "select * from tacGia";
            dt = KetNoi.getData(sql);
            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(selectedRowIndex >= 0)
            {
                string id = dt.Rows[selectedRowIndex][0].ToString();
                selectedRowIndex = -1;
                string sql = "delete from tacGia where id ='" + id + "'";
                cmd = KetNoi.sqlconfig(cmd, sql);
                cmd.ExecuteNonQuery();
                sql = "select * from tacGia";
                dt = KetNoi.getData(sql);
                dataGridView1.DataSource = dt;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRowIndex = e.RowIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex >= 0)
                {
                    string id = dt.Rows[selectedRowIndex][0].ToString();
                    selectedRowIndex = -1;
                    string sql = "update tacGia set ten ='" + textBox1.Text + "' ,queQuan ='" + textBox2.Text + "', namSinh ='" + textBox3.Text + "' where id ='" + id + "'  ";
                    cmd = KetNoi.sqlconfig(cmd, sql);
                    cmd.ExecuteNonQuery();
                    sql = "select * from tacGia";
                    dt = KetNoi.getData(sql);
                    dataGridView1.DataSource = dt;
                }
            }
        }
}
