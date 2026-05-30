using qldsv.Service;

namespace qldsv
{
    partial class MenuSV
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

            // Header controls
            lblRole      = new Label();
            label29      = new Label();  // tên sinh viên (header)
            label31      = new Label();  // mã SV
            button12     = new Button(); // settings
            button11     = new Button(); // X

            // Sidebar buttons
            button7  = new Button(); // Tổng quan
            button8  = new Button(); // Bảng điểm
            button10 = new Button(); // Đăng ký môn
            button2  = new Button(); // Hồ sơ
            button3  = new Button(); // Đổi MK
            button1  = new Button(); // TKB
            label2   = new Label();  // section Học Tập
            label3   = new Label();  // section Cá Nhân

            // Greeting
            label30  = new Label();  // "Xin chào, ..."
            label4   = new Label();  // role badge

            // Stat cards (dùng StatCard từ PremiumDashboard)
            panel28 = new Panel(); label45 = new Label(); label44 = new Label(); // GPA
            panel27 = new Panel(); label43 = new Label(); label42 = new Label(); // TC
            panel26 = new Panel(); label41 = new Label(); label40 = new Label(); // Môn kì
            panel25 = new Panel(); label39 = new Label(); label38 = new Label(); // Chưa điểm

            // Table
            panel18     = new Panel();
            label1      = new Label();
            dataGridView1 = new DataGridView();
            cotMH = new DataGridViewTextBoxColumn();
            cotCC = new DataGridViewTextBoxColumn();
            cotGK = new DataGridViewTextBoxColumn();
            cotCK = new DataGridViewTextBoxColumn();
            cotDTB= new DataGridViewTextBoxColumn();
            cotXL = new DataGridViewTextBoxColumn();

            // Unused legacy labels (keep for compatibility)
            label6  = new Label(); label7  = new Label();
            label8  = new Label(); label9  = new Label();
            label10 = new Label(); label11 = new Label();
            label12 = new Label(); label13 = new Label();
            panel19 = new Panel();
            panel3  = new Panel(); panel4  = new Panel();
            panel5  = new Panel(); panel6  = new Panel(); panel7 = new Panel();

            panel16 = new Panel();

            panelHeader.SuspendLayout();
            panelSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();

            // ── Form ──────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(980, 680);
            Name                = "MenuSV";
            Text                = "Menu Sinh Viên";
            BackColor           = Color.FromArgb(244, 246, 248);
            Font                = new Font("Segoe UI", 9.5f);
            StartPosition       = FormStartPosition.CenterScreen;

            // ══════════════════════════════════════════════════════
            //  HEADER
            // ══════════════════════════════════════════════════════
            panelHeader.Dock      = DockStyle.Top;
            panelHeader.Height    = 64;
            panelHeader.BackColor = T.Primary;
            panelHeader.Paint    += PaintHeader;

            lblRole.Text      = "SINH VIÊN";
            lblRole.Font      = new Font("Segoe UI", 8f, FontStyle.Bold);
            lblRole.ForeColor  = Color.FromArgb(180, 255, 255, 255);
            lblRole.BackColor  = Color.Transparent;
            lblRole.Location   = new Point(20, 10);
            lblRole.Size       = new Size(100, 16);

            label29.Text      = ""; // sẽ set bằng code
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
            label31.TextAlign  = ContentAlignment.MiddleLeft;

            StyleIconBtn(button12, "⚙", 890, 16);
            StyleIconBtn(button11, "✕", 936, 16, Color.FromArgb(255, 200, 200));

            panelHeader.Controls.AddRange(new Control[]
                { lblRole, label29, label31, button12, button11 });

            // ══════════════════════════════════════════════════════
            //  SIDEBAR
            // ══════════════════════════════════════════════════════
            panelSidebar.Location  = new Point(0, 64);
            panelSidebar.Size      = new Size(210, 616);
            panelSidebar.BackColor = Color.FromArgb(4, 100, 72);
            panelSidebar.Paint    += PaintSidebar;

            // Avatar area
            var lblAvatar = new Label
            {
                Name      = "lblAvatar",
                Text      = "👤",
                Font      = new Font("Segoe UI Emoji", 26f),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(40, 255, 255, 255),
                Location  = new Point(75, 22),
                Size      = new Size(60, 60),
                TextAlign = ContentAlignment.MiddleCenter
            };
            lblAvatar.Region = new Region(T.RoundedRect(new Rectangle(0, 0, 60, 60), 30));

