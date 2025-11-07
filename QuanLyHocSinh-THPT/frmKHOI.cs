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
    public partial class frmKHOI : Form
    {
        SqlConnection conn = new SqlConnection(
       @"Data Source=DESKTOP-5INP67J\SQLEXPRESS;Initial Catalog=QuanLyHocSinhTHPT;Integrated Security=True");

        bool isAdding = false;
        public frmKHOI()
        {
            InitializeComponent();
            gridKHOI.AutoGenerateColumns = true;
        }

        private void frmKHOI_Load(object sender, EventArgs e)
        {
            LoadData();

        }
        private void LoadData()
        {
            try
            {
                conn.Open();
                string sql = "SELECT * FROM KHOI";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridKHOI.DataSource = dt;
                gridKHOI.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            txtMaKhoi.Clear();
            txtTenkhoi.Clear();
            txtGhichu.Clear();
            txtMaKhoi.Focus();

            MessageBox.Show("Chế độ thêm mới khối đã bật. Hãy nhập thông tin và nhấn Lưu.",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!isAdding)
            {
                MessageBox.Show("Hãy nhấn Thêm trước khi lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(txtMaKhoi.Text) || string.IsNullOrWhiteSpace(txtTenkhoi.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ Mã khối và Tên khối!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sql = @"INSERT INTO KHOI (MaKhoi, TenKhoi, GhiChu)
                               VALUES (@MaKhoi, @TenKhoi, @GhiChu)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaKhoi", txtMaKhoi.Text);
                cmd.Parameters.AddWithValue("@TenKhoi", txtTenkhoi.Text);
                cmd.Parameters.AddWithValue("@GhiChu", txtGhichu.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Thêm khối mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isAdding = false;
                LoadData();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        private void gridKHOI_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = gridKHOI.Rows[e.RowIndex];

                // Gán dữ liệu từ dòng được chọn vào ô nhập
                txtMaKhoi.Text = row.Cells["MaKhoi"].Value.ToString();
                txtTenkhoi.Text = row.Cells["TenKhoi"].Value.ToString();
                txtGhichu.Text = row.Cells["GhiChu"].Value.ToString();

                // Khóa ô mã khối để tránh đổi mã
                txtMaKhoi.Enabled = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaKhoi.Text))
                {
                    MessageBox.Show("Vui lòng chọn khối cần sửa từ bảng!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sql = @"UPDATE KHOI
                       SET TenKhoi = @TenKhoi,
                           GhiChu = @GhiChu
                       WHERE MaKhoi = @MaKhoi";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaKhoi", txtMaKhoi.Text);
                cmd.Parameters.AddWithValue("@TenKhoi", txtTenkhoi.Text);
                cmd.Parameters.AddWithValue("@GhiChu", txtGhichu.Text);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();

                if (rows > 0)
                {
                    MessageBox.Show("Cập nhật thông tin khối thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    txtMaKhoi.Enabled = true; // mở lại cho lần thêm mới sau
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khối cần sửa!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật khối: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaKhoi.Text))
                {
                    MessageBox.Show("Vui lòng chọn khối cần xóa từ bảng!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirm = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa khối này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm == DialogResult.No)
                    return;

                string sql = "DELETE FROM KHOI WHERE MaKhoi = @MaKhoi";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaKhoi", txtMaKhoi.Text);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();

                if (rows > 0)
                {
                    MessageBox.Show("Xóa khối thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();

                    // Xóa trắng form sau khi xóa
                    txtMaKhoi.Clear();
                    txtTenkhoi.Clear();
                    txtGhichu.Clear();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khối cần xóa!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa khối: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "Bạn có chắc chắn muốn thoát khỏi chương trình không?",
        "Xác nhận thoát",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    );

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}

