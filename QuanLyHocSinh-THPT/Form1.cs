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
    public partial class frmHOCSINH : Form
    {
        SqlConnection conn = new SqlConnection(
        @"Data Source=DESKTOP-5INP67J\SQLEXPRESS;Initial Catalog=QuanLyHocSinhTHPT;Integrated Security=True");
        public frmHOCSINH()
        {
            InitializeComponent();
            gridHOCSINH.AutoGenerateColumns = true;
        }

        private void frmHOCSINH_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void lblPHUTHAI_Click(object sender, EventArgs e)
        {

        }

        private void lblLOP_Click(object sender, EventArgs e)
        {

        }

        private void gridHOCSINH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        void LoadData()
        {
            string query = "SELECT * FROM HOCSINH ORDER BY MaHS";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridHOCSINH.DataSource = dt;
        }
     

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            txtMaHS.Clear();
            txtHotenHS.Clear();
            rbtnNam.Checked = false;
            rbtnNu.Checked = false;
            txtDiachi.Clear();
            txtLOP.Clear();
            txtNamhoc.Clear();
            NgaysinhHS.Value = DateTime.Now;

            txtMaHS.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1️⃣ Kiểm tra trống dữ liệu
            if (string.IsNullOrWhiteSpace(txtMaHS.Text) ||
                string.IsNullOrWhiteSpace(txtHotenHS.Text) ||
                (!rbtnNam.Checked && !rbtnNu.Checked) ||
                string.IsNullOrWhiteSpace(txtDiachi.Text) ||
                string.IsNullOrWhiteSpace(txtLOP.Text) ||
                string.IsNullOrWhiteSpace(txtNamhoc.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin học sinh!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2️⃣ Lấy giá trị từ RadioButton giới tính
            string gioiTinh = rbtnNam.Checked ? "Nam" : "Nữ";

            // 3️⃣ Câu lệnh SQL thêm dữ liệu
            string sql = "INSERT INTO HOCSINH (MaHS, HoTen, NgaySinh, GioiTinh, DiaChi, Lop, NamHoc) " +
                         "VALUES (@MaHS, @HoTen, @NgaySinh, @GioiTinh, @DiaChi, @Lop, @NamHoc)";

            try
            {
                // 4️⃣ Tạo command và gán tham số
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaHS", txtMaHS.Text);
                cmd.Parameters.AddWithValue("@HoTen", txtHotenHS.Text);
                cmd.Parameters.AddWithValue("@NgaySinh", NgaysinhHS.Value);
                cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                cmd.Parameters.AddWithValue("@DiaChi", txtDiachi.Text);
                cmd.Parameters.AddWithValue("@Lop", txtLOP.Text);
                cmd.Parameters.AddWithValue("@NamHoc", txtNamhoc.Text);

                // 5️⃣ Mở kết nối, thực thi, đóng kết nối
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Đã lưu thông tin học sinh thành công!", "Thông báo",
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // 1️⃣ Kiểm tra xem người dùng đã chọn học sinh chưa
                if (string.IsNullOrWhiteSpace(txtMaHS.Text))
                {
                    MessageBox.Show("Vui lòng chọn học sinh cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2️⃣ Xác nhận trước khi sửa
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn cập nhật thông tin học sinh này không?",
                    "Xác nhận sửa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.No)
                    return;

                // 3️⃣ Lấy giới tính từ radio button
                string gioiTinh = rbtnNam.Checked ? "Nam" : "Nữ";

                // 4️⃣ Chuẩn bị câu lệnh SQL UPDATE
                string sql = @"UPDATE HOCSINH 
                       SET HoTen = @HoTen, 
                           NgaySinh = @NgaySinh, 
                           GioiTinh = @GioiTinh, 
                           DiaChi = @DiaChi, 
                           Lop = @Lop, 
                           NamHoc = @NamHoc 
                       WHERE MaHS = @MaHS";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // 5️⃣ Truyền tham số
                    cmd.Parameters.AddWithValue("@MaHS", txtMaHS.Text);
                    cmd.Parameters.AddWithValue("@HoTen", txtHotenHS.Text);
                    cmd.Parameters.AddWithValue("@NgaySinh", NgaysinhHS.Value);
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiachi.Text);
                    cmd.Parameters.AddWithValue("@Lop", txtLOP.Text);
                    cmd.Parameters.AddWithValue("@NamHoc", txtNamhoc.Text);

                    // 6️⃣ Mở kết nối và thực thi
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    // 7️⃣ Kiểm tra kết quả
                    if (rows > 0)
                    {
                        MessageBox.Show("Cập nhật thông tin học sinh thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // 🔄 Làm mới lại DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy học sinh để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // 1️⃣ Kiểm tra xem đã chọn học sinh cần xóa chưa
                if (txtMaHS.Text == "")
                {
                    MessageBox.Show("Vui lòng chọn học sinh cần xóa!");
                    return;
                }

                // 2️⃣ Hỏi lại để xác nhận trước khi xóa (tránh bấm nhầm)
                DialogResult dr = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa học sinh có mã " + txtMaHS.Text + " không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dr == DialogResult.No) return;

                // 3️⃣ Tạo câu lệnh SQL xóa
                string sql = "DELETE FROM HOCSINH WHERE MaHS = @MaHS";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaHS", txtMaHS.Text);

                // 4️⃣ Mở kết nối, thực thi lệnh
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();

                // 5️⃣ Thông báo kết quả
                if (rows > 0)
                {
                    MessageBox.Show("Đã xóa học sinh thành công!");

                    // Xóa trắng form nhập
                    txtMaHS.Clear();
                    txtHotenHS.Clear();
                    rbtnNam.Checked = false;
                    rbtnNu.Checked = false;
                    txtDiachi.Clear();
                    txtLOP.Clear();
                    txtNamhoc.Clear();
                    NgaysinhHS.Value = DateTime.Now;

                    // Cập nhật lại dữ liệu trên DataGridView
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy học sinh có mã này để xóa!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                conn.Close();
            }
        }

        private void gridHOCSINH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = gridHOCSINH.Rows[e.RowIndex];

                // Gán dữ liệu từ dòng được chọn lên các ô nhập
                txtMaHS.Text = row.Cells["MaHS"].Value.ToString();
                txtHotenHS.Text = row.Cells["HoTen"].Value.ToString();
                NgaysinhHS.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
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
                txtLOP.Text = row.Cells["Lop"].Value.ToString();
                txtNamhoc.Text = row.Cells["NamHoc"].Value.ToString();
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

        private void btnTimkiem_Click(object sender, EventArgs e)
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
                string sql = @"SELECT * FROM HOCSINH 
                       WHERE MaHS LIKE @TuKhoa 
                          OR HoTen LIKE @TuKhoa 
                          OR Lop LIKE @TuKhoa";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TuKhoa", "%" + tukhoa + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Hiển thị kết quả tìm kiếm lên DataGridView
                    gridHOCSINH.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy học sinh nào phù hợp!", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
