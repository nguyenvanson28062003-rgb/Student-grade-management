#nullable disable
using Microsoft.Data.SqlClient;
using qldsv.Service;

namespace quản_lí_điểm_sinh_viên
{
    public partial class DangKi : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;
        private string _maSVTimDuoc = null;   // MaSV tìm được sau khi xác minh

        private readonly System.Windows.Forms.Timer _fadeTmr = new() { Interval = 12 };

        public DangKi()
        {
            InitializeComponent();
            // Không gọi ThemeApplier vì form đã tự custom-paint
            button1.Click += btnDangKi_Click;
            label5.Click  += lblDangNhap_Click;

            textBox1.TextChanged += (s, e) => { _maSVTimDuoc = null; lblError.Visible = false; };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Fade-in
            Opacity = 0;
            _fadeTmr.Tick += (s, ev) =>
            {
                Opacity = Math.Min(1.0, Opacity + 0.07);
                if (Opacity >= 1) { _fadeTmr.Stop(); _fadeTmr.Dispose(); }
            };
            _fadeTmr.Start();
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
                ShowError("⚠  Vui lòng nhập đầy đủ tất cả thông tin!"); return;
            }
            if (matKhau.Length < 6)
            {
                ShowError("⚠  Mật khẩu phải có ít nhất 6 ký tự!"); textBox2.Focus(); return;
            }
            if (matKhau != xacNhan)
            {
                ShowError("⚠  Mật khẩu xác nhận không khớp!");
                textBox3.Clear(); textBox3.Focus(); return;
            }

            if (_maSVTimDuoc == null)
            {
                string ketQua = TimMaSV(hoTen);

                if (ketQua == null)
                {
                    ShowError($"⚠  Không tìm thấy \"{hoTen}\" trong hệ thống. Kiểm tra lại họ tên.");
                    return;
                }
                if (ketQua == "")
                {
                    ShowError($"⚠  Có nhiều SV trùng tên \"{hoTen}\". Nhập đầy đủ họ tên hơn.");
                    return;
                }

                _maSVTimDuoc = ketQua;
            }


            if (DaCoTaiKhoan(_maSVTimDuoc))
            {
                ShowError($"⚠  Tài khoản [{_maSVTimDuoc}] đã tồn tại. Liên hệ Admin nếu quên mật khẩu.");
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

        private void ShowError(string msg)
        {
            lblError.Text    = msg;
            lblError.Visible = true;
        }

        private void lblDangNhap_Click(object sender, EventArgs e) => this.Close();

        protected override void Dispose(bool disposing)
        {
            if (disposing) _fadeTmr?.Dispose();
            base.Dispose(disposing);
        }
    }
}