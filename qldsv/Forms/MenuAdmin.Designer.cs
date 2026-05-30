using qldsv.Service;

namespace qldsv
{
    partial class MenuAdmin
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelHeader  = new Panel();
            panelSidebar = new Panel();
            panel3       = new Panel();  // content (cards + charts)
            panel9       = new Panel();  // recent activity

            lblRole  = new Label();
            label2   = new Label();      // greeting "Xin chào, Admin"
            lblSubHd = new Label();
            button11 = new Button();     // X
            button12 = new Button();     // settings

            // Sidebar section labels
            label1 = new Label();        // NGƯỜI DÙNG
            label3 = new Label();        // HỌC TẬP
            label4 = new Label();        // BÁO CÁO

            // Sidebar buttons
            button9  = new Button(); // Sinh Viên
            button1  = new Button(); // Giảng Viên
            button2  = new Button(); // Tài Khoản
            button3  = new Button(); // Phân Quyền
            button4  = new Button(); // Môn Học
            button5  = new Button(); // Lớp Học Phần
            button6  = new Button(); // Nhập Điểm
            button7  = new Button(); // Bảng Điểm
            button8  = new Button(); // Thống Kê
            button10 = new Button(); // Xuất Báo Cáo

            panelHeader.SuspendLayout();
            panelSidebar.SuspendLayout();
            SuspendLayout();

            // ── Form ──────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(1010, 680);
            StartPosition       = FormStartPosition.CenterScreen;
            Name                = "MenuAdmin";
            Text                = "Menu Quản Trị";
            BackColor           = Color.FromArgb(244, 246, 248);
            Font                = new Font("Segoe UI", 9.5f);

            // ══════════ HEADER ══════════
            panelHeader.Dock      = DockStyle.Top;
            panelHeader.Height    = 64;
            panelHeader.BackColor = T.Primary;
            panelHeader.Paint    += PaintHeader;

            lblRole.Text      = "QUẢN TRỊ VIÊN";
            lblRole.Font      = new Font("Segoe UI", 8f, FontStyle.Bold);
            lblRole.ForeColor  = Color.FromArgb(180, 255, 255, 255);
            lblRole.BackColor  = Color.Transparent;
            lblRole.Location   = new Point(20, 10);
            lblRole.Size       = new Size(120, 16);

            label2.Text      = "Xin chào, Admin";
            label2.Font      = new Font("Segoe UI", 14f, FontStyle.Bold);
            label2.ForeColor  = Color.White;
            label2.BackColor  = Color.Transparent;
            label2.Location   = new Point(20, 27);
            label2.Size       = new Size(500, 28);

            lblSubHd.Text      = "Bảng điều khiển hệ thống quản lý điểm";
            lblSubHd.Font      = new Font("Segoe UI", 9f);
            lblSubHd.ForeColor  = Color.FromArgb(200, 255, 255, 255);
            lblSubHd.BackColor  = Color.Transparent;
            lblSubHd.Location   = new Point(560, 28);
            lblSubHd.Size       = new Size(360, 24);
            lblSubHd.TextAlign  = ContentAlignment.MiddleRight;

            StyleIconBtn(button12, "⚙", 920, 16);
            StyleIconBtn(button11, "✕", 966, 16, Color.FromArgb(255, 200, 200));

            panelHeader.Controls.AddRange(new Control[]
                { lblRole, label2, lblSubHd, button12, button11 });

            // ══════════ SIDEBAR ══════════
            panelSidebar.Location  = new Point(0, 64);
            panelSidebar.Size      = new Size(200, 616);
            panelSidebar.BackColor = Color.FromArgb(4, 100, 72);
            panelSidebar.Paint    += PaintSidebar;

            var lblAvatar = new Label
            {
                Text      = "🛡️",
                Font      = new Font("Segoe UI Emoji", 22f),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(40, 255, 255, 255),
                Location  = new Point(72, 16),
                Size      = new Size(56, 56),
                TextAlign = ContentAlignment.MiddleCenter
            };
            lblAvatar.Region = new Region(T.RoundedRect(new Rectangle(0, 0, 56, 56), 28));

            SetupSection(label1, "NGƯỜI DÙNG", 84);
            StyleSideBtn(button9, "🎓   Sinh viên",   106, true);
            StyleSideBtn(button1, "👨‍🏫   Giảng viên",  142);
            StyleSideBtn(button2, "🔐   Tài khoản",   178);
            StyleSideBtn(button3, "⚖️   Phân quyền",  214);

