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
using muontra_newdesign;

namespace quanLyThuVien
{
    public partial class QLNXB : Form
    {
        public QLNXB()
        {
            InitializeComponent();
        }

        class NhaXb {
            public string id,ten, diachi, website;
        }


        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt;
        int i, index = -1;
        ListViewItem item;
        string ac;

        public bool kiemtrarong()
        {
            if (textBox4.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "")
            {
                return true;
            }
            else return false;
        }

        public bool kiemtratrungten()
        {
            string qr = "select * from nhaXB where ten='" + textBox2.Text.Trim() + "'";
            SqlConnection con = KetNoi.connectDB();
            SqlCommand cmd= new SqlCommand(qr, con);
            con.Open();
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            DataTable d = new DataTable();
            adt.Fill(d);
            con.Close();
            if(d.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        

        private DataTable getData()
        {
            DataTable dt2 = new DataTable();
            string sql = "select * from nhaXB";
            SqlConnection con = KetNoi.connectDB();
            cmd=new SqlCommand(sql,con);
            adt = new SqlDataAdapter(cmd);
            adt.Fill(dt2);

            return dt2;
        }

        private DataTable getDatasearch(string t)
        {
            DataTable dt2 = new DataTable();
            string sql = "select * from nhaXB where ten like '%"+t+"%' or diachi like '%"+t+"%' or website like '%"+t+"%'";
            SqlConnection con = KetNoi.connectDB();
            cmd = new SqlCommand(sql, con);
            adt = new SqlDataAdapter(cmd);
            adt.Fill(dt2);

            return dt2;
        }

        public void render(DataTable dt)
        {
            // xoa het item trong listview1
            if (listView1.Items.Count > 0)
            {
                listView1.Items.Clear();
            }
            //tao danh sach nxb moi 
            List<NhaXb> arr = new List<NhaXb>();
            foreach (DataRow dr in dt.Rows)
            {
                NhaXb nhaxb = new NhaXb();
                nhaxb.id = dr[0].ToString();
                nhaxb.ten = dr[1].ToString();
                nhaxb.diachi = dr[2].ToString();
                nhaxb.website = dr[3].ToString();
                arr.Add(nhaxb);
            }

            //cho danh sach nxb moi vao listview1
            for (i = 0; i < arr.Count; i++)
            {
                ListViewItem item = new ListViewItem(new[] {arr[i].id, arr[i].ten, arr[i].diachi, arr[i].website });
                listView1.Items.Add(item);
            }

            label7.Text = "Bảng này có "+arr.Count + " dòng";
        }

        public void getDataSach(string id)
        {
            string sql = "select tenSach as 'Tên sách' from sach where idNXB=" + id;
            DataTable dt = new DataTable();
            SqlConnection con = KetNoi.connectDB();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql,con);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            adt.Fill(dt);
            dataGridView1.DataSource = dt;
            label8.Text = "Bảng này có "+ dt.Rows.Count +" dòng";
        }
        public void xoaTrang()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        public void khoa(bool b)
        {
            button1.Enabled = b;
            button2.Enabled = b;
            button3.Enabled = b;
            button5.Enabled = !b;
            button6.Enabled = !b;

            textBox1.Enabled = b;

            listView1.Enabled = b;
            textBox2.Enabled = !b;
            textBox3.Enabled = !b;
            textBox4.Enabled = !b;

            if (b == false)
            {
                if (ac == "them")
                {
                    listView1.HideSelection = true;
                    textBox5.Text = "";
                    dataGridView1.DataSource = new DataTable();
                    label8.Text = "Bảng này có 0 dòng";
                }
                else
                {
                    listView1.HideSelection = false;
                }
            }
            else
            {
                listView1.HideSelection = false;
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.Items.Count <= 0) return;
            if (listView1.SelectedItems.Count <= 0) return;
            item = listView1.SelectedItems[0];

            textBox2.Text = item.SubItems[1].Text;
            textBox3.Text = item.SubItems[2].Text;
            textBox4.Text = item.SubItems[3].Text;
            textBox5.Text = item.Text;

            label5.Text="Sách thuộc NXB "+ item.SubItems[1].Text;
            getDataSach(item.Text);

            button2.Enabled =true ;
            button3.Enabled = true;
        }


        private void handle()
        {
            string ten = textBox2.Text;
            string diachi = textBox3.Text;
            string ws = textBox4.Text;
            string query = "";
            string idHandle = textBox5.Text;
            if (ac == "them")
            {
                query = "insert into nhaXB(ten,diachi,website) values(N'"+ten+"',N'"+diachi+"','"+ws+"')";
            }
            else if (ac == "sua")
            {
                query = "update nhaXB set ten=N'"+ten+"', diachi=N'"+diachi+"',website='"+ws+"' where id="+idHandle;

            }
            else if (ac == "xoa")
            {
                query = "delete from nhaXB where id="+idHandle;
            }
            
            SqlConnection con = KetNoi.connectDB();
            cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
                
        }
        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            xoaTrang();
            DataTable dt3 = new DataTable();
            if(textBox1.Text.Trim() != "")
            {
                dt3 = getDatasearch(textBox1.Text);
                button4.Enabled = true;
            }
            else
            {
                dt3 = getData();
                button4.Enabled = false;
            }
            render(dt3);
            dataGridView1.DataSource = new DataTable();
            
        }



        private void button1_Click(object sender, EventArgs e)
        {
            xoaTrang();
            ac = "them";
            khoa(false);
            listView1.HideSelection = true;
            textBox1.Text = "";
            textBox2.Focus();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ac = "sua";
            khoa(false);
            listView1.HideSelection = false;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            
            if (dataGridView1.Rows.Count > 1)
            {
                if(MessageBox.Show("Không thể xóa NXB này","Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    khoa(true);
                }
            }
            else
            {
                if (MessageBox.Show("Xóa NXB "+textBox2.Text, "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ac = "xoa";
                    handle();
                    dt = new DataTable();
                    dt = getData();
                    
                    render(dt);
                    xoaTrang();
                    textBox1.Text = "";
                    dataGridView1.DataSource = new DataTable();

                }

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {


        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        public void handleInBtn5()
        {
            khoa(true);
            handle();
            dt = new DataTable();
            dt = getData();
            render(dt);
            xoaTrang();
            button2.Enabled = false;
            button3.Enabled = false;
            textBox1.Text = "";
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (ac == "them")
            {
                if (kiemtrarong() == true)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (kiemtratrungten() == true)
                {
                    DialogResult dl=MessageBox.Show("NXB "+textBox2.Text+" đã tồn tại trong hệ thống, vẫn thêm ?", "Nhắc nhở", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dl == DialogResult.Yes)
                    {
                        handleInBtn5();
                    }
                }else
                {
                    handleInBtn5();
                }
            }else if (ac == "sua")
            {
                if (kiemtrarong() == true)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (kiemtratrungten() == true)
                {
                    DialogResult dl = MessageBox.Show("NXB " + textBox2.Text + " đã tồn tại trong hệ thống, vẫn sửa ?", "Nhắc nhở", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dl == DialogResult.Yes)
                    {
                        handleInBtn5();
                    }
                }
                else
                {
                    handleInBtn5();
                }
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            khoa(true);
            xoaTrang();
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "";

            DataTable dt4 = new DataTable();
            dt4 = getData();
            render(dt4);
            dataGridView1.DataSource = new DataTable();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            dt = new DataTable();
            dt = getData();
            render(dt);
            khoa(true);
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
        }
    }
}