            // Section labels
            label2.Text      = "HỌC TẬP";
            label2.Font      = new Font("Segoe UI", 8f, FontStyle.Bold);
            label2.ForeColor  = Color.FromArgb(150, 255, 255, 255);
            label2.BackColor  = Color.Transparent;
            label2.Location   = new Point(20, 102);
            label2.Size       = new Size(170, 18);

            label3.Text      = "CÁ NHÂN";
            label3.Font      = new Font("Segoe UI", 8f, FontStyle.Bold);
            label3.ForeColor  = Color.FromArgb(150, 255, 255, 255);
            label3.BackColor  = Color.Transparent;
            label3.Location   = new Point(20, 300);
            label3.Size       = new Size(170, 18);

            // Sidebar buttons
            StyleSideBtn(button7,  "🏠   Tổng quan",       128, true);
            StyleSideBtn(button8,  "📊   Bảng điểm",       170);
            StyleSideBtn(button10, "📝   Đăng ký môn",      212);
            StyleSideBtn(button1,  "📅   Thời khóa biểu",   254);
            StyleSideBtn(button2,  "👤   Hồ sơ cá nhân",   326);
            StyleSideBtn(button3,  "🔑   Đổi mật khẩu",    368);

            panelSidebar.Controls.AddRange(new Control[]
                { lblAvatar, label2, label3, button7, button8, button10, button1, button2, button3 });

            // ══════════════════════════════════════════════════════
            //  MAIN CONTENT
            // ══════════════════════════════════════════════════════
            panelContent.Location   = new Point(210, 64);
            panelContent.Size       = new Size(770, 616);
            panelContent.BackColor  = Color.FromArgb(244, 246, 248);
            panelContent.AutoScroll = true;
            panelContent.Padding    = new Padding(24, 20, 24, 20);

            // ── Greeting bar ──────────────────────────────────────
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

            label4.Text       = "Sinh Viên";
            label4.Font       = new Font("Segoe UI", 9f, FontStyle.Bold);
            label4.ForeColor   = T.Primary;
            label4.BackColor   = Color.FromArgb(209, 250, 229);
            label4.Location    = new Point(606, 24);
            label4.Size        = new Size(90, 26);
            label4.TextAlign   = ContentAlignment.MiddleCenter;

            panelGreet.Controls.AddRange(new Control[] { label30, label4 });

            // ── 4 Stat Cards (716px → 4×172+3×12=724? dùng 170) ──
            panelCards.Location  = new Point(24, 108);
            panelCards.Size      = new Size(716, 140);
            panelCards.BackColor = Color.Transparent;

            int cw = 170, ch = 132, cgap = 12;
            SetupStatCard(panel28, label45, label44, "GPA TÍCH LŨY",   "📈",
                Color.FromArgb(209,250,229), T.Primary,  0,          cw, ch);
            SetupStatCard(panel27, label43, label42, "TÍN CHỈ ĐÃ QUA", "🎓",
                Color.FromArgb(219,234,254), Color.FromArgb(59,130,246), cw+cgap, cw, ch);
            SetupStatCard(panel26, label41, label40, "MÔN KÌ NÀY",     "📚",
                Color.FromArgb(220,252,231), Color.FromArgb(22,163,74),  (cw+cgap)*2, cw, ch);
            SetupStatCard(panel25, label39, label38, "CHƯA CÓ ĐIỂM",   "⏳",
                Color.FromArgb(254,243,199), Color.FromArgb(217,119,6),  (cw+cgap)*3, cw, ch);

            panelCards.Controls.AddRange(new Control[] { panel28, panel27, panel26, panel25 });

            // ── Recent grades table ───────────────────────────────
            panelTable.Location  = new Point(24, 264);
            panelTable.Size      = new Size(716, 320);
            panelTable.BackColor = Color.White;
            panelTable.Paint    += PaintTableCard;

            label1.Text      = "📋  Điểm gần nhất";
            label1.Font      = new Font("Segoe UI", 11.5f, FontStyle.Bold);
            label1.ForeColor  = T.TextMain;
            label1.BackColor  = Color.Transparent;
            label1.Location   = new Point(18, 14);
            label1.Size       = new Size(280, 26);

