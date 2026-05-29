using qldsv.Service;

namespace quản_lí_điểm_sinh_viên
{
    partial class DangKi
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            panelLeft  = new Panel();
            panelRight = new Panel();
            panelDiv   = new Panel();

            lblTitle   = new Label();
            lblSubTitle= new Label();
            lblHoTen   = new Label();
            lblMatKhau = new Label();
            lblXacNhan = new Label();
            lblGhiChu  = new Label();
            lblError   = new Label();
            label5     = new Label();

            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            button1  = new Button();

            panelLeft.SuspendLayout();
            panelRight.SuspendLayout();
            SuspendLayout();

            // ── Form ──────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(860, 500);
            FormBorderStyle     = FormBorderStyle.None;
            StartPosition       = FormStartPosition.CenterScreen;
            BackColor           = T.BG;
            Name                = "DangKi";
            Text                = "Đăng Ký";
            Opacity             = 0;

            // ── panelLeft (banner xanh, giống DangNhap) ──────────
            panelLeft.Location  = new Point(0, 0);
            panelLeft.Size      = new Size(340, 500);
            panelLeft.BackColor = Color.Transparent;
            panelLeft.Paint    += PanelLeft_Paint;

            // ── panelRight (form đăng ký) ─────────────────────────
            panelRight.Location  = new Point(340, 0);
            panelRight.Size      = new Size(520, 500);
            panelRight.BackColor = Color.FromArgb(244, 246, 248);
            panelRight.Paint    += PanelRight_Paint;

            // Tiêu đề
            lblTitle.Text      = "ĐĂNG KÝ TÀI KHOẢN";
            lblTitle.Font      = new Font("Segoe UI", 18f, FontStyle.Bold);
            lblTitle.ForeColor = T.TextMain;
            lblTitle.Location  = new Point(46, 50);
            lblTitle.Size      = new Size(420, 38);
            lblTitle.BackColor = Color.Transparent;

            lblSubTitle.Text      = "Nhập họ tên đầy đủ như trong hệ thống";
            lblSubTitle.Font      = new Font("Segoe UI", 9.5f);
            lblSubTitle.ForeColor = T.TextSub;
            lblSubTitle.Location  = new Point(46, 92);
            lblSubTitle.Size      = new Size(420, 20);
            lblSubTitle.BackColor = Color.Transparent;

            panelDiv.Location  = new Point(46, 118);
            panelDiv.Size      = new Size(60, 3);
            panelDiv.BackColor = T.Primary;

            // ── Họ Tên ────────────────────────────────────────────
            lblHoTen.Text      = "Họ và Tên";
            lblHoTen.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            lblHoTen.ForeColor = T.TextSub;
            lblHoTen.Location  = new Point(46, 134);
            lblHoTen.Size      = new Size(120, 18);
            lblHoTen.BackColor = Color.Transparent;

            textBox1.Location       = new Point(46, 154);
            textBox1.Size           = new Size(424, 44);
            textBox1.Font           = new Font("Segoe UI", 10.5f);
            textBox1.BackColor      = Color.FromArgb(240, 253, 248);
            textBox1.ForeColor      = T.TextMain;
            textBox1.BorderStyle    = BorderStyle.FixedSingle;
            textBox1.Multiline      = true;
            textBox1.TabIndex       = 0;
            textBox1.PlaceholderText= "Nhập họ tên đầy đủ (vd: Nguyễn Văn A)";

            // ── Mật Khẩu ─────────────────────────────────────────
            lblMatKhau.Text      = "Mật Khẩu";
            lblMatKhau.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            lblMatKhau.ForeColor = T.TextSub;
            lblMatKhau.Location  = new Point(46, 212);
            lblMatKhau.Size      = new Size(120, 18);
            lblMatKhau.BackColor = Color.Transparent;

            textBox2.Location       = new Point(46, 232);
            textBox2.Size           = new Size(424, 44);
            textBox2.Font           = new Font("Segoe UI", 10.5f);
            textBox2.BackColor      = Color.FromArgb(240, 253, 248);
            textBox2.ForeColor      = T.TextMain;
            textBox2.BorderStyle    = BorderStyle.FixedSingle;
            textBox2.Multiline      = true;
            textBox2.PasswordChar   = '●';
            textBox2.TabIndex       = 1;
            textBox2.PlaceholderText= "Tối thiểu 6 ký tự";

            // ── Xác Nhận Mật Khẩu ────────────────────────────────
            lblXacNhan.Text      = "Xác Nhận Mật Khẩu";
            lblXacNhan.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            lblXacNhan.ForeColor = T.TextSub;
            lblXacNhan.Location  = new Point(46, 290);
            lblXacNhan.Size      = new Size(180, 18);
            lblXacNhan.BackColor = Color.Transparent;

            textBox3.Location       = new Point(46, 310);
            textBox3.Size           = new Size(424, 44);
            textBox3.Font           = new Font("Segoe UI", 10.5f);
            textBox3.BackColor      = Color.FromArgb(240, 253, 248);
            textBox3.ForeColor      = T.TextMain;
            textBox3.BorderStyle    = BorderStyle.FixedSingle;
            textBox3.Multiline      = true;
            textBox3.PasswordChar   = '●';
            textBox3.TabIndex       = 2;
            textBox3.PlaceholderText= "Nhập lại mật khẩu";

            // ── Error label ───────────────────────────────────────
            lblError.Text      = "";
            lblError.Font      = new Font("Segoe UI", 8.5f);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location  = new Point(46, 358);
            lblError.Size      = new Size(424, 18);
            lblError.BackColor = Color.Transparent;
            lblError.Visible   = false;

