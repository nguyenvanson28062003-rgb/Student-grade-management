#nullable disable
using System.Data;
using System.Drawing.Drawing2D;
using Microsoft.Data.SqlClient;
using qldsv.Service;

namespace qldsv
{
    internal class HoSoCaNhanGVDialog : Form
    {
        private readonly DatabaseHelper _db;
        private readonly string _maGV;

        public HoSoCaNhanGVDialog(DatabaseHelper db, string maGV)
        {
            _db   = db;
            _maGV = maGV;

            Text            = "Hồ Sơ Cá Nhân";
            ClientSize      = new Size(720, 480);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition   = FormStartPosition.CenterParent;
            BackColor       = Color.FromArgb(244, 246, 248);

            BuildUI();
        }

        private void BuildUI()
        {
            var header = new Panel { Dock = DockStyle.Top, Height = 130 };
            header.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using var br = new LinearGradientBrush(header.ClientRectangle,
                    Color.FromArgb(4, 120, 87), Color.FromArgb(6, 160, 112),
                    LinearGradientMode.Horizontal);
                g.FillRectangle(br, header.ClientRectangle);
                // Avatar circle
                using var aBr = new SolidBrush(Color.FromArgb(45, 255, 255, 255));
                g.FillEllipse(aBr, 40, 26, 78, 78);
                using var aPen = new Pen(Color.FromArgb(180, 255, 255, 255), 2.5f);
                g.DrawEllipse(aPen, 40, 26, 78, 78);
                using var icFont = new Font("Segoe UI Emoji", 30f);
                var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString("👨‍🏫", icFont, Brushes.White, new RectangleF(40, 26, 78, 78), sf);
            };
            Controls.Add(header);

            var lblName = new Label
            {
                Name      = "lblName",
                Font      = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location  = new Point(140, 38),
                Size      = new Size(480, 32)
            };
            var lblSub = new Label
            {
                Name      = "lblSub",
                Font      = new Font("Segoe UI", 10f),
                ForeColor = Color.FromArgb(210, 255, 255, 255),
                BackColor = Color.Transparent,
                Location  = new Point(140, 72),
                Size      = new Size(480, 22)
            };
            header.Controls.Add(lblName);
            header.Controls.Add(lblSub);

            // Nút đóng
            var btnClose = new Button
            {
                Text = "✕", Font = new Font("Segoe UI", 12f),
                Location = new Point(674, 12), Size = new Size(34, 30),
                FlatStyle = FlatStyle.Flat, ForeColor = Color.White,
                BackColor = Color.Transparent, Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 255, 255, 255);
            btnClose.Click += (s, e) => Close();
            header.Controls.Add(btnClose);
            btnClose.BringToFront();

            var card = new Panel
            {
                Location = new Point(40, 150), Size = new Size(640, 290),
                BackColor = Color.White
            };
            card.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using var path = T.RoundedRect(new Rectangle(0,0,card.Width-1,card.Height-1), 12);
                g.FillPath(Brushes.White, path);
                using var pen = new Pen(Color.FromArgb(220, 230, 225), 1);
                g.DrawPath(pen, path);
            };
            Controls.Add(card);

