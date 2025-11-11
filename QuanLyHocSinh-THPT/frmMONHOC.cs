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
    public partial class frmMONHOC : Form
    {
        SqlConnection conn = new SqlConnection(
      @"Data Source=DESKTOP-JH4OR7J\SQLEXPRESS;Initial Catalog=QuanLyHocSinhTHPT;Integrated Security=True");
        public frmMONHOC()
        {
            InitializeComponent();
            gridMONHOC.AutoGenerateColumns = true;
        }

        private void frmMONHOC_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            string query = "SELECT * FROM MONHOC ORDER BY MaMH";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridMONHOC.DataSource = dt;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string tukhoa = txtTimkiem.Text.Trim();

                if (string.IsNullOrEmpty(tukhoa))
                {
                    MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string sql = @"
            SELECT 
                M.MaMH, 
                M.TenMH, 
                M.SoTiet, 
                M.GhiChu, 
                ISNULL(G.HoTen, N'Không có giáo viên dạy môn này') AS GiaoVienPhuTrach
            FROM MONHOC M
            LEFT JOIN GIAOVIEN G ON M.TenMH = G.MonDay
            WHERE M.TenMH LIKE @TuKhoa OR M.MaMH LIKE @TuKhoa";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TuKhoa", "%" + tukhoa + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gridMONHOC.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy môn học phù hợp!", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            
                // Xóa trắng các ô nhập để chuẩn bị thêm mới
                txtMaMH.Clear();
                txttenMH.Clear();
                numSotiet.Value = 0;
                txtGhichu.Clear();

                // Đặt con trỏ vào ô mã môn học
                txtMaMH.Focus();
            
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // 1️⃣ Kiểm tra dữ liệu nhập vào
                if (string.IsNullOrWhiteSpace(txtMaMH.Text) ||
                    string.IsNullOrWhiteSpace(txttenMH.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ Mã môn học và Tên môn học!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2️⃣ Kiểm tra trùng mã môn học
                string checkSql = "SELECT COUNT(*) FROM MONHOC WHERE MaMH = @MaMH";
                using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@MaMH", txtMaMH.Text);
                    conn.Open();
                    int exists = (int)checkCmd.ExecuteScalar();
                    conn.Close();

                    if (exists > 0)
                    {
                        MessageBox.Show("Mã môn học này đã tồn tại!", "Cảnh báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // 3️⃣ Câu lệnh thêm dữ liệu
                string insertSql = @"INSERT INTO MONHOC (MaMH, TenMH, SoTiet, GhiChu)
                             VALUES (@MaMH, @TenMH, @SoTiet, @GhiChu)";
                using (SqlCommand cmd = new SqlCommand(insertSql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaMH", txtMaMH.Text);
                    cmd.Parameters.AddWithValue("@TenMH", txttenMH.Text);
                    cmd.Parameters.AddWithValue("@SoTiet", numSotiet.Value);
                    cmd.Parameters.AddWithValue("@GhiChu", txtGhichu.Text);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("Thêm môn học mới thành công!",
                                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Làm mới DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm môn học!", "Lỗi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm môn học: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        //Cellclick + sửa
       
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaMH.Text))
                {
                    MessageBox.Show("Vui lòng chọn môn học cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc muốn cập nhật thông tin môn học này không?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;

                string sql = @"UPDATE MONHOC
                       SET TenMH = @TenMH,
                           SoTiet = @SoTiet,
                           GhiChu = @GhiChu
                       WHERE MaMH = @MaMH";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaMH", txtMaMH.Text);
                    cmd.Parameters.AddWithValue("@TenMH", txttenMH.Text);
                    cmd.Parameters.AddWithValue("@SoTiet", (int)numSotiet.Value);
                    cmd.Parameters.AddWithValue("@GhiChu", txtGhichu.Text);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Làm mới grid view
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy môn học để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        private void gridMONHOC_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = gridMONHOC.Rows[e.RowIndex];
                txtMaMH.Text = row.Cells["MaMH"].Value.ToString();
                txttenMH.Text = row.Cells["TenMH"].Value.ToString();
                numSotiet.Value = Convert.ToInt32(row.Cells["SoTiet"].Value);
                txtGhichu.Text = row.Cells["GhiChu"].Value.ToString();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaMH.Text))
                {
                    MessageBox.Show("Vui lòng chọn môn học cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa môn học này không?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;

                string sql = "DELETE FROM MONHOC WHERE MaMH = @MaMH";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaMH", txtMaMH.Text);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy môn học để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?",
        "Thoát chương trình", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close(); // Đóng form hiện tại
            }
        }

        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimkiem.Text))
            {
                LoadData(); // Gọi lại danh sách đầy đủ khi ô tìm kiếm trống
            }
        }

        private void lblMAMH_Click(object sender, EventArgs e)
        {

        }
    }

}
