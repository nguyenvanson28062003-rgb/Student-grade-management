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
    public partial class NhapDiem : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;
        private string _maLHP = "";
        private DataTable _dtDiem = new DataTable();

        // Nhận MaLHP từ form gọi
        public NhapDiem(string maLHP)
        {
            InitializeComponent();
            ThemeApplier.Apply(this);
            _maLHP = maLHP;
            this.Load += NhapDiem_Load;

            button2.Click += btnLuuDiem_Click;    // Lưu điểm
            button3.Click += btnChotDiem_Click;   // Chốt điểm
            button1.Click += btnImport_Click;     // Import Excel
            button4.Click += btnExport_Click;     // Export
            button5.Click += btnThoat_Click;      // Thoát
            button6.Click += btnQuayLai_Click;    // Quay lại Menu
        }

        private void NhapDiem_Load(object sender, EventArgs e)
        {
            TaiThongTinLopVaDiem();
        }

        private void TaiThongTinLopVaDiem()
        {
            try
            {
                var ds = _db.ExecuteSPMultiResult("sp_LayDanhSachSVTrongLHP",
                    DatabaseHelper.Param("@MaLHP", _maLHP)
                );

                // Thông tin lớp — prefix cố định trong Designer, chỉ gán value
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var infoRow = ds.Tables[0].Rows[0];
                    label7.Text  = infoRow["MaLHP"]?.ToString()   ?? "";
                    label8.Text  = infoRow["TenMH"]?.ToString()   ?? "";
                    label9.Text  = infoRow["HoTenGV"]?.ToString() ?? "";
                    label10.Text = infoRow["TenHK"]?.ToString()   ?? "";
                }

                // Danh sách SV + điểm
                if (ds.Tables.Count > 1)
                {
                    _dtDiem = ds.Tables[1];

                    // Xóa các cột designer cũ, dùng auto-generate từ DataTable
                    dataGridView1.AutoGenerateColumns = true;
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = _dtDiem;

                    // Ẩn cột không cần hiển thị
                    HideCol("MaDK"); HideCol("MaDiem"); HideCol("KetQua"); HideCol("NgayNhap");

                    // Đặt ReadOnly cho cột tính toán
                    SetReadOnly("DiemTB", true);
                    SetReadOnly("DiemGPA", true);
                    SetReadOnly("XepLoai", true);

                    // Tên header thân thiện
                    SetHeader("MaSV", "MSV");
                    SetHeader("HoTen", "Họ Tên");
                    SetHeader("DiemCC", "Chuyên Cần");
                    SetHeader("DiemGK", "Giữa Kỳ");
                    SetHeader("DiemCK", "Cuối Kỳ");
                    SetHeader("DiemTB", "DTB");
                    SetHeader("DiemGPA", "GPA");
                    SetHeader("XepLoai", "Xếp Loại");
                    SetHeader("DaChotDiem", "Đã Chốt");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        private void SetReadOnly(string colName, bool readOnly)
        {
            if (dataGridView1.Columns[colName] != null)
                dataGridView1.Columns[colName].ReadOnly = readOnly;
        }

        private void btnLuuDiem_Click(object sender, EventArgs e)
        {
            LuuDiem(chotDiem: false);
        }

        private void btnChotDiem_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Sau khi chốt điểm sẽ KHÔNG thể sửa!\nBạn có chắc muốn chốt?",
                "Xác nhận chốt điểm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
                LuuDiem(chotDiem: true);
        }

        private void LuuDiem(bool chotDiem)
        {
            int loi = 0, thanhCong = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                if (!int.TryParse(row.Cells["MaDK"]?.Value?.ToString(), out int maDK)) continue;

                if (!decimal.TryParse(row.Cells["DiemCC"]?.Value?.ToString(), out decimal diemCC)) diemCC = 0;
                if (!decimal.TryParse(row.Cells["DiemGK"]?.Value?.ToString(), out decimal diemGK)) diemGK = 0;
                if (!decimal.TryParse(row.Cells["DiemCK"]?.Value?.ToString(), out decimal diemCK)) diemCK = 0;

                if (diemCC < 0 || diemCC > 10 || diemGK < 0 || diemGK > 10 || diemCK < 0 || diemCK > 10)
                {
                    MessageBox.Show($"Điểm phải từ 0–10 (dòng: {row.Cells["HoTen"]?.Value})",
                        "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    loi++;
                    continue;
                }

                try
                {
                    var ketQua = _db.ExecuteSPSingleRow("sp_NhapDiem",
                        DatabaseHelper.Param("@MaDK", maDK),
                        DatabaseHelper.Param("@DiemCC", diemCC),
                        DatabaseHelper.Param("@DiemGK", diemGK),
                        DatabaseHelper.Param("@DiemCK", diemCK),
                        DatabaseHelper.Param("@MaGV", SessionInfo.MaGV),
                        DatabaseHelper.Param("@ChotDiem", chotDiem ? 1 : 0),
                        DatabaseHelper.Param("@LyDo", (object?)null)
                    );

                    if (Convert.ToInt32(ketQua["KetQua"]) == 1)
                        thanhCong++;
                    else
                        loi++;
                }
                catch (SqlException)
                {
                    loi++;
                }
            }

            string thongBao = chotDiem
                ? $"Chốt điểm: {thanhCong} sinh viên thành công, {loi} lỗi."
                : $"Lưu điểm: {thanhCong} sinh viên thành công, {loi} lỗi.";

            MessageBox.Show(thongBao, "Kết quả",
                MessageBoxButtons.OK,
                loi > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

            TaiThongTinLopVaDiem();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelPackage.License.SetNonCommercialPersonal("qldsv");

                using var dlg = new OpenFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Chọn file Excel điểm"
                };
                if (dlg.ShowDialog() != DialogResult.OK) return;

                using var package = new ExcelPackage(new FileInfo(dlg.FileName));
                var ws = package.Workbook.Worksheets[0];
                if (ws == null || ws.Dimension == null)
                {
                    MessageBox.Show("File Excel trống hoặc không hợp lệ.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int ok = 0, fail = 0;
                for (int row = 2; row <= ws.Dimension.End.Row; row++)
                {
                    string maSV = ws.Cells[row, 1].Text.Trim();
                    if (string.IsNullOrEmpty(maSV)) continue;

                    // Tìm dòng trong DataGridView theo MaSV
                    DataGridViewRow dgvRow = null;
                    foreach (DataGridViewRow r in dataGridView1.Rows)
                    {
                        if (r.IsNewRow) continue;
                        if (r.Cells["MaSV"]?.Value?.ToString() == maSV)
                        {
                            dgvRow = r;
                            break;
                        }
                    }
                    if (dgvRow == null) { fail++; continue; }

                    string ccStr = ws.Cells[row, 2].Text.Trim();
                    string gkStr = ws.Cells[row, 3].Text.Trim();
                    string ckStr = ws.Cells[row, 4].Text.Trim();

                    bool valid = true;
                    if (!string.IsNullOrEmpty(ccStr))
                    {
                        if (decimal.TryParse(ccStr, out decimal cc) && cc >= 0 && cc <= 10)
                            dgvRow.Cells["DiemCC"].Value = cc;
                        else valid = false;
                    }
                    if (!string.IsNullOrEmpty(gkStr))
                    {
                        if (decimal.TryParse(gkStr, out decimal gk) && gk >= 0 && gk <= 10)
                            dgvRow.Cells["DiemGK"].Value = gk;
                        else valid = false;
                    }
                    if (!string.IsNullOrEmpty(ckStr))
                    {
                        if (decimal.TryParse(ckStr, out decimal ck) && ck >= 0 && ck <= 10)
                            dgvRow.Cells["DiemCK"].Value = ck;
                        else valid = false;
                    }

                    if (valid) ok++; else fail++;
                }

                MessageBox.Show($"Import thành công: {ok} sinh viên\nKhông hợp lệ: {fail}",
                    "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi import: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelPackage.License.SetNonCommercialPersonal("qldsv");

                using var dlg = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Lưu file điểm",
                    FileName = $"Diem_{_maLHP}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };
                if (dlg.ShowDialog() != DialogResult.OK) return;

                using var package = new ExcelPackage();
                var ws = package.Workbook.Worksheets.Add("Diem");

                // Header
                string[] headers = { "MSV", "Họ Tên", "Chuyên Cần", "Giữa Kỳ", "Cuối Kỳ", "DTB", "GPA", "Xếp Loại" };
                for (int col = 0; col < headers.Length; col++)
                    ws.Cells[1, col + 1].Value = headers[col];

                using (var rng = ws.Cells[1, 1, 1, headers.Length])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSteelBlue);
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // Data từ _dtDiem
                for (int i = 0; i < _dtDiem.Rows.Count; i++)
                {
                    var dr = _dtDiem.Rows[i];
                    ws.Cells[i + 2, 1].Value = dr["MaSV"]?.ToString();
                    ws.Cells[i + 2, 2].Value = dr["HoTen"]?.ToString();
                    ws.Cells[i + 2, 3].Value = dr["DiemCC"] == DBNull.Value ? "" : dr["DiemCC"].ToString();
                    ws.Cells[i + 2, 4].Value = dr["DiemGK"] == DBNull.Value ? "" : dr["DiemGK"].ToString();
                    ws.Cells[i + 2, 5].Value = dr["DiemCK"] == DBNull.Value ? "" : dr["DiemCK"].ToString();
                    ws.Cells[i + 2, 6].Value = dr["DiemTB"] == DBNull.Value ? "" : dr["DiemTB"].ToString();
                    ws.Cells[i + 2, 7].Value = dr["DiemGPA"] == DBNull.Value ? "" : dr["DiemGPA"].ToString();
                    ws.Cells[i + 2, 8].Value = dr["XepLoai"]?.ToString();
                }

                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                File.WriteAllBytes(dlg.FileName, package.GetAsByteArray());

                MessageBox.Show("Xuất Excel thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}