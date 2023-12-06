using quanLyThuVien;
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

namespace formthongke
{
    public partial class thongkeTheLoai : Form
    {
        private SqlConnection conn = Connection.GetSqlConnection();
        public thongkeTheLoai()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from theLoai", conn);
            conn.Open();
            da.Fill(dt);
            conn.Close();

            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Số Lượng";
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Tên thể loại";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                chart1.Series["Thể Loại"].Points.AddXY(dt.Rows[i]["ten"], dt.Rows[i]["soluong"]);
            }
        }
    }
}
