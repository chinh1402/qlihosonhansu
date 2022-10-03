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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool daMuon = true;
        private bool chuaMuon = true;

        class Sach
        {
            public string id, ten, tacGia, theLoai, namXuatBan, daMuon;
        }

        List<Sach> arr;
        ListViewItem item;
        int i;
        int index = -1;

        public DataTable search(string keyword = "",bool daMuon=false,bool chuaMuon=false)
        {
            DataTable dt = new DataTable();
            SqlConnection conn;
            SqlCommand cmd;
            SqlDataAdapter dr;
            string sql;
            keyword = keyword.Trim();

            string str = @"Data Source=.\SQLEXPRESS;Initial Catalog=quanLyThuVien;Integrated Security=True";

            if (keyword != "")
            {
                if (keyword.All(char.IsDigit) == true)
                {
                    sql = "select s.id,s.ten,tg.ten,tl.ten,s.namXuatBan,s.daMuon from sach as s inner join theLoai as tl on s.idTheLoai=tl.id inner join tacGia as tg on s.idTacGia=tg.id where (s.id=" + keyword + " or s.ten like N'%" + keyword + "%' or tg.ten like N'%" + keyword + "%' or tl.ten like N'%" + keyword + "%' or s.namXuatBan=" + keyword + ")";
                    if (daMuon == true && chuaMuon==false)
                    {
                        sql += " and s.daMuon=1";
                    }
                    else if (chuaMuon == true && daMuon==false)
                    {
                        sql += " and s.daMuon=0";
                    }
                    else if(chuaMuon==false && daMuon == false)
                    {
                        sql += " and s.daMuon=2";
                    }

                }
                else
                {
                    sql = "select s.id,s.ten,tg.ten,tl.ten,s.namXuatBan,s.daMuon from sach as s inner join theLoai as tl on s.idTheLoai=tl.id inner join tacGia as tg on s.idTacGia=tg.id where (s.ten like N'%" + keyword + "%' or tg.ten like N'%" + keyword + "%' or tl.ten like N'%" + keyword + "%' )";
                    if (daMuon == true && chuaMuon == false)
                    {
                        sql += " and s.daMuon=1";
                    }
                    else if (chuaMuon == true && daMuon == false)
                    {
                        sql += " and s.daMuon=0";
                    }
                    else if (chuaMuon == false && daMuon == false)
                    {
                        sql += " and s.daMuon=2";
                    }
                }

            }
            else
            {
                sql = "select s.id,s.ten,tg.ten,tl.ten,s.namXuatBan,s.daMuon from sach as s inner join theLoai as tl on s.idTheLoai=tl.id inner join tacGia as tg on s.idTacGia=tg.id";
                if (daMuon == true && chuaMuon == false)
                {
                    sql += " where s.daMuon=1";
                }
                else if (chuaMuon == true && daMuon == false)
                {
                    sql += " where s.daMuon=0";
                }
                else if (chuaMuon == false && daMuon == false)
                {
                    sql += " where s.daMuon=2";
                }
            }
            

            //select s.id,s.ten,tg.ten,tl.ten,s.namXuatBan,s.daMuon from sach as s inner join theLoai as tl on s.idTheLoai=tl.id inner join tacGia as tg on s.idTacGia=tg.id where s.id=1 or s.ten like '%a%' or tg.ten like '%a%' or tl.ten like '%a%' or s.namXuatBan=2000

            conn = new SqlConnection(str);
            conn.Open();
            cmd = new SqlCommand(sql, conn);
            dr = new SqlDataAdapter(cmd);
            dr.Fill(dt);
            conn.Close();

            return dt;
           

        }

        public void render(DataTable dt)
        {
            arr =new List<Sach>();
            foreach (DataRow dr in dt.Rows)
            {
                Sach sach = new Sach();
                sach.id = dr[0].ToString();
                sach.ten = dr[1].ToString();
                sach.tacGia = dr[2].ToString();
                sach.theLoai = dr[4].ToString();
                sach.namXuatBan = dr[3].ToString();
                sach.daMuon = dr[5].ToString() == "True" ? "Đã bị mượn" : "Có thể mượn";

                arr.Add(sach);
            }

            
            for(i = 0; i < arr.Count; i++)
            {
                item=new ListViewItem(new[] { arr[i].id, arr[i].ten, arr[i].tacGia, arr[i].theLoai, arr[i].namXuatBan, arr[i].daMuon });
                listView1.Items.Add(item);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("ok");
            DataTable dt = search(textBox1.Text, daMuon, chuaMuon);
            listView1.Items.Clear();
            render(dt);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.Items.Count <= 0) return;
            if(listView1.SelectedItems.Count<=0) return;
            item = listView1.SelectedItems[0];
            index = item.Index;
           
            string ten = item.SubItems[1].Text;
            string tacGia = item.SubItems[2].Text;
            string namXuatBan = item.SubItems[3].Text;
            string theLoai = item.SubItems[4].Text;
            string daMuon = item.SubItems[5].Text;
            string anh = @"D:\\Documents\\c_sharp\\bai-tap-tren-lop\\quanLyThuVien\\bin\\images\\image1.png";
            Form2 form2 = new Form2(ten,tacGia,namXuatBan,theLoai,daMuon,anh) ;

            form2.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = search(textBox1.Text, daMuon, chuaMuon);
            listView1.Items.Clear();
            render(dt);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                daMuon = true;
            }
            else
            {
                daMuon = false;
            }

            DataTable dt = search(textBox1.Text, daMuon, chuaMuon);
            listView1.Items.Clear();
            render(dt);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                chuaMuon = true;
            }
            else
            {
                chuaMuon = false;
            }

            DataTable dt = search(textBox1.Text,daMuon,chuaMuon);
            listView1.Items.Clear();
            render(dt);
        }
    }
}
