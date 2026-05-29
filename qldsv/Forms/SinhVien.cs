#nullable disable
// ================================================================
//  SinhVien.cs  –  Ví dụ kết nối database cho form Quản lý Sinh Viên
//  Dùng: load DataGrid, tìm kiếm, lọc theo Khoa/Lớp/Tình trạng
// ================================================================

using Microsoft.Data.SqlClient;
using System.Data;
using qldsv.Service;

namespace qldsv
{
    public partial class SinhVien : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;
        private DataTable _dtSinhVien = new DataTable();   // cache để tìm kiếm cục bộ

        public SinhVien()
        {
            InitializeComponent();
            this.Load += SinhVien_Load;

            // Gán sự kiện cho các nút
            button9.Click += btnThemMoi_Click;   // Thêm mới
            button5.Click += btnSua_Click;        // Sửa
            button2.Click += btnXoa_Click;        // Xóa
            button10.Click += btnTimKiem_Click;   // 🔍 Tìm kiếm
            button1.Click += btnQuayVe_Click;     // Quay về MN
            button6.Click += btnThoat_Click;      // Thoát

            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged; // lọc lớp
        }

        // --------------------------------------------------------
        //  Load form: nạp dữ liệu vào ComboBox + DataGrid
        // --------------------------------------------------------
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

        // --------------------------------------------------------
        //  Tải danh sách SV (có lọc theo điều kiện)
        // --------------------------------------------------------
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

        // --------------------------------------------------------
        //  Nút 🔍 Tìm kiếm
        // --------------------------------------------------------
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = textBox2.Text.Trim();
            // comboBox2 chứa MaLop (string), comboBox3 chứa TinhTrang
            string maLop = comboBox2.SelectedIndex > 0 ? comboBox2.SelectedItem?.ToString() ?? "" : "";
            string tinhTrang = comboBox3.SelectedIndex > 0 ? comboBox3.SelectedItem?.ToString() ?? "" : "";
            TaiDanhSachSV(tuKhoa, maLop, tinhTrang);
        }

        // --------------------------------------------------------
        //  Lọc Lớp khi thay đổi comboBox2
        // --------------------------------------------------------
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnTimKiem_Click(sender, e);
        }

        // --------------------------------------------------------
        //  Nút Thêm mới → mở form ThemSuaSV ở chế độ Thêm
        // --------------------------------------------------------
        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            var frm = new ThemSV(null);   // null = thêm mới
            frm.ShowDialog();
            NapComboBoxLop();             // Refresh lớp (có thể vừa thêm lớp mới)
            TaiDanhSachSV();
        }

        // --------------------------------------------------------
        //  Nút Sửa → lấy dòng đang chọn → mở ThemSuaSV với MaSV
        // --------------------------------------------------------
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            string maSV = dataGridView1.CurrentRow.Cells["MaSV"].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(maSV)) return;

            var frm = new ThemSV(maSV);   // truyền MaSV = chế độ sửa
            frm.ShowDialog();
            TaiDanhSachSV();
        }

        // --------------------------------------------------------
        //  Nút Xóa
        // --------------------------------------------------------
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
    }
}