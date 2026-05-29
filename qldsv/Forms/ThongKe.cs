#nullable disable
using Microsoft.Data.SqlClient;
using System.Data;
using System.IO;
using System.Drawing.Drawing2D;
using qldsv;
using qldsv.Service;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace quản_lí_điểm_sinh_viên
{
    public partial class ThongKe : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;

        // ── Dashboard controls ──
        private StatCard        _cardSV, _cardMon, _cardTiLe, _cardDTB;
        private BarChartPanel   _barChart;
        private DonutChartPanel _donut;

        // ── Cache xuất báo cáo ──
        private int     _tongSV, _tongMon, _tongDat, _tongRot;
        private decimal _diemTBChung;
        private int     _soXuatSac, _soGioi, _soKha, _soTB, _soYeu, _soKem;
        private DataTable _dtMon = new DataTable();

        public ThongKe()
        {
            InitializeComponent();
            ThemeApplier.Apply(this);
            KhoiTaoDashboard();

            btnXemTK.Click   += (s, e) => XemThongKe();
            btnXuat.Click    += btnXuatBaoCao_Click;
            btnThoat.Click   += (s, e) => this.Close();

            comboHK.SelectedIndexChanged   += (s, e) => XemThongKe();
            comboKhoa.SelectedIndexChanged += (s, e) => XemThongKe();

            // Bo góc logo
            lblAppIcon.Region = new Region(T.RoundedRect(new Rectangle(0,0,44,44), 10));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            NapComboHocKy();
            NapComboKhoa();
            XemThongKe();
        }

        // ─────────────────────────────────────────────────────────
        //  KHỞI TẠO DASHBOARD
        // ─────────────────────────────────────────────────────────
        private void KhoiTaoDashboard()
        {
            // ── 4 Stat Cards — panelCards 868×112 ─────────────────
            int cw = 208, ch = 108, gap = 8;

            _cardSV = new StatCard
            {
                CardTitle = "TỔNG SINH VIÊN",
                Icon = "👥", IconBg = Color.FromArgb(209,250,229), IconColor = T.Primary,
                Value = "—", BadgeText = "đang tải...", BadgeUp = true,
                Location = new Point(0, 0), Size = new Size(cw, ch)
            };
            _cardMon = new StatCard
            {
                CardTitle = "SỐ MÔN HỌC",
                Icon = "📚", IconBg = Color.FromArgb(219,234,254), IconColor = Color.FromArgb(59,130,246),
                Value = "—", BadgeText = "trong kỳ lọc", BadgeUp = true,
                Location = new Point(cw+gap, 0), Size = new Size(cw, ch)
            };
            _cardTiLe = new StatCard
            {
                CardTitle = "TỶ LỆ ĐẠT",
                Icon = "✅", IconBg = Color.FromArgb(220,252,231), IconColor = Color.FromArgb(22,163,74),
                Value = "—", BadgeText = "đang tải...", BadgeUp = true,
                Location = new Point((cw+gap)*2, 0), Size = new Size(cw, ch)
            };
            _cardDTB = new StatCard
            {
                CardTitle = "ĐIỂM TB CHUNG",
                Icon = "📊", IconBg = Color.FromArgb(254,243,199), IconColor = Color.FromArgb(217,119,6),
                Value = "—", BadgeText = "đang tải...", BadgeUp = true,
                Location = new Point((cw+gap)*3, 0), Size = new Size(cw, ch)
            };
            panelCards.Controls.AddRange(new Control[] { _cardSV, _cardMon, _cardTiLe, _cardDTB });

            // ── Charts — panelCharts 868×280 ──────────────────────
            // Bar: 560px | Donut: 296px  → 560+8+296+4=868 ✓
            _barChart = new BarChartPanel
            {
                Title    = "Điểm trung bình theo môn học",
                SubTitle = "",
                Location = new Point(0, 0), Size = new Size(560, 280)
            };
            _donut = new DonutChartPanel
            {
                Title    = "Phân loại học lực",
                SubTitle = "Theo điểm TB tích lũy",
                Location = new Point(568, 0), Size = new Size(300, 280)
            };

            // Wrap vào card trắng
            var wBar   = CardWrap(_barChart,  0,    0, 560, 280);
            var wDonut = CardWrap(_donut,    568,   0, 300, 280);
            panelCharts.Controls.AddRange(new Control[] { wBar, wDonut });

            // ── Table subtitle ────────────────────────────────────
            var lblTableSub = new Label
            {
                Text      = "3 môn · học kỳ đang lọc",
                Name      = "lblTableSub",
                Font      = new Font("Segoe UI", 8.5f),
                ForeColor = T.TextSub,
                Location  = new Point(16, 34),
                Size      = new Size(400, 18),
                BackColor = Color.Transparent
            };
            panelTable.Controls.Add(lblTableSub);
        }

        // Bọc control trong card trắng bo góc
        private static Panel CardWrap(Control ctrl, int x, int y, int w, int h)
        {
            var wrap = new Panel { Location = new Point(x,y), Size = new Size(w,h), BackColor = Color.White };
            wrap.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using var path = T.RoundedRect(new Rectangle(0,0,w-1,h-1), 12);
                g.FillPath(Brushes.White, path);
                using var pen = new Pen(Color.FromArgb(229,231,235), 1);
                g.DrawPath(pen, path);
            };
            ctrl.Location = new Point(0, 0);
            wrap.Controls.Add(ctrl);
            return wrap;
        }

        // ─────────────────────────────────────────────────────────
        //  COMBOS
        // ─────────────────────────────────────────────────────────
        private void NapComboHocKy()
        {
            comboHK.Items.Clear();
            comboHK.Items.Add(new ComboHKItem("", "📅 Tất cả học kỳ"));
            try
            {
                using var conn = _db.GetConnection(); conn.Open();
                using var cmd  = new SqlCommand("SELECT MaHK,TenHK FROM HocKy ORDER BY MaHK DESC", conn);
                using var r    = cmd.ExecuteReader();
                while (r.Read())
                    comboHK.Items.Add(new ComboHKItem(r["MaHK"]!.ToString(), r["TenHK"]!.ToString()));
            }
            catch { }
            comboHK.SelectedIndex = 0;
        }

        private void NapComboKhoa()
        {
            comboKhoa.Items.Clear();
            comboKhoa.Items.Add(new TKKhoaItem("", "🏫 Tất cả khoa"));
            try
            {
                using var conn = _db.GetConnection(); conn.Open();
                using var cmd  = new SqlCommand("SELECT MaKhoa,TenKhoa FROM Khoa ORDER BY TenKhoa", conn);
                using var r    = cmd.ExecuteReader();
                while (r.Read())
                    comboKhoa.Items.Add(new TKKhoaItem(r["MaKhoa"]!.ToString(), r["TenKhoa"]!.ToString()));
            }
            catch { }
            comboKhoa.SelectedIndex = 0;
        }

        // ─────────────────────────────────────────────────────────
        //  XEM THỐNG KÊ
        // ─────────────────────────────────────────────────────────
        private void XemThongKe()
        {
            string maHK   = comboHK.SelectedItem   is ComboHKItem hk && hk.MaHK   != "" ? hk.MaHK   : null;
            string maKhoa = comboKhoa.SelectedItem is TKKhoaItem  ki && ki.MaKhoa != "" ? ki.MaKhoa : null;

            try
            {
                using var conn = _db.GetConnection(); conn.Open();

                string where = "WHERE dk.TrangThai = 'HoanThanh'";
                var ps = new List<SqlParameter>();
                if (maHK   != null) { where += " AND lhp.MaHK  = @MaHK";   ps.Add(new SqlParameter("@MaHK",   maHK)); }
                if (maKhoa != null) { where += " AND mh.MaKhoa = @MaKhoa"; ps.Add(new SqlParameter("@MaKhoa", maKhoa)); }

                // ── Tổng quan ──────────────────────────────────────
                var c1 = new SqlCommand($@"
                    SELECT COUNT(DISTINCT dk.MaSV)  AS TongSV,
                           COUNT(DISTINCT lhp.MaMH) AS TongMon,
                           CAST(ISNULL(AVG(CAST(d.DiemTB AS FLOAT)),0) AS DECIMAL(4,2)) AS DTB,
                           SUM(CASE WHEN d.KetQua='Dat' THEN 1 ELSE 0 END)      AS TongDat,
                           SUM(CASE WHEN d.KetQua='KhongDat' THEN 1 ELSE 0 END) AS TongRot
                    FROM Diem d
                    JOIN DangKyHocPhan dk ON d.MaDK  = dk.MaDK
                    JOIN LopHocPhan lhp   ON dk.MaLHP = lhp.MaLHP
                    JOIN MonHoc mh        ON lhp.MaMH = mh.MaMH {where}", conn);
                ps.ForEach(p => c1.Parameters.Add(new SqlParameter(p.ParameterName, p.Value)));

                using (var r1 = c1.ExecuteReader())
                {
                    if (r1.Read())
                    {
                        _tongSV      = SafeInt(r1, "TongSV");
                        _tongMon     = SafeInt(r1, "TongMon");
                        _diemTBChung = SafeDec(r1, "DTB");
                        _tongDat     = SafeInt(r1, "TongDat");
                        _tongRot     = SafeInt(r1, "TongRot");
                    }
                }

                int tongKQ = _tongDat + _tongRot;
                double tiLe = tongKQ > 0 ? _tongDat * 100.0 / tongKQ : 0;

                _cardSV.Value      = _tongSV.ToString("N0");
                _cardSV.BadgeText  = $"{_tongMon} môn học đang học";
                _cardSV.BadgeUp    = true;

                _cardMon.Value     = _tongMon.ToString();
                _cardMon.BadgeText = tongKQ > 0 ? $"{_tongDat} trong kỳ lọc" : "trong kỳ lọc";
                _cardMon.BadgeUp   = true;

                _cardTiLe.Value    = tiLe.ToString("F0") + "%";
                _cardTiLe.BadgeText= $"{_tongDat} đạt · {_tongRot} rớt";
                _cardTiLe.BadgeUp  = tiLe >= 80;

                _cardDTB.Value     = _diemTBChung.ToString("F2");
                _cardDTB.BadgeText = (double)_diemTBChung >= 7 ? "Mức tốt" : "Cần cải thiện";
                _cardDTB.BadgeUp   = (double)_diemTBChung >= 7;

                foreach (var c in new[] { _cardSV, _cardMon, _cardTiLe, _cardDTB }) c.Invalidate();

                // ── Xếp loại → Donut ──────────────────────────────
                var c2 = new SqlCommand($@"
                    SELECT d.XepLoai, COUNT(*) AS SL FROM Diem d
                    JOIN DangKyHocPhan dk ON d.MaDK=dk.MaDK
                    JOIN LopHocPhan lhp   ON dk.MaLHP=lhp.MaLHP
                    JOIN MonHoc mh        ON lhp.MaMH=mh.MaMH {where}
                    GROUP BY d.XepLoai", conn);
                ps.ForEach(p => c2.Parameters.Add(new SqlParameter(p.ParameterName, p.Value)));

                var xm = new Dictionary<string,int>
                { ["A"]=0,["B+"]=0,["B"]=0,["C+"]=0,["C"]=0,["D+"]=0,["D"]=0,["F"]=0 };
                using (var r2 = c2.ExecuteReader())
                {
                    while (r2.Read())
                    {
                        string xl = r2["XepLoai"] == DBNull.Value ? "" : r2["XepLoai"].ToString();
                        if (xm.ContainsKey(xl)) xm[xl] = SafeInt(r2, "SL");
                    }
                }

                _soXuatSac = xm["A"];
                _soGioi    = xm["B+"];
                _soKha     = xm["B"];
                _soTB      = xm["C+"] + xm["C"];
                _soYeu     = xm["D+"];
                _soKem     = xm["D"] + xm["F"];

                _donut.Labels = new[] { "Xuất sắc", "Giỏi", "Khá", "TB", "Yếu", "Kém" };
                _donut.Values = new[] { _soXuatSac, _soGioi, _soKha, _soTB, _soYeu, _soKem };
                _donut.Invalidate();

                // ── Bar chart DTB theo môn ─────────────────────────
                var c3 = new SqlCommand($@"
                    SELECT TOP 8 mh.TenMH,
                           CAST(AVG(CAST(d.DiemTB AS FLOAT)) AS DECIMAL(4,2)) AS DTBMon
                    FROM Diem d
                    JOIN DangKyHocPhan dk ON d.MaDK=dk.MaDK
                    JOIN LopHocPhan lhp   ON dk.MaLHP=lhp.MaLHP
                    JOIN MonHoc mh        ON lhp.MaMH=mh.MaMH {where}
                    GROUP BY mh.MaMH, mh.TenMH ORDER BY DTBMon DESC", conn);
                ps.ForEach(p => c3.Parameters.Add(new SqlParameter(p.ParameterName, p.Value)));

                var lbls = new List<string>(); var vals = new List<double>();
                using (var r3 = c3.ExecuteReader())
                {
                    while (r3.Read())
                    {
                        string ten = r3["TenMH"] == DBNull.Value ? "" : r3["TenMH"].ToString();
                        lbls.Add(ten.Length > 14 ? RutGon(ten) : ten);
                        vals.Add(SafeDbl(r3, "DTBMon"));
                    }
                }

                _barChart.Labels   = lbls.ToArray();
                _barChart.Values   = vals.ToArray();
                _barChart.SubTitle = $"Học kỳ hiện tại · {lbls.Count} môn";
                _barChart.Invalidate();

                // ── Bảng Đạt/Rớt ──────────────────────────────────
                var c4 = new SqlCommand($@"
                    SELECT mh.MaMH, mh.TenMH,
                        COUNT(CASE WHEN d.KetQua='Dat' THEN 1 END)      AS SoDat,
                        COUNT(CASE WHEN d.KetQua='KhongDat' THEN 1 END) AS SoRot,
                        CAST(ISNULL(AVG(CAST(d.DiemTB AS FLOAT)),0) AS DECIMAL(4,2)) AS DTBMon
                    FROM Diem d
                    JOIN DangKyHocPhan dk ON d.MaDK=dk.MaDK
                    JOIN LopHocPhan lhp   ON dk.MaLHP=lhp.MaLHP
                    JOIN MonHoc mh        ON lhp.MaMH=mh.MaMH {where}
                    GROUP BY mh.MaMH, mh.TenMH ORDER BY mh.TenMH", conn);
                ps.ForEach(p => c4.Parameters.Add(new SqlParameter(p.ParameterName, p.Value)));

                using var da = new SqlDataAdapter(c4);
                _dtMon = new DataTable(); da.Fill(_dtMon);

                // Thêm cột TrangThai
                if (!_dtMon.Columns.Contains("TrangThai"))
                    _dtMon.Columns.Add("TrangThai", typeof(string));
                foreach (DataRow row in _dtMon.Rows)
                {
                    int rot = row["SoRot"] == DBNull.Value ? 0 : Convert.ToInt32(row["SoRot"]);
                    row["TrangThai"] = rot == 0 ? "Đạt" : "Có rớt";
                }

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = _dtMon;

                // Tên cột
                void H(string col, string hdr, int fw = 100)
                {
                    if (dataGridView1.Columns[col] == null) return;
                    dataGridView1.Columns[col].HeaderText = hdr.ToUpper();
                    dataGridView1.Columns[col].FillWeight = fw;
                    dataGridView1.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                H("MaMH",    "Mã MH",    55);
                H("TenMH",   "Môn Học", 200);
                H("SoDat",   "Đạt",      55);
                H("SoRot",   "Rớt",      55);
                H("DTBMon",  "Điểm TB",  80);
                H("TrangThai","Trạng Thái", 80);

                // Custom cell rendering
                dataGridView1.CellPainting -= DgvCellPainting;
                dataGridView1.CellPainting += DgvCellPainting;

                // Subtitle bảng
                var subCtrl = panelTable.Controls.Find("lblTableSub", false);
                if (subCtrl.Length > 0)
                    subCtrl[0].Text = $"{_dtMon.Rows.Count} môn · học kỳ đang lọc";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Safe conversion helpers ────────────────────────────
        private static int     SafeInt(System.Data.IDataRecord r, string col)
            => r[col] == DBNull.Value ? 0 : Convert.ToInt32(r[col]);
        private static double  SafeDbl(System.Data.IDataRecord r, string col)
            => r[col] == DBNull.Value ? 0.0 : Convert.ToDouble(r[col]);
        private static decimal SafeDec(System.Data.IDataRecord r, string col)
            => r[col] == DBNull.Value ? 0m : Convert.ToDecimal(r[col]);

        // Rút gọn tên môn dài
        private static string RutGon(string ten)
        {
            var words = ten.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (words.Length <= 2) return ten[..Math.Min(12, ten.Length)] + ".";
            return string.Concat(words.Take(3).Select(w => char.ToUpper(w[0])));
        }

        // ─────────────────────────────────────────────────────────
        //  CUSTOM CELL RENDERING
        // ─────────────────────────────────────────────────────────
        private void DgvCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Nền
            bool sel = (e.State & DataGridViewElementStates.Selected) != 0;
            using var bgBr = new SolidBrush(sel ? Color.FromArgb(236,253,245) : Color.White);
            e.PaintBackground(e.ClipBounds, false);
            g.FillRectangle(bgBr, e.CellBounds);

            // Đường kẻ ngang dưới ô
            using var linePen = new Pen(Color.FromArgb(241,245,241), 1);
            g.DrawLine(linePen, e.CellBounds.Left, e.CellBounds.Bottom-1,
                                e.CellBounds.Right, e.CellBounds.Bottom-1);

            string colName = dataGridView1.Columns[e.ColumnIndex]?.Name ?? "";
            string val     = (e.Value == null || e.Value == DBNull.Value) ? "" : e.Value.ToString();
            int    py      = e.CellBounds.Y + (e.CellBounds.Height - 20) / 2;
            int    px      = e.CellBounds.X + 12;

            if (colName == "MaMH")
            {
                // Teal pill badge
                using var bFont = new Font("Segoe UI", 8f, FontStyle.Bold);
                var sz   = g.MeasureString(val, bFont);
                var rect = new Rectangle(px, py, (int)sz.Width + 16, 22);
                using var bPath = T.RoundedRect(rect, 6);
                g.FillPath(new SolidBrush(Color.FromArgb(204,251,241)), bPath);
                g.DrawPath(new Pen(Color.FromArgb(167,243,208), 1), bPath);
                var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(val, bFont, new SolidBrush(Color.FromArgb(4,120,87)),
                    new RectangleF(rect.X, rect.Y, rect.Width, rect.Height), sf);
            }
            else if (colName == "SoRot")
            {
                using var rFont = new Font("Segoe UI", 9.5f);
                bool isRot = int.TryParse(val, out int rv) && rv > 0;
                var color  = isRot ? Color.FromArgb(220, 53, 69) : T.TextMain;
                g.DrawString(val, rFont, new SolidBrush(color), new PointF(px, py + 1));
            }
            else if (colName == "TrangThai")
            {
                // Badge trạng thái
                bool ok = val != "Có rớt";
                using var tFont = new Font("Segoe UI", 8.5f, FontStyle.Bold);
                string txt = ok ? "✓ Đạt" : "⚠ Có rớt";
                var   sz   = g.MeasureString(txt, tFont);
                var   rect = new Rectangle(px, py, (int)sz.Width + 14, 22);
                using var bPath = T.RoundedRect(rect, 6);
                g.FillPath(new SolidBrush(ok ? Color.FromArgb(220,252,231) : Color.FromArgb(254,226,226)), bPath);
                var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(txt, tFont,
                    new SolidBrush(ok ? Color.FromArgb(22,163,74) : Color.FromArgb(185,28,28)),
                    new RectangleF(rect.X, rect.Y, rect.Width, rect.Height), sf);
            }
            else
            {
                using var dFont = new Font("Segoe UI", 9.5f);
                g.DrawString(val, dFont, new SolidBrush(T.TextMain), new PointF(px, py + 1));
            }

            e.Handled = true;
        }

        // ─────────────────────────────────────────────────────────
        //  XUẤT BÁO CÁO
        // ─────────────────────────────────────────────────────────
        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelPackage.License.SetNonCommercialPersonal("qldsv");
                using var dlg = new SaveFileDialog
                {
                    Filter   = "Excel Files|*.xlsx",
                    FileName = $"BaoCaoThongKe_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };
                if (dlg.ShowDialog() != DialogResult.OK) return;

                using var pkg = new ExcelPackage();
                var ws = pkg.Workbook.Worksheets.Add("ThongKe");

                ws.Cells[1,1].Value = "BÁO CÁO THỐNG KÊ ĐIỂM";
                ws.Cells[1,1,1,5].Merge = true;
                ws.Cells[1,1].Style.Font.Bold = true;
                ws.Cells[1,1].Style.Font.Size = 14;
                ws.Cells[1,1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[1,1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1,1].Style.Fill.BackgroundColor.SetColor(T.Primary);
                ws.Cells[1,1].Style.Font.Color.SetColor(Color.White);

                int row = 3;
                ws.Cells[row,1].Value = "Tổng SV"; ws.Cells[row,2].Value = _tongSV;
                ws.Cells[row,3].Value = "Số Môn";  ws.Cells[row,4].Value = _tongMon; row++;
                ws.Cells[row,1].Value = "Lượt Đạt"; ws.Cells[row,2].Value = _tongDat;
                ws.Cells[row,3].Value = "Lượt Rớt"; ws.Cells[row,4].Value = _tongRot; row++;
                int tq = _tongDat + _tongRot;
                ws.Cells[row,1].Value = "Tỉ lệ Đạt"; ws.Cells[row,2].Value = tq>0 ? $"{_tongDat*100.0/tq:F1}%" : "N/A";
                ws.Cells[row,3].Value = "ĐTB chung"; ws.Cells[row,4].Value = _diemTBChung.ToString("F2"); row += 2;

                // Bảng môn
                ws.Cells[row,1].Value="Mã MH"; ws.Cells[row,2].Value="Môn Học";
                ws.Cells[row,3].Value="Đạt";   ws.Cells[row,4].Value="Rớt";
                ws.Cells[row,5].Value="ĐTB";   ws.Cells[row,6].Value="Trạng Thái";
                var hdrRange = ws.Cells[row,1,row,6];
                hdrRange.Style.Font.Bold = true;
                hdrRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                hdrRange.Style.Fill.BackgroundColor.SetColor(T.Primary);
                hdrRange.Style.Font.Color.SetColor(Color.White); row++;

                foreach (DataRow dr in _dtMon.Rows)
                {
                    ws.Cells[row,1].Value = dr["MaMH"];   ws.Cells[row,2].Value = dr["TenMH"];
                    ws.Cells[row,3].Value = dr["SoDat"];  ws.Cells[row,4].Value = dr["SoRot"];
                    ws.Cells[row,5].Value = dr["DTBMon"]; ws.Cells[row,6].Value = dr["TrangThai"];
                    row++;
                }
                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                File.WriteAllBytes(dlg.FileName, pkg.GetAsByteArray());
                MessageBox.Show("Xuất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    internal class ComboHKItem
    {
        public string MaHK  { get; }
        public string TenHK { get; }
        public ComboHKItem(string maHK, string tenHK) { MaHK = maHK; TenHK = tenHK; }
        public override string ToString() => TenHK;
    }

    internal class TKKhoaItem
    {
        public string MaKhoa { get; }
        private readonly string _ten;
        public TKKhoaItem(string maKhoa, string ten) { MaKhoa = maKhoa; _ten = ten; }
        public override string ToString() => _ten;
    }
}