            // ── Nút Đăng Ký ───────────────────────────────────────
            button1.Text        = "ĐĂNG KÝ";
            button1.Font        = new Font("Segoe UI", 11f, FontStyle.Bold);
            button1.Location    = new Point(46, 380);
            button1.Size        = new Size(424, 48);
            button1.BackColor   = T.Primary;
            button1.ForeColor   = Color.White;
            button1.FlatStyle   = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize         = 0;
            button1.FlatAppearance.MouseOverBackColor = T.RedHover;
            button1.FlatAppearance.MouseDownBackColor = T.RedDark;
            button1.Cursor      = Cursors.Hand;
            button1.TabIndex    = 3;

            // ── Link đã có tài khoản ──────────────────────────────
            label5.Text      = "Đã có tài khoản? Đăng nhập ngay";
            label5.Font      = new Font("Segoe UI", 9f, FontStyle.Underline);
            label5.ForeColor = T.Primary;
            label5.Location  = new Point(46, 440);
            label5.Size      = new Size(424, 22);
            label5.TextAlign = ContentAlignment.MiddleCenter;
            label5.BackColor = Color.Transparent;

            // Ghi chú nhỏ
            lblGhiChu.Text      = "ℹ  Tên đăng nhập sẽ là mã sinh viên tìm thấy trong hệ thống";
            lblGhiChu.Font      = new Font("Segoe UI", 7.5f);
            lblGhiChu.ForeColor = T.TextSub;
            lblGhiChu.Location  = new Point(46, 466);
            lblGhiChu.Size      = new Size(424, 18);
            lblGhiChu.BackColor = Color.Transparent;

            // Add to panelRight
            panelRight.Controls.AddRange(new Control[]
            {
                lblTitle, lblSubTitle, panelDiv,
                lblHoTen, textBox1,
                lblMatKhau, textBox2,
                lblXacNhan, textBox3,
                lblError, button1, label5, lblGhiChu
            });

            // Hover link
            label5.MouseEnter += (s, e) => ((Label)s).ForeColor = T.Accent;
            label5.MouseLeave += (s, e) => ((Label)s).ForeColor = T.Primary;

            // Ẩn error khi gõ
            textBox1.TextChanged += (s, e) => lblError.Visible = false;
            textBox2.TextChanged += (s, e) => lblError.Visible = false;
            textBox3.TextChanged += (s, e) => lblError.Visible = false;

            Controls.Add(panelLeft);
            Controls.Add(panelRight);

            panelLeft.ResumeLayout(false);
            panelRight.ResumeLayout(false);
            ResumeLayout(false);
        }

        private static void PanelLeft_Paint(object s, PaintEventArgs e)
        {
            var p = (Panel)s;
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var r = p.ClientRectangle;

            // Gradient xanh emerald
            using var bgBr = new System.Drawing.Drawing2D.LinearGradientBrush(r,
                Color.FromArgb(3, 102, 74), Color.FromArgb(6, 160, 112),
                System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal);
            g.FillRectangle(bgBr, r);

            // Grid overlay nhẹ
            using var linePen = new Pen(Color.FromArgb(25, 255, 255, 255), 1);
            for (int y = 0; y < r.Height; y += 38) g.DrawLine(linePen, 0, y, r.Width, y);
            for (int x = 0; x < r.Width; x += 38) g.DrawLine(linePen, x, 0, x, r.Height);

            // Logo
            int cx = r.Width / 2, cy = r.Height / 2 - 50;
            using var cirBr = new SolidBrush(Color.FromArgb(35, 255, 255, 255));
            g.FillEllipse(cirBr, cx - 52, cy - 52, 104, 104);
            using var cirPen = new Pen(Color.FromArgb(180, 255, 255, 255), 2.5f);
            g.DrawEllipse(cirPen, cx - 52, cy - 52, 104, 104);
            using var iFont = new Font("Segoe UI Emoji", 28f);
            var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString("📝", iFont, Brushes.White, new RectangleF(cx - 52, cy - 52, 104, 104), sf);

            // Text
            using var tFont = new Font("Segoe UI", 14f, FontStyle.Bold);
            g.DrawString("TẠO TÀI KHOẢN", tFont, Brushes.White,
                new RectangleF(0, cy + 65, r.Width, 34), sf);
            using var sFont = new Font("Segoe UI", 9f);
            using var sBr   = new SolidBrush(Color.FromArgb(200, 255, 255, 255));
            g.DrawString("Hệ Thống Quản Lý Điểm Sinh Viên", sFont, sBr,
                new RectangleF(0, cy + 102, r.Width, 22), sf);

            // Đường phân cách
            using var sep = new Pen(Color.FromArgb(120, 255, 255, 255), 1.5f);
            g.DrawLine(sep, 60, cy + 130, r.Width - 60, cy + 130);

            // Version
            using var vBr   = new SolidBrush(Color.FromArgb(80, 255, 255, 255));
            using var vFont = new Font("Segoe UI", 7.5f);
            g.DrawString("v2.0 Premium Edition", vFont, vBr,
                new RectangleF(0, r.Height - 28, r.Width, 22), sf);
        }

        private static void PanelRight_Paint(object s, PaintEventArgs e)
        {
            var p = (Panel)s;
            var g = e.Graphics;
            g.FillRectangle(new SolidBrush(Color.FromArgb(244, 246, 248)), p.ClientRectangle);
            // Viền trái teal
            using var pen = new Pen(T.Primary, 3);
            g.DrawLine(pen, 0, 20, 0, p.Height - 20);
        }

        private Panel    panelLeft, panelRight, panelDiv;
        private Label    lblTitle, lblSubTitle, lblHoTen, lblMatKhau, lblXacNhan, lblGhiChu, lblError;
        private TextBox  textBox1, textBox2, textBox3;
        private Button   button1;
        private Label    label5;
    }
}
