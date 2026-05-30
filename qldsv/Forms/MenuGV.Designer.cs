using qldsv.Service;

namespace qldsv
{
    partial class MenuGV
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
            panelContent = new Panel();
            panelGreet   = new Panel();
            panelCards   = new Panel();
            panelTable   = new Panel();
            panelHistory = new Panel();

            lblRole  = new Label();
            label29  = new Label();
            label31  = new Label();
            button11 = new Button();
            button12 = new Button();

            button5  = new Button(); // Lớp HP
            button6  = new Button(); // Danh sách SV
            button7  = new Button(); // Nhập điểm
            button8  = new Button(); // Xem bảng điểm
            button10 = new Button(); // Thống kê lớp
            button4  = new Button(); // Lịch sử nhập điểm (mới)
            button1  = new Button(); // Xuất điểm
            button2  = new Button(); // Hồ sơ
            button3  = new Button(); // Đổi MK
            label1   = new Label();  // section Giảng dạy
            label2   = new Label();  // section Quản lý điểm
            label3   = new Label();  // section Cá nhân

            label30 = new Label(); // greeting
            label4  = new Label(); // role badge

            panel28 = new Panel(); label45 = new Label(); label44 = new Label();
            panel27 = new Panel(); label43 = new Label(); label42 = new Label();
            panel26 = new Panel(); label41 = new Label(); label40 = new Label();
            panel25 = new Panel(); label39 = new Label(); label38 = new Label();

            lblTbl    = new Label();
            lblHis    = new Label();
            dataGridView1 = new DataGridView();
            dgvLichSu     = new DataGridView();
            cotMHP = new DataGridViewTextBoxColumn();
            cotMH  = new DataGridViewTextBoxColumn();
            cotSS  = new DataGridViewTextBoxColumn();
            cotTT  = new DataGridViewTextBoxColumn();

