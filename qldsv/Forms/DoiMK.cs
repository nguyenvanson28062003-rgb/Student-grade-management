#nullable disable
using Microsoft.Data.SqlClient;
using System.Data;
using qldsv.Service;

namespace quản_lí_điểm_sinh_viên
{
    public partial class DoiMK : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;
        public DoiMK()
        {
            InitializeComponent();
            ThemeApplier.Apply(this);
            this.Load += DoiMK_Load;
            btnDangnhap.Click += Button1_Click; // Lưu/Xác nhận
            lblQuen.Click += Button2_Click; // Thoát
        }

        private void DoiMK_Load(object? sender, EventArgs e)
        {
            // Không làm gì đặc biệt
        }

        private void Button1_Click(object? sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string currentPassword = textBox2.Text.Trim();
            string newPassword = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var result = _db.ExecuteSPSingleRow("sp_DoiMatKhau",
                    DatabaseHelper.Param("@TenDangNhap", username),
                    DatabaseHelper.Param("@MatKhauCu", currentPassword),
                    DatabaseHelper.Param("@MatKhauMoi", newPassword)
                );

                if (result.Count > 0 && Convert.ToInt32(result["KetQua"]) == 1)
                {
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    string message = result.TryGetValue("ThongBao", out var msg) ? msg.ToString() : "Đổi mật khẩu thất bại!";
                    MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối database: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button2_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
