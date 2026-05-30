using System.Drawing.Drawing2D;

namespace qldsv.Service
{
    /// <summary>
    /// Light theme engine — Teal/Emerald (#059669), Off-White (#F4F6F8), Charcoal (#2C3E50).
    /// Gọi ThemeApplier.Apply(this) trong constructor mỗi form.
    /// </summary>
    public static class ThemeApplier
    {
        private static readonly Color _rowEven   = Color.FromArgb(255, 255, 255);
        private static readonly Color _rowOdd    = Color.FromArgb(240, 253, 248); // green-50
        private static readonly Color _rowHover  = Color.FromArgb(209, 250, 229); // green-100
        private static readonly Color _selBg     = Color.FromArgb(5, 150, 105);   // primary
        private static readonly Color _headerBg  = Color.FromArgb(4,  120, 87);   // emerald-700

        public static void Apply(Form form)
        {
            form.BackColor = T.BG;
            form.ForeColor = T.TextMain;
            form.Font      = T.Body;

            // Vẽ nền nhẹ
            form.Paint += (s, e) =>
            {
                using var br = new LinearGradientBrush(
                    form.ClientRectangle,
                    Color.FromArgb(244, 246, 248),
                    Color.FromArgb(232, 244, 238),
                    LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(br, form.ClientRectangle);
            };

            // Fade-in mượt
            form.Opacity = 0;
            var fade = new System.Windows.Forms.Timer { Interval = 12 };
            fade.Tick += (s, e) =>
            {
                form.Opacity = Math.Min(1.0, form.Opacity + 0.08);
                if (form.Opacity >= 1) { fade.Stop(); fade.Dispose(); }
            };
            form.Shown += (s, e) => fade.Start();

            ApplyTree(form.Controls);
        }

        private static void ApplyTree(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                switch (c)
                {
                    case PremiumButton:
                    case PremiumTextBox:
                        break;

                    case DataGridView dgv: StyleGrid(dgv);     break;
                    case Button        btn: StyleButton(btn);  break;
                    case TextBox       tb:  StyleTextBox(tb);  break;
                    case ComboBox      cb:  StyleCombo(cb);    break;
                    case GroupBox      gb:  StyleGroupBox(gb); break;
                    case Label         lbl: StyleLabel(lbl);   break;
                    case Panel         p:   StylePanel(p);     break;
                    case TabControl    tc:  StyleTab(tc);      break;
                    case ListBox       lb:  StyleListBox(lb);  break;

                    case CheckBox chk:
                        chk.ForeColor = T.TextMain;
                        chk.BackColor = Color.Transparent;
                        break;
                    case RadioButton rb:
                        rb.ForeColor = T.TextMain;
                        rb.BackColor = Color.Transparent;
                        break;
                    case NumericUpDown nud:
                        nud.BackColor = T.BgInput;
                        nud.ForeColor = T.TextMain;
                        break;
                    case DateTimePicker dtp:
                        dtp.BackColor = T.BgInput;
                        dtp.ForeColor = T.TextMain;
                        break;
                }

                if (c.Controls.Count > 0)
                    ApplyTree(c.Controls);
            }
        }

        private static void StyleButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font   = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.ForeColor = Color.White;

            string name = btn.Name.ToLower();
            string text = (btn.Text ?? "").ToLower();

            bool isPrimary = name.Contains("luu")  || name.Contains("them")  ||
                             name.Contains("xac")  || name.Contains("dang")  ||
                             name.Contains("cap")  || name.Contains("chot")  ||
                             name.Contains("nhap") || name.Contains("import")||
                             name.Contains("export")|| name.Contains("xuat");

            bool isDanger  = name.Contains("xoa")  || name.Contains("huy");
            bool isNeutral = name.Contains("thoat")|| name.Contains("quay") ||
                             name.Contains("dong") || name.Contains("back");

