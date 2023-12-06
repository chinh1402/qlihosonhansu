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
using quanLyThuVien;

namespace QLTV
{
    public partial class QLTL : Form
    {
        SqlConnection connection;
        SqlCommand command;

        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from theLoai";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgv.DataSource = table;
        }
        public QLTL()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MenuTacGia_Click(object sender, EventArgs e)
        {
        }

        private void MenuTheLoai_Click(object sender, EventArgs e)
        {
            this.Hide();
            QLTL form = new QLTL();
            form.Show();
        }

        private void MenuSach_Click(object sender, EventArgs e)
        {
            this.Hide();
            QLSach form = new QLSach();
            form.Show();
        }

        private void QLTL_Load(object sender, EventArgs e)
        {
            connection = Connection.GetSqlConnection();
            connection.Open();
            loaddata();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(txtTheLoai.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập thể loại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTheLoai.Focus();
                return;
            }

            if (MessageBox.Show("Bạn có muốn thêm thể loại mới không ?", "Thông Báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                command = connection.CreateCommand();
                command.CommandText = "insert into theLoai (ten) values ('" + txtTheLoai.Text + "')";
                command.ExecuteNonQuery();
                loaddata();
            }
                
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgv.CurrentRow.Index;
            txtID.Text = dgv.Rows[i].Cells[0].Value.ToString();
            txtTheLoai.Text = dgv.Rows[i].Cells[1].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn sửa không ?", "Thông Báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                command = connection.CreateCommand();
                command.CommandText = "update theLoai set ten = '" + txtTheLoai.Text + "' where id = '" + txtID.Text + "'";
                command.ExecuteNonQuery();
                loaddata();
            }
                
        }

        public bool ktXoa(string id)
        {
            string sql = "select * from sach where idTheLoai=" + id;
            SqlConnection conn= Connection.GetSqlConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(ktXoa(txtID.Text))
            {
                //cho phep xoa
                string sql = "delete from theLoai where id=" + txtID.Text;
                SqlConnection con = Connection.GetSqlConnection();
                SqlCommand cmd=new SqlCommand(sql, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                connection = Connection.GetSqlConnection();
                connection.Open();
                loaddata();
            }
            else
            {
                //khong xoa duoc
                MessageBox.Show("Không thể xóa hàng này", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }    
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            TrangChu trangchu = new TrangChu();
            trangchu.MdiParent = mdi.ActiveForm;
            this.Close();
            trangchu.Show();
        }

        private void QLTL_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult rt = MessageBox.Show("Bạn có muốn thoát không ?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (rt == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
