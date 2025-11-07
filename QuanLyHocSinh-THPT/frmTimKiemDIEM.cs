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
    public partial class frmTimKiemDIEM : Form
    {
        SqlConnection conn = new SqlConnection(
            @"Data Source=DESKTOP-5INP67J\SQLEXPRESS;Initial Catalog=QuanLyHocSinhTHPT;Integrated Security=True");
        public frmTimKiemDIEM()
        {
            InitializeComponent();
        }

        private void lblTimkiem_Click(object sender, EventArgs e)
        {

        }

        private void frmTimKiemDIEM_Load(object sender, EventArgs e)
        {
            gridKetQua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            foreach (DataGridViewColumn col in gridKetQua.Columns)
            {
                Console.WriteLine($"Tên cột: {col.Name} | Tiêu đề hiển thị: {col.HeaderText}");
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            try
            {
                string tukhoa = txtTuKhoa.Text.Trim();

                if (string.IsNullOrEmpty(tukhoa))
                {
                    MessageBox.Show("Vui lòng nhập tên hoặc mã học sinh để tra cứu!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sql = @"
                    SELECT 
                        HS.MaHS,
                        HS.HoTen AS N'Họ tên học sinh',
                        MH.TenMH AS N'Môn học',
                        D.Diem15P AS N'Điểm 15 phút',
                        D.Diem1T AS N'Điểm 1 tiết',
                        D.DiemCK AS N'Điểm cuối kỳ',
                        ROUND(D.Diem15P*0.2 + D.Diem1T*0.3 + D.DiemCK*0.5, 2) AS N'Điểm trung bình'
                    FROM DIEM D
                    INNER JOIN HOCSINH HS ON D.MaHS = HS.MaHS
                    INNER JOIN MONHOC MH ON D.MaMH = MH.MaMH
                    WHERE HS.MaHS LIKE @TuKhoa OR HS.HoTen LIKE @TuKhoa
                    ORDER BY HS.MaHS, MH.TenMH, D.MaHS";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@TuKhoa", "%" + tukhoa + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);

                gridKetQua.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy điểm cho học sinh này.",
                        "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void gridKetQua_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                var currentRow = gridKetQua.Rows[e.RowIndex];
                var previousRow = gridKetQua.Rows[e.RowIndex - 1];

                // Ẩn Mã HS trùng
                if (gridKetQua.Columns[e.ColumnIndex].Name == "MaHS" &&
                    currentRow.Cells["MaHS"].Value?.ToString() == previousRow.Cells["MaHS"].Value?.ToString())
                {
                    e.Value = "";
                }

                // Ẩn Họ tên trùng
                if (gridKetQua.Columns[e.ColumnIndex].Name == "HoTen" &&
                    currentRow.Cells["HoTen"].Value?.ToString() == previousRow.Cells["HoTen"].Value?.ToString())
                {
                    e.Value = "";
                }
            }
        }
    }
    }