            panelHeader.SuspendLayout();
            panelSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvLichSu).BeginInit();
            SuspendLayout();

            // ── Form ──────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(980, 680);
            StartPosition       = FormStartPosition.CenterScreen;
            Name                = "MenuGV";
            Text                = "Menu Giảng Viên";
            BackColor           = Color.FromArgb(244, 246, 248);
            Font                = new Font("Segoe UI", 9.5f);

            // ══════════ HEADER ══════════
            panelHeader.Dock      = DockStyle.Top;
            panelHeader.Height    = 64;
            panelHeader.BackColor = T.Primary;
            panelHeader.Paint    += PaintHeader;

            lblRole.Text      = "GIẢNG VIÊN";
            lblRole.Font      = new Font("Segoe UI", 8f, FontStyle.Bold);
            lblRole.ForeColor  = Color.FromArgb(180, 255, 255, 255);
            lblRole.BackColor  = Color.Transparent;
            lblRole.Location   = new Point(20, 10);
            lblRole.Size       = new Size(110, 16);

            label29.Text      = "";
            label29.Font      = new Font("Segoe UI", 14f, FontStyle.Bold);
            label29.ForeColor  = Color.White;
            label29.BackColor  = Color.Transparent;
            label29.Location   = new Point(20, 27);
            label29.Size       = new Size(420, 28);

            label31.Text      = "";
            label31.Font      = new Font("Segoe UI", 10f);
            label31.ForeColor  = Color.FromArgb(210, 255, 255, 255);
            label31.BackColor  = Color.Transparent;
            label31.Location   = new Point(450, 28);
            label31.Size       = new Size(280, 24);

            StyleIconBtn(button12, "⚙", 890, 16);
            StyleIconBtn(button11, "✕", 936, 16, Color.FromArgb(255, 200, 200));

            panelHeader.Controls.AddRange(new Control[]
                { lblRole, label29, label31, button12, button11 });

            // ══════════ SIDEBAR ══════════
            panelSidebar.Location  = new Point(0, 64);
            panelSidebar.Size      = new Size(210, 616);
            panelSidebar.BackColor = Color.FromArgb(4, 100, 72);
            panelSidebar.Paint    += PaintSidebar;

            var lblAvatar = new Label
            {
                Text      = "👨‍🏫",
                Font      = new Font("Segoe UI Emoji", 24f),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(40, 255, 255, 255),
                Location  = new Point(75, 22),
                Size      = new Size(60, 60),
                TextAlign = ContentAlignment.MiddleCenter
            };
            lblAvatar.Region = new Region(T.RoundedRect(new Rectangle(0, 0, 60, 60), 30));

            label1.Text = "GIẢNG DẠY";
            label1.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(150, 255, 255, 255);
            label1.BackColor = Color.Transparent;
            label1.Location  = new Point(20, 100);
            label1.Size      = new Size(170, 18);

            label2.Text = "QUẢN LÝ ĐIỂM";
            label2.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(150, 255, 255, 255);
            label2.BackColor = Color.Transparent;
            label2.Location  = new Point(20, 226);
            label2.Size      = new Size(170, 18);

            label3.Text = "CÁ NHÂN";
            label3.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(150, 255, 255, 255);
            label3.BackColor = Color.Transparent;
            label3.Location  = new Point(20, 446);
            label3.Size      = new Size(170, 18);

            StyleSideBtn(button5,  "📚   Lớp học phần",     124, true);
            StyleSideBtn(button6,  "👥   Danh sách SV",     166);
            StyleSideBtn(button7,  "✏️   Nhập điểm",         250);
            StyleSideBtn(button8,  "📊   Xem bảng điểm",     292);
            StyleSideBtn(button10, "📈   Thống kê lớp",      334);
            StyleSideBtn(button4,  "🕘   Lịch sử nhập điểm", 376);
            StyleSideBtn(button2,  "👤   Hồ sơ cá nhân",     470);
            StyleSideBtn(button3,  "🔑   Đổi mật khẩu",      512);

            panelSidebar.Controls.AddRange(new Control[]
                { lblAvatar, label1, label2, label3,
                  button5, button6, button7, button8, button10, button4, button2, button3 });

            // ══════════ CONTENT ══════════
            panelContent.Location   = new Point(210, 64);
            panelContent.Size       = new Size(770, 616);
            panelContent.BackColor  = Color.FromArgb(244, 246, 248);
            panelContent.AutoScroll = true;
            panelContent.Padding    = new Padding(24, 20, 24, 20);

            // Greeting
            panelGreet.Location  = new Point(24, 20);
            panelGreet.Size      = new Size(716, 72);
            panelGreet.BackColor = Color.White;
            panelGreet.Paint    += PaintGreetCard;

            label30.Text      = "Xin chào! 👋";
            label30.Font      = new Font("Segoe UI", 15f, FontStyle.Bold);
            label30.ForeColor  = T.TextMain;
            label30.BackColor  = Color.Transparent;
            label30.Location   = new Point(20, 20);
            label30.Size       = new Size(480, 34);

            label4.Text       = "Giảng Viên";
            label4.Font       = new Font("Segoe UI", 9f, FontStyle.Bold);
            label4.ForeColor   = T.Primary;
            label4.BackColor   = Color.FromArgb(209, 250, 229);
            label4.Location    = new Point(596, 24);
            label4.Size        = new Size(100, 26);
            label4.TextAlign   = ContentAlignment.MiddleCenter;

            panelGreet.Controls.AddRange(new Control[] { label30, label4 });

            // 4 Stat cards
            panelCards.Location  = new Point(24, 108);
            panelCards.Size      = new Size(716, 140);
            panelCards.BackColor = Color.Transparent;

            int cw = 170, ch = 132, cgap = 12;
            SetupStatCard(panel28, label45, label44, "LỚP PHÂN CÔNG", "📚",
                Color.FromArgb(209,250,229), T.Primary, 0, cw, ch);
            SetupStatCard(panel27, label43, label42, "TỔNG SINH VIÊN", "👥",
                Color.FromArgb(219,234,254), Color.FromArgb(59,130,246), cw+cgap, cw, ch);
            SetupStatCard(panel26, label41, label40, "CHƯA NHẬP ĐIỂM", "✏️",
                Color.FromArgb(254,243,199), Color.FromArgb(217,119,6), (cw+cgap)*2, cw, ch);
            SetupStatCard(panel25, label39, label38, "TỈ LỆ ĐẠT", "✅",
                Color.FromArgb(220,252,231), Color.FromArgb(22,163,74), (cw+cgap)*3, cw, ch);

            panelCards.Controls.AddRange(new Control[] { panel28, panel27, panel26, panel25 });

            // ── Bảng Lớp HP ───────────────────────────────────────
            panelTable.Location  = new Point(24, 264);
            panelTable.Size      = new Size(716, 200);
            panelTable.BackColor = Color.White;
            panelTable.Paint    += PaintTableCard;

            lblTbl.Text      = "📚  Lớp học phần của tôi";
            lblTbl.Font      = new Font("Segoe UI", 11.5f, FontStyle.Bold);
            lblTbl.ForeColor  = T.TextMain;
            lblTbl.BackColor  = Color.Transparent;
            lblTbl.Location   = new Point(18, 12);
            lblTbl.Size       = new Size(320, 26);

            ConfigGrid(dataGridView1);
            dataGridView1.Location = new Point(0, 46);
            dataGridView1.Size     = new Size(716, 154);
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { cotMHP, cotMH, cotSS, cotTT });
            cotMHP.HeaderText = "Mã LHP";     cotMHP.Name = "cotMHP"; cotMHP.MinimumWidth = 6;
            cotMH.HeaderText  = "Môn Học";    cotMH.Name  = "cotMH";  cotMH.MinimumWidth  = 6;
            cotSS.HeaderText  = "Sĩ Số";      cotSS.Name  = "cotSS";  cotSS.MinimumWidth  = 6;
            cotTT.HeaderText  = "Trạng Thái"; cotTT.Name  = "cotTT";  cotTT.MinimumWidth  = 6;

            panelTable.Controls.Add(lblTbl);
            panelTable.Controls.Add(dataGridView1);

            // ── Bảng Lịch sử nhập điểm ────────────────────────────
            panelHistory.Location  = new Point(24, 480);
            panelHistory.Size      = new Size(716, 220);
            panelHistory.BackColor = Color.White;
            panelHistory.Paint    += PaintTableCard;

            lblHis.Text      = "🕘  Lịch sử nhập điểm gần đây";
            lblHis.Font      = new Font("Segoe UI", 11.5f, FontStyle.Bold);
            lblHis.ForeColor  = T.TextMain;
            lblHis.BackColor  = Color.Transparent;
            lblHis.Location   = new Point(18, 12);
            lblHis.Size       = new Size(360, 26);

            ConfigGrid(dgvLichSu);
            dgvLichSu.Location           = new Point(0, 46);
            dgvLichSu.Size               = new Size(716, 174);
            dgvLichSu.AutoGenerateColumns= true;

            panelHistory.Controls.Add(lblHis);
            panelHistory.Controls.Add(dgvLichSu);

            panelContent.Controls.AddRange(new Control[]
                { panelGreet, panelCards, panelTable, panelHistory });

            // compatibility legacy fields
            panel16 = panelContent;
            label30Ref = label30;

            Controls.Add(panelSidebar);
            Controls.Add(panelContent);
            Controls.Add(panelHeader);

            panelHeader.ResumeLayout(false);
            panelSidebar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvLichSu).EndInit();
            ResumeLayout(false);
        }

        // ── Helpers (giống MenuSV) ────────────────────────────────
        private static void ConfigGrid(DataGridView dgv)
        {
            dgv.AllowUserToAddRows    = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible     = false;
            dgv.AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BorderStyle           = BorderStyle.None;
            dgv.BackgroundColor       = Color.White;
            dgv.CellBorderStyle       = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor             = Color.FromArgb(230, 245, 235);
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight   = 40;
            dgv.RowTemplate.Height    = 38;
            dgv.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
            dgv.ColumnHeadersDefaultCellStyle.BackColor  = Color.FromArgb(4, 100, 72);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor  = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font       = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(4,100,72);
            dgv.ColumnHeadersDefaultCellStyle.Padding    = new Padding(12, 0, 0, 0);
            dgv.ColumnHeadersBorderStyle                 = DataGridViewHeaderBorderStyle.None;
            dgv.DefaultCellStyle.BackColor               = Color.White;
            dgv.DefaultCellStyle.ForeColor               = T.TextMain;
            dgv.DefaultCellStyle.Font                    = new Font("Segoe UI", 10f);
            dgv.DefaultCellStyle.SelectionBackColor      = Color.FromArgb(209, 250, 229);
            dgv.DefaultCellStyle.SelectionForeColor      = T.TextMain;
            dgv.DefaultCellStyle.Padding                 = new Padding(12, 0, 0, 0);
            dgv.AlternatingRowsDefaultCellStyle.BackColor= Color.FromArgb(245, 252, 248);
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
            btn.Font = new Font("Segoe UI", 10f, selected ? FontStyle.Bold : FontStyle.Regular);
            btn.Location = new Point(0, y);
            btn.Size = new Size(210, 38);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 255, 255, 255);
            btn.BackColor = selected ? Color.FromArgb(60, 255, 255, 255) : Color.Transparent;
            btn.ForeColor = Color.White;
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(14, 0, 0, 0);
        }

        private static void SetupStatCard(Panel card, Label valLbl, Label captLbl,
            string caption, string icon, Color iconBg, Color iconColor, int x, int w, int h)
        {
            card.Location  = new Point(x, 0);
            card.Size      = new Size(w, h);
            card.BackColor = Color.White;
            card.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                for (int i = 2; i >= 1; i--)
                {
                    using var sp = T.RoundedRect(new Rectangle(i, i+1, w-i*2, h-i*2), 10);
                    g.DrawPath(new Pen(Color.FromArgb(10*i, 0, 100, 60), 1), sp);
                }
                using var path = T.RoundedRect(new Rectangle(1,1,w-2,h-2), 10);
                g.FillPath(Brushes.White, path);
                using var aBr = new SolidBrush(iconColor);
                using var tp = T.RoundedRect(new Rectangle(1,1,w-2,12), 9);
                g.FillPath(aBr, tp);
                using var icBr = new SolidBrush(iconBg);
                g.FillEllipse(icBr, w - 50, 16, 36, 36);
                using var icFont = new Font("Segoe UI Emoji", 14f);
                var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(icon, icFont, new SolidBrush(iconColor), new RectangleF(w-50, 16, 36, 36), sf);
            };

            captLbl.Text     = caption;
            captLbl.Font     = new Font("Segoe UI", 8f, FontStyle.Bold);
            captLbl.ForeColor= T.TextSub;
            captLbl.BackColor= Color.Transparent;
            captLbl.Location = new Point(16, 20);
            captLbl.Size     = new Size(w - 60, 18);

            valLbl.Text      = "—";
            valLbl.Font      = new Font("Segoe UI", 28f, FontStyle.Bold);
            valLbl.ForeColor = T.TextMain;
            valLbl.BackColor = Color.Transparent;
            valLbl.Location  = new Point(14, 52);
            valLbl.Size      = new Size(w - 28, 56);
            valLbl.TextAlign = ContentAlignment.MiddleLeft;

            card.Controls.Add(captLbl);
            card.Controls.Add(valLbl);
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

        private static void PaintGreetCard(object s, PaintEventArgs e)
        {
            var p = (Panel)s; var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var path = T.RoundedRect(new Rectangle(0,0,p.Width-1,p.Height-1), 10);
            g.FillPath(Brushes.White, path);
            using var pen = new Pen(Color.FromArgb(209, 228, 220), 1);
            g.DrawPath(pen, path);
            using var lBr = new SolidBrush(T.Primary);
            g.FillRectangle(lBr, 0, 0, 4, p.Height);
        }

        private static void PaintTableCard(object s, PaintEventArgs e)
        {
            var p = (Panel)s; var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var path = T.RoundedRect(new Rectangle(0,0,p.Width-1,p.Height-1), 10);
            g.FillPath(Brushes.White, path);
            using var pen = new Pen(Color.FromArgb(209,228,220), 1);
            g.DrawPath(pen, path);
        }

        // Fields
        private Panel panelHeader, panelSidebar, panelContent;
        private Panel panelGreet, panelCards, panelTable, panelHistory;
        private Label lblRole, label29, label31, label30, label4;
        private Label label1, label2, label3, lblTbl, lblHis;
        private Button button5, button6, button7, button8, button10, button4, button1, button2, button3;
        private Button button11, button12;
        private Panel panel28, panel27, panel26, panel25;
        private Label label45, label44, label43, label42, label41, label40, label39, label38;
        private DataGridView dataGridView1, dgvLichSu;
        private DataGridViewTextBoxColumn cotMHP, cotMH, cotSS, cotTT;
        private Panel panel16;
        private Label label30Ref;
    }
}