            // Load data
            string hoTen = "", hocVi = "", cnganh = "", khoa = "",
                   email = "", sdt = "", ngaySinh = "", gioiTinh = "";
            int tongLop = 0, tongSV = 0;
            try
            {
                using var conn = _db.GetConnection(); conn.Open();
                using var cmd = new SqlCommand(@"
                    SELECT gv.HoTen, gv.HocVi, gv.ChuyenNganh, k.TenKhoa,
                           gv.Email, gv.SoDienThoai, gv.NgaySinh, gv.GioiTinh,
                           (SELECT COUNT(*) FROM LopHocPhan l WHERE l.MaGV=gv.MaGV) AS TongLop,
                           (SELECT COUNT(DISTINCT dk.MaSV) FROM DangKyHocPhan dk
                            JOIN LopHocPhan l ON dk.MaLHP=l.MaLHP WHERE l.MaGV=gv.MaGV) AS TongSV
                    FROM GiangVien gv
                    JOIN Khoa k ON gv.MaKhoa = k.MaKhoa
                    WHERE gv.MaGV = @MaGV", conn);
                cmd.Parameters.AddWithValue("@MaGV", _maGV);
                using var r = cmd.ExecuteReader();
                if (r.Read())
                {
                    hoTen    = r["HoTen"]?.ToString() ?? "";
                    hocVi    = r["HocVi"]?.ToString() ?? "";
                    cnganh   = r["ChuyenNganh"]?.ToString() ?? "";
                    khoa     = r["TenKhoa"]?.ToString() ?? "";
                    email    = r["Email"]?.ToString() ?? "—";
                    sdt      = r["SoDienThoai"]?.ToString() ?? "—";
                    gioiTinh = r["GioiTinh"]?.ToString() == "Nam" ? "Nam"
                             : r["GioiTinh"]?.ToString() == "Nu" ? "Nữ" : "—";
                    ngaySinh = r["NgaySinh"] != DBNull.Value
                        ? Convert.ToDateTime(r["NgaySinh"]).ToString("dd/MM/yyyy") : "—";
                    tongLop  = r["TongLop"] == DBNull.Value ? 0 : Convert.ToInt32(r["TongLop"]);
                    tongSV   = r["TongSV"]  == DBNull.Value ? 0 : Convert.ToInt32(r["TongSV"]);
                }
            }
            catch { }

            lblName.Text = hoTen;
            lblSub.Text  = $"{hocVi}  ·  Mã GV: {_maGV}  ·  {khoa}";

            // Info rows: 2 cột
            (string icon, string label, string value)[] rows =
            {
                ("🎓", "Học vị",       hocVi),
                ("📖", "Chuyên ngành", cnganh),
                ("🏫", "Khoa",         khoa),
                ("⚧",  "Giới tính",    gioiTinh),
                ("🎂", "Ngày sinh",    ngaySinh),
                ("📧", "Email",        email),
                ("📱", "Điện thoại",   sdt),
                ("📚", "Tổng lớp dạy", tongLop + " lớp"),
            };

            int colW = 300;
            for (int i = 0; i < rows.Length; i++)
            {
                int col = i % 2, row = i / 2;
                int x = 24 + col * (colW + 16);
                int y = 24 + row * 62;

                var pnRow = new Panel
                {
                    Location = new Point(x, y), Size = new Size(colW, 52),
                    BackColor = Color.FromArgb(247, 252, 250)
                };
                var (ic, lb, vl) = rows[i];
                pnRow.Paint += (s, e) =>
                {
                    var g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using var p = T.RoundedRect(new Rectangle(0,0,pnRow.Width-1,pnRow.Height-1), 8);
                    g.FillPath(new SolidBrush(Color.FromArgb(247, 252, 250)), p);
                    using var icF = new Font("Segoe UI Emoji", 13f);
                    g.DrawString(ic, icF, new SolidBrush(T.Primary), new PointF(10, 12));
                    using var lF = new Font("Segoe UI", 8f);
                    g.DrawString(lb, lF, new SolidBrush(T.TextSub), new PointF(46, 8));
                    using var vF = new Font("Segoe UI", 10.5f, FontStyle.Bold);
                    g.DrawString(vl, vF, new SolidBrush(T.TextMain),
                        new RectangleF(46, 24, pnRow.Width - 52, 22));
                };
                card.Controls.Add(pnRow);
            }

            // Drag để di chuyển form (vì borderless)
            header.MouseDown += DragMove;
            lblName.MouseDown += DragMove;
        }

