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
using System.Drawing.Text;
using quanLyThuVien;

namespace formthongke
{
    public partial class thongkeSach : Form
    {
        private SqlConnection conn = Connection.GetSqlConnection();
        public thongkeSach()
        {
            
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from sach", conn);
            conn.Open();
            da.Fill(dt);
            conn.Close();
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Tồn Kho";
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Tên Sách";
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                chart1.Series["Biểu Đồ Sách"].Points.AddXY(dt.Rows[i]["tenSach"], dt.Rows[i]["tonKho"]);
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            //try
           // {

              
           // }
           // catch (Exception exp)
           // {
               // throw exp;
         //  }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            thongkeTheLoai thongkeTheLoai = new thongkeTheLoai();
            this.Close();
            thongkeTheLoai.MdiParent = mdi.ActiveForm;
            thongkeTheLoai.Show();
        }
    }
    
}
