using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace qldsv.Service
{
    public static class T
    {
        public static readonly Color BG         = Color.FromArgb(244, 246, 248); // Off-White #F4F6F8
        public static readonly Color Surface    = Color.FromArgb(255, 255, 255); // trắng thuần (card)
        public static readonly Color Surface2   = Color.FromArgb(235, 245, 240); // mint nhạt (input)
        public static readonly Color Red        = Color.FromArgb(5,  150, 105);  // Teal/Emerald #059669
        public static readonly Color RedHover   = Color.FromArgb(52, 211, 153);  // Mint Green #34D399
        public static readonly Color RedDark    = Color.FromArgb(4,  120,  87);  // Emerald tối hơn
        public static readonly Color RedGlow    = Color.FromArgb(60,  5, 150,105);// glow teal
        public static readonly Color White      = Color.FromArgb(255, 255, 255);
        public static readonly Color Gray       = Color.FromArgb(120, 140, 130); // xám xanh
        public static readonly Color GrayLight  = Color.FromArgb(100, 116, 139); // Slate-500
        public static readonly Color Border     = Color.FromArgb(209, 228, 220); // viền mint nhạt
        // Alias dễ đọc
        public static Color Primary   => Red;        // #059669
        public static Color Accent    => RedHover;   // #34D399
        public static Color TextMain  => Color.FromArgb(44, 62, 80);   // Charcoal #2C3E50
        public static Color TextSub   => Color.FromArgb(100,116,139);  // Slate
        public static Color BgCard    => Surface;
        public static Color BgInput   => Color.FromArgb(240, 253, 248); // green-50

        // Font
        public static Font H1  => new Font("Segoe UI", 22f, FontStyle.Bold);
        public static Font H2  => new Font("Segoe UI", 16f, FontStyle.Bold);
        public static Font H3  => new Font("Segoe UI", 12f, FontStyle.Bold);
        public static Font Body => new Font("Segoe UI", 10f);
        public static Font Small=> new Font("Segoe UI",  8f);

        public static GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        public static void FillGradient(Graphics g, Rectangle r, Color c1, Color c2)
        {
            using var br = new LinearGradientBrush(r, c1, c2, LinearGradientMode.Vertical);
            g.FillRectangle(br, r);
        }

        public static void DrawGlow(Graphics g, Rectangle r, Color color, int radius = 8)
        {
            for (int i = radius; i > 0; i--)
            {
                int alpha = (int)(60.0 * i / radius);
                using var pen = new Pen(Color.FromArgb(alpha, color), i * 2);
                pen.LineJoin = LineJoin.Round;
                var gr = new Rectangle(r.X - i, r.Y - i, r.Width + i * 2, r.Height + i * 2);
                using var path = RoundedRect(gr, radius + i);
                g.DrawPath(pen, path);
            }
        }

        public static Color Lerp(Color a, Color b, float t)
        {
            t = Math.Clamp(t, 0f, 1f);
            return Color.FromArgb(
                (int)(a.A + (b.A - a.A) * t),
                (int)(a.R + (b.R - a.R) * t),
                (int)(a.G + (b.G - a.G) * t),
                (int)(a.B + (b.B - a.B) * t));
        }
    }

    public class PremiumButton : Button
    {
        private float _hover;           // 0→1
        private float _press;           // 0→1
        private readonly System.Windows.Forms.Timer _tmr = new() { Interval = 16 };

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int   CornerRadius { get; set; } = 8;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool  Outline      { get; set; } = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BaseColor    { get; set; } = T.Primary;

        public PremiumButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            FlatStyle     = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Cursor        = Cursors.Hand;
            Font          = T.H3;
            ForeColor     = T.White;
            BackColor     = Color.Transparent;

            _tmr.Tick += (s, e) =>
            {
                bool changed = false;
                float target = ClientRectangle.Contains(PointToClient(Cursor.Position)) ? 1f : 0f;
                if (Math.Abs(_hover - target) > 0.01f)
                { _hover += (_hover < target ? 1 : -1) * 0.1f; changed = true; }
                else _hover = target;

                float pt = (MouseButtons == MouseButtons.Left) ? 1f : 0f;
                if (Math.Abs(_press - pt) > 0.01f)
                { _press += (_press < pt ? 1 : -1) * 0.15f; changed = true; }
                else _press = pt;

                if (changed) Invalidate();
                else if (Math.Abs(_hover - target) < 0.01f && Math.Abs(_press - pt) < 0.01f)
                    _tmr.Stop();
            };
            MouseEnter += (s, e) => _tmr.Start();
            MouseLeave += (s, e) => _tmr.Start();
            MouseDown  += (s, e) => _tmr.Start();
            MouseUp    += (s, e) => _tmr.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var r = ClientRectangle;
            var bgRect = new Rectangle(r.X + 1, r.Y + 1, r.Width - 2, r.Height - 2);

            if (Outline)
            {
                // Outline style (border đỏ, nền trong suốt → hover fill)
                var fillColor = T.Lerp(Color.Transparent, T.Lerp(BaseColor, T.RedHover, _hover), _hover * 0.4f + _press * 0.3f);
                using var path = T.RoundedRect(bgRect, CornerRadius);
                if (fillColor.A > 0)
                {
                    using var br = new SolidBrush(fillColor);
                    g.FillPath(br, path);
                }
                using var pen = new Pen(T.Lerp(T.Gray, BaseColor, _hover), 1.5f);
                g.DrawPath(pen, path);
            }
            else
            {
                // Solid style với gradient + glow
                var c1 = T.Lerp(BaseColor, T.RedHover, _hover * 0.7f);
                var c2 = T.Lerp(T.RedDark, BaseColor, _hover * 0.3f);
                c1 = T.Lerp(c1, Color.FromArgb(180, 10, 10), _press * 0.4f);

                using var path = T.RoundedRect(bgRect, CornerRadius);
                using var br = new LinearGradientBrush(bgRect, c1, c2, LinearGradientMode.Vertical);
                g.FillPath(br, path);

                if (_hover > 0.1f)
                    T.DrawGlow(g, bgRect, T.Red, (int)(4 * _hover));

                // Highlight trên (ánh sáng)
                var hiRect = new Rectangle(bgRect.X + 2, bgRect.Y + 2, bgRect.Width - 4, bgRect.Height / 2);
                using var hiPath = T.RoundedRect(hiRect, CornerRadius - 1);
                using var hiBr = new LinearGradientBrush(hiRect,
                    Color.FromArgb(60, 255, 255, 255), Color.Transparent, LinearGradientMode.Vertical);
                g.FillPath(hiBr, hiPath);
            }

            // Text
            var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            using var textBr = new SolidBrush(ForeColor);
            g.DrawString(Text, Font, textBr, ClientRectangle, sf);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _tmr.Dispose();
            base.Dispose(disposing);
        }
    }

    public class PremiumTextBox : Panel
    {
        private readonly TextBox _tb = new TextBox();
        private float _focus;
        private readonly System.Windows.Forms.Timer _tmr = new() { Interval = 16 };
        private string _placeholder = "";
        private bool _isPass;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Placeholder
        {
            get => _placeholder;
            set { _placeholder = value; _tb.PlaceholderText = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPassword
        {
            get => _isPass;
            set { _isPass = value; _tb.PasswordChar = value ? '●' : '\0'; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Text  { get => _tb.Text; set => _tb.Text = value; }
        public new event EventHandler TextChanged { add => _tb.TextChanged += value; remove => _tb.TextChanged -= value; }

        public PremiumTextBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            BackColor = Color.Transparent;
            Padding   = new Padding(12, 8, 12, 8);
            Height    = 48;

            _tb.BorderStyle   = BorderStyle.None;
            _tb.BackColor     = T.BgInput;
            _tb.ForeColor     = T.TextMain;   // Charcoal — nhìn thấy trên nền sáng
            _tb.Font          = T.Body;
            _tb.Dock          = DockStyle.None;

            _tmr.Tick += (s, e) =>
            {
                float target = _tb.Focused ? 1f : 0f;
                if (Math.Abs(_focus - target) > 0.01f)
                { _focus += (_focus < target ? 1 : -1) * 0.12f; Invalidate(); }
                else { _focus = target; Invalidate(); _tmr.Stop(); }
            };
            _tb.GotFocus  += (s, e) => _tmr.Start();
            _tb.LostFocus += (s, e) => _tmr.Start();

            Controls.Add(_tb);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            _tb.Location = new Point(14, (Height - _tb.PreferredHeight) / 2);
            _tb.Width    = Width - 28;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var r = new Rectangle(1, 1, Width - 2, Height - 2);

            using var path = T.RoundedRect(r, 6);
            // Nền input sáng
            using var br   = new SolidBrush(T.BgInput);
            g.FillPath(br, path);

            // Viền: xám nhạt → teal khi focus
            var borderColor = T.Lerp(T.Border, T.Primary, _focus);
            if (_focus > 0.1f)
                T.DrawGlow(g, r, T.Primary, (int)(4 * _focus));

            using var pen = new Pen(borderColor, 1.5f);
            g.DrawPath(pen, path);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _tmr.Dispose();
            base.Dispose(disposing);
        }
    }

    public class PremiumForm : Form
    {
        private readonly System.Windows.Forms.Timer _fadeIn = new() { Interval = 12 };

        public PremiumForm()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor         = T.BG;
            ForeColor         = T.White;
            Font              = T.Body;
            FormBorderStyle   = FormBorderStyle.None;
            StartPosition     = FormStartPosition.CenterScreen;
            Opacity           = 0;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _fadeIn.Tick += (s, ev) =>
            {
                Opacity = Math.Min(1.0, Opacity + 0.06);
                if (Opacity >= 1) _fadeIn.Stop();
            };
            _fadeIn.Start();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            T.FillGradient(e.Graphics, ClientRectangle, T.BG, T.Surface);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _fadeIn.Dispose();
            base.Dispose(disposing);
        }
    }
}
