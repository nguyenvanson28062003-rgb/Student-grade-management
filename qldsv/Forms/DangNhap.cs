#nullable disable
using Microsoft.Data.SqlClient;
using qldsv;
using qldsv.Service;

namespace quản_lí_điểm_sinh_viên
{
    public partial class DangNhap : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;

        public DangNhap()
        {
            InitializeComponent();
            // Ẩn ký tự mật khẩu
            txtPass.PasswordChar = '*';
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Cho phép nhấn Enter để đăng nhập
            this.AcceptButton = btnDangnhap;
        }
        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            string tenDN = txtUser.Text.Trim();
            string matKhau = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(tenDN) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var ketQua = _db.ExecuteSPSingleRow("sp_DangNhap",
                    DatabaseHelper.Param("@TenDangNhap", tenDN),
                    DatabaseHelper.Param("@MatKhau", matKhau)
                );

                if (ketQua.Count == 0)
                {
                    MessageBox.Show("Lỗi hệ thống, vui lòng thử lại!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool thanhCong = Convert.ToInt32(ketQua["ThanhCong"]) == 1;

                if (!thanhCong)
                {
                    MessageBox.Show(ketQua["ThongBao"]?.ToString(),
                        "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPass.Clear();
                    txtPass.Focus();
                    return;
                }

                // Lưu thông tin phiên làm việc
                SessionInfo.TenDangNhap = tenDN;
                SessionInfo.VaiTro = ketQua["VaiTro"]?.ToString() ?? "";

                // Nếu là GiangVien → lấy thêm MaGV + HoTen
                if (SessionInfo.VaiTro == "GiangVien")
                    LayMaGV(tenDN);

                // Nếu là SinhVien → lấy thêm MaSV + HoTen
                if (SessionInfo.VaiTro == "SinhVien")
                    LayMaSV(tenDN);

                // Mở Menu tương ứng vai trò, đóng hẳn DangNhap
                Form menuForm = SessionInfo.VaiTro switch
                {
                    "Admin" => new MenuAdmin(),
                    "GiangVien" => new MenuGV(),
                    "SinhVien" => new MenuSV(),
                    _ => null
                };

                if (menuForm == null)
                {
                    MessageBox.Show("Vai trò không hợp lệ!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Khi Menu đóng → thoát hẳn ứng dụng
                menuForm.FormClosed += (s, args) => Application.Exit();

                this.Hide();
                menuForm.Show();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối database:\n" + ex.Message,
                    "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Lấy MaGV + HoTen từ bảng GiangVien theo TenDangNhap
        private void LayMaGV(string tenDangNhap)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT gv.MaGV, gv.HoTen FROM GiangVien gv JOIN NguoiDung nd ON gv.MaND = nd.MaND WHERE nd.TenDangNhap = @TDN", conn);
                cmd.Parameters.AddWithValue("@TDN", tenDangNhap);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    SessionInfo.MaGV = reader["MaGV"].ToString();
                    SessionInfo.HoTen = reader["HoTen"].ToString();
                }
            }
            catch { }
        }

        // Lấy MaSV + HoTen từ bảng SinhVien theo TenDangNhap
        private void LayMaSV(string tenDangNhap)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT sv.MaSV, sv.HoTen FROM SinhVien sv JOIN NguoiDung nd ON sv.MaND = nd.MaND WHERE nd.TenDangNhap = @TDN", conn);
                cmd.Parameters.AddWithValue("@TDN", tenDangNhap);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    SessionInfo.MaSV = reader["MaSV"].ToString();
                    SessionInfo.HoTen = reader["HoTen"].ToString();
                }
            }
            catch { }
        }

        private void lblDangki_Click(object sender, EventArgs e)
        {
            new DangKi().ShowDialog();
        }

        private void lblQuen_Click(object sender, EventArgs e)
        {
            new DoiMK().ShowDialog();
        }

        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void txtUser_TextChanged(object sender, EventArgs e) { }
        private void lbllogin_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}