            dataGridView1.Location              = new Point(0, 48);
            dataGridView1.Size                  = new Size(716, 272);
            dataGridView1.Anchor                = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            dataGridView1.AllowUserToAddRows    = false;
            dataGridView1.RowHeadersVisible     = false;
            dataGridView1.AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BorderStyle           = BorderStyle.None;
            dataGridView1.BackgroundColor       = Color.White;
            dataGridView1.CellBorderStyle       = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.GridColor             = Color.FromArgb(230, 245, 235);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersHeight   = 42;
            dataGridView1.RowTemplate.Height    = 40;
            dataGridView1.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToResizeRows = false;

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor   = Color.FromArgb(4, 100, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor   = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font        = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(4,100,72);
            dataGridView1.ColumnHeadersDefaultCellStyle.Padding     = new Padding(12, 0, 0, 0);
            dataGridView1.ColumnHeadersBorderStyle                  = DataGridViewHeaderBorderStyle.None;

            dataGridView1.DefaultCellStyle.BackColor                = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor                = T.TextMain;
            dataGridView1.DefaultCellStyle.Font                     = new Font("Segoe UI", 10f);
            dataGridView1.DefaultCellStyle.SelectionBackColor       = Color.FromArgb(209, 250, 229);
            dataGridView1.DefaultCellStyle.SelectionForeColor       = T.TextMain;
            dataGridView1.DefaultCellStyle.Padding                  = new Padding(12, 0, 0, 0);

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 252, 248);

            dataGridView1.Columns.AddRange(new DataGridViewColumn[]
                { cotMH, cotCC, cotGK, cotCK, cotDTB, cotXL });

            cotMH.HeaderText = "Môn Học";   cotMH.Name = "cotMH";  cotMH.MinimumWidth = 6;
            cotCC.HeaderText = "CC";        cotCC.Name = "cotCC";  cotCC.MinimumWidth = 6;
            cotGK.HeaderText = "GK";        cotGK.Name = "cotGK";  cotGK.MinimumWidth = 6;
            cotCK.HeaderText = "CK";        cotCK.Name = "cotCK";  cotCK.MinimumWidth = 6;
            cotDTB.HeaderText= "ĐTB";       cotDTB.Name= "cotDTB"; cotDTB.MinimumWidth = 6;
            cotXL.HeaderText = "Xếp Loại"; cotXL.Name = "cotXL";  cotXL.MinimumWidth = 6;

            panelTable.Controls.Add(label1);
            panelTable.Controls.Add(dataGridView1);

            panelContent.Controls.AddRange(new Control[]
                { panelGreet, panelCards, panelTable });

            // ── Đặt panel16 = panelContent (compatibility) ────────
            panel16 = panelContent;

            Controls.Add(panelSidebar);
            Controls.Add(panelContent);
            Controls.Add(panelHeader);

