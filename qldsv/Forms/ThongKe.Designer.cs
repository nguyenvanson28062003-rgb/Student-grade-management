using qldsv.Service;

namespace quản_lí_điểm_sinh_viên
{
    partial class ThongKe
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
            panelCards   = new Panel();
            panelCharts  = new Panel();
            panelTable   = new Panel();
            panelScroll  = new Panel();

            lblAppIcon   = new Label();
            lblTitle     = new Label();
            lblSub       = new Label();
            comboHK      = new ComboBox();
            comboKhoa    = new ComboBox();
            btnXemTK     = new Button();
            btnXuat      = new Button();
            btnThoat     = new Button();

            dataGridView1 = new System.Windows.Forms.DataGridView();

            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(900, 620);
            StartPosition       = FormStartPosition.CenterScreen;
            Text                = "Báo Cáo Thống Kê";
            Font                = T.Body;
            BackColor           = Color.FromArgb(248, 249, 250);

            panelHeader.Dock      = DockStyle.Top;
            panelHeader.Height    = 68;
            panelHeader.BackColor = Color.White;
            panelHeader.Padding   = new Padding(16, 10, 16, 10);
            panelHeader.Paint    += PanelHeader_Paint;

            // Logo icon (green square)
            lblAppIcon.Text      = "📊";
            lblAppIcon.Font      = new Font("Segoe UI Emoji", 18f);
            lblAppIcon.ForeColor = Color.White;
            lblAppIcon.BackColor = T.Primary;
            lblAppIcon.Size      = new Size(44, 44);
            lblAppIcon.Location  = new Point(16, 12);
            lblAppIcon.TextAlign = ContentAlignment.MiddleCenter;

            // Title + subtitle
            lblTitle.Text      = "Báo cáo thống kê";
            lblTitle.Font      = new Font("Segoe UI", 11f, FontStyle.Bold);
            lblTitle.ForeColor = T.TextMain;
            lblTitle.Location  = new Point(68, 12);
            lblTitle.Size      = new Size(240, 22);
            lblTitle.BackColor = Color.Transparent;

            lblSub.Text      = "Tổng hợp kết quả học tập sinh viên";
            lblSub.Font      = new Font("Segoe UI", 8.5f);
            lblSub.ForeColor = T.TextSub;
            lblSub.Location  = new Point(68, 34);
            lblSub.Size      = new Size(280, 18);
            lblSub.BackColor = Color.Transparent;

            // Filter combos
            StyleFilterCombo(comboHK,    "📅 Tất cả học kỳ",  330, 18, 160);
            StyleFilterCombo(comboKhoa,  "🏫 Tất cả khoa",    500, 18, 150);

            // Buttons
            StyleHeaderBtn(btnXemTK, "🔍", 662, 16, 40, 36, T.Primary, Color.White, true);
            StyleHeaderBtn(btnXuat,  "⬇",  710, 16, 40, 36, Color.White, T.TextMain, false);
            StyleHeaderBtn(btnThoat, "✕",  760, 16, 40, 36, Color.White, Color.FromArgb(220,53,69), false);

            panelHeader.Controls.AddRange(new Control[]
            {
                lblAppIcon, lblTitle, lblSub, comboHK, comboKhoa,
                btnXemTK, btnXuat, btnThoat
            });

            panelScroll.Dock       = DockStyle.Fill;
            panelScroll.AutoScroll = true;
            panelScroll.BackColor  = Color.FromArgb(244, 246, 248);
            panelScroll.Padding    = new Padding(16, 12, 16, 16);

            // Cards panel
            panelCards.Location  = new Point(16, 12);
            panelCards.Size      = new Size(868, 112);
            panelCards.BackColor = Color.Transparent;

            // Charts panel
            panelCharts.Location  = new Point(16, 132);
            panelCharts.Size      = new Size(868, 280);
            panelCharts.BackColor = Color.Transparent;

            // Table panel
            panelTable.Location  = new Point(16, 420);
            panelTable.Size      = new Size(868, 180);
            panelTable.BackColor = Color.White;
            panelTable.Paint    += PanelTable_Paint;

