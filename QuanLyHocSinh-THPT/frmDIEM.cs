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
    public partial class frmDIEM : Form
    {
        SqlConnection conn = new SqlConnection(
            @"Data Source=DESKTOP-5INP67J\SQLEXPRESS;Initial Catalog=QuanLyHocSinhTHPT;Integrated Security=True");
        SqlDataAdapter da;
        DataTable dt;
        bool isAdding = false;


        public frmDIEM()
        {
            InitializeComponent();
            gridDIEM.AutoGenerateColumns = true;
        }

        private void frmDIEM_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComboboxes();
            SetupNumericControls();
        }

        private void LoadData()
        {
            string sql = "SELECT * FROM DIEM";
            da = new SqlDataAdapter(sql, conn);
            dt = new DataTable();
            da.Fill(dt);
            gridDIEM.DataSource = dt;
            
        }

        private void LoadComboboxes()
        {
            // Mã học sinh
            SqlDataAdapter daHS = new SqlDataAdapter("SELECT MaHS FROM HOCSINH", conn);
            DataTable dtHS = new DataTable();
            daHS.Fill(dtHS);
            cbMaHS.DataSource = dtHS;
            cbMaHS.DisplayMember = "MaHS";
            cbMaHS.ValueMember = "MaHS";

            // Mã môn học
            SqlDataAdapter daMH = new SqlDataAdapter("SELECT MaMH FROM MONHOC", conn);
            DataTable dtMH = new DataTable();
            daMH.Fill(dtMH);
            cbMaMH.DataSource = dtMH;
            cbMaMH.DisplayMember = "MaMH";
            cbMaMH.ValueMember = "MaMH";
        }

        private void SetupNumericControls()
        {
            numDiem15P.Minimum = 0; numDiem15P.Maximum = 10;
            numDiem1T.Minimum = 0; numDiem1T.Maximum = 10;
            numDiemCK.Minimum = 0; numDiemCK.Maximum = 10;

            // Khi người dùng đổi điểm, tự động tính Điểm TB
            numDiem15P.ValueChanged += TinhDiemTrungBinh;
            numDiem1T.ValueChanged += TinhDiemTrungBinh;
            numDiemCK.ValueChanged += TinhDiemTrungBinh;
        }

        private void TinhDiemTrungBinh(object sender, EventArgs e)
        {
            double tb = (double)numDiem15P.Value * 0.2 +
             (double)numDiem1T.Value * 0.3 +
             (double)numDiemCK.Value * 0.5;
            txtDiemTB.Text = tb.ToString("0.00");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;

            txtMadiem.Clear();
            cbMaHS.SelectedIndex = -1;
            cbMaMH.SelectedIndex = -1;
            numDiem15P.Value = 0;
            numDiem1T.Value = 0;
            numDiemCK.Value = 0;
            txtDiemTB.Clear();

            txtMadiem.Focus();

            MessageBox.Show("Chế độ thêm điểm mới đã được bật. Hãy nhập thông tin và nhấn Lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!isAdding)
            {
                MessageBox.Show("Vui lòng nhấn Thêm trước khi lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(txtMadiem.Text) || cbMaHS.SelectedIndex == -1 || cbMaMH.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal d15 = numDiem15P.Value;
                decimal d1T = numDiem1T.Value;
                decimal dCK = numDiemCK.Value;

                // Tính điểm trung bình (quy ước 15p 20%, 1T 30%, CK 50%)
                decimal diemTB = Math.Round((d15 * 0.2m + d1T * 0.3m + dCK * 0.5m), 2);
                txtDiemTB.Text = diemTB.ToString();

                string sql = @"INSERT INTO DIEM (MaDiem, MaHS, MaMH, Diem15P, Diem1T, DiemCK, DiemTB)
                       VALUES (@MaDiem, @MaHS, @MaMH, @Diem15P, @Diem1T, @DiemCK, @DiemTB)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaDiem", txtMadiem.Text);
                cmd.Parameters.AddWithValue("@MaHS", cbMaHS.SelectedValue);
                cmd.Parameters.AddWithValue("@MaMH", cbMaMH.SelectedValue);
                cmd.Parameters.AddWithValue("@Diem15P", d15);
                cmd.Parameters.AddWithValue("@Diem1T", d1T);
                cmd.Parameters.AddWithValue("@DiemCK", dCK);
                cmd.Parameters.AddWithValue("@DiemTB", diemTB);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Lưu điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isAdding = false;
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        private void gridDIEM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = gridDIEM.Rows[e.RowIndex];

                txtMadiem.Text = row.Cells["MaDiem"].Value.ToString();
                cbMaHS.Text = row.Cells["MaHS"].Value.ToString();
                cbMaMH.Text = row.Cells["MaMH"].Value.ToString();
                numDiem15P.Value = Convert.ToDecimal(row.Cells["Diem15P"].Value);
                numDiem1T.Value = Convert.ToDecimal(row.Cells["Diem1T"].Value);
                numDiemCK.Value = Convert.ToDecimal(row.Cells["DiemCK"].Value);
                txtDiemTB.Text = row.Cells["DiemTB"].Value.ToString();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMadiem.Text))
                {
                    MessageBox.Show("Vui lòng chọn bản ghi cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal d15 = numDiem15P.Value;
                decimal d1T = numDiem1T.Value;
                decimal dCK = numDiemCK.Value;
                decimal diemTB = Math.Round((d15 * 0.2m + d1T * 0.3m + dCK * 0.5m), 2);
                txtDiemTB.Text = diemTB.ToString();

                string sql = @"UPDATE DIEM
                       SET MaHS = @MaHS,
                           MaMH = @MaMH,
                           Diem15P = @Diem15P,
                           Diem1T = @Diem1T,
                           DiemCK = @DiemCK,
                           DiemTB = @DiemTB
                       WHERE MaDiem = @MaDiem";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaDiem", txtMadiem.Text);
                cmd.Parameters.AddWithValue("@MaHS", cbMaHS.SelectedValue);
                cmd.Parameters.AddWithValue("@MaMH", cbMaMH.SelectedValue);
                cmd.Parameters.AddWithValue("@Diem15P", d15);
                cmd.Parameters.AddWithValue("@Diem1T", d1T);
                cmd.Parameters.AddWithValue("@DiemCK", dCK);
                cmd.Parameters.AddWithValue("@DiemTB", diemTB);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();

                if (rows > 0)
                {
                    MessageBox.Show("Cập nhật điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mã điểm để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMadiem.Text))
                {
                    MessageBox.Show("Vui lòng chọn điểm cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa điểm này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    string sql = "DELETE FROM DIEM WHERE MaDiem = @MaDiem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaDiem", txtMadiem.Text);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("Xóa điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearForm(); // Xóa trắng sau khi xóa
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy bản ghi cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        private void ClearForm()
        {
            txtMadiem.Clear();
            cbMaHS.SelectedIndex = -1;
            cbMaMH.SelectedIndex = -1;
            numDiem15P.Value = 0;
            numDiem1T.Value = 0;
            numDiemCK.Value = 0;
            txtDiemTB.Clear();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "Bạn có chắc chắn muốn thoát khỏi form điểm?",
        "Xác nhận thoát",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    );

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Mở form tìm kiếm điểm
                frmTimKiemDIEM frm = new frmTimKiemDIEM();
                frm.ShowDialog(); // Dùng ShowDialog để mở dạng modal (chờ đóng form con)
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở form tìm kiếm: " + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
}
