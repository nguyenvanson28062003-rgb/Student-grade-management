using qldsv.Service;

namespace quản_lí_điểm_sinh_viên
{
    partial class DoiMK
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            panelLeft  = new Panel();
            panelRight = new Panel();
            panelDiv   = new Panel();

            lblTitle    = new Label();
            lblSubTitle = new Label();
            lblUser     = new Label();
            lblCurPw    = new Label();
            lblNewPw    = new Label();
            lblError    = new Label();
            label5      = new Label();
            lblQuen     = new Label();

            textBox1     = new TextBox();
            textBox2     = new TextBox();
            textBox3     = new TextBox();
            btnDangnhap  = new Button();

            panelLeft.SuspendLayout();
            panelRight.SuspendLayout();
            SuspendLayout();

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(860, 480);
            FormBorderStyle     = FormBorderStyle.None;
            StartPosition       = FormStartPosition.CenterScreen;
            BackColor           = T.BG;
            Name                = "DoiMK";
            Text                = "Đổi Mật Khẩu";
            Opacity             = 0;

            panelLeft.Location  = new Point(0, 0);
            panelLeft.Size      = new Size(340, 480);
            panelLeft.BackColor = Color.Transparent;
            panelLeft.Paint    += PanelLeft_Paint;

            panelRight.Location  = new Point(340, 0);
            panelRight.Size      = new Size(520, 480);
            panelRight.BackColor = Color.FromArgb(244, 246, 248);
            panelRight.Paint    += PanelRight_Paint;

            lblTitle.Text      = "ĐỔI MẬT KHẨU";
            lblTitle.Font      = new Font("Segoe UI", 18f, FontStyle.Bold);
            lblTitle.ForeColor = T.TextMain;
            lblTitle.Location  = new Point(46, 46);
            lblTitle.Size      = new Size(420, 38);
            lblTitle.BackColor = Color.Transparent;

            lblSubTitle.Text      = "Nhập thông tin để đổi mật khẩu tài khoản";
            lblSubTitle.Font      = new Font("Segoe UI", 9.5f);
            lblSubTitle.ForeColor = T.TextSub;
            lblSubTitle.Location  = new Point(46, 88);
            lblSubTitle.Size      = new Size(420, 20);
            lblSubTitle.BackColor = Color.Transparent;

            panelDiv.Location  = new Point(46, 114);
            panelDiv.Size      = new Size(60, 3);
            panelDiv.BackColor = T.Primary;

            lblUser.Text      = "Tên Đăng Nhập";
            lblUser.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            lblUser.ForeColor = T.TextSub;
            lblUser.Location  = new Point(46, 128);
            lblUser.Size      = new Size(160, 18);
            lblUser.BackColor = Color.Transparent;

            textBox1.Location        = new Point(46, 148);
            textBox1.Size            = new Size(424, 44);
            textBox1.Font            = new Font("Segoe UI", 10.5f);
            textBox1.BackColor       = Color.FromArgb(240, 253, 248);
            textBox1.ForeColor       = T.TextMain;
            textBox1.BorderStyle     = BorderStyle.FixedSingle;
            textBox1.Multiline       = true;
            textBox1.TabIndex        = 0;
            textBox1.PlaceholderText = "Nhập tên đăng nhập của bạn";

            lblCurPw.Text      = "Mật Khẩu Hiện Tại";
            lblCurPw.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            lblCurPw.ForeColor = T.TextSub;
            lblCurPw.Location  = new Point(46, 206);
            lblCurPw.Size      = new Size(180, 18);
            lblCurPw.BackColor = Color.Transparent;

            textBox2.Location        = new Point(46, 226);
            textBox2.Size            = new Size(424, 44);
            textBox2.Font            = new Font("Segoe UI", 10.5f);
            textBox2.BackColor       = Color.FromArgb(240, 253, 248);
            textBox2.ForeColor       = T.TextMain;
            textBox2.BorderStyle     = BorderStyle.FixedSingle;
            textBox2.Multiline       = true;
            textBox2.PasswordChar    = '●';
            textBox2.TabIndex        = 1;
            textBox2.PlaceholderText = "Nhập mật khẩu hiện tại";

            lblNewPw.Text      = "Mật Khẩu Mới";
            lblNewPw.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            lblNewPw.ForeColor = T.TextSub;
            lblNewPw.Location  = new Point(46, 284);
            lblNewPw.Size      = new Size(160, 18);
            lblNewPw.BackColor = Color.Transparent;

            textBox3.Location        = new Point(46, 304);
            textBox3.Size            = new Size(424, 44);
            textBox3.Font            = new Font("Segoe UI", 10.5f);
            textBox3.BackColor       = Color.FromArgb(240, 253, 248);
            textBox3.ForeColor       = T.TextMain;
            textBox3.BorderStyle     = BorderStyle.FixedSingle;
            textBox3.Multiline       = true;
            textBox3.PasswordChar    = '●';
            textBox3.TabIndex        = 2;
            textBox3.PlaceholderText = "Tối thiểu 6 ký tự";

            lblError.Text      = "";
            lblError.Font      = new Font("Segoe UI", 8.5f);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location  = new Point(46, 354);
            lblError.Size      = new Size(424, 18);
            lblError.BackColor = Color.Transparent;
            lblError.Visible   = false;

