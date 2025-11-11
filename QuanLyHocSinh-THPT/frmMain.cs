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

namespace QuanLyHocSinh_THPT
{
    public partial class frmMain: Form
    {
        public frmMain()
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


        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void lsvBill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void flpTable_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnTimkiem_Click_1(object sender, EventArgs e)
        {
            string keyword = txtTimkiem.Text.Trim();
            try
            {
                string query = "SELECT * FROM DM_GIAOVIEN WHERE MaGV LIKE '%" + keyword + "%'";

                db.OpenConnection();
                DataTable dt = db.ExecuteQuery(query);

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
                //db.CloseConnection();
            }
        }

        private void quảnLýHọcSinhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHOCSINH frm = new frmHOCSINH();
            frm.Show();
        }

        private void quảnLýToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmLOP frm = new frmLOP();
            frm.Show();
        }

        private void quảnLýMônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMONHOC frm = new frmMONHOC();
            frm.Show();
        }

        private void quảnLýTổBộMônToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKHOI frm = new frmKHOI();
            frm.Show();
        }

        private void quảnLýĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDIEM frm = new frmDIEM();
            frm.Show();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void quảnLýKếtQuảHọcTậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKETQUA_HOCTAP frm = new frmKETQUA_HOCTAP();
            frm.Show();
        }
    }
}