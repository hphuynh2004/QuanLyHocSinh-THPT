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
    public partial class frmGIAOVIEN : Form
    {
        SqlConnection conn = new SqlConnection(
       @"Data Source=DESKTOP-JH4OR7J\SQLEXPRESS;Initial Catalog=QuanLyHocSinhTHPT;Integrated Security=True");
        public frmGIAOVIEN()
        {
            InitializeComponent();
            gridGIAOVIEN.AutoGenerateColumns = true;
        }

        private void frmGIAOVIEN_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            string query = "SELECT * FROM GIAOVIEN ORDER BY MaGV";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridGIAOVIEN.DataSource = dt;
        }

        //Nút thêm & Lưu    
      
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaGV.Clear();
            txtHotenGV.Clear();
            rbtnNam.Checked = false;
            rbtnNu.Checked = false;
            txtDiachi.Clear();
            txtSDT.Clear();
            txtMonhoc.Clear();
            NgaysinhGV.Value = DateTime.Now;

            txtMaGV.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1️⃣ Kiểm tra trống dữ liệu
            if (string.IsNullOrWhiteSpace(txtMaGV.Text) ||
                string.IsNullOrWhiteSpace(txtHotenGV.Text) ||
                (!rbtnNam.Checked && !rbtnNu.Checked) ||
                string.IsNullOrWhiteSpace(txtDiachi.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text) ||
                string.IsNullOrWhiteSpace(txtMonhoc.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin giáo viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2️⃣ Lấy giá trị từ RadioButton giới tính
            string gioiTinh = rbtnNam.Checked ? "Nam" : "Nữ";

            // 3️⃣ Câu lệnh SQL thêm dữ liệu
            string sql = "INSERT INTO GIAOVIEN (MaGV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, MonDay) " +
                         "VALUES (@MaGV, @HoTen, @NgaySinh, @GioiTinh, @DiaChi, @SoDienThoai, @MonDay)";

            try
            {
                // 4️⃣ Tạo command và gán tham số
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaGV", txtMaGV.Text);
                cmd.Parameters.AddWithValue("@HoTen", txtHotenGV.Text);
                cmd.Parameters.AddWithValue("@NgaySinh", NgaysinhGV.Value);
                cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                cmd.Parameters.AddWithValue("@DiaChi", txtDiachi.Text);
                cmd.Parameters.AddWithValue("@SoDienThoai", txtSDT.Text);
                cmd.Parameters.AddWithValue("@MonDay", txtMonhoc.Text);

                // 5️⃣ Mở kết nối, thực thi, đóng kết nối
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Đã lưu thông tin giáo viên thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 6️⃣ (Tùy chọn) – Gọi lại hàm LoadData() để hiển thị dữ liệu mới lên DataGridView
                LoadData();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        //Nút sửa

        private void gridGIAOVIEN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = gridGIAOVIEN.Rows[e.RowIndex];

                // Gán dữ liệu từ dòng được chọn lên các ô nhập
                txtMaGV.Text = row.Cells["MaGV"].Value.ToString();
                txtHotenGV.Text = row.Cells["HoTen"].Value.ToString();
                NgaysinhGV.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                if (gioiTinh == "Nam")
                {
                    rbtnNam.Checked = true;
                    rbtnNu.Checked = false;
                }
                else
                {
                    rbtnNu.Checked = true;
                    rbtnNam.Checked = false;
                }
                txtDiachi.Text = row.Cells["DiaChi"].Value.ToString();
                txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtMonhoc.Text = row.Cells["MonDay"].Value.ToString();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // 1️⃣ Kiểm tra xem người dùng đã chọn học sinh chưa
                if (string.IsNullOrWhiteSpace(txtMaGV.Text))
                {
                    MessageBox.Show("Vui lòng chọn giáo viên cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2️⃣ Xác nhận trước khi sửa
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn cập nhật thông tin giáo viên này không?",
                    "Xác nhận sửa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.No)
                    return;

                // 3️⃣ Lấy giới tính từ radio button
                string gioiTinh = rbtnNam.Checked ? "Nam" : "Nữ";

                // 4️⃣ Chuẩn bị câu lệnh SQL UPDATE
                string sql = @"UPDATE GIAOVIEN 
                       SET HoTen = @HoTen, 
                           NgaySinh = @NgaySinh, 
                           GioiTinh = @GioiTinh, 
                           DiaChi = @DiaChi, 
                           SoDienThoai = @SoDienThoai, 
                           MonDay = @MonDay 
                       WHERE MaGV = @MaGV";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // 5️⃣ Truyền tham số
                    cmd.Parameters.AddWithValue("@MaGV", txtMaGV.Text);
                    cmd.Parameters.AddWithValue("@HoTen", txtHotenGV.Text);
                    cmd.Parameters.AddWithValue("@NgaySinh", NgaysinhGV.Value);
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiachi.Text);
                    cmd.Parameters.AddWithValue("@SoDienThoai", txtSDT.Text);
                    cmd.Parameters.AddWithValue("@MonDay", txtMonhoc.Text);

                    // 6️⃣ Mở kết nối và thực thi
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    // 7️⃣ Kiểm tra kết quả
                    if (rows > 0)
                    {
                        MessageBox.Show("Cập nhật thông tin giáo viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // 🔄 Làm mới lại DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy giáo viên để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
    "Bạn có chắc chắn muốn thoát không?",
    "Xác nhận thoát",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Question
);

            if (result == DialogResult.Yes)
            {
                this.Close(); // Đóng form hiện tại
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // 1️⃣ Kiểm tra xem đã chọn học sinh cần xóa chưa
                if (txtMaGV.Text == "")
                {
                    MessageBox.Show("Vui lòng chọn giáo viên cần xóa!");
                    return;
                }

                // 2️⃣ Hỏi lại để xác nhận trước khi xóa (tránh bấm nhầm)
                DialogResult dr = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa giáo viên có mã " + txtMaGV.Text + " không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dr == DialogResult.No) return;

                // 3️⃣ Tạo câu lệnh SQL xóa
                string sql = "DELETE FROM GIAOVIEN WHERE MaGV = @MaGV";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaGV", txtMaGV.Text);

                // 4️⃣ Mở kết nối, thực thi lệnh
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();

                // 5️⃣ Thông báo kết quả
                if (rows > 0)
                {
                    MessageBox.Show("Đã xóa học sinh thành công!");

                    // Xóa trắng form nhập
                    txtMaGV.Clear();
                    txtHotenGV.Clear();
                    rbtnNam.Checked = false;
                    rbtnNu.Checked = false;
                    txtDiachi.Clear();
                    txtSDT.Clear();
                    txtMonhoc.Clear();
                    NgaysinhGV.Value = DateTime.Now;

                    // Cập nhật lại dữ liệu trên DataGridView
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy giáo viên có mã này để xóa!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                conn.Close();
            }
        }

        private void gridGIAOVIEN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

                // Tạo câu lệnh SQL: tìm theo Mã HS, Họ tên hoặc Lớp
                string sql = @"SELECT * FROM GIAOVIEN 
                       WHERE MaGV LIKE @TuKhoa 
                          OR HoTen LIKE @TuKhoa 
                          OR SoDienThoai LIKE @TuKhoa
                          OR MonDay LIKE @TuKhoa";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TuKhoa", "%" + tukhoa + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Hiển thị kết quả tìm kiếm lên DataGridView
                    gridGIAOVIEN.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy giáo viên nào phù hợp!", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimkiem.Text))
            {
                LoadData(); // Gọi lại danh sách đầy đủ khi ô tìm kiếm trống
            }
        }
    }
}
