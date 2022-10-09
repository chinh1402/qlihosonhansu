using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace muontra_newdesign
{
    class macros
    {
        public static void Render(string sql, DataGridView gridview)
        {
            DataTable dt2 = new DataTable();
            dt2 = KetNoi.getData(sql);
            gridview.DataSource = dt2;
        }

    }
}