        // Cho phép kéo form không viền
        private bool _drag; private Point _start;
        private void DragMove(object s, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _drag = true; _start = e.Location;
            var ctrl = (Control)s;
            ctrl.MouseMove += OnDragMove;
            ctrl.MouseUp   += (a, b) => { _drag = false; ctrl.MouseMove -= OnDragMove; };
        }
        private void OnDragMove(object s, MouseEventArgs e)
        {
            if (_drag) Location = new Point(Location.X + e.X - _start.X, Location.Y + e.Y - _start.Y);
        }
    }

    internal class ThongKeLopGVDialog : Form
    {
        private readonly DatabaseHelper _db;
        private readonly string _maGV;
        private DataGridView _grid;
        private Label _lblTongLop, _lblTongSV, _lblTiLe, _lblDTB;

        public ThongKeLopGVDialog(DatabaseHelper db, string maGV)
        {
            _db   = db;
            _maGV = maGV;

            Text            = "Thống Kê Lớp";
            ClientSize      = new Size(800, 560);
            FormBorderStyle = FormBorderStyle.Sizable;
            StartPosition   = FormStartPosition.CenterParent;
            BackColor       = Color.FromArgb(244, 246, 248);
            MinimumSize     = new Size(700, 480);

            BuildUI();
            LoadData();
        }

        private void BuildUI()
        {
            // Header
            var header = new Panel { Dock = DockStyle.Top, Height = 60 };
            header.Paint += (s, e) =>
            {
                using var br = new LinearGradientBrush(header.ClientRectangle,
                    Color.FromArgb(4, 120, 87), Color.FromArgb(5, 150, 105),
                    LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(br, header.ClientRectangle);
            };
            var lblTitle = new Label
            {
                Text = "📈  Thống Kê Lớp Học Phần",
                Font = new Font("Segoe UI", 13f, FontStyle.Bold),
                ForeColor = Color.White, BackColor = Color.Transparent,
                Location = new Point(20, 14), AutoSize = true
            };
            header.Controls.Add(lblTitle);
            Controls.Add(header);

            // Summary cards row
            var cardRow = new Panel { Dock = DockStyle.Top, Height = 96, Padding = new Padding(16, 12, 16, 4),
                                      BackColor = Color.FromArgb(244, 246, 248) };
            Controls.Add(cardRow);

            _lblTongLop = MakeSummaryCard(cardRow, "📚 Tổng Lớp",    T.Primary,                  0);
            _lblTongSV  = MakeSummaryCard(cardRow, "👥 Tổng SV",     Color.FromArgb(59,130,246), 1);
            _lblTiLe    = MakeSummaryCard(cardRow, "✅ Tỉ Lệ Đạt",   Color.FromArgb(22,163,74),  2);
            _lblDTB     = MakeSummaryCard(cardRow, "📊 ĐTB Chung",   Color.FromArgb(217,119,6),  3);

            // Grid
            var gridWrap = new Panel { Dock = DockStyle.Fill, Padding = new Padding(16, 4, 16, 16),
                                       BackColor = Color.FromArgb(244, 246, 248) };
            Controls.Add(gridWrap);
            gridWrap.BringToFront();

            _grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false, RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                BackgroundColor = Color.White,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(230, 245, 235),
                EnableHeadersVisualStyles = false,
                ColumnHeadersHeight = 42, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToResizeRows = false
            };
            _grid.RowTemplate.Height = 40;
            _grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(4, 100, 72);
            _grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            _grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            _grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            _grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(4,100,72);
            _grid.DefaultCellStyle.Font = new Font("Segoe UI", 10f);
            _grid.DefaultCellStyle.ForeColor = T.TextMain;
            _grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(209, 250, 229);
            _grid.DefaultCellStyle.SelectionForeColor = T.TextMain;
            _grid.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            _grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 252, 248);
            gridWrap.Controls.Add(_grid);
        }

        private static Label MakeSummaryCard(Panel parent, string caption, Color accent, int idx)
        {
            int w = 176, gap = 12;
            var card = new Panel { Location = new Point(16 + idx*(w+gap), 12), Size = new Size(w, 76),
                                   BackColor = Color.White };
            var val = new Label
            {
                Text = "—", Font = new Font("Segoe UI", 19f, FontStyle.Bold),
                ForeColor = T.TextMain, BackColor = Color.Transparent,
                Location = new Point(12, 30), Size = new Size(w-20, 36),
                TextAlign = ContentAlignment.MiddleLeft
            };
            var cap = new Label
            {
                Text = caption, Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor = T.TextSub, BackColor = Color.Transparent,
                Location = new Point(12, 8), Size = new Size(w-20, 18)
            };
            card.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using var path = T.RoundedRect(new Rectangle(0,0,w-1,75), 10);
                g.FillPath(Brushes.White, path);
                using var pen = new Pen(Color.FromArgb(225,232,228), 1);
                g.DrawPath(pen, path);
                using var aBr = new SolidBrush(accent);
                g.FillRectangle(aBr, 0, 0, 4, 76);
            };
            card.Controls.Add(val); card.Controls.Add(cap);
            parent.Controls.Add(card);
            return val;
        }

        private void LoadData()
        {
            try
            {
                using var conn = _db.GetConnection(); conn.Open();

                // Summary
                using (var cmd = new SqlCommand(@"
                    SELECT
                        (SELECT COUNT(*) FROM LopHocPhan WHERE MaGV=@MaGV) AS TongLop,
                        (SELECT COUNT(DISTINCT dk.MaSV) FROM DangKyHocPhan dk
                         JOIN LopHocPhan l ON dk.MaLHP=l.MaLHP WHERE l.MaGV=@MaGV) AS TongSV,
                        CAST(ISNULL((SELECT COUNT(CASE WHEN d.KetQua='Dat' THEN 1 END)*100.0/NULLIF(COUNT(*),0)
                            FROM Diem d JOIN DangKyHocPhan dk ON d.MaDK=dk.MaDK
                            JOIN LopHocPhan l ON dk.MaLHP=l.MaLHP
                            WHERE l.MaGV=@MaGV AND dk.TrangThai='HoanThanh'),0) AS DECIMAL(5,1)) AS TiLe,
                        CAST(ISNULL((SELECT AVG(CAST(d.DiemTB AS FLOAT))
                            FROM Diem d JOIN DangKyHocPhan dk ON d.MaDK=dk.MaDK
                            JOIN LopHocPhan l ON dk.MaLHP=l.MaLHP
                            WHERE l.MaGV=@MaGV AND dk.TrangThai='HoanThanh'),0) AS DECIMAL(4,2)) AS DTB", conn))
                {
                    cmd.Parameters.AddWithValue("@MaGV", _maGV);
                    using var r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        _lblTongLop.Text = SInt(r,"TongLop").ToString();
                        _lblTongSV.Text  = SInt(r,"TongSV").ToString();
                        _lblTiLe.Text    = SDbl(r,"TiLe").ToString("F1") + "%";
                        _lblDTB.Text     = SDbl(r,"DTB").ToString("F2");
                    }
                }

                // Per-class detail
                using (var cmd = new SqlCommand(@"
                    SELECT
                        lhp.MaLHP                                              AS [Mã LHP],
                        mh.TenMH                                               AS [Môn Học],
                        hk.TenHK                                               AS [Học Kỳ],
                        lhp.SiSoHienTai                                        AS [Sĩ Số],
                        SUM(CASE WHEN d.DiemCK IS NOT NULL THEN 1 ELSE 0 END)  AS [Đã Nhập],
                        SUM(CASE WHEN d.KetQua='Dat' THEN 1 ELSE 0 END)        AS [Đạt],
                        SUM(CASE WHEN d.KetQua='KhongDat' THEN 1 ELSE 0 END)   AS [Rớt],
                        CAST(ISNULL(AVG(CAST(d.DiemTB AS FLOAT)),0) AS DECIMAL(4,2)) AS [ĐTB Lớp]
                    FROM LopHocPhan lhp
                    JOIN MonHoc mh ON lhp.MaMH = mh.MaMH
                    JOIN HocKy hk  ON lhp.MaHK = hk.MaHK
                    LEFT JOIN DangKyHocPhan dk ON dk.MaLHP = lhp.MaLHP
                    LEFT JOIN Diem d ON d.MaDK = dk.MaDK
                    WHERE lhp.MaGV = @MaGV
                    GROUP BY lhp.MaLHP, mh.TenMH, hk.TenHK, lhp.SiSoHienTai
                    ORDER BY lhp.MaLHP", conn))
                {
                    cmd.Parameters.AddWithValue("@MaGV", _maGV);
                    using var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable(); da.Fill(dt);
                    _grid.DataSource = dt;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi thống kê:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static int    SInt(IDataRecord r, string c) => r[c]==DBNull.Value?0:Convert.ToInt32(r[c]);
        private static double SDbl(IDataRecord r, string c) => r[c]==DBNull.Value?0:Convert.ToDouble(r[c]);
    }
}
