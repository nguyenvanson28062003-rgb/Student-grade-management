#nullable disable
using System.Drawing.Drawing2D;
using Microsoft.Data.SqlClient;
using qldsv;
using qldsv.Service;

namespace quản_lí_điểm_sinh_viên
{
    public partial class DangNhap : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;

        private struct Particle { public float X, Y, Speed, Alpha, Size; }
        private Particle[] _particles = new Particle[40];
        private readonly Random _rnd = new();
        private readonly System.Windows.Forms.Timer _particleTmr = new() { Interval = 30 };
        private readonly System.Windows.Forms.Timer _fadeTmr = new() { Interval = 12 };

        private float _pulse;
        private float _pulseDir = 0.03f;

        public DangNhap()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AcceptButton = btnDangnhap;

            // Fade-in form
            Opacity = 0;
            _fadeTmr.Tick += (s, ev) =>
            {
                Opacity = Math.Min(1.0, Opacity + 0.06);
                if (Opacity >= 1) _fadeTmr.Stop();
            };
            _fadeTmr.Start();

            // Khởi tạo particles
            for (int i = 0; i < _particles.Length; i++) ResetParticle(ref _particles[i], true);
            _particleTmr.Tick += (s, ev) =>
            {
                for (int i = 0; i < _particles.Length; i++)
                {
                    _particles[i].Y -= _particles[i].Speed;
                    _particles[i].Alpha -= 0.004f;
                    if (_particles[i].Alpha <= 0 || _particles[i].Y < -10)
                        ResetParticle(ref _particles[i], false);
                }
                _pulse += _pulseDir;
                if (_pulse > 1f || _pulse < 0f) _pulseDir = -_pulseDir;
                panelLeft.Invalidate();
            };
            _particleTmr.Start();
        }

        private void ResetParticle(ref Particle p, bool randomY)
        {
            p.X     = (float)(_rnd.NextDouble() * 350);
            p.Y     = randomY ? (float)(_rnd.NextDouble() * 500) : 510;
            p.Speed = (float)(_rnd.NextDouble() * 0.8 + 0.3);
            p.Alpha = (float)(_rnd.NextDouble() * 0.5 + 0.1);
            p.Size  = (float)(_rnd.NextDouble() * 3 + 1);
        }

        private void panelLeft_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var r = panelLeft.ClientRectangle;

            // Gradient trái: Emerald đậm → teal
            using var bgBr = new LinearGradientBrush(r,
                Color.FromArgb(3, 102, 74),
                Color.FromArgb(6, 160, 112),
                LinearGradientMode.ForwardDiagonal);
            g.FillRectangle(bgBr, r);

            // Particles: bọt trắng/mint bay lên
            foreach (var p in _particles)
            {
                int a = (int)(p.Alpha * 160);
                if (a <= 0) continue;
                using var br = new SolidBrush(Color.FromArgb(a, 220, 255, 240));
                g.FillEllipse(br, p.X - p.Size, p.Y - p.Size, p.Size * 2, p.Size * 2);
            }

            // Đường kẻ trang trí ngang mờ
            using var linePen = new Pen(Color.FromArgb(30, 255, 255, 255), 1);
            for (int y = 0; y < r.Height; y += 40)
                g.DrawLine(linePen, 0, y, r.Width, y);

            // Logo hình tròn phát sáng
            int cx = r.Width / 2, cy = r.Height / 2 - 40;
            int glowR = (int)(55 + _pulse * 8);
            T.DrawGlow(g, new Rectangle(cx - glowR, cy - glowR, glowR * 2, glowR * 2), T.Red, 12);

            using var circlePen = new Pen(Color.FromArgb(200, 255, 255, 255), 2.5f);
            g.DrawEllipse(circlePen, cx - 48, cy - 48, 96, 96);
            using var innerBr = new SolidBrush(Color.FromArgb(40, 255, 255, 255));
            g.FillEllipse(innerBr, cx - 48, cy - 48, 96, 96);

            // Chữ "QL" trong logo
            using var logoFont = new Font("Segoe UI", 26f, FontStyle.Bold);
            var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString("QL", logoFont, Brushes.White, new RectangleF(cx - 48, cy - 48, 96, 96), sf);

            // Tiêu đề hệ thống
            using var titleFont = new Font("Segoe UI", 14f, FontStyle.Bold);
            using var titleBr   = new SolidBrush(T.White);
            g.DrawString("QUẢN LÝ ĐIỂM", titleFont, titleBr,
                new RectangleF(0, cy + 65, r.Width, 36), sf);

            using var subFont = new Font("Segoe UI", 9f);
            using var subBr   = new SolidBrush(T.GrayLight);
            g.DrawString("HỆ THỐNG THÔNG TIN SINH VIÊN", subFont, subBr,
                new RectangleF(0, cy + 102, r.Width, 24), sf);

            // Đường phân cách dưới
            using var sepPen = new Pen(Color.FromArgb(140, 255, 255, 255), 2);
            int sx = 60, sw = r.Width - 120;
            g.DrawLine(sepPen, sx, cy + 136, sx + sw, cy + 136);

            // Version
            using var verBr = new SolidBrush(Color.FromArgb(80, T.White));
            using var verFont = new Font("Segoe UI", 7.5f);
            g.DrawString("v2.0 Premium Edition", verFont, verBr,
                new RectangleF(0, r.Height - 28, r.Width, 22), sf);
        }

        private void panelRight_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var r = panelRight.ClientRectangle;

            // Nền off-white
            g.FillRectangle(new SolidBrush(Color.FromArgb(244, 246, 248)), r);

            // Đường viền bên trái (teal)
            using var lPen = new Pen(T.Primary, 3);
            g.DrawLine(lPen, 0, 20, 0, r.Height - 20);

            // Góc trang trí teal nhạt
            using var cornerPen = new Pen(Color.FromArgb(80, 5, 150, 105), 1.5f);
            g.DrawLine(cornerPen, r.Width - 80, r.Height - 2, r.Width - 2, r.Height - 2);
            g.DrawLine(cornerPen, r.Width - 2, r.Height - 80, r.Width - 2, r.Height - 2);
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            string tenDN  = txtUser.Text.Trim();
            string matKhau = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(tenDN) || string.IsNullOrEmpty(matKhau))
            {
                ShowError("Hãy nhâp đầy đủ tên đăng nhập và mật khẩu!");
                return;
            }

            try
            {
                var ketQua = _db.ExecuteSPSingleRow("sp_DangNhap",
                    DatabaseHelper.Param("@TenDangNhap", tenDN),
                    DatabaseHelper.Param("@MatKhau", matKhau));

                if (ketQua.Count == 0) { ShowError("Lỗi hệ thống!"); return; }

                if (Convert.ToInt32(ketQua["ThanhCong"]) != 1)
                {
                    ShowError(ketQua["ThongBao"]?.ToString() ?? "Đăng nhập thất bại");
                    txtPass.Text = "";
                    return;
                }

                SessionInfo.TenDangNhap = tenDN;
                SessionInfo.VaiTro      = ketQua["VaiTro"]?.ToString() ?? "";

                if (SessionInfo.VaiTro == "GiangVien") LayMaGV(tenDN);
                if (SessionInfo.VaiTro == "SinhVien")  LayMaSV(tenDN);

                Form menuForm = SessionInfo.VaiTro switch
                {
                    "Admin"      => new MenuAdmin(),
                    "GiangVien"  => new MenuGV(),
                    "SinhVien"   => new MenuSV(),
                    _            => null
                };

                if (menuForm == null) { ShowError("Vai trò không hợp lệ!"); return; }

                _particleTmr.Stop();
                menuForm.FormClosed += (s, args) => Application.Exit();
                this.Hide();
                menuForm.Show();
            }
            catch (SqlException ex)
            {
                ShowError("Lỗi kết nối:\n" + ex.Message);
            }
        }

        private void ShowError(string msg)
        {
            lblError.Text    = "⚠  " + msg;
            lblError.Visible = true;
        }

        private void LayMaGV(string tdn)
        {
            try
            {
                using var conn = _db.GetConnection(); conn.Open();
                var cmd = new SqlCommand(
                    "SELECT gv.MaGV, gv.HoTen FROM GiangVien gv JOIN NguoiDung nd ON gv.MaND=nd.MaND WHERE nd.TenDangNhap=@T", conn);
                cmd.Parameters.AddWithValue("@T", tdn);
                using var r = cmd.ExecuteReader();
                if (r.Read()) { SessionInfo.MaGV = r["MaGV"].ToString(); SessionInfo.HoTen = r["HoTen"].ToString(); }
            }
            catch { }
        }

        private void LayMaSV(string tdn)
        {
            try
            {
                using var conn = _db.GetConnection(); conn.Open();
                var cmd = new SqlCommand(
                    "SELECT sv.MaSV, sv.HoTen FROM SinhVien sv JOIN NguoiDung nd ON sv.MaND=nd.MaND WHERE nd.TenDangNhap=@T", conn);
                cmd.Parameters.AddWithValue("@T", tdn);
                using var r = cmd.ExecuteReader();
                if (r.Read()) { SessionInfo.MaSV = r["MaSV"].ToString(); SessionInfo.HoTen = r["HoTen"].ToString(); }
            }
            catch { }
        }

        private void lblDangki_Click(object sender, EventArgs e) => new DangKi().ShowDialog();
        private void lblQuen_Click(object sender, EventArgs e)    => new DoiMK().ShowDialog();

        protected override void Dispose(bool disposing)
        {
            if (disposing) { _particleTmr.Dispose(); _fadeTmr.Dispose(); }
            base.Dispose(disposing);
        }
    }
}
