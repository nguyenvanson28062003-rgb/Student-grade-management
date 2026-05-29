using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace qldsv.Service
{
    // ═══════════════════════════════════════════════════════════════
    //  STAT CARD — theo mẫu: title+icon hàng trên, số lớn, badge dưới
    // ═══════════════════════════════════════════════════════════════
    public class StatCard : Panel
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string  CardTitle  { get; set; } = "";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string  Value      { get; set; } = "—";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string  BadgeText  { get; set; } = "";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool    BadgeUp    { get; set; } = true;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string  Icon       { get; set; } = "📊";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color   IconBg     { get; set; } = Color.FromArgb(209, 250, 229);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color   IconColor  { get; set; } = T.Primary;

        private float _hov;
        private readonly System.Windows.Forms.Timer _tmr = new() { Interval = 16 };

        public StatCard()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Color.Transparent;
            Cursor    = Cursors.Hand;

            _tmr.Tick += (s, e) =>
            {
                float t = ClientRectangle.Contains(PointToClient(Cursor.Position)) ? 1f : 0f;
                if (Math.Abs(_hov - t) > 0.02f) { _hov += (_hov < t ? 1 : -1) * 0.1f; Invalidate(); }
                else { _hov = t; _tmr.Stop(); }
            };
            MouseEnter += (s, e) => _tmr.Start();
            MouseLeave += (s, e) => _tmr.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode     = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Shadow
            for (int i = 3; i >= 1; i--)
            {
                var sR = new Rectangle(i, i + 1, Width - i * 2, Height - i * 2);
                using var sP = T.RoundedRect(sR, 12);
                using var sPen = new Pen(Color.FromArgb((int)(8 + _hov * 12) * i / 3, 0, 0, 0), 1);
                g.DrawPath(sPen, sP);
            }

            // Nền trắng
            var card = new Rectangle(1, 1, Width - 2, Height - 2);
            using var cPath = T.RoundedRect(card, 12);
            g.FillPath(Brushes.White, cPath);

            // Hover border
            if (_hov > 0.05f)
            {
                using var hPen = new Pen(Color.FromArgb((int)(60 * _hov), IconColor), 1.5f);
                g.DrawPath(hPen, cPath);
            }

            int p = 14;

            // ── Hàng 1: Title (trái) + Icon circle (phải) ─────────
            using var tFont = new Font("Segoe UI", 7.5f, FontStyle.Bold);
            g.DrawString(CardTitle.ToUpper(), tFont, new SolidBrush(T.TextSub),
                new RectangleF(p, p, Width - p*2 - 36, 18));

            // Icon circle
            int icX = Width - p - 30, icY = p - 2;
            using var icBr = new SolidBrush(IconBg);
            g.FillEllipse(icBr, icX, icY, 30, 30);
            using var icFont = new Font("Segoe UI Emoji", 11f);
            var icSf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString(Icon, icFont, new SolidBrush(IconColor),
                new RectangleF(icX, icY, 30, 30), icSf);

            // ── Hàng 2: Số lớn ────────────────────────────────────
            using var vFont = new Font("Segoe UI", 26f, FontStyle.Bold);
            g.DrawString(Value, vFont, new SolidBrush(T.TextMain),
                new RectangleF(p, p + 22, Width - p * 2, 38));

            // ── Hàng 3: Badge ─────────────────────────────────────
            if (!string.IsNullOrEmpty(BadgeText))
            {
                var badgeColor = BadgeUp
                    ? Color.FromArgb(5, 150, 105)
                    : Color.FromArgb(220, 53, 69);
                var badgeBg = BadgeUp
                    ? Color.FromArgb(209, 250, 229)
                    : Color.FromArgb(254, 226, 226);

                string arrow = BadgeUp ? "↑ " : "↓ ";
                using var bFont = new Font("Segoe UI", 8f, FontStyle.Bold);
                var bSz = g.MeasureString(arrow + BadgeText, bFont);
                var bRect = new RectangleF(p, Height - p - bSz.Height - 4,
                    bSz.Width + 14, bSz.Height + 6);

                using var bPath = T.RoundedRect(
                    new Rectangle((int)bRect.X, (int)bRect.Y, (int)bRect.Width, (int)bRect.Height), 10);
                g.FillPath(new SolidBrush(badgeBg), bPath);
                g.DrawString(arrow + BadgeText, bFont, new SolidBrush(badgeColor),
                    new PointF(bRect.X + 7, bRect.Y + 3));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _tmr.Dispose();
            base.Dispose(disposing);
        }
    }

    // ═══════════════════════════════════════════════════════════════
    //  BAR CHART — theo mẫu: legend inline top, bars rounded, labels đầy đủ
    // ═══════════════════════════════════════════════════════════════
    public class BarChartPanel : Panel
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string   Title    { get; set; } = "Điểm trung bình theo môn học";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string   SubTitle { get; set; } = "";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] Labels   { get; set; } = Array.Empty<string>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double[] Values   { get; set; } = Array.Empty<double>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double   MaxValue { get; set; } = 10.0;

        private Color GetBarColor(double v) => v switch
        {
            >= 8.5 => Color.FromArgb(16, 185, 129),
            >= 7.0 => Color.FromArgb(59, 130, 246),
            >= 5.0 => Color.FromArgb(251, 146, 60),
            _      => Color.FromArgb(239, 68, 68),
        };

        public BarChartPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode     = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.FillRectangle(Brushes.White, ClientRectangle);

            int p = 16;

            // Title
            using var tf = new Font("Segoe UI", 11f, FontStyle.Bold);
            g.DrawString(Title, tf, new SolidBrush(T.TextMain), new PointF(p, p));

            // Subtitle
            if (!string.IsNullOrEmpty(SubTitle))
            {
                using var sf2 = new Font("Segoe UI", 8.5f);
                g.DrawString(SubTitle, sf2, new SolidBrush(T.TextSub), new PointF(p, p + 22));
            }

            // Legend inline
            int legY = p + (string.IsNullOrEmpty(SubTitle) ? 26 : 42);
            DrawLegend(g, p, legY);

            if (Values == null || Values.Length == 0)
            {
                using var nf = new Font("Segoe UI", 9.5f);
                g.DrawString("Chưa có dữ liệu", nf, new SolidBrush(T.Gray),
                    new RectangleF(0, 0, Width, Height),
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                return;
            }

            // Chart area
            int padL = 40, padR = 16, padT = legY + 20, padB = 40;
            var chart = new Rectangle(padL, padT, Width - padL - padR, Height - padT - padB);

            // Grid lines
            int steps = 5;
            for (int i = 0; i <= steps; i++)
            {
                int gy = chart.Bottom - (int)(i * chart.Height / (double)steps);
                double yv = MaxValue * i / steps;

                using var gPen = new Pen(Color.FromArgb(i == 0 ? 180 : 220, 230, 220), i == 0 ? 1.5f : 1f);
                g.DrawLine(gPen, chart.Left, gy, chart.Right, gy);

                using var yFont = new Font("Segoe UI", 8f);
                g.DrawString(yv.ToString("F0"), yFont, new SolidBrush(T.TextSub),
                    chart.Left - 34, gy - 9);
            }

            // Bars
            int   n  = Values.Length;
            float gw = chart.Width / (float)n;
            float bw = Math.Clamp(gw * 0.55f, 8, 50);

            for (int i = 0; i < n; i++)
            {
                double v  = Math.Clamp(Values[i], 0, MaxValue);
                int    bh = (int)(v / MaxValue * chart.Height);
                float  bx = chart.Left + i * gw + (gw - bw) / 2f;
                int    by = chart.Bottom - bh;

                if (bh > 3)
                {
                    Color bc = GetBarColor(v);
                    using var bBr = new LinearGradientBrush(
                        new RectangleF(bx, by, bw, Math.Max(bh, 1)),
                        Color.FromArgb(180, bc), bc, LinearGradientMode.Vertical);
                    using var bPath = T.RoundedRect(new Rectangle((int)bx, by, (int)bw, bh), 6);
                    g.FillPath(bBr, bPath);

                    // Số trên cột
                    using var vf = new Font("Segoe UI", 8.5f, FontStyle.Bold);
                    g.DrawString(v.ToString("F1"), vf, new SolidBrush(bc),
                        new RectangleF(bx - 6, by - 20, bw + 12, 18),
                        new StringFormat { Alignment = StringAlignment.Center });
                }

                // Label đầy đủ dưới cột
                if (i < (Labels?.Length ?? 0))
                {
                    using var lf = new Font("Segoe UI", 8.5f);
                    g.DrawString(Labels[i], lf, new SolidBrush(T.TextMain),
                        new RectangleF(bx - 20, chart.Bottom + 6, bw + 40, 20),
                        new StringFormat { Alignment = StringAlignment.Center,
                                           Trimming  = StringTrimming.EllipsisCharacter });
                }
            }
        }

        private static void DrawLegend(Graphics g, int x, int y)
        {
            (string label, Color color)[] items =
            {
                ("≥8.5 Xuất sắc", Color.FromArgb(16, 185, 129)),
                ("≥7.0 Giỏi",     Color.FromArgb(59, 130, 246)),
                ("≥5.0 TB",        Color.FromArgb(251,146,  60)),
                ("<5.0 Yếu",       Color.FromArgb(239, 68,  68)),
            };
            using var lf = new Font("Segoe UI", 8f, FontStyle.Bold);
            int ox = x;
            foreach (var (label, color) in items)
            {
                g.FillRectangle(new SolidBrush(color), ox, y + 4, 10, 10);
                g.DrawString(label, lf, new SolidBrush(T.TextSub), ox + 13, y);
                ox += (int)g.MeasureString(label, lf).Width + 22;
            }
        }
    }

    // ═══════════════════════════════════════════════════════════════
    //  DONUT CHART — theo mẫu: donut to, legend dạng thanh ngang
    // ═══════════════════════════════════════════════════════════════
    public class DonutChartPanel : Panel
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string   Title    { get; set; } = "Phân loại học lực";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string   SubTitle { get; set; } = "Theo điểm trung bình tích lũy";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] Labels   { get; set; } = { "A", "B+", "B", "C+", "C", "D/F" };
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[]    Values   { get; set; } = Array.Empty<int>();

        private static readonly Color[] _colors =
        {
            Color.FromArgb(16, 185, 129),   // Xuất sắc
            Color.FromArgb(59, 130, 246),   // Giỏi
            Color.FromArgb(251, 146, 60),   // Khá
            Color.FromArgb(251, 191, 36),   // TB
            Color.FromArgb(239, 68,  68),   // Yếu
            Color.FromArgb(156,163, 175),   // Kém
        };

        // Label gộp cho legend
        private static readonly string[] _legLabels =
            { "Xuất sắc ≥8.5", "Giỏi ≥7", "Khá ≥6", "TB ≥5", "Yếu ≥4", "Kém <4" };

        public DonutChartPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode     = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.FillRectangle(Brushes.White, ClientRectangle);

            int p = 16;

            // Title + Subtitle
            using var tf  = new Font("Segoe UI", 11f, FontStyle.Bold);
            using var stf = new Font("Segoe UI", 8.5f);
            g.DrawString(Title,    tf,  new SolidBrush(T.TextMain), new PointF(p, p));
            g.DrawString(SubTitle, stf, new SolidBrush(T.TextSub),  new PointF(p, p + 22));

            int total = Values?.Sum() ?? 0;

            // Donut vùng
            int donutTop    = p + 42;
            int donutH      = Height - donutTop - 110; // để chỗ legend
            int donutSize   = Math.Min(Width - p * 2, donutH);
            if (donutSize < 40) donutSize = 40;
            int cx = Width / 2;
            int cy = donutTop + donutSize / 2;
            int outerR = donutSize / 2 - 4;
            int innerR = (int)(outerR * 0.60f);

            if (total > 0 && Values != null)
            {
                float angle = -90f;
                for (int i = 0; i < Math.Min(Values.Length, _colors.Length); i++)
                {
                    if (Values[i] <= 0) continue;
                    float sweep = 360f * Values[i] / total;
                    using var br = new SolidBrush(_colors[i]);
                    g.FillPie(br, cx - outerR, cy - outerR, outerR * 2, outerR * 2, angle, sweep);
                    angle += sweep;
                }
            }
            else
            {
                using var pb = new SolidBrush(Color.FromArgb(229, 231, 235));
                g.FillEllipse(pb, cx - outerR, cy - outerR, outerR * 2, outerR * 2);
            }

            // Đục lỗ + viền trắng
            g.FillEllipse(Brushes.White, cx - innerR, cy - innerR, innerR * 2, innerR * 2);
            using var wPen = new Pen(Color.White, 3f);
            g.DrawEllipse(wPen, cx - outerR, cy - outerR, outerR * 2, outerR * 2);

            // ── Legend dạng hàng: icon màu + tên + thanh progress + số ──
            int legY   = cy + outerR + 16;
            int legW   = Width - p * 2;
            int barMaxW = legW - 100;

            for (int i = 0; i < Math.Min(Values?.Length ?? 0, _legLabels.Length); i++)
            {
                if (i >= _colors.Length) break;
                int cnt = Values?[i] ?? 0;
                if (cnt == 0 && total > 0) continue; // ẩn hàng = 0

                int ry = legY;

                // Chấm màu
                using var cBr = new SolidBrush(_colors[i]);
                g.FillEllipse(cBr, p, ry + 4, 10, 10);

                // Label
                using var lf = new Font("Segoe UI", 8.5f, FontStyle.Bold);
                g.DrawString(_legLabels[i], lf, new SolidBrush(T.TextMain), p + 15, ry);

                // Thanh ngang progress
                int barX = p + 15;
                int barY = ry + 17;
                int barH = 5;
                int barW = barMaxW;

                // Nền thanh
                using var bgR = T.RoundedRect(new Rectangle(barX, barY, barW, barH), 3);
                g.FillPath(new SolidBrush(Color.FromArgb(229, 231, 235)), bgR);

                // Fill thanh
                int fillW = total > 0 ? (int)(cnt * barW / (double)total) : 0;
                if (fillW > 0)
                {
                    using var fR = T.RoundedRect(new Rectangle(barX, barY, fillW, barH), 3);
                    g.FillPath(new SolidBrush(_colors[i]), fR);
                }

                // Số sv bên phải
                using var nf = new Font("Segoe UI", 8.5f, FontStyle.Bold);
                g.DrawString($"{cnt} sv", nf, new SolidBrush(T.TextSub),
                    barX + barW + 6, ry);

                legY += 30;
            }
        }
    }
}
