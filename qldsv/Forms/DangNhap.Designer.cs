using qldsv.Service;

namespace quản_lí_điểm_sinh_viên
{
    partial class DangNhap
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            panelLeft    = new Panel();
            panelRight   = new Panel();
            lblLogin     = new Label();
            lblSubTitle  = new Label();
            lblUserIcon  = new Label();
            lblPassIcon  = new Label();
            txtUser      = new PremiumTextBox();
            txtPass      = new PremiumTextBox();
            btnDangnhap  = new PremiumButton();
            lblError     = new Label();
            lblDangki    = new Label();
            lblQuen      = new Label();
            lblOr        = new Label();
            lbllogin     = new Label();
            panelDivider = new Panel();

            panelLeft.SuspendLayout();
            panelRight.SuspendLayout();
            SuspendLayout();

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(860, 500);
            FormBorderStyle     = FormBorderStyle.None;
            StartPosition       = FormStartPosition.CenterScreen;
            BackColor           = T.BG;
            Name                = "DangNhap";
            Text                = "Đăng Nhập";

            panelLeft.Location    = new Point(0, 0);
            panelLeft.Size        = new Size(350, 500);
            panelLeft.BackColor   = Color.Transparent;
            panelLeft.Paint      += panelLeft_Paint;

            panelRight.Location   = new Point(350, 0);
            panelRight.Size       = new Size(510, 500);
            panelRight.BackColor  = Color.FromArgb(244, 246, 248);
            panelRight.Paint     += panelRight_Paint;

            lblLogin.Text          = "ĐĂNG NHẬP";
            lblLogin.Font          = new Font("Segoe UI", 22f, FontStyle.Bold);
            lblLogin.ForeColor     = T.TextMain;
            lblLogin.Location      = new Point(50, 60);
            lblLogin.Size          = new Size(410, 45);
            lblLogin.TextAlign     = ContentAlignment.MiddleLeft;

            lblSubTitle.Text       = "Chào mừng bạn quay lại hệ thống";
            lblSubTitle.Font       = new Font("Segoe UI", 9.5f);
            lblSubTitle.ForeColor  = T.TextSub;
            lblSubTitle.Location   = new Point(50, 108);
            lblSubTitle.Size       = new Size(410, 22);

            // Đường kẻ đỏ dưới tiêu đề
            panelDivider.Location  = new Point(50, 136);
            panelDivider.Size      = new Size(60, 3);
            panelDivider.BackColor = T.Primary;

            lblUserIcon.Text       = "👤";
            lblUserIcon.Font       = new Font("Segoe UI", 11f);
            lblUserIcon.ForeColor  = T.Primary;
            lblUserIcon.Location   = new Point(50, 165);
            lblUserIcon.Size       = new Size(30, 20);
            lblUserIcon.TextAlign  = ContentAlignment.MiddleCenter;

            lblPassIcon.Text       = "🔒";
            lblPassIcon.Font       = new Font("Segoe UI", 11f);
            lblPassIcon.ForeColor  = T.Primary;
            lblPassIcon.Location   = new Point(50, 255);
            lblPassIcon.Size       = new Size(30, 20);
            lblPassIcon.TextAlign  = ContentAlignment.MiddleCenter;

            lbllogin.Text          = "Tên đăng nhập";
            lbllogin.Font          = new Font("Segoe UI", 9f, FontStyle.Bold);
            lbllogin.ForeColor     = T.TextSub;
            lbllogin.Location      = new Point(84, 162);
            lbllogin.Size          = new Size(200, 20);

            var lblPassLbl = new Label
            {
                Text      = "Mật khẩu",
                Font      = new Font("Segoe UI", 9f, FontStyle.Bold),
                ForeColor = T.GrayLight,
                Location  = new Point(84, 252),
                Size      = new Size(200, 20)
            };

            txtUser.Location    = new Point(50, 185);
            txtUser.Size        = new Size(410, 48);
            txtUser.Placeholder = "Nhập tên đăng nhập...";

            txtPass.Location    = new Point(50, 275);
            txtPass.Size        = new Size(410, 48);
            txtPass.Placeholder = "Nhập mật khẩu...";
            txtPass.IsPassword  = true;

            lblError.Text      = "";
            lblError.Font      = new Font("Segoe UI", 8.5f);
            lblError.ForeColor = Color.FromArgb(220, 53, 69);
            lblError.Location  = new Point(50, 330);
            lblError.Size      = new Size(410, 20);
            lblError.Visible   = false;

            btnDangnhap.Text       = "ĐĂNG NHẬP";
            btnDangnhap.Font       = new Font("Segoe UI", 11f, FontStyle.Bold);
            btnDangnhap.Location   = new Point(50, 358);
            btnDangnhap.Size       = new Size(410, 50);
            btnDangnhap.CornerRadius = 8;
            btnDangnhap.BaseColor  = T.Primary;
            btnDangnhap.Click     += btnDangnhap_Click;

            lblOr.Text             = "─────────  hoặc  ─────────";
            lblOr.Font             = new Font("Segoe UI", 8f);
            lblOr.ForeColor        = T.TextSub;
            lblOr.Location         = new Point(50, 418);
            lblOr.Size             = new Size(410, 20);
            lblOr.TextAlign        = ContentAlignment.MiddleCenter;

            lblDangki.Text         = "Đăng ký tài khoản";
            lblDangki.Font         = new Font("Segoe UI", 9f, FontStyle.Underline);
            lblDangki.ForeColor    = T.Primary;
            lblDangki.Location     = new Point(50, 444);
            lblDangki.Size         = new Size(160, 24);
            lblDangki.TextAlign    = ContentAlignment.MiddleLeft;
            lblDangki.Cursor       = Cursors.Hand;
            lblDangki.Click       += lblDangki_Click;

            lblQuen.Text           = "Quên mật khẩu?";
            lblQuen.Font           = new Font("Segoe UI", 9f, FontStyle.Underline);
            lblQuen.ForeColor      = T.TextSub;
            lblQuen.Location       = new Point(300, 444);
            lblQuen.Size           = new Size(160, 24);
            lblQuen.TextAlign      = ContentAlignment.MiddleRight;
            lblQuen.Cursor         = Cursors.Hand;
            lblQuen.Click         += lblQuen_Click;

            panelRight.Controls.AddRange(new Control[] {
                lblLogin, lblSubTitle, panelDivider,
                lblUserIcon, lbllogin, txtUser,
                lblPassIcon, lblPassLbl, txtPass,
                lblError, btnDangnhap,
                lblOr, lblDangki, lblQuen
            });

            Controls.Add(panelLeft);
            Controls.Add(panelRight);

            // Hover effect cho links
            lblDangki.MouseEnter += (s, e) => ((Label)s).ForeColor = T.Accent;
            lblDangki.MouseLeave += (s, e) => ((Label)s).ForeColor = T.Primary;
            lblQuen.MouseEnter   += (s, e) => ((Label)s).ForeColor = T.Primary;
            lblQuen.MouseLeave   += (s, e) => ((Label)s).ForeColor = T.TextSub;

            // Ẩn error khi gõ
            txtUser.TextChanged += (s, e) => lblError.Visible = false;
            txtPass.TextChanged += (s, e) => lblError.Visible = false;

            panelLeft.ResumeLayout(false);
            panelRight.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Panel          panelLeft, panelRight, panelDivider;
        private Label          lblLogin, lblSubTitle, lbllogin, lblError;
        private Label          lblDangki, lblQuen, lblOr;
        private Label          lblUserIcon, lblPassIcon;
        private PremiumTextBox txtUser, txtPass;
        private PremiumButton  btnDangnhap;
    }
}
