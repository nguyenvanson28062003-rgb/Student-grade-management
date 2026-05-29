#nullable disable
using Microsoft.Data.SqlClient;
using System.Data;
using qldsv.Service;
using OfficeOpenXml;
using System.IO;

namespace qldsv
{
    public partial class SinhVien : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;
        private DataTable _dtSinhVien = new DataTable();   // cache để tìm kiếm cục bộ

        public SinhVien()
        {
            InitializeComponent();
            ThemeApplier.Apply(this);
            this.Load += SinhVien_Load;

            // Gán sự kiện cho các nút
            button9.Click += btnThemMoi_Click;   
            button5.Click += btnSua_Click;        
            button2.Click += btnXoa_Click;        
            button10.Click += btnTimKiem_Click;   
            button1.Click += btnQuayVe_Click;     
            button6.Click += btnThoat_Click;      
            button3.Click += btnXemDiem_Click;    
            button4.Click += btnXemHoSo_Click;    
            button7.Click += btnExport_Click;     
            button8.Click += btnImport_Click;    

            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged; // lọc lớp
        }

        private void SinhVien_Load(object sender, EventArgs e)
        {
            NapComboBoxKhoa();
            NapComboBoxLop();
            NapComboBoxTinhTrang();
            TaiDanhSachSV();
        }

