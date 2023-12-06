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
using muontra_newdesign;

namespace QLTV
{
    public partial class QLSach : Form
    {

        SqlConnection connection;
        SqlCommand command;

        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        public static void GanNguonCombo
            (ComboBox cbo, string tenbang, string truonghienthi, string truongma)
        {
            string sql = "Select " + truongma + "," + truonghienthi + " from " + tenbang + " ";
            cbo.DataSource = KetNoi.getData(sql);
            cbo.DisplayMember = truonghienthi;
            cbo.ValueMember = truongma;
            cbo.SelectedValue = 0;
        }
        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "select sach.maSach, sach.tenSach, tacGia.ten AS tenTacGia, nhaXB.ten AS tenNXB, theLoai.ten AS tenTheLoai," +
                "sach.namXuatBan, sach.donGia, sach.slnhap, sach.tonKho, sach.anh " +
                "from sach, theLoai, tacGia, nhaXB where sach.idTheLoai = theLoai.id AND sach.idTacGia = tacGia.id AND sach.idNXB = nhaXB.id";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgv.DataSource = table;
        }
        
        

        public QLSach()
        {
            InitializeComponent();
        }

        private void QLSach_Load(object sender, EventArgs e)
        {
            connection = Connection.GetSqlConnection();
            connection.Open();
            loaddata();
            txtMaSach.Enabled = false;

            GanNguonCombo(comboBoxTacGia, "tacGia", "ten", "id");
            GanNguonCombo(comboBoxTheLoai, "theLoai", "ten", "id");
            GanNguonCombo(comboBoxNhaXB, "nhaXB", "ten", "id");

        }

        private void QLSach_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult rt = MessageBox.Show("Bạn có muốn thoát không ?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if(rt == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }


        }

        private void MenuTG_Click(object sender, EventArgs e)
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
            this.Close();
            QLSach form = new QLSach();
            form.Show();
        }
     
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            /*
            command = connection.CreateCommand();
            command.CommandText = "insert into sach values('" + txtMaSach.Text + "', '" + txtTenSach.Text + "', '" + txtTacGia.Text + "', '" + txtNXB.Text + "', '" + txtNam.Text + "', '" + txtCDe.Text + "', '" + txtSL.Text + "', '" + txtDG.Text + "', '" + txtTT.Text + "', '" + txtGC + "')";
            command.ExecuteNonQuery();
            loaddata();
            */

            
            if (txtTenSach.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSach.Focus();
                return;
            }

            //if (txtTacGia.Text == "")
            //{
            //    MessageBox.Show("Bạn chưa nhập tên tác giả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtTacGia.Focus();
            //    return;
            //}

            //if (txtNXB.Text == "")
            //{
            //    MessageBox.Show("Bạn chưa nhập mã nhà xuất bản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtNXB.Focus();
            //    return;
            //}

            if (txtNam.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập năm xuất bản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNam.Focus();
                return;
            }

            //if (txtCDe.Text == "")
            //{
            //    MessageBox.Show("Bạn chưa nhập chủ đề", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtCDe.Focus();
            //    return;
            //}

            if (txtSL.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSL.Focus();
                return;
            }

            if (txtDG.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập đơn giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDG.Focus();
                return;
            }
            if (MessageBox.Show("Bạn có muốn thêm sách mới không ?", "Thông Báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
            command = connection.CreateCommand();
            command.CommandText = "insert into sach (tenSach, idTacGia, idNXB, namXuatBan, idTheLoai, slnhap, donGia, tonKho  ,anh)" +
                    " values('" + txtTenSach.Text + "', '" + comboBoxTacGia.SelectedValue + "', '" + comboBoxNhaXB.SelectedValue + "', '" + txtNam.Text + "', '" + comboBoxTheLoai.SelectedValue + "', '" + txtSL.Text + "', '" + txtDG.Text + "', '" + txtTT.Text + "', '" + txtGC.Text + "')";
            command.ExecuteNonQuery();
            loaddata();
                
            }
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            /*
            command = connection.CreateCommand();
            command.CommandText = "delete from sach where MaSach = '" + txtMaSach.Text + "'";
            command.ExecuteNonQuery();
            loaddata();
            */

            
            if (MessageBox.Show("Bạn có muốn xóa không ?", "Thông Báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
            command = connection.CreateCommand();
                string maSach = txtMaSach.Text;
            command.CommandText = "delete from sach where maSach = '" + maSach + "'";
            // dùng id ở sach nếu tìm thấy trong sachMuon thì k đc xóa
            if (!editing.checkSachMuon(maSach))
            command.ExecuteNonQuery();
            else { MessageBox.Show("không thể xóa"); }
            loaddata();    
            }
            


        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = e.RowIndex;
            if (i>=0)
            {
                txtMaSach.Text = dgv.Rows[i].Cells[0].Value.ToString();
                txtTenSach.Text = dgv.Rows[i].Cells[1].Value.ToString();
                //txtTacGia.Text = dgv.Rows[i].Cells[2].Value.ToString();
                //txtNXB.Text = dgv.Rows[i].Cells[3].Value.ToString();
                //txtCDe.Text = dgv.Rows[i].Cells[4].Value.ToString();

                //comboBoxTacGia.SelectedValue = dgv.Rows[i].Cells[2].Value.ToString();
                //comboBoxNhaXB.SelectedValue = dgv.Rows[i].Cells[3].Value.ToString();
                //comboBoxTheLoai.SelectedValue = dgv.Rows[i].Cells[4].Value.ToString();
                txtNam.Text = dgv.Rows[i].Cells[5].Value.ToString();
                txtDG.Text = dgv.Rows[i].Cells[6].Value.ToString();
                txtSL.Text = dgv.Rows[i].Cells[7].Value.ToString();
                txtTT.Text = dgv.Rows[i].Cells[8].Value.ToString();
                txtGC.Text = dgv.Rows[i].Cells[9].Value.ToString();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Bạn có muốn sửa không ?", "Thông Báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
            command = connection.CreateCommand();
            command.CommandText = "update sach set tenSach = '" + txtTenSach.Text + "',idTacGia= '" + comboBoxTacGia.SelectedValue + "',idnxb= '" + comboBoxNhaXB.SelectedValue + "',namXuatBan= '" + txtNam.Text + "',idTheLoai= '" + comboBoxTheLoai.SelectedValue + "',slnhap= '" + txtSL.Text + "',donGia= '" + txtDG.Text + "',tonKho= '" + txtTT.Text + "',anh= '" + txtGC.Text + "' where maSach = '" + txtMaSach.Text + "'";
            command.ExecuteNonQuery();
            loaddata();    
            }
        }

        private void txtTrangChu_Click(object sender, EventArgs e)
        {
            TrangChu trangchu = new TrangChu();
            trangchu.MdiParent = mdi.ActiveForm;
            this.Close();
            trangchu.Show();
        }

        private void txtNam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {

            }
            else if (e.KeyChar == ((char)Keys.Back) || e.KeyChar == ((char)Keys.Delete))
            {

            }

            else { e.Handled = true; }
        }

        private void txtSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {

            }
            else if (e.KeyChar == ((char)Keys.Back) || e.KeyChar == ((char)Keys.Delete))
            {

            }

            else { e.Handled = true; }
        }

        private void txtTT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {

            }
            else if (e.KeyChar == ((char)Keys.Back) || e.KeyChar == ((char)Keys.Delete))
            {

            }

            else { e.Handled = true; }
        }

        private void txtDG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {

            }
            else if (e.KeyChar == ((char)Keys.Back) || e.KeyChar == ((char)Keys.Delete))
            {

            }

            else { e.Handled = true; }
        }
    }
}