            // DataGridView
            dataGridView1.Location             = new Point(0, 54);
            dataGridView1.Size                 = new Size(868, 126);
            dataGridView1.AllowUserToAddRows   = false;
            dataGridView1.RowHeadersVisible    = false;
            dataGridView1.AutoSizeColumnsMode  = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ScrollBars           = System.Windows.Forms.ScrollBars.Vertical;
            dataGridView1.BackgroundColor      = Color.White;
            dataGridView1.BorderStyle          = BorderStyle.None;
            dataGridView1.GridColor            = Color.FromArgb(241, 245, 241);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.CellBorderStyle      = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersHeight  = 36;
            dataGridView1.RowTemplate.Height   = 44;
            dataGridView1.SelectionMode        = DataGridViewSelectionMode.FullRowSelect;

            // Header style
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor  = Color.FromArgb(248, 250, 248);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor  = T.TextSub;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font       = new Font("Segoe UI", 8f, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Padding    = new Padding(12, 0, 0, 0);
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(248,250,248);
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor = T.TextSub;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Row style
            dataGridView1.DefaultCellStyle.BackColor               = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor               = T.TextMain;
            dataGridView1.DefaultCellStyle.Font                    = new Font("Segoe UI", 9.5f);
            dataGridView1.DefaultCellStyle.SelectionBackColor      = Color.FromArgb(236, 253, 245);
            dataGridView1.DefaultCellStyle.SelectionForeColor      = T.TextMain;
            dataGridView1.DefaultCellStyle.Padding                 = new Padding(12, 0, 0, 0);

            panelTable.Controls.Add(dataGridView1);

            panelScroll.Controls.AddRange(new Control[]
            {
                panelCards, panelCharts, panelTable
            });

            Controls.Add(panelScroll);
            Controls.Add(panelHeader);

            panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        private static void StyleFilterCombo(ComboBox cb, string placeholder, int x, int y, int w)
        {
            cb.Location      = new Point(x, y);
            cb.Size          = new Size(w, 32);
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.BackColor     = Color.FromArgb(244, 246, 248);
            cb.ForeColor     = T.TextMain;
            cb.FlatStyle     = FlatStyle.Flat;
            cb.Font          = new Font("Segoe UI", 9f);
        }

        private static void StyleHeaderBtn(Button btn, string text, int x, int y,
                                           int w, int h, Color bg, Color fg, bool fill)
        {
            btn.Text      = text;
            btn.Location  = new Point(x, y);
            btn.Size      = new Size(w, h);
            btn.Font      = new Font("Segoe UI Emoji", 11f);
            btn.FlatStyle = FlatStyle.Flat;
            btn.Cursor    = Cursors.Hand;
            btn.BackColor = bg;
            btn.ForeColor = fg;
            btn.FlatAppearance.BorderColor        = fill ? T.Primary : Color.FromArgb(210, 215, 210);
            btn.FlatAppearance.BorderSize         = 1;
            btn.FlatAppearance.MouseOverBackColor = fill
                ? Color.FromArgb(4, 130, 92)
                : Color.FromArgb(240, 245, 240);
        }

        private static void PanelHeader_Paint(object s, PaintEventArgs e)
        {
            var p = (Panel)s;
            // Shadow dưới header
            using var sh = new System.Drawing.Drawing2D.LinearGradientBrush(
                new Rectangle(0, p.Height - 6, p.Width, 6),
                Color.FromArgb(18, 0, 0, 0), Color.Transparent,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(sh, 0, p.Height - 6, p.Width, 6);
        }

        private static void PanelTable_Paint(object s, PaintEventArgs e)
        {
            var p  = (Panel)s;
            var g  = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            // Nền trắng
            g.FillRectangle(Brushes.White, p.ClientRectangle);
            // Border nhẹ
            using var pen = new Pen(Color.FromArgb(229, 231, 235), 1);
            g.DrawRectangle(pen, 0, 0, p.Width-1, p.Height-1);
            // Title
            using var tf  = new Font("Segoe UI", 11f, FontStyle.Bold);
            using var stf = new Font("Segoe UI", 8.5f);
            g.DrawString("Tỉ lệ đạt / rớt theo môn học", tf,
                new SolidBrush(T.TextMain), new PointF(16, 12));
        }

        private Panel    panelHeader, panelCards, panelCharts, panelTable, panelScroll;
        private Label    lblAppIcon, lblTitle, lblSub;
        private ComboBox comboHK, comboKhoa;
        private Button   btnXemTK, btnXuat, btnThoat;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}