        // Nạp danh sách Khoa vào comboBox1
        private void NapComboBoxKhoa()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("-- Tất cả --");
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                // Thử lấy từ bảng Khoa trước
                try
                {
                    var cmd = new SqlCommand("SELECT MaKhoa, TenKhoa FROM Khoa ORDER BY TenKhoa", conn);
                    using var r = cmd.ExecuteReader();
                    while (r.Read())
                        comboBox1.Items.Add(r["TenKhoa"].ToString());
                }
                catch
                {
                    // Fallback: lấy distinct từ SinhVien
                    var cmd2 = new SqlCommand(
                        "SELECT DISTINCT k.TenKhoa FROM SinhVien sv JOIN Lop l ON sv.MaLop=l.MaLop JOIN Khoa k ON l.MaKhoa=k.MaKhoa ORDER BY k.TenKhoa", conn);
                    using var r2 = cmd2.ExecuteReader();
                    while (r2.Read())
                        comboBox1.Items.Add(r2["TenKhoa"].ToString());
                }
            }
            catch { }
            comboBox1.SelectedIndex = 0;
        }

        // Nạp danh sách Lớp vào comboBox2
        private void NapComboBoxLop()
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("-- Tất cả --");
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                // Thử bảng Lop trước
                try
                {
                    var cmd = new SqlCommand("SELECT MaLop FROM Lop ORDER BY MaLop", conn);
                    using var r = cmd.ExecuteReader();
                    while (r.Read())
                        comboBox2.Items.Add(r["MaLop"].ToString());
                }
                catch
                {
                    // Fallback: lấy distinct MaLop từ SinhVien
                    var cmd2 = new SqlCommand(
                        "SELECT DISTINCT MaLop FROM SinhVien WHERE MaLop IS NOT NULL ORDER BY MaLop", conn);
                    using var r2 = cmd2.ExecuteReader();
                    while (r2.Read())
                        comboBox2.Items.Add(r2["MaLop"].ToString());
                }
            }
            catch { }
            comboBox2.SelectedIndex = 0;
        }

        // Nạp Tình trạng vào comboBox3
        private void NapComboBoxTinhTrang()
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("-- Tất cả --");
            comboBox3.Items.Add("DangHoc");
            comboBox3.Items.Add("TotNghiep");
            comboBox3.Items.Add("BoHoc");
            comboBox3.Items.Add("BaoLuu");
            comboBox3.SelectedIndex = 0;
        }

        private void TaiDanhSachSV(string tuKhoa = "", string maLop = "", string tinhTrang = "")
        {
            try
            {
                // Thử query có JOIN Lop/Khoa trước, nếu không có bảng thì fallback
                string sql = @"
                    SELECT
                        sv.MaSV,
                        sv.HoTen,
                        CONVERT(NVARCHAR, sv.NgaySinh, 103) AS NgaySinh,
                        sv.MaLop,
                        k.TenKhoa AS Khoa,
                        sv.TinhTrang,
                        ISNULL(g.GPATichLuy, 0) AS GPA
                    FROM SinhVien sv
                    LEFT JOIN Lop l          ON sv.MaLop  = l.MaLop
                    LEFT JOIN Khoa k         ON l.MaKhoa  = k.MaKhoa
                    LEFT JOIN vw_GPATichLuy g ON sv.MaSV   = g.MaSV
                    WHERE 1=1
                      AND (@TuKhoa    = '' OR sv.MaSV LIKE '%'+@TuKhoa+'%'
                                          OR sv.HoTen LIKE '%'+@TuKhoa+'%')
                      AND (@MaLop     = '' OR sv.MaLop    = @MaLop)
                      AND (@TinhTrang = '' OR sv.TinhTrang = @TinhTrang)
                    ORDER BY sv.HoTen";

                using var conn = _db.GetConnection();
                conn.Open();
                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TuKhoa", tuKhoa);
                cmd.Parameters.AddWithValue("@MaLop", maLop);
                cmd.Parameters.AddWithValue("@TinhTrang", tinhTrang);

                using var da = new SqlDataAdapter(cmd);
                _dtSinhVien = new DataTable();
                da.Fill(_dtSinhVien);

                // Xóa cột Designer cũ, dùng auto-generate từ DataTable
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = _dtSinhVien;

                // Gán tên cột hiển thị
                if (dataGridView1.Columns["MaSV"] != null) dataGridView1.Columns["MaSV"].HeaderText = "MSV";
                if (dataGridView1.Columns["HoTen"] != null) dataGridView1.Columns["HoTen"].HeaderText = "Họ Tên";
                if (dataGridView1.Columns["NgaySinh"] != null) dataGridView1.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
                if (dataGridView1.Columns["MaLop"] != null) dataGridView1.Columns["MaLop"].HeaderText = "Lớp";
                if (dataGridView1.Columns["Khoa"] != null) dataGridView1.Columns["Khoa"].HeaderText = "Khoa";
                if (dataGridView1.Columns["TinhTrang"] != null) dataGridView1.Columns["TinhTrang"].HeaderText = "Tình Trạng";
                if (dataGridView1.Columns["GPA"] != null) dataGridView1.Columns["GPA"].HeaderText = "GPA";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải danh sách sinh viên:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = textBox2.Text.Trim();
            // comboBox2 chứa MaLop (string), comboBox3 chứa TinhTrang
            string maLop = comboBox2.SelectedIndex > 0 ? comboBox2.SelectedItem?.ToString() ?? "" : "";
            string tinhTrang = comboBox3.SelectedIndex > 0 ? comboBox3.SelectedItem?.ToString() ?? "" : "";
            TaiDanhSachSV(tuKhoa, maLop, tinhTrang);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnTimKiem_Click(sender, e);
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            var frm = new ThemSV(null);   // null = thêm mới
            frm.ShowDialog();
            NapComboBoxLop();             // Refresh lớp (có thể vừa thêm lớp mới)
            TaiDanhSachSV();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            string maSV = dataGridView1.CurrentRow.Cells["MaSV"].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(maSV)) return;

            var frm = new ThemSV(maSV);   // truyền MaSV = chế độ sửa
            frm.ShowDialog();
            TaiDanhSachSV();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            string maSV = dataGridView1.CurrentRow.Cells["MaSV"].Value?.ToString() ?? "";
            string hoTen = dataGridView1.CurrentRow.Cells["HoTen"].Value?.ToString() ?? "";

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa sinh viên [{maSV}] {hoTen}?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var transaction = conn.BeginTransaction(); // Sử dụng transaction để đảm bảo tính nguyên tử
                try
                {
                    // Kiểm tra SV đã có dữ liệu đăng ký / điểm chưa
                    var chk = new SqlCommand(
                        "SELECT COUNT(*) FROM DangKyHocPhan WHERE MaSV = @MaSV", conn, transaction);
                    chk.Parameters.AddWithValue("@MaSV", maSV);
                    int soDK = (int)chk.ExecuteScalar();

                    if (soDK > 0)
                    {
                        // Có lịch sử → chỉ đổi tình trạng, không xóa
                        var res = MessageBox.Show(
                            $"Sinh viên [{maSV}] đã có {soDK} lần đăng ký học phần.\n" +
                            "Không thể xóa để bảo toàn hồ sơ học tập.\n\n" +
                            "Bạn có muốn chuyển tình trạng sang \"BoHoc\" (Bỏ học) thay thế không?",
                            "Không thể xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (res == DialogResult.Yes)
                        {
                            var upd = new SqlCommand(
                                "UPDATE SinhVien SET TinhTrang = 'BoHoc' WHERE MaSV = @MaSV", conn, transaction);
                            upd.Parameters.AddWithValue("@MaSV", maSV);
                            upd.ExecuteNonQuery();
                            transaction.Commit();
                            MessageBox.Show("Đã chuyển tình trạng sang Bỏ Học.", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            TaiDanhSachSV();
                        }
                        return;
                    }

                    // Chưa có dữ liệu → xóa thật
                    // 1. Lấy MaND từ SinhVien trước
                    var cmdGetMaND = new SqlCommand("SELECT MaND FROM SinhVien WHERE MaSV = @MaSV", conn, transaction);
                    cmdGetMaND.Parameters.AddWithValue("@MaSV", maSV);
                    var objMaND = cmdGetMaND.ExecuteScalar();
                    int maND = objMaND != DBNull.Value ? Convert.ToInt32(objMaND) : 0;

                    // 2. Xóa SinhVien trước (vì nó có FK đến NguoiDung)
                    var cmdDelSV = new SqlCommand("DELETE FROM SinhVien WHERE MaSV = @MaSV", conn, transaction);
                    cmdDelSV.Parameters.AddWithValue("@MaSV", maSV);
                    int rowsDelSV = cmdDelSV.ExecuteNonQuery();

                    // 3. Xóa NguoiDung
                    if (maND > 0)
                    {
                        var cmdDelND = new SqlCommand("DELETE FROM NguoiDung WHERE MaND = @MaND", conn, transaction);
                        cmdDelND.Parameters.AddWithValue("@MaND", maND);
                        cmdDelND.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    if (rowsDelSV > 0)
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TaiDanhSachSV();
                    }
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuayVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnXemDiem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên trước!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maSV = dataGridView1.CurrentRow.Cells["MaSV"].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(maSV)) return;
            var frm = new quản_lí_điểm_sinh_viên.BangDiemcs(maSV);
            frm.ShowDialog();
        }

        private void btnXemHoSo_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên trước!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maSV = dataGridView1.CurrentRow.Cells["MaSV"].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(maSV)) return;
            var frm = new XemHoSo(maSV);
            frm.ShowDialog();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelPackage.License.SetNonCommercialPersonal("qldsv");
                
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel Files|*.xlsx";
                saveDialog.Title = "Lưu Danh Sách Sinh Viên";
                saveDialog.FileName = $"DanhSachSinhVien_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("DanhSachSV");
                        
                        // Add headers
                        worksheet.Cells[1, 1].Value = "Mã Sinh Viên";
                        worksheet.Cells[1, 2].Value = "Họ Tên";
                        worksheet.Cells[1, 3].Value = "Ngày Sinh";
                        worksheet.Cells[1, 4].Value = "Lớp";
                        worksheet.Cells[1, 5].Value = "Khoa";
                        worksheet.Cells[1, 6].Value = "Tình Trạng";
                        worksheet.Cells[1, 7].Value = "GPA";
                        
                        // Apply header styling
                        using (var range = worksheet.Cells[1, 1, 1, 7])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                            range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        }
                        
                        // Fill data
                        for (int i = 0; i < _dtSinhVien.Rows.Count; i++)
                        {
                            worksheet.Cells[i + 2, 1].Value = _dtSinhVien.Rows[i]["MaSV"]?.ToString();
                            worksheet.Cells[i + 2, 2].Value = _dtSinhVien.Rows[i]["HoTen"]?.ToString();
                            worksheet.Cells[i + 2, 3].Value = _dtSinhVien.Rows[i]["NgaySinh"]?.ToString();
                            worksheet.Cells[i + 2, 4].Value = _dtSinhVien.Rows[i]["MaLop"]?.ToString();
                            worksheet.Cells[i + 2, 5].Value = _dtSinhVien.Rows[i]["Khoa"]?.ToString();
                            worksheet.Cells[i + 2, 6].Value = _dtSinhVien.Rows[i]["TinhTrang"]?.ToString();
                            worksheet.Cells[i + 2, 7].Value = _dtSinhVien.Rows[i]["GPA"]?.ToString();
                        }
                        
                        // Auto-fit columns
                        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                        
                        // Save file
                        File.WriteAllBytes(saveDialog.FileName, package.GetAsByteArray());
                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelPackage.License.SetNonCommercialPersonal("qldsv");
                
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Filter = "Excel Files|*.xlsx;*.xls";
                openDialog.Title = "Chọn File Excel";
                
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    using (ExcelPackage package = new ExcelPackage(new FileInfo(openDialog.FileName)))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        
                        int successCount = 0;
                        int failCount = 0;
                        
                        for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                        {
                            try
                            {
                                string maSV = worksheet.Cells[row, 1].Text.Trim();
                                string hoTen = worksheet.Cells[row, 2].Text.Trim();
                                string ngaySinhStr = worksheet.Cells[row, 3].Text.Trim();
                                string maLop = worksheet.Cells[row, 4].Text.Trim();
                                string khoa = worksheet.Cells[row, 5].Text.Trim();
                                string tinhTrang = worksheet.Cells[row, 6].Text.Trim();
                                
                                if (string.IsNullOrEmpty(maSV) || string.IsNullOrEmpty(hoTen))
                                {
                                    failCount++;
                                    continue;
                                }
                                
                                // Convert string to DateTime (try different formats)
                                DateTime? ngaySinh = null;
                                if (!string.IsNullOrEmpty(ngaySinhStr))
                                {
                                    if (DateTime.TryParseExact(ngaySinhStr, "dd/MM/yyyy", 
                                        System.Globalization.CultureInfo.InvariantCulture, 
                                        System.Globalization.DateTimeStyles.None, out DateTime dt))
                                    {
                                        ngaySinh = dt;
                                    }
                                    else if (DateTime.TryParse(ngaySinhStr, out dt))
                                    {
                                        ngaySinh = dt;
                                    }
                                }
                                
                                // Try to find MaKhoa from Khoa table
                                string maKhoa = null;
                                if (!string.IsNullOrEmpty(khoa))
                                {
                                    using (var conn = _db.GetConnection())
                                    {
                                        conn.Open();
                                        var cmd = new SqlCommand("SELECT MaKhoa FROM Khoa WHERE TenKhoa = @TenKhoa", conn);
                                        cmd.Parameters.AddWithValue("@TenKhoa", khoa);
                                        var result = cmd.ExecuteScalar();
                                        if (result != null && result != DBNull.Value)
                                        {
                                            maKhoa = result.ToString();
                                        }
                                    }
                                }
                                
                                // Convert TinhTrang to correct format
                                string tinhTrangDB = "DangHoc";
                                if (!string.IsNullOrEmpty(tinhTrang))
                                {
                                    tinhTrang = tinhTrang.ToLower();
                                    if (tinhTrang == "đang học" || tinhTrang == "danghoc") tinhTrangDB = "DangHoc";
                                    else if (tinhTrang == "tốt nghiệp" || tinhTrang == "totnghiep") tinhTrangDB = "TotNghiep";
                                    else if (tinhTrang == "bảo lưu" || tinhTrang == "baoluu") tinhTrangDB = "BaoLuu";
                                    else if (tinhTrang == "bỏ học" || tinhTrang == "bohoc") tinhTrangDB = "BoHoc";
                                }
                                
                                // Check if MaSV already exists
                                bool exists = false;
                                using (var conn = _db.GetConnection())
                                {
                                    conn.Open();
                                    var cmd = new SqlCommand("SELECT 1 FROM SinhVien WHERE MaSV = @MaSV", conn);
                                    cmd.Parameters.AddWithValue("@MaSV", maSV);
                                    exists = cmd.ExecuteScalar() != null;
                                }
                                
                                if (!exists)
                                {
                                    // First create NguoiDung
                                    using (var conn = _db.GetConnection())
                                    {
                                        conn.Open();
                                        var cmd = new SqlCommand("INSERT INTO NguoiDung (TenDangNhap, MatKhau, VaiTro) VALUES (@TenDN, @MatKhau, 'SinhVien'); SELECT SCOPE_IDENTITY();", conn);
                                        cmd.Parameters.AddWithValue("@TenDN", maSV);
                                        cmd.Parameters.AddWithValue("@MatKhau", maSV); // Default password = MaSV
                                        var maNDObj = cmd.ExecuteScalar();
                                        if (maNDObj != null && maNDObj != DBNull.Value)
                                        {
                                            int maND = Convert.ToInt32(maNDObj);
                                            
                                            // Now create SinhVien
                                            cmd = new SqlCommand(@"INSERT INTO SinhVien (MaSV, MaND, HoTen, NgaySinh, MaLop, TinhTrang, NamNhapHoc) 
                                                VALUES (@MaSV, @MaND, @HoTen, @NgaySinh, @MaLop, @TinhTrang, @NamNhapHoc)", conn);
                                            cmd.Parameters.AddWithValue("@MaSV", maSV);
                                            cmd.Parameters.AddWithValue("@MaND", maND);
                                            cmd.Parameters.AddWithValue("@HoTen", hoTen);
                                            cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh.HasValue ? (object)ngaySinh.Value : DBNull.Value);
                                            cmd.Parameters.AddWithValue("@MaLop", !string.IsNullOrEmpty(maLop) ? (object)maLop : DBNull.Value);
                                            cmd.Parameters.AddWithValue("@TinhTrang", tinhTrangDB);
                                            cmd.Parameters.AddWithValue("@NamNhapHoc", DateTime.Now.Year); // Default to current year
                                            
                                            int affectedRows = cmd.ExecuteNonQuery();
                                            if (affectedRows > 0)
                                            {
                                                successCount++;
                                            }
                                            else
                                            {
                                                failCount++;
                                            }
                                        }
                                        else
                                        {
                                            failCount++;
                                        }
                                    }
                                }
                                else
                                {
                                    failCount++;
                                }
                            }
                            catch
                            {
                                failCount++;
                            }
                        }
                        
                        MessageBox.Show($"Nhập thành công: {successCount} sinh viên\nNhập không thành công: {failCount} sinh viên", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // Refresh the data
                        TaiDanhSachSV();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nhập Excel: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}