            SetupSection(label3, "HỌC TẬP", 256);
            StyleSideBtn(button4, "📖   Môn học",      278);
            StyleSideBtn(button5, "🏫   Lớp học phần", 314);
            StyleSideBtn(button6, "✏️   Nhập điểm",     350);
            StyleSideBtn(button7, "📊   Bảng điểm",    386);

            SetupSection(label4, "BÁO CÁO", 428);
            StyleSideBtn(button8,  "📈   Thống kê",     450);
            StyleSideBtn(button10, "📥   Xuất báo cáo", 486);

            panelSidebar.Controls.AddRange(new Control[]
                { lblAvatar, label1, label3, label4,
                  button9, button1, button2, button3,
                  button4, button5, button6, button7,
                  button8, button10 });

            // ══════════ CONTENT (panel3) ══════════
            panel3.Location   = new Point(208, 78);
            panel3.Size       = new Size(580, 588);
            panel3.BackColor  = Color.Transparent;
            panel3.AutoScroll = true;

            // ══════════ RECENT ACTIVITY (panel9) ══════════
            panel9.Location   = new Point(796, 78);
            panel9.Size       = new Size(202, 588);
            panel9.BackColor  = Color.White;

            Controls.Add(panel3);
            Controls.Add(panel9);
            Controls.Add(panelSidebar);
            Controls.Add(panelHeader);

            panelHeader.ResumeLayout(false);
            panelSidebar.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Helpers ───────────────────────────────────────────────
        private static void SetupSection(Label lbl, string text, int y)
        {
            lbl.Text      = text;
            lbl.Font      = new Font("Segoe UI", 8f, FontStyle.Bold);
            lbl.ForeColor  = Color.FromArgb(150, 255, 255, 255);
            lbl.BackColor  = Color.Transparent;
            lbl.Location   = new Point(18, y);
            lbl.Size       = new Size(170, 18);
        }

        private static void StyleIconBtn(Button btn, string text, int x, int y, Color? fg = null)
        {
            btn.Text = text;
            btn.Font = new Font("Segoe UI", 12f);
            btn.Location = new Point(x, y);
            btn.Size = new Size(36, 32);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 255, 255, 255);
            btn.BackColor = Color.Transparent;
            btn.ForeColor = fg ?? Color.White;
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleCenter;
        }

        private static void StyleSideBtn(Button btn, string text, int y, bool selected = false)
        {
            btn.Text = text;
            btn.Font = new Font("Segoe UI", 9.5f, selected ? FontStyle.Bold : FontStyle.Regular);
            btn.Location = new Point(0, y);
            btn.Size = new Size(200, 34);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 255, 255, 255);
            btn.BackColor = selected ? Color.FromArgb(60, 255, 255, 255) : Color.Transparent;
            btn.ForeColor = Color.White;
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(14, 0, 0, 0);
        }

        private static void PaintHeader(object s, PaintEventArgs e)
        {
            var p = (Panel)s; var g = e.Graphics;
            using var br = new System.Drawing.Drawing2D.LinearGradientBrush(p.ClientRectangle,
                Color.FromArgb(4, 120, 87), Color.FromArgb(5, 150, 105),
                System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            g.FillRectangle(br, p.ClientRectangle);
            using var sh = new System.Drawing.Drawing2D.LinearGradientBrush(
                new Rectangle(0, p.Height-5, p.Width, 5),
                Color.FromArgb(30, 0, 0, 0), Color.Transparent,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            g.FillRectangle(sh, 0, p.Height-5, p.Width, 5);
        }

        private static void PaintSidebar(object s, PaintEventArgs e)
        {
            var p = (Panel)s; var g = e.Graphics;
            using var br = new System.Drawing.Drawing2D.LinearGradientBrush(p.ClientRectangle,
                Color.FromArgb(3, 90, 65), Color.FromArgb(5, 120, 88),
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            g.FillRectangle(br, p.ClientRectangle);
            using var pen = new Pen(Color.FromArgb(60, 255, 255, 255), 1);
            g.DrawLine(pen, p.Width-1, 10, p.Width-1, p.Height-10);
        }

        // Fields
        private Panel  panelHeader, panelSidebar, panel3, panel9;
        private Label  lblRole, label2, lblSubHd, label1, label3, label4;
        private Button button9, button1, button2, button3, button4, button5,
                       button6, button7, button8, button10, button11, button12;
    }
}
