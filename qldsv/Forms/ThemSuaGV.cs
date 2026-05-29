#nullable disable
using Microsoft.Data.SqlClient;
using System.Data;
using qldsv.Service;

namespace qldsv
{
    public partial class ThemSuaGV : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;
        private readonly string _maGV;           // null = Thêm mới, có giá trị = Sửa
        private bool _isEdit => _maGV != null;
        private readonly List<ComboItem> _khoaItems = new();

        public ThemSuaGV(string maGV)
        {
            InitializeComponent();
            ThemeApplier.Apply(this);
            _maGV = maGV;

            this.Load += ThemSuaGV_Load;
            button1.Click += btnLuu_Click;
            button2.Click += btnDatLai_Click;
            button3.Click += btnHuy_Click;
        }

        private void ThemSuaGV_Load(object sender, EventArgs e)
        {
            this.Text = _isEdit ? "Sửa Giảng Viên" : "Thêm Giảng Viên";

            NapComboKhoa(comboBox5);
            NapComboHocVi(comboBox4);
            NapComboChuyenNganh(comboBox3);

            if (_isEdit)
            {
                textBox2.ReadOnly = true;
                NapThongTinGV();
            }
            else
            {
                SinhMaGV();
            }
        }

        private void SinhMaGV()
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT ISNULL(MAX(CAST(SUBSTRING(MaGV,3,LEN(MaGV)) AS INT)),0)+1 FROM GiangVien WHERE MaGV LIKE 'GV%'",
                    conn);
                int next = Convert.ToInt32(cmd.ExecuteScalar());
                textBox2.Text = "GV" + next.ToString("D3");
            }
            catch
            {
                textBox2.Text = "GV001";
            }
        }

        private void NapThongTinGV()
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT MaGV, HoTen, NgaySinh, GioiTinh, Email, SoDienThoai,
                           HocVi, ChuyenNganh, MaKhoa
                    FROM GiangVien
                    WHERE MaGV = @MaGV", conn);
                cmd.Parameters.AddWithValue("@MaGV", _maGV);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox2.Text = reader["MaGV"].ToString();
                    textBox1.Text = reader["HoTen"].ToString();
                    textBox3.Text = reader["NgaySinh"] != DBNull.Value
                        ? Convert.ToDateTime(reader["NgaySinh"]).ToString("dd/MM/yyyy")
                        : "";
                    textBox7.Text = reader["Email"].ToString();
                    textBox5.Text = reader["SoDienThoai"].ToString();

                    string gioiTinh = reader["GioiTinh"]?.ToString() ?? "Nam";
                    radioButton1.Checked = gioiTinh == "Nam";
                    radioButton2.Checked = gioiTinh == "Nu";

                    SetComboByKey(comboBox5, reader["MaKhoa"]?.ToString() ?? "");
                    SetComboByText(comboBox4, reader["HocVi"]?.ToString() ?? "");
                    SetComboByText(comboBox3, reader["ChuyenNganh"]?.ToString() ?? "");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải thông tin giảng viên:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateGV()) return;

            string maGV = textBox2.Text.Trim();
            string hoTen = textBox1.Text.Trim();
            string email = textBox7.Text.Trim();
            string sdt = textBox5.Text.Trim();
            string maKhoa = comboBox5.SelectedItem is ComboItem ci ? ci.Key : "";
            string hocVi = comboBox4.SelectedItem?.ToString() ?? "";
            string cnganh = comboBox3.SelectedItem?.ToString() ?? "";
            string gioiTinh = radioButton1.Checked ? "Nam" : "Nu";

            DateTime? ngaySinh = null;
            if (DateTime.TryParseExact(textBox3.Text.Trim(), "dd/MM/yyyy",
                    null, System.Globalization.DateTimeStyles.None, out var dt))
                ngaySinh = dt;

            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {
                    if (_isEdit)
                    {
                        // UPDATE
                        var cmd = new SqlCommand(@"
                            UPDATE GiangVien SET
                                HoTen        = @HoTen,
                                NgaySinh     = @NgaySinh,
                                GioiTinh     = @GioiTinh,
                                Email        = @Email,
                                SoDienThoai  = @SDT,
                                HocVi        = @HocVi,
                                ChuyenNganh  = @ChuyenNganh,
                                MaKhoa       = @MaKhoa
                            WHERE MaGV = @MaGV", conn, transaction);
                        cmd.Parameters.AddWithValue("@HoTen", hoTen);
                        cmd.Parameters.AddWithValue("@NgaySinh", (object)ngaySinh ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@SDT", sdt);
                        cmd.Parameters.AddWithValue("@HocVi", hocVi);
                        cmd.Parameters.AddWithValue("@ChuyenNganh", cnganh);
                        cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);
                        cmd.Parameters.AddWithValue("@MaGV", _maGV);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // INSERT giảng viên: tạo NguoiDung trước
                        string matKhauMacDinh = maGV + "@123";
                        var cmdTK = new SqlCommand(@"
                            INSERT INTO NguoiDung (TenDangNhap, MatKhau, VaiTro, Email, TrangThai)
                            VALUES (@TenDangNhap, @MatKhau, 'GiangVien', @Email, 1);
                            SELECT SCOPE_IDENTITY();", conn, transaction);
                        cmdTK.Parameters.AddWithValue("@TenDangNhap", maGV);
                        cmdTK.Parameters.AddWithValue("@MatKhau", matKhauMacDinh);
                        cmdTK.Parameters.AddWithValue("@Email", email);
                        int maND = Convert.ToInt32(cmdTK.ExecuteScalar());

                        // INSERT GiangVien
                        var cmd = new SqlCommand(@"
                            INSERT INTO GiangVien
                                (MaGV, MaND, HoTen, NgaySinh, GioiTinh, SoDienThoai,
                                 Email, MaKhoa, HocVi, ChuyenNganh)
                            VALUES
                                (@MaGV, @MaND, @HoTen, @NgaySinh, @GioiTinh, @SDT,
                                 @Email, @MaKhoa, @HocVi, @ChuyenNganh)", conn, transaction);
                        cmd.Parameters.AddWithValue("@MaGV", maGV);
                        cmd.Parameters.AddWithValue("@MaND", maND);
                        cmd.Parameters.AddWithValue("@HoTen", hoTen);
                        cmd.Parameters.AddWithValue("@NgaySinh", (object)ngaySinh ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                        cmd.Parameters.AddWithValue("@SDT", sdt);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);
                        cmd.Parameters.AddWithValue("@HocVi", hocVi);
                        cmd.Parameters.AddWithValue("@ChuyenNganh", cnganh);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show(_isEdit ? "Cập nhật giảng viên thành công!" : "Thêm giảng viên thành công!",
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
            if (_isEdit)
                NapThongTinGV();
            else
            {
                textBox1.Clear();
                textBox3.Clear();
                textBox7.Clear();
                textBox5.Clear();
                radioButton1.Checked = true;
                if (comboBox5.Items.Count > 0) comboBox5.SelectedIndex = 0;
                if (comboBox4.Items.Count > 0) comboBox4.SelectedIndex = 0;
                if (comboBox3.Items.Count > 0) comboBox3.SelectedIndex = 0;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e) => this.Close();
        private bool ValidateGV()
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã GV.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ Tên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void NapComboKhoa(ComboBox cb)
        {
            cb.Items.Clear();
            _khoaItems.Clear();
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand("SELECT MaKhoa, TenKhoa FROM Khoa ORDER BY TenKhoa", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var item = new ComboItem(reader["MaKhoa"].ToString(), reader["TenKhoa"].ToString());
                    cb.Items.Add(item);
                    _khoaItems.Add(item);
                }
            }
            catch { }
            if (cb.Items.Count == 0)
            {
                cb.Items.Add(new ComboItem("CNTT", "Công nghệ thông tin"));
                cb.Items.Add(new ComboItem("QHCC", "Quan hệ công chúng"));
            }
            if (cb.Items.Count > 0) cb.SelectedIndex = 0;
        }

        private void NapComboHocVi(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.AddRange(new[] { "ThS", "TS", "PGS.TS", "GS.TS", "Cử nhân" });
            if (cb.Items.Count > 0) cb.SelectedIndex = 0;
        }

        private void NapComboChuyenNganh(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.AddRange(new[] { "Khoa học máy tính", "Trí tuệ nhân tạo", "Mạng máy tính" });
            if (cb.Items.Count > 0) cb.SelectedIndex = 0;
        }

        private void SetComboByText(ComboBox cb, string value)
        {
            if (string.IsNullOrEmpty(value)) return;
            for (int i = 0; i < cb.Items.Count; i++)
            {
                string itemText = cb.Items[i].ToString();
                if (string.Equals(itemText, value, StringComparison.OrdinalIgnoreCase))
                {
                    cb.SelectedIndex = i;
                    return;
                }
            }
            if (cb.Items.Count > 0) cb.SelectedIndex = 0;
        }

        private void SetComboByKey(ComboBox cb, string key)
        {
            if (string.IsNullOrEmpty(key)) return;
            for (int i = 0; i < cb.Items.Count; i++)
            {
                if (cb.Items[i] is ComboItem ci && ci.Key == key)
                {
                    cb.SelectedIndex = i;
                    return;
                }
            }
            if (cb.Items.Count > 0) cb.SelectedIndex = 0;
        }
    }
}
