#nullable disable
using Microsoft.Data.SqlClient;
using System.Data;
using qldsv.Service;

namespace qldsv
{
    public partial class MenuAdmin : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;

        // ── Dashboard controls ──
        private StatCard      _cardSV, _cardDTB, _cardTiLe, _cardXuatSac;
        private BarChartPanel _barChart;
        private DonutChartPanel _donut;
        private DataGridView  _dgvHoatDong;

        public MenuAdmin()
        {
            InitializeComponent();
            ThemeApplier.Apply(this);
            KhoiTaoDashboard();
            this.Load += MenuAdmin_Load;

            button9.Click  += (s, e) => MoForm(new SinhVien());
            button1.Click  += (s, e) => MoForm(new GiangVien());
            button2.Click  += (s, e) => MoForm(new quản_lí_điểm_sinh_viên.QLTK());
            button3.Click  += (s, e) => MoForm(new quản_lí_điểm_sinh_viên.QLTK());
            button4.Click  += (s, e) => MoForm(new MonHoc());
            button5.Click  += (s, e) => MoForm(new LopHoc());
            button6.Click  += (s, e) => MoForm(new LopHoc());
            button7.Click  += (s, e) => MoFormBangDiem();
            button8.Click  += (s, e) => MoForm(new quản_lí_điểm_sinh_viên.ThongKe());
            button10.Click += (s, e) => MoForm(new quản_lí_điểm_sinh_viên.ThongKe());
            button11.Click += (s, e) => this.Close();
            button12.Click += (s, e) => new quản_lí_điểm_sinh_viên.DoiMK().ShowDialog();
        }

        // ─────────────────────────────────────────────────────────
        //  KHỞI TẠO CÁC CONTROL DASHBOARD
        // ─────────────────────────────────────────────────────────
        private void KhoiTaoDashboard()
        {
            // Xóa hết các panel cũ trong panel3 (khu nội dung chính)
            panel3.Controls.Clear();
            panel3.BackColor = Color.Transparent;

            // ── Tiêu đề Dashboard ──────────────────────────────────
            var lblDash = new Label
            {
                Text      = "Tổng quan hệ thống",
                Font      = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = T.TextMain,
                Location  = new Point(8, 6),
                Size      = new Size(280, 28),
                BackColor = Color.Transparent
            };
            var lblSub = new Label
            {
                Name      = "lblDashSub",
                Text      = "Đang tải...",
                Font      = new Font("Segoe UI", 8.5f),
                ForeColor = T.TextSub,
                Location  = new Point(panel3.Width - 220, 10),
                Size      = new Size(210, 20),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleRight
            };
            panel3.Controls.Add(lblDash);
            panel3.Controls.Add(lblSub);

            // ── 4 Stat Cards ────────────────────────────────────────
            var cardColors = new[]
            {
                T.Primary,                          // Tổng SV
                Color.FromArgb(6, 182, 212),        // DTB (cyan)
                Color.FromArgb(16, 185, 129),       // Tỉ lệ (emerald)
                Color.FromArgb(245, 158, 11),       // Xuất sắc (amber)
            };

            // 4 cards: panel3=568px. Mỗi card 132px, gap=6 → 4*132+3*6=546 ✓
            int cw = 132, ch = 110, gap = 6, cx0 = 8;

            _cardSV = new StatCard
            {
                CardTitle  = "Tổng Sinh Viên",
                Icon       = "👥", Value = "—", BadgeText = "đang tải...", BadgeUp = true,
                IconBg     = Color.FromArgb(209, 250, 229), IconColor = T.Primary,
                Location   = new Point(cx0, 36), Size = new Size(cw, ch)
            };
            _cardDTB = new StatCard
            {
                CardTitle  = "Điểm TB",
                Icon       = "📈", Value = "—", BadgeText = "đang tải...", BadgeUp = true,
                IconBg     = Color.FromArgb(219, 234, 254), IconColor = Color.FromArgb(59, 130, 246),
                Location   = new Point(cx0+(cw+gap), 36), Size = new Size(cw, ch)
            };
            _cardTiLe = new StatCard
            {
                CardTitle  = "Tỉ Lệ Đạt",
                Icon       = "✅", Value = "—", BadgeText = "đang tải...", BadgeUp = true,
                IconBg     = Color.FromArgb(220, 252, 231), IconColor = Color.FromArgb(22, 163, 74),
                Location   = new Point(cx0+(cw+gap)*2, 36), Size = new Size(cw, ch)
            };
            _cardXuatSac = new StatCard
            {
                CardTitle  = "Xuất Sắc (A)",
                Icon       = "⭐", Value = "—", BadgeText = "≥8.5 điểm", BadgeUp = true,
                IconBg     = Color.FromArgb(254, 243, 199), IconColor = Color.FromArgb(217, 119, 6),
                Location   = new Point(cx0+(cw+gap)*3, 36), Size = new Size(cw, ch)
            };

            // Bar x=8, w=318 | Donut x=332, w=228 → 8+318+6+228+8=568 ✓
            _barChart = new BarChartPanel
            {
                Location = new Point(8, 154), Size = new Size(318, 224),
                Title    = "Điểm TB theo môn học"
            };
            WrapInCard(_barChart, panel3);

            _donut = new DonutChartPanel
            {
                Location = new Point(332, 154), Size = new Size(228, 224),
                Title    = "Phân loại học lực", SubTitle = "Theo điểm trung bình tích lũy"
            };
            WrapInCard(_donut, panel3);

            panel3.Controls.AddRange(new Control[]
            {
                lblDash, _cardSV, _cardDTB, _cardTiLe, _cardXuatSac
            });

            // ── Hoạt động gần đây (panel9 vẫn giữ) ───────────────
            KhoiTaoHoatDong();
        }