            panelHeader.ResumeLayout(false);
            panelSidebar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        // ── Helpers ───────────────────────────────────────────────
        private static void StyleIconBtn(Button btn, string text, int x, int y,
                                         Color? fg = null)
        {
            btn.Text      = text;
            btn.Font      = new Font("Segoe UI", 12f);
            btn.Location  = new Point(x, y);
            btn.Size      = new Size(36, 32);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize         = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 255, 255, 255);
            btn.BackColor = Color.Transparent;
            btn.ForeColor = fg ?? Color.White;
            btn.Cursor    = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleCenter;
        }

        private static void StyleSideBtn(Button btn, string text, int y,
                                         bool selected = false)
        {
            btn.Text      = text;
            btn.Font      = new Font("Segoe UI", 10f, selected ? FontStyle.Bold : FontStyle.Regular);
            btn.Location  = new Point(0, y);
            btn.Size      = new Size(210, 38);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize         = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 255, 255, 255);
            btn.BackColor = selected ? Color.FromArgb(60, 255, 255, 255) : Color.Transparent;
            btn.ForeColor = Color.White;
            btn.Cursor    = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding   = new Padding(14, 0, 0, 0);
        }

        private static void SetupStatCard(Panel card, Label valLbl, Label captLbl,
            string caption, string icon, Color iconBg, Color iconColor, int x, int w, int h)
        {
            card.Location  = new Point(x, 0);
            card.Size      = new Size(w, h);
            card.BackColor = Color.White;
            card.Paint    += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                // Shadow
                for (int i = 2; i >= 1; i--)
                {
                    using var sp = T.RoundedRect(new Rectangle(i, i+1, w-i*2, h-i*2), 10);
                    g.DrawPath(new Pen(Color.FromArgb(10*i, 0, 100, 60), 1), sp);
                }
                using var path = T.RoundedRect(new Rectangle(1,1,w-2,h-2), 10);
                g.FillPath(Brushes.White, path);
                // Top accent
                using var aBr = new System.Drawing.SolidBrush(iconColor);
                g.FillRectangle(aBr, 1, 1, w-2, 4);
                using var tp = T.RoundedRect(new Rectangle(1,1,w-2,12), 9);
                g.FillPath(aBr, tp);
                // Icon circle
                using var icBr = new SolidBrush(iconBg);
                g.FillEllipse(icBr, w - 50, 16, 36, 36);
                using var icFont = new Font("Segoe UI Emoji", 14f);
                var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(icon, icFont, new SolidBrush(iconColor),
                    new RectangleF(w-50, 16, 36, 36), sf);
            };

            // Caption (top)
            captLbl.Text      = caption;
            captLbl.Font      = new Font("Segoe UI", 8f, FontStyle.Bold);
            captLbl.ForeColor  = T.TextSub;
            captLbl.BackColor  = Color.Transparent;
            captLbl.Location   = new Point(16, 20);
            captLbl.Size       = new Size(w - 60, 18);

            // Value (big number)
            valLbl.Text       = "—";
            valLbl.Font       = new Font("Segoe UI", 28f, FontStyle.Bold);
            valLbl.ForeColor   = T.TextMain;
            valLbl.BackColor   = Color.Transparent;
            valLbl.Location    = new Point(14, 52);
            valLbl.Size        = new Size(w - 28, 56);
            valLbl.TextAlign   = ContentAlignment.MiddleLeft;

            card.Controls.Add(captLbl);
            card.Controls.Add(valLbl);
        }

        private static void PaintHeader(object s, PaintEventArgs e)
        {
            var p = (Panel)s;
            var g = e.Graphics;
            using var br = new System.Drawing.Drawing2D.LinearGradientBrush(
                p.ClientRectangle,
                Color.FromArgb(4, 120, 87), Color.FromArgb(5, 150, 105),
                System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            g.FillRectangle(br, p.ClientRectangle);
            // Shadow bottom
            using var sh = new System.Drawing.Drawing2D.LinearGradientBrush(
                new Rectangle(0, p.Height-5, p.Width, 5),
                Color.FromArgb(30, 0, 0, 0), Color.Transparent,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            g.FillRectangle(sh, 0, p.Height-5, p.Width, 5);
        }

        private static void PaintSidebar(object s, PaintEventArgs e)
        {
            var p = (Panel)s;
            var g = e.Graphics;
            using var br = new System.Drawing.Drawing2D.LinearGradientBrush(
                p.ClientRectangle,
                Color.FromArgb(3, 90, 65), Color.FromArgb(5, 120, 88),
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            g.FillRectangle(br, p.ClientRectangle);
            // Right border
            using var pen = new Pen(Color.FromArgb(60, 255, 255, 255), 1);
            g.DrawLine(pen, p.Width-1, 10, p.Width-1, p.Height-10);
        }

        private static void PaintGreetCard(object s, PaintEventArgs e)
        {
            var p = (Panel)s;
            var g = e.Graphics;
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
            var p = (Panel)s;
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var path = T.RoundedRect(new Rectangle(0,0,p.Width-1,p.Height-1), 10);
            g.FillPath(Brushes.White, path);
            using var pen = new Pen(Color.FromArgb(209,228,220), 1);
            g.DrawPath(pen, path);
        }

        // Fields
        private Panel panelHeader, panelSidebar, panelContent;
        private Panel panelGreet, panelCards, panelTable;
        private Label lblRole, label29, label31, label30, label4;
        private Label label1, label2, label3;
        private Button button7, button8, button10, button1, button2, button3;
        private Button button11, button12;

        // Stat cards
        private Panel panel28, panel27, panel26, panel25;
        private Label label45, label44, label43, label42, label41, label40, label39, label38;

        // Table
        private Panel panel18;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn cotMH, cotCC, cotGK, cotCK, cotDTB, cotXL;

        // Legacy (unused but kept for cs references)
        private Panel panel16, panel3, panel4, panel5, panel6, panel7, panel19;
        private Label label6, label7, label8, label9, label10, label11, label12, label13;
    }
}
