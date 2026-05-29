#nullable disable
using Microsoft.Data.SqlClient;
using System.Data;
using qldsv.Service;

namespace qldsv
{
    public partial class ThemSV : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;
        private readonly string _maSV;      // null = Thêm mới, có giá trị = Sửa
        private bool _isEdit => _maSV != null;

        public ThemSV(string maSV = null)
        {
            InitializeComponent();
            ThemeApplier.Apply(this);
            _maSV = maSV;
            this.Load += ThemSV_Load;
            button1.Click += btnLuu_Click;
            button2.Click += btnDatLai_Click;
            button3.Click += btnHuy_Click;
        }

        private void ThemSV_Load(object sender, EventArgs e)
        {
            this.Text = _isEdit ? "Sửa Sinh Viên" : "Thêm Sinh Viên";

            // comboBox5 (Khoa) – lấy từ DB
            NapComboKhoa(comboBox5);
            comboBox5.SelectedIndexChanged += ComboBox5_SelectedIndexChanged;

            // comboBox4 (Ngành) – since we don't have Nganh table, maybe hide it? But for now, let's just ignore
            if (comboBox4.Items.Count == 0)
                comboBox4.Items.AddRange(new[] { "Kế toán", "Công nghệ thông tin", "Kỹ thuật", "Ngoại ngữ" });
            if (comboBox4.Items.Count > 0) comboBox4.SelectedIndex = 0;

            // comboBox3 (Lớp) cần lấy từ DB vì phụ thuộc dữ liệu thực
            string maKhoa = comboBox5.SelectedItem is ComboItem ci ? ci.Key : "";
            NapComboLop(comboBox3, maKhoa);

            // comboBox1 (Tình trạng) – map display text to DB values
            comboBox1.Items.Clear();
            comboBox1.Items.Add(new ComboItem("DangHoc", "Đang học"));
            comboBox1.Items.Add(new ComboItem("TotNghiep", "Đã tốt nghiệp"));
            comboBox1.Items.Add(new ComboItem("BoHoc", "Thôi học"));
            comboBox1.Items.Add(new ComboItem("BaoLuu", "Bảo lưu"));
            comboBox1.SelectedIndex = 0;

            textBox6.Text = DateTime.Now.Year.ToString();

            if (_isEdit)
            {
                textBox2.ReadOnly = true;
                NapThongTinSV();
            }
            else
            {
                SinhMaSV();
            }
        }

        private void NapComboKhoa(ComboBox cb)
        {
            cb.Items.Clear();
            try
            {
                using var conn = _db.GetConnection(); conn.Open();
                var cmd = new SqlCommand("SELECT MaKhoa, TenKhoa FROM Khoa ORDER BY TenKhoa", conn);
                using var r = cmd.ExecuteReader();
                while (r.Read()) cb.Items.Add(new ComboItem(r["MaKhoa"].ToString(), r["TenKhoa"].ToString()));
            }
            catch { }

            if (cb.Items.Count == 0)
                cb.Items.AddRange(new ComboItem[] { 
                    new ComboItem("CNTT", "Công nghệ thông tin"), 
                    new ComboItem("KT", "Kinh tế"), 
                    new ComboItem("KTMT", "Kỹ thuật"), 
                    new ComboItem("NN", "Ngoại ngữ") });

            if (cb.Items.Count > 0) cb.SelectedIndex = 0;
        }

        private void SinhMaSV()
        {
            try
            {
                using var conn = _db.GetConnection(); conn.Open();
                string nam = DateTime.Now.Year.ToString();
                var cmd = new SqlCommand(
                    $"SELECT ISNULL(MAX(CAST(SUBSTRING(MaSV,7,LEN(MaSV)) AS INT)),0)+1 FROM SinhVien WHERE MaSV LIKE 'SV{nam}%'",
                    conn);
                int next = Convert.ToInt32(cmd.ExecuteScalar());
                textBox2.Text = $"SV{nam}{next:D3}";
            }
            catch { textBox2.Text = "SV" + DateTime.Now.Year + "001"; }
        }

