#nullable disable
// ================================================================
//  ThemSuaMH.cs  –  Thêm / Sửa Môn Học
//  Namespace: qldsv
// ================================================================

using Microsoft.Data.SqlClient;
using System.Data;
using qldsv.Service;

namespace qldsv
{
    public partial class ThemSuaMH : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;
        private readonly string? _maMH;      // null = Thêm mới, có giá trị = Sửa
        private bool _isEdit => _maMH != null;
        private readonly List<ComboItem> _khoaItems = new();
        private readonly List<ComboItem> _hocKyItems = new();
        private readonly List<ComboItem> _gvItems = new();

        // --------------------------------------------------------
        //  Constructor
        // --------------------------------------------------------
        public ThemSuaMH(string? maMH)
        {
            InitializeComponent();
            _maMH = maMH;
            this.Load += ThemSuaMH_Load;
            button1.Click += btnLuu_Click;    // Lưu
            button2.Click += btnDatLai_Click; // Đặt Lại
            button3.Click += btnHuy_Click;    // Hủy
        }

        // --------------------------------------------------------
        //  Load
        // --------------------------------------------------------
        private void ThemSuaMH_Load(object sender, EventArgs e)
        {
            this.Text = _isEdit ? "Sửa Môn Học" : "Thêm Môn Học";

            // Clear designer's default items
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox7.Items.Clear();
            comboBox6.Items.Clear();
            comboBox1.Items.Clear();

            // Điền sẵn ComboBox loại môn (Key = DB value, Value = Vietnamese display)
            comboBox2.Items.Add(new ComboItem("LiThuyet", "Lí thuyết"));
            comboBox2.Items.Add(new ComboItem("ThucHanh", "Thực hành"));
            comboBox2.Items.Add(new ComboItem("DaiCuong", "Đại cương"));
            comboBox2.SelectedIndex = 0;

            // Khoa (lấy từ DB)
            NapComboKhoa(comboBox6);

            // Học kỳ áp dụng (Key = DB value, Value = Vietnamese display)
            comboBox7.Items.Add(new ComboItem("", "-- Không chọn --"));
            comboBox7.Items.Add(new ComboItem("HK1Nam1", "HK1 - Năm 1"));
            comboBox7.Items.Add(new ComboItem("HK2Nam1", "HK2 - Năm 1"));
            comboBox7.Items.Add(new ComboItem("HK1Nam2", "HK1 - Năm 2"));
            comboBox7.Items.Add(new ComboItem("HK2Nam2", "HK2 - Năm 2"));
            comboBox7.Items.Add(new ComboItem("HK1Nam3", "HK1 - Năm 3"));
            comboBox7.Items.Add(new ComboItem("HK2Nam3", "HK2 - Năm 3"));
            comboBox7.Items.Add(new ComboItem("HK1Nam4", "HK1 - Năm 4"));
            comboBox7.Items.Add(new ComboItem("HK2Nam4", "HK2 - Năm 4"));
            comboBox7.Items.Add(new ComboItem("HKHe", "Học kỳ hè"));
            comboBox7.SelectedIndex = 0;

            // Giảng viên (lấy từ DB)
            NapComboGiangVien(comboBox1);

            // Trạng thái (Key = DB value, Value = Vietnamese display)
            comboBox3.Items.Add(new ComboItem("DangMoLop", "Đang mở lớp"));
            comboBox3.Items.Add(new ComboItem("TamNgung", "Tạm ngưng"));
            comboBox3.Items.Add(new ComboItem("DaKhoa", "Đã khóa"));
            comboBox3.Items.Add(new ComboItem("DangCapNhat", "Đang cập nhật"));
            comboBox3.Items.Add(new ComboItem("ChoPheDuyet", "Chờ phê duyệt"));
            comboBox3.Items.Add(new ComboItem("DaHoanThanh", "Đã hoàn thành"));
            comboBox3.Items.Add(new ComboItem("ConMoDangKy", "Còn mở đăng ký"));
            comboBox3.Items.Add(new ComboItem("HetHanDangKy", "Hết hạn đăng ký"));
            comboBox3.SelectedIndex = 0;

            if (_isEdit)
            {
                textBox2.ReadOnly = true; // Mã môn không cho sửa
                NapThongTinMon();
            }
        }

