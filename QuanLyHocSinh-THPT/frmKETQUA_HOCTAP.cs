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
    public partial class frmKETQUA_HOCTAP : Form


    {
        SqlConnection conn = new SqlConnection(
           @"Data Source=DESKTOP-JH4OR7J\SQLEXPRESS;Initial Catalog=QuanLyHocSinhTHPT;Integrated Security=True");
        SqlDataAdapter da;
        DataTable dt;
        public frmKETQUA_HOCTAP()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                string sql = "SELECT * FROM KETQUA";
                da = new SqlDataAdapter(sql, conn);
                dt = new DataTable();
                da.Fill(dt);

                gridKETQUA_HOCTAP.DataSource = dt;
                gridKETQUA_HOCTAP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridKETQUA_HOCTAP.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                gridKETQUA_HOCTAP.ReadOnly = true;

              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmKETQUA_HOCTAP_Load(object sender, EventArgs e)
        {
          
            LoadKetQuaTongHop();
        }

        private void LoadKetQuaTongHop()
        {
            try
            {
                string sql = @"
    SELECT 
        HS.MaHS,
        HS.HoTen AS TenHS,
        HS.Lop,
        L.GVCN,
        ROUND(AVG((D.Diem15P + D.Diem1T * 2 + D.DiemCK * 3) / 6.0), 2) AS DiemTB,
        CASE 
            WHEN ROUND(AVG((D.Diem15P + D.Diem1T * 2 + D.DiemCK * 3) / 6.0), 2) >= 8 THEN N'Giỏi'
            WHEN ROUND(AVG((D.Diem15P + D.Diem1T * 2 + D.DiemCK * 3) / 6.0), 2) >= 6.5 THEN N'Khá'
            WHEN ROUND(AVG((D.Diem15P + D.Diem1T * 2 + D.DiemCK * 3) / 6.0), 2) >= 5 THEN N'Trung bình'
            ELSE N'Yếu'
        END AS XepLoai
    FROM HOCSINH HS
    JOIN DIEM D ON HS.MaHS = D.MaHS
    LEFT JOIN LOP L ON HS.Lop = L.TenLOP
    GROUP BY HS.MaHS, HS.HoTen, HS.Lop, L.GVCN";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow r in dt.Rows)
                {
                    Console.WriteLine($"HS: {r["TenHS"]} | Lớp: {r["Lop"]} | GVCN: {r["GVCN"]}");
                }

                gridKETQUA_HOCTAP.DataSource = dt;
                gridKETQUA_HOCTAP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải kết quả học tập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            foreach (DataGridViewRow row in gridKETQUA_HOCTAP.Rows)
            {
                if (row.Cells["DiemTB"].Value != null)
                {
                    double diemTB = Convert.ToDouble(row.Cells["DiemTB"].Value);
                    string xepLoai;

                    if (diemTB >= 8) xepLoai = "Giỏi";
                    else if (diemTB >= 6.5) xepLoai = "Khá";
                    else if (diemTB >= 5) xepLoai = "Trung bình";
                    else xepLoai = "Yếu";

                    row.Cells["XepLoai"].Value = xepLoai;
                }
            }
        }



        

        private void btnThem_Click(object sender, EventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {

        }
    }
}
