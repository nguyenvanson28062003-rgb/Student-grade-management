#nullable disable
using Microsoft.Data.SqlClient;
using System.Data;
using qldsv;
using qldsv.Service;

namespace quản_lí_điểm_sinh_viên
{
    public partial class DoiMK : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;
        private readonly System.Windows.Forms.Timer _fadeTmr = new() { Interval = 12 };

        public DoiMK()
        {
            InitializeComponent();
            // Không gọi ThemeApplier — form tự custom-paint

            btnDangnhap.Click += Button1_Click;
            lblQuen.Click     += Button2_Click;
            label5.Click      += (s, e) => this.Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Tự điền username nếu đã đăng nhập
            if (!string.IsNullOrEmpty(SessionInfo.TenDangNhap))
            {
                textBox1.Text     = SessionInfo.TenDangNhap;
                textBox1.ReadOnly = true;
                textBox1.BackColor= Color.FromArgb(230, 245, 240);
            }

            textBox2.Focus();

            // Fade-in
            Opacity = 0;
            _fadeTmr.Tick += (s, ev) =>
            {
                Opacity = Math.Min(1.0, Opacity + 0.07);
                if (Opacity >= 1) { _fadeTmr.Stop(); _fadeTmr.Dispose(); }
            };
            _fadeTmr.Start();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string username    = textBox1.Text.Trim();
            string matKhauCu   = textBox2.Text.Trim();
            string matKhauMoi  = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(matKhauCu) || string.IsNullOrEmpty(matKhauMoi))
            {
                ShowError("⚠  Vui lòng nhập đầy đủ tất cả thông tin!"); return;
            }
            if (matKhauMoi.Length < 6)
            {
                ShowError("⚠  Mật khẩu mới phải có ít nhất 6 ký tự!"); textBox3.Focus(); return;
            }
            if (matKhauMoi == matKhauCu)
            {
                ShowError("⚠  Mật khẩu mới phải khác mật khẩu hiện tại!"); textBox3.Focus(); return;
            }

            try
            {
                var result = _db.ExecuteSPSingleRow("sp_DoiMatKhau",
                    DatabaseHelper.Param("@TenDangNhap", username),
                    DatabaseHelper.Param("@MatKhauCu",   matKhauCu),
                    DatabaseHelper.Param("@MatKhauMoi",  matKhauMoi)
                );

                if (result.Count > 0 && Convert.ToInt32(result["KetQua"]) == 1)
                {
                    // Thành công — hiện thông báo xanh
                    lblError.ForeColor = Color.FromArgb(5, 150, 105);
                    lblError.Text      = "✔  Đổi mật khẩu thành công!";
                    lblError.Visible   = true;

                    btnDangnhap.Enabled = false;
                    textBox2.Clear();
                    textBox3.Clear();

                    // Tự đóng sau 2 giây
                    var t = new System.Windows.Forms.Timer { Interval = 2000 };
                    t.Tick += (s, ev) => { t.Stop(); t.Dispose(); this.Close(); };
                    t.Start();
                }
                else
                {
                    string msg = result.TryGetValue("ThongBao", out var tb) && tb != null
                        ? tb.ToString()
                        : "Mật khẩu hiện tại không đúng!";
                    ShowError("⚠  " + msg);
                    textBox2.Clear(); textBox2.Focus();
                }
            }
            catch (Exception ex)
            {
                ShowError("⚠  Lỗi hệ thống: " + ex.Message);
            }
        }

        private void Button2_Click(object sender, EventArgs e) => this.Close();

        private void ShowError(string msg)
        {
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Text      = msg;
            lblError.Visible   = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _fadeTmr?.Dispose();
            base.Dispose(disposing);
        }
    }
}
