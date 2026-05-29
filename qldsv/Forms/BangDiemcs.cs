#nullable disable
using Microsoft.Data.SqlClient;
using System.Data;
using System.IO;
using qldsv;
using qldsv.Service;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace quản_lí_điểm_sinh_viên
{
    public partial class BangDiemcs : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;
        private readonly string _maSV;
        private DataTable _dtDiem = new DataTable();

        // Thông tin SV để dùng khi xuất
        private string _hoTen = "", _maLop = "", _tenKhoa = "";
        private decimal _gpa;
        private int _tongTC;

        public BangDiemcs(string maSV)
        {
            InitializeComponent();
            ThemeApplier.Apply(this);
            _maSV = maSV;

            this.Load += BangDiem_Load;
            comboBox1.SelectedIndexChanged += (s, e) => TaiBangDiem();
            button1.Click += btnXuatDiem_Click;      // Xuất điểm Excel
            button2.Click += btnLichSuDiem_Click;    // Lịch sử nhập điểm
            // button3 wired in Designer (Quay Lại)
        }

        private void BangDiem_Load(object sender, EventArgs e)
        {
            NapThongTinSV();
            NapComboHocKy();
            TaiBangDiem();
        }

        private void NapThongTinSV()
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT sv.MaSV, sv.HoTen, sv.MaLop, k.TenKhoa,
                           ISNULL(g.GPATichLuy, 0) AS GPA,
                           ISNULL(g.TongTinChi,  0) AS TongTC
                    FROM SinhVien sv
                    LEFT JOIN Lop l ON sv.MaLop = l.MaLop
                    LEFT JOIN Khoa k ON l.MaKhoa = k.MaKhoa
                    LEFT JOIN vw_GPATichLuy g ON sv.MaSV = g.MaSV
                    WHERE sv.MaSV = @MaSV", conn);
                cmd.Parameters.AddWithValue("@MaSV", _maSV);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    _hoTen   = reader["HoTen"]?.ToString() ?? "";
                    _maLop   = reader["MaLop"]?.ToString() ?? "";
                    _tenKhoa = reader["TenKhoa"]?.ToString() ?? "";
                    _gpa     = Convert.ToDecimal(reader["GPA"]);
                    _tongTC  = Convert.ToInt32(reader["TongTC"]);

                    label1.Text = "MSV: "   + reader["MaSV"].ToString();
                    label2.Text = "Họ Tên: " + _hoTen;
                    label3.Text = "Lớp: "   + _maLop;
                    label4.Text = "Khoa: "  + _tenKhoa;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải thông tin sinh viên:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapComboHocKy()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add(new BDComboHKItem("", "-- Tất cả học kỳ --"));
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT DISTINCT hk.MaHK, hk.TenHK
                    FROM HocKy hk
                    JOIN LopHocPhan lhp ON lhp.MaHK = hk.MaHK
                    JOIN DangKyHocPhan dk ON dk.MaLHP = lhp.MaLHP
                    WHERE dk.MaSV = @MaSV
                    ORDER BY hk.MaHK DESC", conn);
                cmd.Parameters.AddWithValue("@MaSV", _maSV);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    comboBox1.Items.Add(new BDComboHKItem(
                        reader["MaHK"].ToString()!,
                        reader["TenHK"].ToString()!));
            }
            catch { }
            comboBox1.SelectedIndex = 0;
        }

        private void TaiBangDiem()
        {
            string maHK = comboBox1.SelectedItem is BDComboHKItem hk && hk.MaHK != "" ? hk.MaHK : null;

            try
            {
                _dtDiem = _db.ExecuteSP("sp_LayBangDiem",
                    DatabaseHelper.Param("@MaSV", _maSV),
                    DatabaseHelper.Param("@MaHK", (object)maHK ?? DBNull.Value)
                );

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = _dtDiem;

                SetHeader("MaMH",    "Mã MH");
                SetHeader("TenMH",   "Môn Học");
                SetHeader("SoTinChi","TC");
                SetHeader("TenHK",   "Học Kỳ");
                SetHeader("DiemCC",  "CC");
                SetHeader("DiemGK",  "GK");
                SetHeader("DiemCK",  "CK");
                SetHeader("DiemTB",  "DTB");
                SetHeader("DiemGPA", "GPA");
                SetHeader("XepLoai", "Xếp Loại");
                SetHeader("KetQua",  "Kết Quả");

                HideCol("MaHK");
                HideCol("NamHoc");
                HideCol("DaChotDiem");

                dataGridView1.CellFormatting -= DataGridView1_CellFormatting;
                dataGridView1.CellFormatting += DataGridView1_CellFormatting;

                CapNhatThongKeTichLuy();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải bảng điểm:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count) return;
            string kq = dataGridView1.Rows[e.RowIndex].Cells["KetQua"]?.Value?.ToString() ?? "";
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = kq switch
            {
                "KhongDat" => Color.Red,
                "Dat"      => Color.FromArgb(0, 128, 0),
                _          => SystemColors.ControlText
            };
        }

        private void CapNhatThongKeTichLuy()
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT ISNULL(GPATichLuy,0) AS GPA, ISNULL(TongTinChi,0) AS TC FROM vw_GPATichLuy WHERE MaSV=@MaSV", conn);
                cmd.Parameters.AddWithValue("@MaSV", _maSV);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    _gpa    = Convert.ToDecimal(reader["GPA"]);
                    _tongTC = Convert.ToInt32(reader["TC"]);
                }
            }
            catch { }
        }

        // ───────────────────────────────────────────────────────────
        //  XUẤT ĐIỂM EXCEL
        // ───────────────────────────────────────────────────────────
        private void btnXuatDiem_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelPackage.License.SetNonCommercialPersonal("qldsv");

                string tenHK = comboBox1.SelectedItem?.ToString() ?? "Tất cả";

                using var dlg = new SaveFileDialog
                {
                    Filter   = "Excel Files|*.xlsx",
                    Title    = "Lưu bảng điểm",
                    FileName = $"BangDiem_{_maSV}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };
                if (dlg.ShowDialog() != DialogResult.OK) return;

                using var pkg = new ExcelPackage();
                var ws = pkg.Workbook.Worksheets.Add("BangDiem");

                // ── Tiêu đề ──
                ws.Cells[1, 1].Value = "BẢNG ĐIỂM SINH VIÊN";
                ws.Cells[1, 1, 1, 9].Merge = true;
                ws.Cells[1, 1].Style.Font.Bold = true;
                ws.Cells[1, 1].Style.Font.Size = 14;
                ws.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 1].Value = $"MSV: {_maSV}";        ws.Cells[2, 1, 2, 3].Merge = true;
                ws.Cells[3, 1].Value = $"Họ tên: {_hoTen}";    ws.Cells[3, 1, 3, 3].Merge = true;
                ws.Cells[4, 1].Value = $"Lớp: {_maLop}";       ws.Cells[4, 1, 4, 3].Merge = true;
                ws.Cells[5, 1].Value = $"Khoa: {_tenKhoa}";    ws.Cells[5, 1, 5, 3].Merge = true;
                ws.Cells[2, 5].Value = $"Học kỳ: {tenHK}";
                ws.Cells[3, 5].Value = $"GPA tích lũy: {_gpa:F2}";
                ws.Cells[4, 5].Value = $"Tín chỉ tích lũy: {_tongTC}";
                ws.Cells[5, 5].Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}";

                // ── Header bảng ──
                int row = 7;
                string[] headers = { "Mã MH", "Môn Học", "TC", "Học Kỳ", "CC", "GK", "CK", "DTB", "GPA", "Xếp Loại", "Kết Quả" };
                string[] cols    = { "MaMH",  "TenMH",   "SoTinChi", "TenHK", "DiemCC", "DiemGK", "DiemCK", "DiemTB", "DiemGPA", "XepLoai", "KetQua" };

                for (int c = 0; c < headers.Length; c++)
                    ws.Cells[row, c + 1].Value = headers[c];

                using (var rng = ws.Cells[row, 1, row, headers.Length])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.LightSteelBlue);
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                row++;

                // ── Dữ liệu ──
                foreach (DataRow dr in _dtDiem.Rows)
                {
                    for (int c = 0; c < cols.Length; c++)
                    {
                        if (_dtDiem.Columns.Contains(cols[c]))
                            ws.Cells[row, c + 1].Value = dr[cols[c]] == DBNull.Value ? "" : dr[cols[c]];
                    }

                    // Tô màu Kết Quả
                    string kq = dr["KetQua"]?.ToString() ?? "";
                    if (kq == "KhongDat")
                        ws.Cells[row, 1, row, headers.Length].Style.Font.Color.SetColor(Color.Red);
                    else if (kq == "Dat")
                        ws.Cells[row, 1, row, headers.Length].Style.Font.Color.SetColor(Color.DarkGreen);
                    row++;
                }

                // ── Tổng kết ──
                row++;
                ws.Cells[row, 1].Value = $"GPA tích lũy: {_gpa:F2}";
                ws.Cells[row, 1].Style.Font.Bold = true;
                ws.Cells[row, 3].Value = $"Tổng tín chỉ: {_tongTC}";
                ws.Cells[row, 3].Style.Font.Bold = true;

                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                File.WriteAllBytes(dlg.FileName, pkg.GetAsByteArray());

                MessageBox.Show("Xuất bảng điểm thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất điểm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ───────────────────────────────────────────────────────────
        //  LỊCH SỬ NHẬP ĐIỂM
        // ───────────────────────────────────────────────────────────
        private void btnLichSuDiem_Click(object sender, EventArgs e)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT
                        mh.TenMH                                            AS MonHoc,
                        hk.TenHK                                            AS HocKy,
                        ls.DiemCC_Cu                                        AS CC_Cu,
                        ls.DiemGK_Cu                                        AS GK_Cu,
                        ls.DiemCK_Cu                                        AS CK_Cu,
                        d.DiemCC                                            AS CC_Moi,
                        d.DiemGK                                            AS GK_Moi,
                        d.DiemCK                                            AS CK_Moi,
                        nd.TenDangNhap                                      AS NguoiSua,
                        FORMAT(ls.NgayThayDoi, N'dd/MM/yyyy HH:mm')        AS ThoiGian,
                        ISNULL(ls.LyDo, '')                                AS LyDo
                    FROM LichSuDiem ls
                    JOIN Diem d              ON ls.MaDiem = d.MaDiem
                    JOIN DangKyHocPhan dk    ON d.MaDK    = dk.MaDK
                    JOIN LopHocPhan lhp      ON dk.MaLHP  = lhp.MaLHP
                    JOIN MonHoc mh           ON lhp.MaMH  = mh.MaMH
                    JOIN HocKy hk            ON lhp.MaHK  = hk.MaHK
                    JOIN NguoiDung nd        ON ls.MaND_ThayDoi = nd.MaND
                    WHERE dk.MaSV = @MaSV
                    ORDER BY ls.NgayThayDoi DESC", conn);
                cmd.Parameters.AddWithValue("@MaSV", _maSV);

                using var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);

                new LichSuDiemDialog(_hoTen, _maSV, dt).ShowDialog(this);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải lịch sử điểm:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e) => this.Close();

        private void SetHeader(string colName, string header)
        {
            if (dataGridView1.Columns[colName] != null)
                dataGridView1.Columns[colName].HeaderText = header;
        }

        private void HideCol(string colName)
        {
            if (dataGridView1.Columns[colName] != null)
                dataGridView1.Columns[colName].Visible = false;
        }
    }

    // ─────────────────────────────────────────────────────────────────
    //  DIALOG LỊCH SỬ NHẬP ĐIỂM
    // ─────────────────────────────────────────────────────────────────
    internal class LichSuDiemDialog : Form
    {
        public LichSuDiemDialog(string hoTen, string maSV, DataTable dt)
        {
            this.Text            = $"Lịch sử nhập điểm – {hoTen} ({maSV})";
            this.Size            = new Size(860, 460);
            this.StartPosition   = FormStartPosition.CenterParent;
            this.MinimizeBox     = false;

            var dgv = new DataGridView
            {
                Dock                  = DockStyle.Fill,
                ReadOnly              = true,
                AllowUserToAddRows    = false,
                RowHeadersVisible     = false,
                AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode         = DataGridViewSelectionMode.FullRowSelect,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                BackgroundColor       = SystemColors.Window
            };

            var btnDong = new Button
            {
                Text        = "Đóng",
                Dock        = DockStyle.Bottom,
                Height      = 36,
                DialogResult = DialogResult.Cancel
            };
            btnDong.Click += (s, e) => this.Close();

            this.Controls.Add(dgv);
            this.Controls.Add(btnDong);
            this.CancelButton = btnDong;

            if (dt.Rows.Count == 0)
            {
                var lbl = new Label
                {
                    Text      = "Chưa có lịch sử nhập điểm nào.",
                    Dock      = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font      = new Font("Segoe UI", 10f)
                };
                this.Controls.Add(lbl);
                lbl.BringToFront();
                return;
            }

            dgv.AutoGenerateColumns = true;
            dgv.DataSource = dt;

            void H(string col, string hdr, int w = 0)
            {
                if (dgv.Columns[col] == null) return;
                dgv.Columns[col].HeaderText = hdr;
                if (w > 0) { dgv.Columns[col].AutoSizeMode = DataGridViewAutoSizeColumnMode.None; dgv.Columns[col].Width = w; }
            }

            H("MonHoc",   "Môn Học",       160);
            H("HocKy",    "Học Kỳ",        130);
            H("CC_Cu",    "CC Cũ",          55);
            H("GK_Cu",    "GK Cũ",          55);
            H("CK_Cu",    "CK Cũ",          55);
            H("CC_Moi",   "CC Mới",         55);
            H("GK_Moi",   "GK Mới",         55);
            H("CK_Moi",   "CK Mới",         55);
            H("NguoiSua", "Người Sửa",      90);
            H("ThoiGian", "Thời Gian",     120);
            H("LyDo",     "Lý Do",         100);

            // Tô màu dòng nếu điểm bị giảm
            dgv.CellFormatting += (s, ev) =>
            {
                if (ev.RowIndex < 0) return;
                var row = dgv.Rows[ev.RowIndex];
                bool giamDiem =
                    TryDec(row, "CK_Cu") > TryDec(row, "CK_Moi") ||
                    TryDec(row, "GK_Cu") > TryDec(row, "GK_Moi");
                row.DefaultCellStyle.BackColor = giamDiem
                    ? Color.FromArgb(255, 230, 230)
                    : Color.FromArgb(230, 255, 230);
            };
        }

        private static decimal TryDec(DataGridViewRow row, string col)
        {
            if (row.Cells[col] == null) return 0;
            return decimal.TryParse(row.Cells[col].Value?.ToString(), out decimal v) ? v : 0;
        }
    }

    internal class BDComboHKItem
    {
        public string MaHK { get; }
        public string TenHK { get; }
        public BDComboHKItem(string maHK, string tenHK) { MaHK = maHK; TenHK = tenHK; }
        public override string ToString() => TenHK;
    }
}
