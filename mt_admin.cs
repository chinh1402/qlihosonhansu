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
    public partial class mt_admin : Form
    {
        // globals
        string sql;
        string state = null;
        string time1;
        string time2;
        string time3;
        string time4;
        int selectedRowIndex1 = -1; //set = -1 để tránh khi click vào button Xóa đầu
        int selectedRowIndex2 = -1; 
        int selectedRowIndex3 = -1;
        bool coSach = false;
        List<string> idSachdachon = new List<string>();
        DateTime now = DateTime.Now;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable(); // second table to prevent recursion
        DataTable dt3 = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection();

        //table data
        DataTable phieumuonData1;
        DataTable sachData = dgv_data.sachTable();
        DataTable phieuMuonData = dgv_data.phieumuonTable();

        // Macros
        public void runOnLoad()
        {
            idThuThu_fetch();
            dateTimeControl.run();
            comboBox1.SelectedValue = 0;
            comboBox2.SelectedValue = 0;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            dateTimePicker1.Value = dateTimePicker3.Value = now;
            
            dateTimePicker2.Value = dateTimePicker4.Value = now.AddDays(3);


            macros.Render("Select sach.maSach, theLoai.ten AS theloaiTen, tacGia.ten AS tacgiaTen," + "nhaXB.ten AS tenNXB" +
                ", sach.namXuatBan, sach.tenSach, sach.donGia, sach.tonKho " +
                "from sach, tacGia, theLoai, nhaXB where tacGia.id = sach.idTacGia AND theLoai.id = sach.idTheLoai " +
                "AND nhaXB.id = sach.idNXB", dataGridView1);

            dataGridView3.DataSource = phieuMuonData;
            // placeholder
            textBox2.CharacterCasing = CharacterCasing.Upper;
            textBox4.CharacterCasing = CharacterCasing.Upper;

            textBox2.Enabled = false;
            textBox3.Enabled = false;
            comboBox1.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;

            textBox4.Enabled = false;
            textBox5.Enabled = false;
            comboBox2.Enabled = false;
            dateTimePicker3.Enabled = false;
            dateTimePicker4.Enabled = false;

            dataGridView1.MultiSelect = false;
            dataGridView2.MultiSelect = false;
            dataGridView3.MultiSelect = false;
            dataGridView4.MultiSelect = false;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            cmdLuu.Enabled = false;
            cmdKhong.Enabled = false;
        }
        public void idThuThu_fetch()
        {
            sql = "Select id from Admin";
            dt = KetNoi.getData(sql);
            comboBox1.DataSource = comboBox2.DataSource = dt;

            comboBox1.ValueMember = "ID";
            comboBox2.ValueMember = "ID";
        }
        

        public string DatetimeConfig(DateTime dateTimePicker_Value)
        {
            // Chuyển từ dạng datetime sang dạng string kiểu M/DD/YYYY; mục đích để parse về DB
            string timeString = dateTimePicker_Value.Month + "/" + dateTimePicker_Value.Day + "/" + dateTimePicker_Value.Year;
            return timeString;
        }
        private bool formControl_edit()
        {
            // check xem có mã sv nhập vào tồn tại trong DB
            sql = "Select id from sinhVien where id ='" + textBox4.Text +
                "'";
            dt2 = KetNoi.getData(sql);

            if (dt2.Rows.Count == 0)
            {
                MessageBox.Show("Masv phải là mã có trong DB");
                ClearTextBox();
                return false;
            }
            // check xem có id thủ thư tồn tại trong DB
            sql = "Select id from Admin where id = '" + comboBox1.SelectedValue +
                "'";
            dt2 = KetNoi.getData(sql);

            if (dt2.Rows.Count == 0)
            {
                MessageBox.Show("idThuthu phải là mã có trong DB");
                ClearTextBox();
                return false;
            }
            return true;
        }

        private bool formControl_add()
        {
            // check xem có mã sv nhập vào tồn tại trong DB
            sql = "Select id from sinhVien where id ='" + textBox2.Text +
                "'";
            dt2 = KetNoi.getData(sql);

            if (dt2.Rows.Count == 0)
            {
                MessageBox.Show("Idsv phải là ID có trong DB");
                ClearTextBox();
                return false;
            }
            // check xem có id thủ thư tồn tại trong DB
            sql = "Select id from Admin where id = '" + comboBox1.SelectedValue +
                "'";
            dt2 = KetNoi.getData(sql);

            if (dt2.Rows.Count == 0)
            {
                MessageBox.Show("idThuthu phải là mã có trong DB");
                ClearTextBox();
                return false;
            }
            return true;
        }
        
        private void ClearTextBox()
        {
            textBox4.Text = null;
            textBox5.Text = null;
            comboBox2.SelectedValue = 0;
        }
        private void ClearTextBox2()
        {
            textBox2.Text = null;
            textBox3.Text = null;
            comboBox1.SelectedValue = 0;
        }

        private void Khoamo(bool p)
        {
            // khoa mo o bang chi tiet phieu muon
            textBox4.Enabled = p;
            textBox5.Enabled = p;
            comboBox2.Enabled = p;
            dateTimePicker3.Enabled = p;
            dateTimePicker4.Enabled = p;

            cmdSua.Enabled = !p;
            cmdXoa.Enabled = !p;

            cmdLuu.Enabled = p;
            cmdKhong.Enabled = p;
        }

        //end macros

        public mt_admin()
        {
            InitializeComponent();
            
        }
        private void mt_admin_Load(object sender, EventArgs e)
        {
            // làm 1 hàm run on load
            runOnLoad();
        }

        private void mt_admin_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRowIndex3 = e.RowIndex;
            if (state == "Sua")
            {
                textBox4.Text = phieuMuonData.Rows[selectedRowIndex3][1].ToString();
                textBox5.Text = phieuMuonData.Rows[selectedRowIndex3][6].ToString();
                comboBox2.SelectedValue = phieuMuonData.Rows[selectedRowIndex3][2].ToString();
                dateTimePicker3.Value = Convert.ToDateTime(phieuMuonData.Rows[selectedRowIndex3][3].ToString());
                dateTimePicker4.Value = Convert.ToDateTime(phieuMuonData.Rows[selectedRowIndex3][4].ToString());
            }

            if (selectedRowIndex3 >=0)
            {
                // Render ra dgv4 để xem phiếu mượn có chứa item gì
                phieumuonData1 = dgv_data.phieumuonTable();
                sql = "Select sachMuon.idSach, sach.tenSach from sach, sachMuon where sachMuon.idSach = sach.maSach AND sachMuon.idPhieuMuon = '"
                + phieumuonData1.Rows[selectedRowIndex3][0].ToString() + "'";
                macros.Render(sql, dataGridView4);
            }
        }
        
        private void cmdSua_Click(object sender, EventArgs e)
        {
            Khoamo(true);
            state = "Sua";
        }

        private void cmdXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa phiếu mượn này? Việc xóa phiếu mượn sẽ ảnh hưởng đến bảng sachMuon","thông báo",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                if (selectedRowIndex3 >= 0)
                {
                    string idPhieuMuonvalue = phieuMuonData.Rows[selectedRowIndex3][0].ToString();
                    if (muontraMethod.checkforHetHan(idPhieuMuonvalue) == false)
                    {
                    // Xóa ở sachMuon trước rồi xóa ở bên phieuMuon (để tránh bug)

                    selectedRowIndex3 = -1; // reset index để prevent bug

                    //update lại tonKho + 1;
                    sql = "Select sach.maSach, sach.tonKho from sachMuon, sach " +
                        "where sachMuon.idSach = sach.maSach AND sachMuon.idPhieuMuon = '" +idPhieuMuonvalue + "'";
                    dt2 = KetNoi.getData(sql);
                    for (int i=0;i<dt2.Rows.Count; i++)
                    {
                        string idSach = dt2.Rows[i][0].ToString();
                        // giả sử trả rồi

                        int tonKho = Convert.ToInt32(dt2.Rows[i][1]) + 1;

                        sql = "Update sach set tonkho = '" +tonKho+ "' Where maSach = '" + idSach + "'";

                        cmd = KetNoi.sqlconfig(cmd, sql);
                        cmd.ExecuteNonQuery();
                    }

                    // Xóa ở sachMuon
                    sql = "Delete from sachMuon where idPhieuMuon = '" + idPhieuMuonvalue +
                        "'";
                    cmd = KetNoi.sqlconfig(cmd, sql);
                    cmd.ExecuteNonQuery();

                    // Xóa ở phieuMuon
                    sql = "Delete from phieuMuon where id = '" + idPhieuMuonvalue +
                        "'";
                    cmd = KetNoi.sqlconfig(cmd, sql);
                    cmd.ExecuteNonQuery();
                    } else { MessageBox.Show("Không thể xóa phiếu mượn đã hết hạn"); return; }
                }

                // Render lại 1 để chuẩn thông tin nếu như quay lại phiếu mượn

                macros.Render("Select sach.maSach, theLoai.ten AS theloaiTen, tacGia.ten AS tacgiaTen," + "nhaXB.ten AS tenNXB" +
                ", sach.namXuatBan, sach.tenSach, sach.donGia, sach.tonKho " +
                "from sach, tacGia, theLoai, nhaXB where tacGia.id = sach.idTacGia AND theLoai.id = sach.idTheLoai " +
                "AND nhaXB.id = sach.idNXB", dataGridView1);

                phieuMuonData = dgv_data.phieumuonTable();
                dataGridView3.DataSource = dgv_data.phieumuonTable();
                MessageBox.Show("Đã xóa item");
            }    
        }

        private void cmdLuu_Click(object sender, EventArgs e)
        {
            Khoamo(false);

            if (state == "Sua")
            {
                state = null;
                if (selectedRowIndex3 >= 0)
                {
                    string idcu = phieuMuonData.Rows[selectedRowIndex3][0].ToString();
                    // có 1 bug nào đó trỏ vào idcu ghi là no row 3 
                    // textBox1.Text = "da sua";
                    sql = "UPDATE phieuMuon SET " +
                        "idSinhVien = '" + textBox4.Text +
                        "', idThuThu = '" + comboBox2.SelectedValue +
                        "', ngayMuon = '" + time3 +
                        "', ngayTra = '" + time4 +
                        "', ghiChu = '" + textBox5.Text +
                        "' Where id = '" + idcu +
                        "'";

                    cmd = KetNoi.sqlconfig(cmd, sql);
                    if (formControl_edit())
                    {
                        cmd.ExecuteNonQuery();
                    }
                    dataGridView3.DataSource = dgv_data.phieumuonTable();
                    ClearTextBox();
                }
                else return;
            }
        }

        private void cmdKhong_Click(object sender, EventArgs e)
        {
            Khoamo(false);
            ClearTextBox();
            state = null;
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker4.Value > dateTimePicker3.Value)
            {
                time4 = DatetimeConfig(dateTimePicker4.Value);
            }
            else { dateTimePicker4.Value = now.AddDays(3); MessageBox.Show("ngày trả không được nhỏ hơn ngày mượn"); }
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker3.Value <= dateTimePicker4.Value)
            {
                time3 = DatetimeConfig(dateTimePicker3.Value);
            }
            else { dateTimePicker3.Value = now; MessageBox.Show("ngày mượn không được lớn hơn ngày trả"); }
           
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value > dateTimePicker1.Value)
            {
                time2 = DatetimeConfig(dateTimePicker2.Value);
            }
            else {  
                dateTimePicker2.Value = now.AddDays(3);
                dateTimePicker1.Value = now;
                MessageBox.Show("ngày trả không được nhỏ hơn ngày mượn"); }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value <= dateTimePicker2.Value)
            {
                time1 = DatetimeConfig(dateTimePicker1.Value);
            }
            else { dateTimePicker1.Value = now;
                dateTimePicker2.Value = now.AddDays(3);
                MessageBox.Show("ngày mượn không được lớn hơn ngày trả"); }
        }

        private void cmdXoaTrang_Click(object sender, EventArgs e)
        {
            ClearTextBox2();
        }

        private void cmdThemphieu_Click(object sender, EventArgs e)
        {
            if (formControl_add())
            {
                if (coSach)
                {
                    if (MessageBox.Show("Bạn có muốn thêm vào database?", "thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // Thêm phiếu mượn (k cho vào method do có textbox với combo box..)
                        sql = "Insert into phieuMuon (idSinhVien,idThuThu,ngayMuon,ngayTra,ghiChu,hetHan)" +
                        " values ('" + textBox2.Text +
                        "', '" + comboBox1.SelectedValue +
                        "', '" + time1 +
                        "', '" + time2 +
                        "', '" + textBox3.Text +
                        "', 'False')";

                        cmd = KetNoi.sqlconfig(cmd, sql);
                        cmd.ExecuteNonQuery();

                        // Thêm sách vào sachMuon

                        // b1: update daMuon = true (lấy idSach từ list)
                        // b2: lấy idphieumuon từ phieumuon (id vừa thêm vào)
                        // b3: lấy idSach từ list
                        // b4: insert lần lượt từng sách

                        muontraMethod.updateTonKho(idSachdachon);

                        string idMuon = muontraMethod.layIdPhieuMuon();

                        muontraMethod.updateSachMuon(idSachdachon, idMuon);

                        ClearTextBox2();

                        // clear list sách để render ra view
                        idSachdachon.Clear();

                        sachData = muontraMethod.updatedSachData(idSachdachon);
                        dt3 = muontraMethod.updatedMuonData(idSachdachon);
                        
                        dataGridView1.DataSource = sachData;
                        dataGridView2.DataSource = dt3;
                        dataGridView3.DataSource = dgv_data.phieumuonTable();
                        phieuMuonData = dgv_data.phieumuonTable();

                        coSach = false;

                        textBox2.Enabled = false;
                        textBox3.Enabled = false;
                        comboBox1.Enabled = false;
                        dateTimePicker1.Enabled = false;
                        dateTimePicker2.Enabled = false;
                        MessageBox.Show("Thành công thêm vào DB");
                    }
                    else return;
                } else { MessageBox.Show("Vui lòng chọn sách mượn!! Hoặc là do bạn chưa lưu sách mượn"); }
            }
                
        }

        private void cmdThemSach_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex1 >=0)
            {
                if (muontraMethod.checkTonKho(sachData.Rows[selectedRowIndex1][0].ToString()))
                {
                    // Thuc hien muon
                    // remove data from sach
                    idSachdachon.Add(sachData.Rows[selectedRowIndex1][0].ToString());

                    selectedRowIndex1 = -1;
                    if (idSachdachon.Count>0)
                    {
                        sachData = muontraMethod.updatedSachData(idSachdachon);
                        dt3 = muontraMethod.updatedMuonData(idSachdachon);
                    
                        dataGridView1.DataSource = sachData;
                        dataGridView2.DataSource = dt3;
                    } else { MessageBox.Show("Chon sach di!!!"); }
                } else { MessageBox.Show("vui long chon quyen khac"); return; }
            }
        }
        private void cmdLuuSachMuon_Click(object sender, EventArgs e)
        {
            if (idSachdachon.Count > 0)
            {
                if (MessageBox.Show("bạn có chắc bạn muốn lưu thông tin?","thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // phải coSach thì mới tạo được phiếu mượn
                    coSach = true;

                    textBox2.Enabled = true;
                    textBox3.Enabled = true;

                    comboBox1.Enabled = true;
                    dateTimePicker1.Enabled = true;
                    dateTimePicker2.Enabled = true;
                    // Thực ra là luôn lưu rồi 
                    MessageBox.Show("Thông tin đã được lưu");
                } else { return; }
            } else { MessageBox.Show("vui long chon sach"); return; }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRowIndex1 = e.RowIndex;
        }

        private void cmdXoaSach_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex2 >= 0)
            {
                idSachdachon.Remove(dt3.Rows[selectedRowIndex2][0].ToString());
                selectedRowIndex2 = -1;
                if (idSachdachon.Count >= 0)
                {
                    sachData = muontraMethod.updatedSachData(idSachdachon);
                    dt3 = muontraMethod.updatedMuonData(idSachdachon);

                    dataGridView1.DataSource = sachData;
                    dataGridView2.DataSource = dt3;
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRowIndex2 = e.RowIndex;
        }

        private void cmdManual_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Chọn những quyển sách khách muốn mượn; \n" +
                "2. Bấm lưu để thêm vào giỏ;\n" +
                "3. Điền những thông tin cần thiết để lập phiếu mượn cho khách\n" +
                "4. Nếu phải sửa/xóa phiếu mượn, hãy thao tác bên tab chi tiết phiếu mượn", 
                "Hướng dẫn sử dụng bảng");
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {

            e.SuppressKeyPress = true;
        }

        private void dateTimePicker3_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void dateTimePicker4_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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
