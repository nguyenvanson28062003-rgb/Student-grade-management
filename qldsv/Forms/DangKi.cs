#nullable disable
using Microsoft.Data.SqlClient;
using qldsv.Service;

namespace quản_lí_điểm_sinh_viên
{
    public partial class DangKi : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;
        private string _maSVTimDuoc = null;   // MaSV tìm được sau khi xác minh

        public DangKi()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            textBox3.PasswordChar = '*';
            button1.Click += btnDangKi_Click;
            label5.Click += lblDangNhap_Click;
            label5.Cursor = Cursors.Hand;
            label5.ForeColor = Color.Blue;

            // Khi người dùng thay đổi tên → reset trạng thái xác minh
            textBox1.TextChanged += (s, e) => { _maSVTimDuoc = null; };
        }

        private string TimMaSV(string hoTen)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                // Tìm chính xác trước, nếu không thấy thì tìm LIKE
                var cmd = new SqlCommand(@"
                    SELECT MaSV FROM SinhVien
                    WHERE HoTen = @HoTen OR HoTen LIKE '%' + @HoTen + '%'
                    ORDER BY CASE WHEN HoTen = @HoTen THEN 0 ELSE 1 END", conn);
                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                using var reader = cmd.ExecuteReader();

                if (!reader.Read()) return null;          // Không tìm thấy SV nào
                string maSV = reader["MaSV"].ToString();
                if (reader.Read()) return "";             // Tìm thấy nhiều hơn 1 → mơ hồ
                return maSV;
            }
            catch { return null; }
        }

        private bool DaCoTaiKhoan(string maSV)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @TDN", conn);
                cmd.Parameters.AddWithValue("@TDN", maSV);
                return (int)cmd.ExecuteScalar() > 0;
            }
            catch { return false; }
        }

        private void btnDangKi_Click(object sender, EventArgs e)
        {
            string hoTen = textBox1.Text.Trim();
            string matKhau = textBox2.Text.Trim();
            string xacNhan = textBox3.Text.Trim();

            // --- Validate ---
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(xacNhan))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (matKhau != xacNhan)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Clear(); textBox3.Focus();
                return;
            }

            if (matKhau.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_maSVTimDuoc == null)
            {
                string ketQua = TimMaSV(hoTen);

                if (ketQua == null)
                {
                    MessageBox.Show(
                        $"Không tìm thấy sinh viên có tên \"{hoTen}\" trong hệ thống.\n" +
                        "Vui lòng kiểm tra lại họ tên hoặc liên hệ phòng đào tạo.",
                        "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (ketQua == "")
                {
                    MessageBox.Show(
                        $"Tìm thấy nhiều sinh viên có tên tương tự \"{hoTen}\".\n" +
                        "Vui lòng nhập đầy đủ họ tên để xác định chính xác.",
                        "Tên trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _maSVTimDuoc = ketQua;
            }


            if (DaCoTaiKhoan(_maSVTimDuoc))
            {
                MessageBox.Show(
                    $"Tài khoản cho sinh viên [{_maSVTimDuoc}] đã tồn tại.\n" +
                    "Nếu quên mật khẩu, vui lòng liên hệ Admin để được cấp lại.",
                    "Tài khoản đã có", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _maSVTimDuoc = null;
                return;
            }

            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(@"
                    INSERT INTO NguoiDung (TenDangNhap, MatKhau, VaiTro, TrangThai, NgayTao)
                    VALUES (@TDN, @MK, 'SinhVien', 1, GETDATE())", conn);
                cmd.Parameters.AddWithValue("@TDN", _maSVTimDuoc);
                cmd.Parameters.AddWithValue("@MK", matKhau);
                cmd.ExecuteNonQuery();

                MessageBox.Show(
                    $"Đăng ký thành công!\n" +
                    $"Tên đăng nhập của bạn là: {_maSVTimDuoc}\n" +
                    "(Chính là mã sinh viên của bạn)",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tạo tài khoản:\n" + ex.Message,
                    "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Link Đăng nhập
        private void lblDangNhap_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}