            btnDangnhap.Text        = "XÁC NHẬN ĐỔI MẬT KHẨU";
            btnDangnhap.Font        = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            btnDangnhap.Location    = new Point(46, 378);
            btnDangnhap.Size        = new Size(424, 48);
            btnDangnhap.BackColor   = T.Primary;
            btnDangnhap.ForeColor   = Color.White;
            btnDangnhap.FlatStyle   = FlatStyle.Flat;
            btnDangnhap.FlatAppearance.BorderSize         = 0;
            btnDangnhap.FlatAppearance.MouseOverBackColor = T.RedHover;
            btnDangnhap.FlatAppearance.MouseDownBackColor = T.RedDark;
            btnDangnhap.Cursor      = Cursors.Hand;
            btnDangnhap.TabIndex    = 3;

            lblQuen.Text      = "✕  Huỷ bỏ";
            lblQuen.Font      = new Font("Segoe UI", 9f, FontStyle.Underline);
            lblQuen.ForeColor = T.TextSub;
            lblQuen.Location  = new Point(46, 440);
            lblQuen.Size      = new Size(200, 22);
            lblQuen.BackColor = Color.Transparent;
            lblQuen.Cursor    = Cursors.Hand;

            label5.Text      = "Về trang đăng nhập →";
            label5.Font      = new Font("Segoe UI", 9f, FontStyle.Underline);
            label5.ForeColor = T.Primary;
            label5.Location  = new Point(270, 440);
            label5.Size      = new Size(200, 22);
            label5.TextAlign = ContentAlignment.MiddleRight;
            label5.BackColor = Color.Transparent;
            label5.Cursor    = Cursors.Hand;

            // Hover
            lblQuen.MouseEnter += (s, e) => ((Label)s).ForeColor = T.TextMain;
            lblQuen.MouseLeave += (s, e) => ((Label)s).ForeColor = T.TextSub;
            label5.MouseEnter  += (s, e) => ((Label)s).ForeColor = T.Accent;
            label5.MouseLeave  += (s, e) => ((Label)s).ForeColor = T.Primary;

            // Ẩn error khi gõ
            textBox1.TextChanged += (s, e) => lblError.Visible = false;
            textBox2.TextChanged += (s, e) => lblError.Visible = false;
            textBox3.TextChanged += (s, e) => lblError.Visible = false;

            panelRight.Controls.AddRange(new Control[]
            {
                lblTitle, lblSubTitle, panelDiv,
                lblUser,   textBox1,
                lblCurPw,  textBox2,
                lblNewPw,  textBox3,
                lblError,  btnDangnhap,
                lblQuen,   label5
            });

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

            using var bgBr = new System.Drawing.Drawing2D.LinearGradientBrush(r,
                Color.FromArgb(3, 102, 74), Color.FromArgb(6, 160, 112),
                System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal);
            g.FillRectangle(bgBr, r);

            using var linePen = new Pen(Color.FromArgb(20, 255, 255, 255), 1);
            for (int y = 0; y < r.Height; y += 38) g.DrawLine(linePen, 0, y, r.Width, y);
            for (int x = 0; x < r.Width; x += 38) g.DrawLine(linePen, x, 0, x, r.Height);

            int cx = r.Width / 2, cy = r.Height / 2 - 50;
            using var cirBr  = new SolidBrush(Color.FromArgb(35, 255, 255, 255));
            g.FillEllipse(cirBr, cx - 52, cy - 52, 104, 104);
            using var cirPen = new Pen(Color.FromArgb(160, 255, 255, 255), 2.5f);
            g.DrawEllipse(cirPen, cx - 52, cy - 52, 104, 104);

            using var iFont = new Font("Segoe UI Emoji", 28f);
            var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString("🔑", iFont, Brushes.White, new RectangleF(cx - 52, cy - 52, 104, 104), sf);

            using var tFont = new Font("Segoe UI", 14f, FontStyle.Bold);
            g.DrawString("ĐỔI MẬT KHẨU", tFont, Brushes.White,
                new RectangleF(0, cy + 65, r.Width, 34), sf);

            using var sFont = new Font("Segoe UI", 9f);
            using var sBr   = new SolidBrush(Color.FromArgb(200, 255, 255, 255));
            g.DrawString("Bảo mật tài khoản của bạn", sFont, sBr,
                new RectangleF(0, cy + 102, r.Width, 22), sf);

            using var sep = new Pen(Color.FromArgb(100, 255, 255, 255), 1.5f);
            g.DrawLine(sep, 60, cy + 128, r.Width - 60, cy + 128);

            // Tips bảo mật
            string[] tips = { "✔ Tối thiểu 6 ký tự", "✔ Không chia sẻ mật khẩu", "✔ Nên có chữ + số" };
            using var tipFont = new Font("Segoe UI", 8.5f);
            using var tipBr   = new SolidBrush(Color.FromArgb(180, 255, 255, 255));
            for (int i = 0; i < tips.Length; i++)
                g.DrawString(tips[i], tipFont, tipBr,
                    new RectangleF(40, cy + 140 + i * 22, r.Width - 80, 20));
        }

        private static void PanelRight_Paint(object s, PaintEventArgs e)
        {
            var p = (Panel)s;
            var g = e.Graphics;
            g.FillRectangle(new SolidBrush(Color.FromArgb(244, 246, 248)), p.ClientRectangle);
            using var pen = new Pen(T.Primary, 3);
            g.DrawLine(pen, 0, 20, 0, p.Height - 20);
        }

        private Panel    panelLeft, panelRight, panelDiv;
        private Label    lblTitle, lblSubTitle, lblUser, lblCurPw, lblNewPw, lblError;
        private TextBox  textBox1, textBox2, textBox3;
        private Button   btnDangnhap;
        private Label    label5, lblQuen;
    }
}
