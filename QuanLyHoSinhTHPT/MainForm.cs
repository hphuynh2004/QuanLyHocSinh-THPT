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

namespace QuanLyHoSinhTHPT
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        // Trong MainForm.cs
        private DBConnect db = new DBConnect();

        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    

        private void lsvBill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void flpTable_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimkiem.Text.Trim();
            try
            {
                string query = "SELECT * FROM DM_GIAOVIEN WHERE MaGV LIKE '%"+keyword+"%'";

                db.OpenConnection();
                DataTable dt =  db.ExecuteQuery(query);

                lsvBill.Items.Clear();
                lsvBill.View = View.Details;
                lsvBill.Columns.Add("Mã GV", 100);
                lsvBill.Columns.Add("Họ Tên", 150);
                lsvBill.Columns.Add("Ngày Sinh", 100);
                lsvBill.Columns.Add("SDT", 100);
                lsvBill.Columns.Add("Chức Vụ", 100);
                lsvBill.Columns.Add("Tổ Bộ Môn", 150);
                foreach (DataRow row in dt.Rows)
                {
                    ListViewItem item = new ListViewItem(row["MaGV"].ToString());
                    item.SubItems.Add(row["HoTen"].ToString());
                    item.SubItems.Add(row["NgaySinh"].ToString());
                    item.SubItems.Add(row["SDT"].ToString());
                    item.SubItems.Add(row["ChucVu"].ToString());
                    item.SubItems.Add(row["ToBoMon"].ToString());
                    lsvBill.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
    }
}