        private void NapThongTinSV()
        {
            try
            {
                using var conn = _db.GetConnection(); conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh,
                           sv.Email, sv.SoDienThoai, sv.DiaChi,
                           sv.MaLop, sv.MaND, sv.NamNhapHoc, sv.TinhTrang,
                           l.MaKhoa
                    FROM SinhVien sv
                    LEFT JOIN Lop  l ON sv.MaLop  = l.MaLop
                    WHERE sv.MaSV = @MaSV", conn);
                cmd.Parameters.AddWithValue("@MaSV", _maSV);

                using var r = cmd.ExecuteReader();
                if (r.Read())
                {
                    textBox2.Text = r["MaSV"].ToString();
                    textBox1.Text = r["HoTen"].ToString();
                    textBox3.Text = r["NgaySinh"] != DBNull.Value
                        ? Convert.ToDateTime(r["NgaySinh"]).ToString("dd/MM/yyyy") : "";
                    textBox7.Text = r["Email"]?.ToString() ?? "";
                    textBox5.Text = r["SoDienThoai"]?.ToString() ?? "";
                    textBox4.Text = r["DiaChi"]?.ToString() ?? "";
                    textBox6.Text = r["NamNhapHoc"]?.ToString() ?? DateTime.Now.Year.ToString();

                    string gt = r["GioiTinh"]?.ToString() ?? "Nam";
                    radioButton1.Checked = gt == "Nam";
                    radioButton2.Checked = gt != "Nam";

                    string maKhoa = r["MaKhoa"]?.ToString() ?? "";
                    SetComboByKey(comboBox5, maKhoa);   // Khoa (MaKhoa)
                    NapComboLop(comboBox3, maKhoa);
                    SetComboByValue(comboBox3, r["MaLop"]?.ToString());      // Lớp (MaLop)
                    
                    SetComboByKey(comboBox1, r["TinhTrang"]?.ToString());  // Tình trạng (by Key)
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải thông tin sinh viên:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            string maSV = textBox2.Text.Trim();
            string hoTen = textBox1.Text.Trim();
            string email = textBox7.Text.Trim();
            string sdt = textBox5.Text.Trim();
            string diaChi = textBox4.Text.Trim();
            string maLop = comboBox3.SelectedItem?.ToString() ?? "";
            string tthai = comboBox1.SelectedItem is ComboItem ci ? ci.Key : "DangHoc";
            string gioiTinh = radioButton1.Checked ? "Nam" : "Nu";

            if (string.IsNullOrEmpty(maLop))
            {
                MessageBox.Show("Vui lòng chọn Lớp.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBox6.Text.Trim(), out int namNhapHoc))
                namNhapHoc = DateTime.Now.Year;

            DateTime? ngaySinh = null;
            if (DateTime.TryParseExact(textBox3.Text.Trim(), "dd/MM/yyyy",
                    null, System.Globalization.DateTimeStyles.None, out var dt))
                ngaySinh = dt;

            try
            {
                using var conn = _db.GetConnection(); conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {
                    int maNDValue = 0;

                    if (_isEdit)
                    {
                        // Get MaND from current SinhVien
                        var cmdGetMaND = new SqlCommand("SELECT MaND FROM SinhVien WHERE MaSV = @MaSV", conn, transaction);
                        cmdGetMaND.Parameters.AddWithValue("@MaSV", _maSV);
                        var objMaND = cmdGetMaND.ExecuteScalar();
                        maNDValue = objMaND != DBNull.Value ? Convert.ToInt32(objMaND) : 0;

                        var cmd = new SqlCommand(@"
                            UPDATE SinhVien SET
                                HoTen       = @HoTen,
                                NgaySinh    = @NgaySinh,
                                GioiTinh    = @GioiTinh,
                                Email       = @Email,
                                SoDienThoai = @SDT,
                                DiaChi      = @DiaChi,
                                MaLop       = @MaLop,
                                NamNhapHoc  = @NamNhapHoc,
                                TinhTrang   = @TinhTrang
                            WHERE MaSV = @MaSV", conn, transaction);
                        cmd.Parameters.AddWithValue("@HoTen", hoTen);
                        cmd.Parameters.AddWithValue("@NgaySinh", (object)ngaySinh ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@SDT", sdt);
                        cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                        cmd.Parameters.AddWithValue("@MaLop", maLop);
                        cmd.Parameters.AddWithValue("@NamNhapHoc", namNhapHoc);
                        cmd.Parameters.AddWithValue("@TinhTrang", tthai);
                        cmd.Parameters.AddWithValue("@MaSV", _maSV);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // Kiểm tra trùng mã
                        var chk = new SqlCommand("SELECT COUNT(*) FROM SinhVien WHERE MaSV=@MaSV", conn, transaction);
                        chk.Parameters.AddWithValue("@MaSV", maSV);
                        if ((int)chk.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Mã sinh viên đã tồn tại!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            transaction.Rollback();
                            return;
                        }

                        // Tạo tài khoản NguoiDung trước
                        var cmdTK = new SqlCommand(@"
                            IF NOT EXISTS (SELECT 1 FROM NguoiDung WHERE TenDangNhap = @TDN)
                            BEGIN
                                INSERT INTO NguoiDung (TenDangNhap, MatKhau, VaiTro, TrangThai, NgayTao)
                                VALUES (@TDN, @MK, 'SinhVien', 1, GETDATE());
                                SELECT SCOPE_IDENTITY();
                            END
                            ELSE
                            BEGIN
                                SELECT MaND FROM NguoiDung WHERE TenDangNhap = @TDN;
                            END", conn, transaction);
                        cmdTK.Parameters.AddWithValue("@TDN", maSV);
                        cmdTK.Parameters.AddWithValue("@MK", maSV);
                        maNDValue = Convert.ToInt32(cmdTK.ExecuteScalar());

                        // Insert SinhVien with MaND
                        var cmd = new SqlCommand(@"
                            INSERT INTO SinhVien
                                (MaSV, HoTen, NgaySinh, GioiTinh, Email, SoDienThoai,
                                 DiaChi, MaLop, MaND, NamNhapHoc, TinhTrang)
                            VALUES
                                (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @Email, @SDT,
                                 @DiaChi, @MaLop, @MaND, @NamNhapHoc, @TinhTrang)", conn, transaction);
                        cmd.Parameters.AddWithValue("@MaSV", maSV);
                        cmd.Parameters.AddWithValue("@HoTen", hoTen);
                        cmd.Parameters.AddWithValue("@NgaySinh", (object)ngaySinh ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@SDT", sdt);
                        cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                        cmd.Parameters.AddWithValue("@MaLop", maLop);
                        cmd.Parameters.AddWithValue("@MaND", maNDValue);
                        cmd.Parameters.AddWithValue("@NamNhapHoc", namNhapHoc);
                        cmd.Parameters.AddWithValue("@TinhTrang", tthai);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    MessageBox.Show(_isEdit ? "Cập nhật sinh viên thành công!" : "Thêm sinh viên thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDatLai_Click(object sender, EventArgs e)
        {
            if (_isEdit) { NapThongTinSV(); return; }

            textBox1.Clear(); textBox3.Clear(); textBox7.Clear();
            textBox5.Clear(); textBox4.Clear();
            textBox6.Text = DateTime.Now.Year.ToString();
            radioButton1.Checked = true;
            comboBox1.SelectedIndex = 0;
            if (comboBox5.Items.Count > 0) comboBox5.SelectedIndex = 0;
            if (comboBox4.Items.Count > 0) comboBox4.SelectedIndex = 0;
            if (comboBox3.Items.Count > 0) comboBox3.SelectedIndex = 0;
        }

        private void btnHuy_Click(object sender, EventArgs e) => this.Close();

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            { MessageBox.Show("Vui lòng nhập Mã SV.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            { MessageBox.Show("Vui lòng nhập Họ Tên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            return true;
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maKhoa = comboBox5.SelectedItem is ComboItem ci ? ci.Key : "";
            NapComboLop(comboBox3, maKhoa);
        }

        private void NapComboLop(ComboBox cb, string maKhoa = "")
        {
            cb.Items.Clear();
            try
            {
                using var conn = _db.GetConnection(); conn.Open();
                string sql = "SELECT MaLop FROM Lop";
                if (!string.IsNullOrEmpty(maKhoa))
                    sql += " WHERE MaKhoa = @MaKhoa";
                sql += " ORDER BY MaLop";
                
                var cmd = new SqlCommand(sql, conn);
                if (!string.IsNullOrEmpty(maKhoa))
                    cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);
                
                using var r = cmd.ExecuteReader();
                while (r.Read()) cb.Items.Add(r["MaLop"].ToString());
            }
            catch { }

            if (cb.Items.Count == 0)
            {
                // Fallback: distinct từ SinhVien
                try
                {
                    using var conn2 = _db.GetConnection(); conn2.Open();
                    var cmd2 = new SqlCommand(
                        "SELECT DISTINCT MaLop FROM SinhVien WHERE MaLop IS NOT NULL ORDER BY MaLop", conn2);
                    using var r2 = cmd2.ExecuteReader();
                    while (r2.Read()) cb.Items.Add(r2["MaLop"].ToString());
                }
                catch { }
            }

            if (cb.Items.Count == 0)
                cb.Items.AddRange(new[] { "CNTT01", "CNTT02", "KT01", "KT02" });

            if (cb.Items.Count > 0) cb.SelectedIndex = 0;
        }

        private void SetComboByValue(ComboBox cb, string value)
        {
            if (string.IsNullOrEmpty(value)) return;
            for (int i = 0; i < cb.Items.Count; i++)
            {
                if (string.Equals(cb.Items[i]?.ToString(), value, StringComparison.OrdinalIgnoreCase))
                {
                    cb.SelectedIndex = i;
                    return;
                }
            }
            // Không tìm thấy → giữ nguyên index 0
            if (cb.Items.Count > 0) cb.SelectedIndex = 0;
        }

        private void SetComboByKey(ComboBox cb, string key)
        {
            if (string.IsNullOrEmpty(key)) return;
            for (int i = 0; i < cb.Items.Count; i++)
            {
                if (cb.Items[i] is ComboItem ci && string.Equals(ci.Key, key, StringComparison.OrdinalIgnoreCase))
                {
                    cb.SelectedIndex = i;
                    return;
                }
            }
            // Không tìm thấy → giữ nguyên index 0
            if (cb.Items.Count > 0) cb.SelectedIndex = 0;
        }
    }
}