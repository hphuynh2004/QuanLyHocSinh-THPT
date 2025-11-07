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
    public partial class frmLOP : Form
    {
        SqlConnection conn = new SqlConnection(
       @"Data Source=DESKTOP-5INP67J\SQLEXPRESS;Initial Catalog=QuanLyHocSinhTHPT;Integrated Security=True");

        
        SqlDataAdapter da;
        DataTable dt;
        bool isAdding = false; // trạng thái thêm mới

        public frmLOP()
        {
            InitializeComponent();
            gridLOP.AutoGenerateColumns = true;
        }

        private void frmLOP_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComboboxes();
        }

        private void LoadData()
        {
            string sql = "SELECT * FROM LOP";
            da = new SqlDataAdapter(sql, conn);
            dt = new DataTable();
            da.Fill(dt);
            gridLOP.DataSource = dt;
        }

        private void LoadComboboxes()
        {
            // Combobox Khối
            cbKhoi.Items.Clear();
            cbKhoi.Items.Add("10");
            cbKhoi.Items.Add("11");
            cbKhoi.Items.Add("12");

            // Combobox Năm học
            cbNamhoc.Items.Clear();
            cbNamhoc.Items.Add("2023-2024");
            cbNamhoc.Items.Add("2024-2025");
            cbNamhoc.Items.Add("2025-2026");

            // Combobox Giáo viên chủ nhiệm (lấy từ bảng GIAOVIEN)
            SqlDataAdapter daGV = new SqlDataAdapter("SELECT HoTen FROM GIAOVIEN", conn);
            DataTable dtGV = new DataTable();
            daGV.Fill(dtGV);
            cbGVCN.DataSource = dtGV;
            cbGVCN.DisplayMember = "HoTen";
            cbGVCN.ValueMember = "HoTen";
        }

       

        
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                isAdding = true;

                // Xóa trắng form nhập
                txtMaLOP.Clear();
                txtTenLOP.Clear();
                cbKhoi.SelectedIndex = -1;
                cbGVCN.SelectedIndex = -1;
                cbNamhoc.SelectedIndex = -1;
                numsiso.Value = 0;
                Ngaytao.Value = DateTime.Now;
                rbtnHD.Checked = true;

                txtMaLOP.Focus();

                MessageBox.Show("Chế độ thêm mới lớp học đã được bật. Hãy nhập thông tin và nhấn Lưu để hoàn tất.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi bật chế độ thêm mới: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!isAdding)
            {
                MessageBox.Show("Bạn cần nhấn nút Thêm trước khi lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(txtMaLOP.Text) || string.IsNullOrWhiteSpace(txtTenLOP.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ Mã lớp và Tên lớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string trangThai = rbtnHD.Checked ? "Hoạt động" : "Ngừng hoạt động";

                string sql = @"INSERT INTO LOP (MaLOP, TenLOP, Khoi, GVCN, SiSo, NamHoc, NgayTao, TrangThai)
                               VALUES (@MaLOP, @TenLOP, @Khoi, @GVCN, @SiSo, @NamHoc, @NgayTao, @TrangThai)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaLOP", txtMaLOP.Text);
                cmd.Parameters.AddWithValue("@TenLOP", txtTenLOP.Text);
                cmd.Parameters.AddWithValue("@Khoi", cbKhoi.Text);
                cmd.Parameters.AddWithValue("@GVCN", cbGVCN.Text);
                cmd.Parameters.AddWithValue("@SiSo", (int)numsiso.Value);
                cmd.Parameters.AddWithValue("@NamHoc", cbNamhoc.Text);
                cmd.Parameters.AddWithValue("@NgayTao", Ngaytao.Value);
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Thêm lớp mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isAdding = false;
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu lớp mới: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        private void gridLOP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = gridLOP.Rows[e.RowIndex];

                txtMaLOP.Text = row.Cells["MaLOP"].Value.ToString();
                txtTenLOP.Text = row.Cells["TenLOP"].Value.ToString();
                cbKhoi.Text = row.Cells["Khoi"].Value.ToString();
                cbGVCN.Text = row.Cells["GVCN"].Value.ToString();
                numsiso.Value = Convert.ToInt32(row.Cells["SiSo"].Value);
                cbNamhoc.Text = row.Cells["NamHoc"].Value.ToString();
                Ngaytao.Value = Convert.ToDateTime(row.Cells["NgayTao"].Value);

                string trangThai = row.Cells["TrangThai"].Value.ToString();
                if (trangThai == "Hoạt động")
                    rbtnHD.Checked = true;
                else
                    rbtnNgungHD.Checked = true;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaLOP.Text))
                {
                    MessageBox.Show("Vui lòng chọn lớp cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string trangThai = rbtnHD.Checked ? "Hoạt động" : "Ngừng hoạt động";

                string sql = @"UPDATE LOP
                       SET TenLOP = @TenLOP,
                           Khoi = @Khoi,
                           GVCN = @GVCN,
                           SiSo = @SiSo,
                           NamHoc = @NamHoc,
                           NgayTao = @NgayTao,
                           TrangThai = @TrangThai
                       WHERE MaLOP = @MaLOP";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaLOP", txtMaLOP.Text);
                cmd.Parameters.AddWithValue("@TenLOP", txtTenLOP.Text);
                cmd.Parameters.AddWithValue("@Khoi", cbKhoi.Text);
                cmd.Parameters.AddWithValue("@GVCN", cbGVCN.Text);
                cmd.Parameters.AddWithValue("@SiSo", (int)numsiso.Value);
                cmd.Parameters.AddWithValue("@NamHoc", cbNamhoc.Text);
                cmd.Parameters.AddWithValue("@NgayTao", Ngaytao.Value);
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();

                if (rows > 0)
                {
                    MessageBox.Show("Cập nhật lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy lớp cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật lớp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra người dùng đã chọn lớp hay chưa
                if (string.IsNullOrWhiteSpace(txtMaLOP.Text))
                {
                    MessageBox.Show("Vui lòng chọn lớp cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Hỏi xác nhận trước khi xóa
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa lớp này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;

                // Thực hiện câu lệnh SQL
                string sql = "DELETE FROM LOP WHERE MaLOP = @MaLOP";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLOP", txtMaLOP.Text);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("Đã xóa lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Làm mới lại DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy lớp để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa lớp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
       MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close(); // Đóng form hiện tại
            }
        }
    }
}