            if (isDanger)
            {
                btn.BackColor = Color.FromArgb(220, 53, 69);
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 35, 51);
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(180, 20, 38);
            }
            else if (isNeutral)
            {
                btn.BackColor = Color.FromArgb(100, 116, 139);
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(80,  96, 120);
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(60,  76, 100);
            }
            else if (isPrimary)
            {
                btn.BackColor = T.Primary;
                btn.FlatAppearance.MouseOverBackColor = T.RedHover;
                btn.FlatAppearance.MouseDownBackColor = T.RedDark;
                btn.FlatAppearance.BorderColor        = T.RedDark;
            }
            else
            {
                // Default: teal nhạt + text xanh
                btn.BackColor = Color.FromArgb(209, 250, 229);
                btn.ForeColor = Color.FromArgb(4, 120, 87);
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(167, 243, 208);
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(110, 231, 183);
                btn.FlatAppearance.BorderColor        = Color.FromArgb(52, 211, 153);
                btn.FlatAppearance.BorderSize         = 1;
            }
        }

        private static void StyleTextBox(TextBox tb)
        {
            tb.BackColor   = T.BgInput;
            tb.ForeColor   = T.TextMain;
            tb.BorderStyle = BorderStyle.FixedSingle;
            tb.Font        = T.Body;
        }

        private static void StyleCombo(ComboBox cb)
        {
            cb.BackColor = T.BgInput;
            cb.ForeColor = T.TextMain;
            cb.FlatStyle = FlatStyle.Flat;
            cb.Font      = T.Body;
        }

        private static void StyleGroupBox(GroupBox gb)
        {
            gb.BackColor = Color.Transparent;
            gb.ForeColor = T.Primary;
            gb.Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold);
        }

        private static void StyleLabel(Label lbl)
        {
            lbl.BackColor = Color.Transparent;

            if (lbl.Font != null && lbl.Font.Bold && lbl.Font.Size >= 12)
            {
                lbl.ForeColor = T.TextMain;
                lbl.Font      = new Font("Segoe UI", lbl.Font.Size, FontStyle.Bold);
            }
            else if (lbl.Font != null && lbl.Font.Bold)
            {
                lbl.ForeColor = Color.FromArgb(4, 120, 87); // emerald đậm
                lbl.Font      = new Font("Segoe UI", lbl.Font.Size, FontStyle.Bold);
            }
            else
            {
                lbl.ForeColor = T.TextMain;
                float sz = lbl.Font?.Size ?? 9f;
                lbl.Font = new Font("Segoe UI", sz);
            }
        }

        private static void StylePanel(Panel p)
        {
            // Sidebar trái
            if (p.Width is >= 200 and <= 270 && p.Left <= 10)
            {
                p.BackColor = Color.FromArgb(4, 120, 87); // emerald-700
                p.Paint += PaintSidebar;
            }
            // Header bar
            else if (p.Dock == DockStyle.Top || (p.Top <= 5 && p.Height <= 60))
            {
                p.BackColor = Color.FromArgb(5, 150, 105); // primary
                p.Paint += PaintHeader;
            }
            // Card lớn
            else if (p.Width > 300 && p.Height > 100)
            {
                p.BackColor = Color.FromArgb(255, 255, 255);
                p.Paint += PaintCard;
            }
            else
            {
                p.BackColor = Color.Transparent;
            }
        }

        private static void PaintSidebar(object s, PaintEventArgs e)
        {
            var p = (Panel)s;
            var g = e.Graphics;
            // Gradient: đậm → nhạt theo chiều dọc
            using var br = new LinearGradientBrush(p.ClientRectangle,
                Color.FromArgb(3, 102, 74),
                Color.FromArgb(6, 160, 112),
                LinearGradientMode.Vertical);
            g.FillRectangle(br, p.ClientRectangle);
            // Viền trắng nhạt bên phải
            using var pen = new Pen(Color.FromArgb(80, 255, 255, 255), 1.5f);
            g.DrawLine(pen, p.Width - 1, 20, p.Width - 1, p.Height - 20);
        }

        private static void PaintHeader(object s, PaintEventArgs e)
        {
            var p = (Panel)s;
            var g = e.Graphics;
            using var br = new LinearGradientBrush(p.ClientRectangle,
                Color.FromArgb(5, 150, 105),
                Color.FromArgb(4, 130, 92),
                LinearGradientMode.Horizontal);
            g.FillRectangle(br, p.ClientRectangle);
            // Shadow dưới header
            using var shadow = new LinearGradientBrush(
                new Rectangle(0, p.Height - 4, p.Width, 4),
                Color.FromArgb(60, 0, 0, 0), Color.Transparent,
                LinearGradientMode.Vertical);
            g.FillRectangle(shadow, 0, p.Height - 4, p.Width, 4);
        }

        private static void PaintCard(object s, PaintEventArgs e)
        {
            var p = (Panel)s;
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            // Nền trắng
            g.FillRectangle(Brushes.White, p.ClientRectangle);
            // Border teal nhạt
            using var pen = new Pen(Color.FromArgb(180, 209, 250, 229), 1.5f);
            g.DrawRectangle(pen, 0, 0, p.Width - 1, p.Height - 1);
            // Accent bar trên đầu
            using var topBr = new SolidBrush(Color.FromArgb(5, 150, 105));
            g.FillRectangle(topBr, 0, 0, p.Width, 3);
        }

        private static void StyleGrid(DataGridView dgv)
        {
            // Bật thanh cuộn
            dgv.ScrollBars              = ScrollBars.Both;
            dgv.AutoSizeColumnsMode     = DataGridViewAutoSizeColumnsMode.None;

            dgv.BackgroundColor         = Color.FromArgb(248, 252, 250);
            dgv.GridColor               = Color.FromArgb(209, 228, 220);
            dgv.BorderStyle             = BorderStyle.FixedSingle;
            dgv.CellBorderStyle         = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.RowHeadersVisible       = false;
            dgv.EnableHeadersVisualStyles = false;
            dgv.SelectionMode           = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToResizeRows   = false;

            // Header — emerald đậm + chữ trắng
            dgv.ColumnHeadersDefaultCellStyle.BackColor  = _headerBg;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor  = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font       = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding    = new Padding(8, 5, 8, 5);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = _headerBg;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.ColumnHeadersBorderStyle                 = DataGridViewHeaderBorderStyle.Single;
            dgv.ColumnHeadersHeight                      = 38;
            dgv.ColumnHeadersHeightSizeMode              = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Row chẵn — trắng
            dgv.DefaultCellStyle.BackColor               = _rowEven;
            dgv.DefaultCellStyle.ForeColor               = T.TextMain;
            dgv.DefaultCellStyle.SelectionBackColor      = _selBg;
            dgv.DefaultCellStyle.SelectionForeColor      = Color.White;
            dgv.DefaultCellStyle.Font                    = T.Body;
            dgv.DefaultCellStyle.Padding                 = new Padding(6, 3, 6, 3);

            // Row lẻ — mint rất nhạt
            dgv.AlternatingRowsDefaultCellStyle.BackColor = _rowOdd;
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = T.TextMain;

            // Row height
            dgv.RowTemplate.Height = 32;

            // Hover highlight
            dgv.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = _rowHover;
            };
            dgv.CellMouseLeave += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                        e.RowIndex % 2 == 0 ? _rowEven : _rowOdd;
            };

            // Auto-fit columns sau khi data load
            dgv.DataSourceChanged += (s, e) => FitGridColumns(dgv);
            dgv.ColumnAdded       += (s, e) => FitGridColumns(dgv);
        }

        private static void FitGridColumns(DataGridView dgv)
        {
            if (dgv.Columns.Count == 0) return;
            // Auto-fit tất cả cột
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            // Sau đó fix lại để scroll hoạt động
            foreach (DataGridViewColumn col in dgv.Columns)
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }

        private static void StyleTab(TabControl tc)
        {
            tc.DrawMode = TabDrawMode.OwnerDrawFixed;
            tc.DrawItem += (s, e) =>
            {
                var tab = tc.TabPages[e.Index];
                bool sel = tc.SelectedIndex == e.Index;
                using var bgBr = new SolidBrush(sel ? T.Primary : Color.FromArgb(209, 250, 229));
                e.Graphics.FillRectangle(bgBr, e.Bounds);
                using var txBr = new SolidBrush(sel ? Color.White : T.TextMain);
                var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString(tab.Text, new Font("Segoe UI", 9f, FontStyle.Bold), txBr, e.Bounds, sf);
            };
            foreach (TabPage tp in tc.TabPages)
            {
                tp.BackColor = T.BG;
                tp.ForeColor = T.TextMain;
            }
        }

        private static void StyleListBox(ListBox lb)
        {
            lb.BackColor   = Color.White;
            lb.ForeColor   = T.TextMain;
            lb.BorderStyle = BorderStyle.FixedSingle;
            lb.DrawMode    = DrawMode.OwnerDrawFixed;
            lb.ItemHeight  = 28;
            lb.DrawItem += (s, e) =>
            {
                if (e.Index < 0) return;
                bool sel = (e.State & DrawItemState.Selected) != 0;
                using var bgBr = new SolidBrush(sel ? T.Primary : (e.Index % 2 == 0 ? _rowEven : _rowOdd));
                e.Graphics.FillRectangle(bgBr, e.Bounds);
                using var txBr = new SolidBrush(sel ? Color.White : T.TextMain);
                e.Graphics.DrawString(lb.Items[e.Index].ToString(),
                    new Font("Segoe UI", 9f), txBr,
                    new RectangleF(e.Bounds.X + 10, e.Bounds.Y + 4, e.Bounds.Width - 10, e.Bounds.Height));
            };
        }
    }
}
