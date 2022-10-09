using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace muontra_newdesign
{
    public partial class pp_admin : Form
    {
        // global:
        int selectedRowIndex = -1;
        string state;
        SqlCommand cmd = new SqlCommand();
        DataTable phieuPhatData = dgv_data.phieuPhatTable();
        DataTable phieuPhatData_ThuThu = new DataTable();
        public pp_admin()
        {
            InitializeComponent();
        }
        // macros:
        private bool formControl_add()
        {
            // check xem có mã sv nhập vào tồn tại trong DB
            string sql = "Select id from sinhVien where id ='" + textBox1.Text +
                "'";
            DataTable dt = KetNoi.getData(sql);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Masv phải là mã có trong DB");
                return false;
            }

            sql = "Select id from Admin where id = '" + comboBox1.SelectedValue +
                "'";
            dt = KetNoi.getData(sql);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("idThuthu phải là mã có trong DB");
                return false;
            }

            return true;
        }
        private void Khoamo(bool g)
        {
            cmdThem.Enabled = g;
            cmdSua.Enabled = g;
            cmdXoa.Enabled = g;
            cmdGhi.Enabled = !g;
            cmdKhong.Enabled = !g;

            textBox1.Enabled = !g;
            textBox2.Enabled = !g;
            textBox3.Enabled = !g;

            comboBox1.Enabled = !g;
        }

        private void clearInputs()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            comboBox1.SelectedValue = 0;
        }
        private void runonLoad()
        {
            Khoamo(true);
            string sql = "Select id from Admin";
            DataTable idThuThu = KetNoi.getData(sql);
            comboBox1.DataSource = idThuThu;
            comboBox1.ValueMember = "ID";
            comboBox1.SelectedValue = 0;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            textBox1.CharacterCasing = CharacterCasing.Upper;

            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            phieuPhatData_ThuThu = dgv_data.phieuPhatTableThuThu();

            dataGridView1.DataSource = dgv_data.phieuPhatTableThuThu();
            dateTimeControl.run();
            muontraMethod.checkForPhieuPhat();
        }

        // end macros

        private void pp_admin_Load(object sender, EventArgs e)
        {
            runonLoad();
        }

        private void pp_admin_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void cmdThem_Click(object sender, EventArgs e)
        {
            Khoamo(false);
            state = "Them";
        }

        private void cmdSua_Click(object sender, EventArgs e)
        {
            Khoamo(false);
            state = "Sua";
        }

        private void cmdXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa phiếu phạt này?", "thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (selectedRowIndex >= 0)
                {
                    if (radioButton1.Checked == true)
                    {
                        string idPhieuPhatvalue = phieuPhatData_ThuThu.Rows[selectedRowIndex][0].ToString();
                        selectedRowIndex = -1; // reset index để prevent bug

                        string sql = "Delete from phieuPhat where id = '" + idPhieuPhatvalue +
                            "'";
                        cmd = KetNoi.sqlconfig(cmd, sql);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thành công xóa phiếu phạt có id:" + idPhieuPhatvalue);
                        // Messagebox vẫn hiện trước
                    }
                    else if (radioButton2.Checked == true)
                    {
                        string idPhieuPhatvalue = phieuPhatData.Rows[selectedRowIndex][0].ToString();
                        selectedRowIndex = -1; // reset index để prevent bug

                        // co id phieu muon
                        string sql = "select idPhieuMuon from phieuPhat where id='" +idPhieuPhatvalue + "'";
                        DataTable dt = KetNoi.getData(sql);
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            MessageBox.Show("khong the xoa phieu phat do qua han sach","thong bao"); return;
                        } 

                        sql = "Delete from phieuPhat where id = '" + idPhieuPhatvalue +
                            "'";
                        cmd = KetNoi.sqlconfig(cmd, sql);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thành công xóa phiếu phạt có id:" + idPhieuPhatvalue);

                    }
                }

                if (radioButton1.Checked == true)
                {
                    phieuPhatData_ThuThu = dgv_data.phieuPhatTableThuThu();
                    dataGridView1.DataSource = phieuPhatData_ThuThu;
                }

                else if (radioButton2.Checked == true)
                {
                    phieuPhatData = dgv_data.phieuPhatTable();
                    dataGridView1.DataSource = phieuPhatData;
                }
            }
        }

        private void cmdGhi_Click(object sender, EventArgs e)
        {
            Khoamo(true);
            if (state == "Them")
            {
                state = null;
                string sql = "Insert into phieuPhat (idSinhVien, idThuThu, idPhieuMuon, tienPhat, lyDo) " +
                    "values ('" + textBox1.Text +
                    "', '" + comboBox1.SelectedValue + "'," +
                    "'0'," + // giá trị 0 cho phiếu phạt của thủ thư
                    "'" + textBox2.Text +
                    "','" + textBox3.Text +
                    "')";

                cmd = KetNoi.sqlconfig(cmd, sql);
                if (formControl_add())
                {
                    cmd.ExecuteNonQuery();
                }
                clearInputs();

                if (radioButton1.Checked == true)
                {
                    phieuPhatData_ThuThu = dgv_data.phieuPhatTableThuThu();
                    dataGridView1.DataSource = phieuPhatData_ThuThu;
                }

                else if (radioButton2.Checked == true)
                {
                    phieuPhatData = dgv_data.phieuPhatTable();
                    dataGridView1.DataSource = phieuPhatData;
                }
            }
            else if (state == "Sua")
            {
                if (selectedRowIndex >=0)
                {
                    state = null;
                    string idcu;
                    if (radioButton1.Checked == true)
                    {
                        idcu = phieuPhatData_ThuThu.Rows[selectedRowIndex][0].ToString();
                    }
                    else if (radioButton2.Checked == true)
                    {
                        idcu = phieuPhatData.Rows[selectedRowIndex][0].ToString();
                    } else { idcu = null; };
                        string sql = "UPDATE phieuPhat SET " +
                                " idSinhVien = '" + textBox1.Text +
                                "', idThuThu = '" + comboBox1.SelectedValue +
                                "', tienPhat = '" + textBox2.Text +
                                "', lyDo = '" + textBox3.Text +
                                "' Where id = '" + idcu +
                                "'";

                    cmd = KetNoi.sqlconfig(cmd, sql);
                    if (formControl_add())
                    {
                        cmd.ExecuteNonQuery();
                    }
                    clearInputs();

                    if (radioButton1.Checked == true)
                    {
                        phieuPhatData_ThuThu = dgv_data.phieuPhatTableThuThu();
                        dataGridView1.DataSource = phieuPhatData_ThuThu;
                    }

                    else if (radioButton2.Checked == true)
                    {
                        phieuPhatData = dgv_data.phieuPhatTable();
                        dataGridView1.DataSource = phieuPhatData;
                    }
                }
            }
        }

        private void cmdKhong_Click(object sender, EventArgs e)
        {
            Khoamo(true);
            state = null;
            clearInputs();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRowIndex = e.RowIndex;
            if (state == "Sua")
            {
                if (radioButton1.Checked == true)
                {
                    textBox1.Text = phieuPhatData_ThuThu.Rows[selectedRowIndex][1].ToString();
                    textBox2.Text = phieuPhatData_ThuThu.Rows[selectedRowIndex][4].ToString();
                    textBox3.Text = phieuPhatData_ThuThu.Rows[selectedRowIndex][5].ToString();
                    // comboBox1.SelectedValue = phieuPhatData_ThuThu.Rows[selectedRowIndex][2].ToString();
                }
                else if (radioButton2.Checked == true)
                {
                    textBox1.Text = phieuPhatData.Rows[selectedRowIndex][1].ToString();
                    textBox2.Text = phieuPhatData.Rows[selectedRowIndex][4].ToString();
                    textBox3.Text = phieuPhatData.Rows[selectedRowIndex][5].ToString();
                    // comboBox1.SelectedValue = phieuPhatData.Rows[selectedRowIndex][2].ToString();
                }

            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                phieuPhatData_ThuThu = dgv_data.phieuPhatTableThuThu();
                dataGridView1.DataSource = phieuPhatData_ThuThu;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                phieuPhatData = dgv_data.phieuPhatTable();
                dataGridView1.DataSource = phieuPhatData;
            }
        }

        private void cmdRef_Click(object sender, EventArgs e)
        {
            dateTimeControl.run();
            muontraMethod.checkForPhieuPhat();

            if (radioButton2.Checked == true)
            {
                phieuPhatData = dgv_data.phieuPhatTable();
                dataGridView1.DataSource = phieuPhatData;
            }
            MessageBox.Show("Phiếu phạt vừa được cập nhật!");
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <='9')
            {
                
            } else if (e.KeyChar == ((char)Keys.Back) || e.KeyChar == ((char)Keys.Delete))
            {

            }
            
            else { e.Handled = true; }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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