        private static void WrapInCard(Control ctrl, Control parent)
        {
            var wrap = new Panel
            {
                Location  = ctrl.Location,
                Size      = new Size(ctrl.Width + 4, ctrl.Height + 4),
                BackColor = Color.White
            };
            wrap.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using var path = T.RoundedRect(new Rectangle(1, 1, wrap.Width - 2, wrap.Height - 2), 10);
                g.FillPath(Brushes.White, path);
                using var pen = new Pen(T.Border, 1.2f);
                g.DrawPath(pen, path);
                // Accent line trên
                using var topBr = new SolidBrush(T.Primary);
                g.FillRectangle(topBr, 1, 1, wrap.Width - 2, 3);
            };
            ctrl.Location = new Point(2, 4);
            wrap.Controls.Add(ctrl);
            parent.Controls.Add(wrap);
        }

        private void KhoiTaoHoatDong()
        {
            panel9.Controls.Clear();
            panel9.BackColor = Color.White;
            panel9.Paint    += PaintHoatDongPanel;

            // Header label
            var lblHD = new Label
            {
                Text      = "⚡  Hoạt Động Gần Đây",
                Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = T.Primary,
                Location  = new Point(0, 0),
                Size      = new Size(panel9.Width, 32),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding   = new Padding(8, 0, 0, 0)
            };
            panel9.Controls.Add(lblHD);

            _dgvHoatDong = new DataGridView
            {
                Location              = new Point(0, 32),
                Size                  = new Size(panel9.Width, panel9.Height - 32),
                ReadOnly              = true,
                AllowUserToAddRows    = false,
                RowHeadersVisible     = false,
                AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode         = DataGridViewSelectionMode.FullRowSelect,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                Font                  = new Font("Segoe UI", 8f),
                BackgroundColor       = Color.White,
                BorderStyle           = BorderStyle.None,
                GridColor             = Color.FromArgb(230, 245, 235),
                EnableHeadersVisualStyles = false
            };
            _dgvHoatDong.ColumnHeadersDefaultCellStyle.BackColor   = Color.FromArgb(4, 120, 87);
            _dgvHoatDong.ColumnHeadersDefaultCellStyle.ForeColor   = Color.White;
            _dgvHoatDong.ColumnHeadersDefaultCellStyle.Font        = new Font("Segoe UI", 8f, FontStyle.Bold);
            _dgvHoatDong.DefaultCellStyle.BackColor                 = Color.White;
            _dgvHoatDong.DefaultCellStyle.ForeColor                 = T.TextMain;
            _dgvHoatDong.DefaultCellStyle.SelectionBackColor        = Color.FromArgb(5, 150, 105);
            _dgvHoatDong.DefaultCellStyle.SelectionForeColor        = Color.White;
            _dgvHoatDong.AlternatingRowsDefaultCellStyle.BackColor  = Color.FromArgb(240, 253, 248);
            _dgvHoatDong.RowTemplate.Height = 28;

            panel9.Controls.Add(_dgvHoatDong);
        }

        private static void PaintHoatDongPanel(object s, PaintEventArgs e)
        {
            var p = (Panel)s;
            var g = e.Graphics;
            // Viền teal nhạt
            using var pen = new Pen(T.Border, 1f);
            g.DrawRectangle(pen, 0, 0, p.Width - 1, p.Height - 1);
            // Accent trên
            using var br = new SolidBrush(T.Primary);
            g.FillRectangle(br, 0, 0, p.Width, 3);
        }

        // Áp dụng StyleGrid cho DataGridView đơn lẻ
        private static void ThemeApplier_ApplyGrid(DataGridView dgv)
        {
        }

        // ─────────────────────────────────────────────────────────
        //  LOAD
        // ─────────────────────────────────────────────────────────
        private void MenuAdmin_Load(object sender, EventArgs e)
        {
            label2.Text = $"Xin chào, Admin: {SessionInfo.TenDangNhap}";
            TaiThongKeDashboard();
        }

        // ─────────────────────────────────────────────────────────
        //  TẢI DỮ LIỆU DASHBOARD
        // ─────────────────────────────────────────────────────────
        private void TaiThongKeDashboard()
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                // ── Tổng quan ─────────────────────────────────────
                using var cmd1 = new SqlCommand(@"
                    SELECT
                        (SELECT COUNT(*) FROM SinhVien WHERE TinhTrang='DangHoc') AS TongSV,
                        (SELECT COUNT(DISTINCT MaMH) FROM LopHocPhan
                         WHERE MaHK IN (SELECT MaHK FROM HocKy WHERE TrangThai='DangDienRa')) AS MonHKNay,
                        (SELECT COUNT(*) FROM LopHocPhan WHERE TrangThai='DangMo') AS LopHPDangMo,
                        CAST(ISNULL((SELECT AVG(CAST(DiemTB AS FLOAT)) FROM Diem
                                     JOIN DangKyHocPhan dk ON Diem.MaDK=dk.MaDK
                                     WHERE dk.TrangThai='HoanThanh'),0) AS DECIMAL(4,2)) AS DTBChung,
                        CAST(ISNULL((SELECT COUNT(CASE WHEN KetQua='Dat' THEN 1 END)*100.0/
                                            NULLIF(COUNT(*),0) FROM Diem
                                     JOIN DangKyHocPhan dk ON Diem.MaDK=dk.MaDK
                                     WHERE dk.TrangThai='HoanThanh'),0) AS DECIMAL(5,1)) AS TiLeDat,
                        (SELECT COUNT(*) FROM Diem WHERE XepLoai='A'
                         AND MaDK IN (SELECT MaDK FROM DangKyHocPhan WHERE TrangThai='HoanThanh')) AS SoXuatSac", conn);

                // Đọc hết r1 trước, đóng reader rồi mới chạy query tiếp
                int    tongSV  = 0;
                double dtb     = 0, tiLe = 0;
                int    xSac    = 0, lopMo = 0;

                using (var r1 = cmd1.ExecuteReader())
                {
                    if (r1.Read())
                    {
                        tongSV = SafeInt(r1, "TongSV");
                        dtb    = SafeDbl(r1, "DTBChung");
                        tiLe   = SafeDbl(r1, "TiLeDat");
                        xSac   = SafeInt(r1, "SoXuatSac");
                        lopMo  = SafeInt(r1, "LopHPDangMo");
                    }
                } // reader đóng ở đây

                // Lấy HK sau khi reader đã đóng
                string tenHK = "";
                using (var cmdHK = new SqlCommand(
                    "SELECT TOP 1 TenHK FROM HocKy WHERE TrangThai='DangDienRa'", conn))
                {
                    var hkVal = cmdHK.ExecuteScalar();
                    tenHK = hkVal != null && hkVal != DBNull.Value ? hkVal.ToString() : "";
                }

                var sub = panel3.Controls.Find("lblDashSub", false);
                if (sub.Length > 0) sub[0].Text = $"{lopMo} lớp đang mở  ·  {tenHK}";

                _cardSV.Value      = tongSV.ToString("N0");
                _cardSV.BadgeText  = lopMo + " lớp đang mở";
                _cardSV.BadgeUp    = true;

                _cardDTB.Value     = dtb.ToString("F2");
                _cardDTB.BadgeText = dtb >= 7 ? "Mức tốt" : "Cần cải thiện";
                _cardDTB.BadgeUp   = dtb >= 7;

                _cardTiLe.Value    = tiLe.ToString("F1") + "%";
                _cardTiLe.BadgeText= tiLe >= 80 ? "Đạt mục tiêu" : "Dưới mục tiêu";
                _cardTiLe.BadgeUp  = tiLe >= 80;

                _cardXuatSac.Value     = xSac.ToString("N0");
                _cardXuatSac.BadgeText = "≥8.5 điểm";
                _cardXuatSac.BadgeUp   = true;

                // ── Điểm TB theo môn (bar chart) ──────────────────
                using var cmd2 = new SqlCommand(@"
                    SELECT TOP 8 mh.TenMH,
                           CAST(AVG(CAST(d.DiemTB AS FLOAT)) AS DECIMAL(4,2)) AS DTBMon
                    FROM Diem d
                    JOIN DangKyHocPhan dk ON d.MaDK = dk.MaDK
                    JOIN LopHocPhan lhp   ON dk.MaLHP = lhp.MaLHP
                    JOIN MonHoc mh        ON lhp.MaMH = mh.MaMH
                    WHERE dk.TrangThai = 'HoanThanh'
                    GROUP BY mh.MaMH, mh.TenMH
                    ORDER BY DTBMon DESC", conn);

                var labels = new System.Collections.Generic.List<string>();
                var values = new System.Collections.Generic.List<double>();
                using var r2 = cmd2.ExecuteReader();
                while (r2.Read())
                {
                    labels.Add(r2["TenMH"] == DBNull.Value ? "" : r2["TenMH"].ToString());
                    values.Add(SafeDbl(r2, "DTBMon"));
                }
                _barChart.Labels   = labels.ToArray();
                _barChart.Values   = values.ToArray();
                _barChart.SubTitle = $"{labels.Count} môn học · học kỳ hiện tại";
                _barChart.Invalidate();

                // ── Phân bố xếp loại (donut) ──────────────────────
                using var cmd3 = new SqlCommand(@"
                    SELECT XepLoai, COUNT(*) AS SoLuong
                    FROM Diem d JOIN DangKyHocPhan dk ON d.MaDK=dk.MaDK
                    WHERE dk.TrangThai='HoanThanh'
                    GROUP BY XepLoai", conn);

                var xepLoaiMap = new Dictionary<string, int>
                {
                    ["A"]=0,["B+"]=0,["B"]=0,["C+"]=0,["C"]=0,["D+"]=0,["D"]=0,["F"]=0
                };
                using var r3 = cmd3.ExecuteReader();
                while (r3.Read())
                {
                    string xl = r3["XepLoai"]?.ToString() ?? "";
                    if (xepLoaiMap.ContainsKey(xl))
                        xepLoaiMap[xl] = Convert.ToInt32(r3["SoLuong"]);
                }
                // Gộp theo nhóm học lực cho legend có nghĩa
                _donut.Labels = new[] { "Xuất sắc", "Giỏi", "Khá", "TB", "Yếu", "Kém" };
                _donut.Values = new[]
                {
                    xepLoaiMap["A"],
                    xepLoaiMap["B+"],
                    xepLoaiMap["B"],
                    xepLoaiMap["C+"] + xepLoaiMap["C"],
                    xepLoaiMap["D+"],
                    xepLoaiMap["D"] + xepLoaiMap["F"]
                };
                _donut.Invalidate();

                // ── Refresh stat cards ─────────────────────────────
                _cardSV.Invalidate();
                _cardDTB.Invalidate();
                _cardTiLe.Invalidate();
                _cardXuatSac.Invalidate();

                // ── Hoạt động gần đây ──────────────────────────────
                TaiHoatDongGanDay();
            }
            catch
            {
                TaiThongKeDonGian();
            }
        }

        private void TaiThongKeDonGian()
        {
            try
            {
                using var conn = _db.GetConnection(); conn.Open();
                using var cmd = new SqlCommand(@"
                    SELECT
                        (SELECT COUNT(*) FROM SinhVien WHERE TinhTrang='DangHoc') AS TongSV,
                        (SELECT COUNT(*) FROM LopHocPhan WHERE TrangThai='DangMo') AS LopHP", conn);
                using var r = cmd.ExecuteReader();
                if (r.Read())
                {
                    _cardSV.Value     = r["TongSV"].ToString();
                    _cardSV.BadgeText = r["LopHP"] + " lớp đang mở";
                }
            }
            catch { }
            TaiHoatDongGanDay();
        }

        private void TaiHoatDongGanDay()
        {
            try
            {
                using var conn = _db.GetConnection(); conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT TOP 12 HanhDong, DoiTuong, ThoiGian FROM (
                        SELECT TOP 5 N'Nhập điểm' AS HanhDong,
                            sv.HoTen+N' – '+mh.TenMH AS DoiTuong, d.NgayNhap AS ThoiGian
                        FROM Diem d
                        JOIN DangKyHocPhan dk ON d.MaDK=dk.MaDK
                        JOIN SinhVien sv ON dk.MaSV=sv.MaSV
                        JOIN LopHocPhan lhp ON dk.MaLHP=lhp.MaLHP
                        JOIN MonHoc mh ON lhp.MaMH=mh.MaMH
                        ORDER BY d.NgayNhap DESC
                        UNION ALL
                        SELECT TOP 5 N'Đăng ký HP',
                            sv.HoTen+N' – '+mh.TenMH, dk.NgayDangKy
                        FROM DangKyHocPhan dk
                        JOIN SinhVien sv ON dk.MaSV=sv.MaSV
                        JOIN LopHocPhan lhp ON dk.MaLHP=lhp.MaLHP
                        JOIN MonHoc mh ON lhp.MaMH=mh.MaMH
                        ORDER BY dk.NgayDangKy DESC
                        UNION ALL
                        SELECT TOP 5 N'Đăng nhập',
                            TenDangNhap+N' ('+VaiTro+N')', LanDangNhapCuoi
                        FROM NguoiDung WHERE LanDangNhapCuoi IS NOT NULL
                        ORDER BY LanDangNhapCuoi DESC
                    ) t ORDER BY ThoiGian DESC", conn);

                using var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);

                _dgvHoatDong.AutoGenerateColumns = true;
                _dgvHoatDong.Columns.Clear();
                _dgvHoatDong.DataSource = dt;

                void H(string col, string hdr, int fw)
                {
                    if (_dgvHoatDong.Columns[col] == null) return;
                    _dgvHoatDong.Columns[col].HeaderText = hdr;
                    _dgvHoatDong.Columns[col].FillWeight  = fw;
                }
                H("HanhDong", "Hành Động", 30);
                H("DoiTuong", "Đối Tượng",  48);
                H("ThoiGian", "Thời Gian",  22);
                if (_dgvHoatDong.Columns["ThoiGian"] != null)
                    _dgvHoatDong.Columns["ThoiGian"].DefaultCellStyle.Format = "dd/MM HH:mm";

                _dgvHoatDong.CellFormatting -= DgvHoatDong_CellFormatting;
                _dgvHoatDong.CellFormatting += DgvHoatDong_CellFormatting;
            }
            catch { }
        }

        private void DgvHoatDong_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _dgvHoatDong.Rows.Count) return;
            string h = _dgvHoatDong.Rows[e.RowIndex].Cells["HanhDong"]?.Value?.ToString() ?? "";
            _dgvHoatDong.Rows[e.RowIndex].DefaultCellStyle.BackColor = h switch
            {
                "Nhập điểm"  => Color.FromArgb(209, 250, 229),
                "Đăng ký HP" => Color.FromArgb(219, 234, 254),
                "Đăng nhập"  => Color.FromArgb(254, 249, 195),
                _            => Color.White
            };
        }

        // ── Safe conversion helpers ────────────────────────────
        private static int    SafeInt(System.Data.IDataRecord r, string col)
            => r[col] == DBNull.Value ? 0 : Convert.ToInt32(r[col]);
        private static double SafeDbl(System.Data.IDataRecord r, string col)
            => r[col] == DBNull.Value ? 0.0 : Convert.ToDouble(r[col]);
        private static decimal SafeDec(System.Data.IDataRecord r, string col)
            => r[col] == DBNull.Value ? 0m : Convert.ToDecimal(r[col]);

        private void MoForm(Form form)
        {
            this.Hide();
            form.FormClosed += (s, e) => { TaiThongKeDashboard(); this.Show(); };
            form.Show();
        }

        private void MoFormBangDiem() => MoForm(new SinhVien());
    }
}