        // --------------------------------------------------------
        //  Nạp danh sách giảng viên vào ComboBox
        // --------------------------------------------------------
        private void NapComboGiangVien(ComboBox cb)
        {
            cb.Items.Clear();
            _gvItems.Clear();
            cb.Items.Add(new ComboItem("", "-- Chưa phân công --"));
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT MaGV, HoTen FROM GiangVien ORDER BY HoTen",
                    conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var item = new ComboItem(reader["MaGV"].ToString()!, reader["HoTen"].ToString()!);
                    cb.Items.Add(item);
                    _gvItems.Add(item);
                }
            }
            catch { }
            cb.SelectedIndex = 0;
        }

        private void NapComboHocKy(ComboBox cb)
        {
            cb.Items.Clear();
            _hocKyItems.Clear();
            cb.Items.Add(new ComboItem("", "-- Không chọn --"));
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT MaHK, TenHK FROM HocKy ORDER BY MaHK DESC",
                    conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var item = new ComboItem(reader["MaHK"].ToString()!, reader["TenHK"].ToString()!);
                    cb.Items.Add(item);
                    _hocKyItems.Add(item);
                }
            }
            catch { }
            cb.SelectedIndex = 0;
        }

        private void NapComboKhoa(ComboBox cb)
        {
            cb.Items.Clear();
            _khoaItems.Clear();
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT MaKhoa, TenKhoa FROM Khoa ORDER BY TenKhoa",
                    conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var item = new ComboItem(reader["MaKhoa"].ToString()!, reader["TenKhoa"].ToString()!);
                    cb.Items.Add(item);
                    _khoaItems.Add(item);
                }
            }
            catch { }
            if (cb.Items.Count > 0) cb.SelectedIndex = 0;
        }

        // --------------------------------------------------------
        //  Nạp thông tin môn khi sửa
        // --------------------------------------------------------
        private void NapThongTinMon()
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT MaMH, TenMH, SoTinChi, LoaiMon, MaKhoa,
                           MoTa, HKApDung, TrangThai
                    FROM MonHoc
                    WHERE MaMH = @MaMH", conn);
                cmd.Parameters.AddWithValue("@MaMH", _maMH!);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox2.Text = reader["MaMH"].ToString();
                    textBox1.Text = reader["TenMH"].ToString();
                    textBox3.Text = reader["SoTinChi"].ToString();
                    textBox4.Text = reader["MoTa"]?.ToString() ?? "";

                    SetComboByKey(comboBox2, reader["LoaiMon"]?.ToString() ?? "");
                    SetComboByKey(comboBox6, reader["MaKhoa"]?.ToString() ?? "");
                    SetComboByKey(comboBox7, reader["HKApDung"]?.ToString() ?? "");
                    SetComboByKey(comboBox3, reader["TrangThai"]?.ToString() ?? "");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải thông tin môn học:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --------------------------------------------------------
        //  Lưu
        // --------------------------------------------------------
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            string maMH = textBox2.Text.Trim();
            string tenMH = textBox1.Text.Trim();
            string moTa = textBox4.Text.Trim();
            string loai = comboBox2.SelectedItem is ComboItem ciL ? ciL.Key : "LiThuyet";
            string maKhoa = comboBox6.SelectedItem is ComboItem ciK ? ciK.Key : "";
            string hkApDung = comboBox7.SelectedItem is ComboItem ciHK ? ciHK.Key : "";
            string tthai = comboBox3.SelectedItem is ComboItem ciT ? ciT.Key : "DangMoLop";

            if (!int.TryParse(textBox3.Text.Trim(), out int soTC) || soTC <= 0 || soTC > 10)
            {
                MessageBox.Show("Số tín chỉ phải là số nguyên từ 1–10.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                if (_isEdit)
                {
                    var cmd = new SqlCommand(@"
                        UPDATE MonHoc SET
                            TenMH       = @TenMH,
                            SoTinChi     = @SoTinChi,
                            LoaiMon      = @LoaiMon,
                            MaKhoa       = @MaKhoa,
                            MoTa         = @MoTa,
                            HKApDung     = @HKApDung,
                            TrangThai    = @TrangThai
                        WHERE MaMH = @MaMH", conn);
                    cmd.Parameters.AddWithValue("@TenMH", tenMH);
                    cmd.Parameters.AddWithValue("@SoTinChi", soTC);
                    cmd.Parameters.AddWithValue("@LoaiMon", loai);
                    cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);
                    cmd.Parameters.AddWithValue("@MoTa", moTa);
                    cmd.Parameters.AddWithValue("@HKApDung", (object?)hkApDung ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TrangThai", tthai);
                    cmd.Parameters.AddWithValue("@MaMH", _maMH!);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    // Kiểm tra trùng mã
                    var chk = new SqlCommand("SELECT COUNT(*) FROM MonHoc WHERE MaMH=@MaMH", conn);
                    chk.Parameters.AddWithValue("@MaMH", maMH);
                    if ((int)chk.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Mã môn học đã tồn tại!",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var cmd = new SqlCommand(@"
                        INSERT INTO MonHoc
                            (MaMH, TenMH, SoTinChi, MaKhoa, LoaiMon, SoTietTuan, HKApDung, TrangThai, MoTa)
                        VALUES
                            (@MaMH, @TenMH, @SoTinChi, @MaKhoa, @LoaiMon, NULL, @HKApDung, @TrangThai, @MoTa)",
                        conn);
                    cmd.Parameters.AddWithValue("@MaMH", maMH);
                    cmd.Parameters.AddWithValue("@TenMH", tenMH);
                    cmd.Parameters.AddWithValue("@SoTinChi", soTC);
                    cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);
                    cmd.Parameters.AddWithValue("@LoaiMon", loai);
                    cmd.Parameters.AddWithValue("@HKApDung", (object?)hkApDung ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TrangThai", tthai);
                    cmd.Parameters.AddWithValue("@MoTa", moTa);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show(_isEdit ? "Cập nhật môn học thành công!" : "Thêm môn học thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --------------------------------------------------------
        //  Đặt lại
        // --------------------------------------------------------
        private void btnDatLai_Click(object sender, EventArgs e)
        {
            if (_isEdit)
                NapThongTinMon();
            else
            {
                textBox1.Clear();
                textBox3.Clear();
                textBox4.Clear();
                comboBox2.SelectedIndex = 0;
                comboBox3.SelectedIndex = 0;
                if (comboBox6.Items.Count > 0) comboBox6.SelectedIndex = 0;
                if (comboBox7.Items.Count > 0) comboBox7.SelectedIndex = 0;
                if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // --------------------------------------------------------
        //  Validate
        // --------------------------------------------------------
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            { MessageBox.Show("Vui lòng nhập Mã Môn.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            { MessageBox.Show("Vui lòng nhập Tên Môn.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            { MessageBox.Show("Vui lòng nhập Số Tín Chỉ.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            return true;
        }

        // --------------------------------------------------------
        //  Helpers
        // --------------------------------------------------------
        private void SetComboByText(ComboBox cb, object value)
        {
            string val = value?.ToString() ?? "";
            int idx = -1;
            for (int i = 0; i < cb.Items.Count; i++)
            {
                string? item = cb.Items[i]?.ToString();
                if (item == val) { idx = i; break; }
            }
            cb.SelectedIndex = idx >= 0 ? idx : (cb.Items.Count > 0 ? 0 : -1);
        }

        private void SetComboByKey(ComboBox cb, string key)
        {
            for (int i = 0; i < cb.Items.Count; i++)
            {
                if (cb.Items[i] is ComboItem ci && ci.Key == key)
                { cb.SelectedIndex = i; return; }
            }
            cb.SelectedIndex = 0;
        }
    }

    // ============================================================
    //  Helper class cho ComboBox có key/value
    // ============================================================
    internal class ComboItem
    {
        public string Key { get; }
        public string Value { get; }
        public ComboItem(string key, string value) { Key = key; Value = value; }
        public override string ToString() => Value;
    }
}