#nullable disable
// ================================================================
//  QLTK.cs  –  Quản Lý Tài Khoản
//  Namespace: quản_lí_điểm_sinh_viên
// ================================================================

using Microsoft.Data.SqlClient;
using System.Data;
using qldsv;
using qldsv.Service;

namespace quản_lí_điểm_sinh_viên
{
    public partial class QLTK : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;
        private DataTable _dtTK = new DataTable();

        public QLTK()
        {
            InitializeComponent();
            this.Load += QLTK_Load;

            button1.Click += btnThemTK_Click;    // Thêm TK
            button4.Click += btnSuaTK_Click;     // Sửa TK
            button3.Click += btnXoaTK_Click;     // Xóa TK
            button6.Click += btnTimKiem_Click;   // Tìm kiếm
            button5.Click += btnQuayLai_Click;   // Quay lại
            button2.Click += btnThoat_Click;     // Thoát

            // button7 (Refresh) wired in Designer to dataGridView1_CellContentClick
            // but we also attach it here for refresh functionality
            button7.Click += (s, e) => TaiDanhSachTK();
        }

        private void QLTK_Load(object sender, EventArgs e)
        {
            TaiDanhSachTK();
        }

        // --------------------------------------------------------
        //  Nạp danh sách tài khoản
        // --------------------------------------------------------
        private void TaiDanhSachTK(string tuKhoa = "")
        {
            try
            {
                string sql = @"
                    SELECT
                        nd.TenDangNhap,
                        nd.VaiTro,
                        CASE nd.TrangThai WHEN 1 THEN N'Hoạt Động' ELSE N'Bị Khóa' END AS TrangThai,
                        nd.NgayTao,
                        nd.LanDangNhapCuoi
                    FROM NguoiDung nd
                    WHERE @TuKhoa = '' OR nd.TenDangNhap LIKE '%'+@TuKhoa+'%'
                    ORDER BY nd.NgayTao DESC";

                using var conn = _db.GetConnection();
                conn.Open();
                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TuKhoa", tuKhoa);

                using var da = new SqlDataAdapter(cmd);
                _dtTK = new DataTable();
                da.Fill(_dtTK);

                // Xóa cột Designer, dùng auto-generate
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = _dtTK;

                // Đặt header
                SetHeader("TenDangNhap", "Tên Đăng Nhập");
                SetHeader("VaiTro", "Vai Trò");
                SetHeader("TrangThai", "Trạng Thái");
                SetHeader("NgayTao", "Ngày Tạo");
                SetHeader("LanDangNhapCuoi", "Đăng Nhập Cuối");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải danh sách tài khoản:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetHeader(string colName, string header)
        {
            if (dataGridView1.Columns[colName] != null)
                dataGridView1.Columns[colName].HeaderText = header;
        }

        // --------------------------------------------------------
        //  Tìm kiếm
        // --------------------------------------------------------
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = textBox2.Text.Trim();
            TaiDanhSachTK(tuKhoa);
        }

        // --------------------------------------------------------
        //  Thêm tài khoản
        // --------------------------------------------------------
        private void btnThemTK_Click(object sender, EventArgs e)
        {
            using var frm = new ThemTKDialog();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Them(frm.TenDangNhap, frm.MatKhau, frm.VaiTro);
            }
        }

        private void Them(string tenDangNhap, string matKhau, string vaiTro)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                // Kiểm tra trùng tên
                var checkCmd = new SqlCommand(
                    "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @TenDangNhap", conn);
                checkCmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var cmd = new SqlCommand(@"
                    INSERT INTO NguoiDung (TenDangNhap, MatKhau, VaiTro, TrangThai, NgayTao)
                    VALUES (@TenDangNhap, @MatKhau, @VaiTro, 1, GETDATE())", conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                cmd.Parameters.AddWithValue("@VaiTro", vaiTro);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Thêm tài khoản thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSachTK();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi thêm tài khoản:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --------------------------------------------------------
        //  Sửa tài khoản (đổi vai trò / trạng thái)
        // --------------------------------------------------------
        private void btnSuaTK_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            string tenDN = dataGridView1.CurrentRow.Cells["TenDangNhap"]?.Value?.ToString() ?? "";
            string vaiTro = dataGridView1.CurrentRow.Cells["VaiTro"]?.Value?.ToString() ?? "";
            string tthaiText = dataGridView1.CurrentRow.Cells["TrangThai"]?.Value?.ToString() ?? "";

            if (string.IsNullOrEmpty(tenDN)) return;

            // Đổi text về giá trị bit
            int tthaiBit = tthaiText == "Hoạt Động" ? 1 : 0;

            using var frm = new SuaTKDialog(tenDN, vaiTro, tthaiBit);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using var conn = _db.GetConnection();
                    conn.Open();
                    var cmd = new SqlCommand(@"
                        UPDATE NguoiDung
                        SET VaiTro = @VaiTro, TrangThai = @TrangThai
                        WHERE TenDangNhap = @TenDangNhap", conn);
                    cmd.Parameters.AddWithValue("@VaiTro", frm.VaiTro);
                    cmd.Parameters.AddWithValue("@TrangThai", frm.TrangThai);
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDN);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Cập nhật thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachTK();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi cập nhật:\n" + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --------------------------------------------------------
        //  Xóa tài khoản
        // --------------------------------------------------------
        private void btnXoaTK_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            string tenDN = dataGridView1.CurrentRow.Cells["TenDangNhap"]?.Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(tenDN)) return;

            // Không xóa tài khoản đang đăng nhập
            if (tenDN == SessionInfo.TenDangNhap)
            {
                MessageBox.Show("Không thể xóa tài khoản đang đăng nhập!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa tài khoản [{tenDN}]?\nTất cả dữ liệu liên quan (đăng ký, điểm, lịch sử, v.v.) cũng sẽ bị xóa hoàn toàn!",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {
                    // 1. Lấy MaND
                    var cmdGetMaND = new SqlCommand("SELECT MaND FROM NguoiDung WHERE TenDangNhap = @TenDangNhap", conn, transaction);
                    cmdGetMaND.Parameters.AddWithValue("@TenDangNhap", tenDN);
                    var objMaND = cmdGetMaND.ExecuteScalar();
                    int maND = objMaND != DBNull.Value ? Convert.ToInt32(objMaND) : 0;

                    if (maND == 0)
                    {
                        MessageBox.Show("Không tìm thấy tài khoản!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        transaction.Rollback();
                        return;
                    }

                    // 2. Kiểm tra SinhVien (nếu có)
                    var cmdGetSV = new SqlCommand("SELECT MaSV FROM SinhVien WHERE MaND = @MaND", conn, transaction);
                    cmdGetSV.Parameters.AddWithValue("@MaND", maND);
                    var objMaSV = cmdGetSV.ExecuteScalar();
                    string maSV = objMaSV != DBNull.Value ? objMaSV.ToString() : "";

                    if (!string.IsNullOrEmpty(maSV))
                    {
                        // Xóa tất cả Diem (thuộc về SinhVien đó (thông qua DangKyHocPhan)
                        var cmdDelDiem = new SqlCommand(@"
                            DELETE Diem
                            FROM Diem
                            JOIN DangKyHocPhan DK ON Diem.MaDK = DK.MaDK
                            WHERE DK.MaSV = @MaSV", conn, transaction);
                        cmdDelDiem.Parameters.AddWithValue("@MaSV", maSV);
                        cmdDelDiem.ExecuteNonQuery();

                        // Xóa tất cả DangKyHocPhan
                        var cmdDelDK = new SqlCommand("DELETE FROM DangKyHocPhan WHERE MaSV = @MaSV", conn, transaction);
                        cmdDelDK.Parameters.AddWithValue("@MaSV", maSV);
                        cmdDelDK.ExecuteNonQuery();

                        // Xóa SinhVien
                        var cmdDelSV = new SqlCommand("DELETE FROM SinhVien WHERE MaND = @MaND", conn, transaction);
                        cmdDelSV.Parameters.AddWithValue("@MaND", maND);
                        cmdDelSV.ExecuteNonQuery();
                    }

                    // 3. Kiểm tra GiangVien (nếu có)
                    var cmdGetGV = new SqlCommand("SELECT MaGV FROM GiangVien WHERE MaND = @MaND", conn, transaction);
                    cmdGetGV.Parameters.AddWithValue("@MaND", maND);
                    var objMaGV = cmdGetGV.ExecuteScalar();
                    string maGV = objMaGV != DBNull.Value ? objMaGV.ToString() : "";

                    if (!string.IsNullOrEmpty(maGV))
                    {
                        // Xóa tất cả Diem thuộc LopHocPhan của GiangVien
                        var cmdDelDiemAll = new SqlCommand(@"
                            DELETE Diem
                            FROM Diem
                            JOIN DangKyHocPhan DK ON Diem.MaDK = DK.MaDK
                            JOIN LopHocPhan LHP ON DK.MaLHP = LHP.MaLHP
                            WHERE LHP.MaGV = @MaGV", conn, transaction);
                        cmdDelDiemAll.Parameters.AddWithValue("@MaGV", maGV);
                        cmdDelDiemAll.ExecuteNonQuery();

                        // Xóa tất cả DangKyHocPhan thuộc LopHocPhan của GiangVien
                        var cmdDelDKAll = new SqlCommand(@"
                            DELETE DangKyHocPhan
                            FROM DangKyHocPhan DK
                            JOIN LopHocPhan LHP ON DK.MaLHP = LHP.MaLHP
                            WHERE LHP.MaGV = @MaGV", conn, transaction);
                        cmdDelDKAll.Parameters.AddWithValue("@MaGV", maGV);
                        cmdDelDKAll.ExecuteNonQuery();

                        // Xóa tất cả LopHocPhan của GiangVien
                        var cmdDelLHPAll = new SqlCommand("DELETE FROM LopHocPhan WHERE MaGV = @MaGV", conn, transaction);
                        cmdDelLHPAll.Parameters.AddWithValue("@MaGV", maGV);
                        cmdDelLHPAll.ExecuteNonQuery();

                        // Xóa GiangVien
                        var cmdDelGV = new SqlCommand("DELETE FROM GiangVien WHERE MaND = @MaND", conn, transaction);
                        cmdDelGV.Parameters.AddWithValue("@MaND", maND);
                        cmdDelGV.ExecuteNonQuery();
                    }

                    // 4. Cuối cùng xóa NguoiDung
                    var cmdDelND = new SqlCommand("DELETE FROM NguoiDung WHERE MaND = @MaND", conn, transaction);
                    cmdDelND.Parameters.AddWithValue("@MaND", maND);
                    cmdDelND.ExecuteNonQuery();

                    transaction.Commit();

                    MessageBox.Show("Xóa tài khoản thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachTK();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không thể xóa tài khoản (có thể đang được liên kết bởi dữ liệu khác):\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Refresh khi click ô (button7 cũng gọi TaiDanhSachTK)
        }
    }

    // ============================================================
    //  Dialog phụ: Thêm tài khoản
    // ============================================================
    internal class ThemTKDialog : Form
    {
        public string TenDangNhap { get; private set; } = "";
        public string MatKhau { get; private set; } = "";
        public string VaiTro { get; private set; } = "SinhVien";

        private TextBox txtTen = new TextBox();
        private TextBox txtMK = new TextBox();
        private ComboBox cboVT = new ComboBox();
        private Button btnOK = new Button();
        private Button btnHuy = new Button();

        public ThemTKDialog()
        {
            this.Text = "Thêm Tài Khoản";
            this.Size = new Size(340, 220);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 4, Padding = new Padding(10) };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            txtMK.PasswordChar = '*';
            cboVT.Items.AddRange(new[] { "SinhVien", "GiangVien", "Admin" });
            cboVT.SelectedIndex = 0;
            cboVT.DropDownStyle = ComboBoxStyle.DropDownList;

            layout.Controls.Add(new Label { Text = "Tên đăng nhập:", Anchor = AnchorStyles.Right }, 0, 0); layout.Controls.Add(txtTen, 1, 0);
            layout.Controls.Add(new Label { Text = "Mật khẩu:", Anchor = AnchorStyles.Right }, 0, 1); layout.Controls.Add(txtMK, 1, 1);
            layout.Controls.Add(new Label { Text = "Vai trò:", Anchor = AnchorStyles.Right }, 0, 2); layout.Controls.Add(cboVT, 1, 2);

            btnOK.Text = "Lưu"; btnOK.DialogResult = DialogResult.OK;
            btnHuy.Text = "Hủy"; btnHuy.DialogResult = DialogResult.Cancel;
            var panBtn = new FlowLayoutPanel { FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Fill };
            panBtn.Controls.Add(btnHuy);
            panBtn.Controls.Add(btnOK);
            layout.Controls.Add(panBtn, 1, 3);

            this.Controls.Add(layout);
            this.AcceptButton = btnOK;
            this.CancelButton = btnHuy;

            btnOK.Click += (s, e) =>
            {
                TenDangNhap = txtTen.Text.Trim();
                MatKhau = txtMK.Text.Trim();
                VaiTro = cboVT.SelectedItem?.ToString() ?? "SinhVien";
                if (string.IsNullOrWhiteSpace(TenDangNhap) || MatKhau.Length < 6)
                {
                    MessageBox.Show("Tên đăng nhập không được trống, mật khẩu tối thiểu 6 ký tự.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.None;
                }
            };
        }
    }

    // ============================================================
    //  Dialog phụ: Sửa tài khoản
    // ============================================================
    internal class SuaTKDialog : Form
    {
        public string VaiTro { get; private set; }
        public int TrangThai { get; private set; }

        private ComboBox cboVT = new ComboBox();
        private ComboBox cboTT = new ComboBox();
        private Button btnOK = new Button();
        private Button btnHuy = new Button();

        public SuaTKDialog(string tenDN, string vaiTro, int trangThai)
        {
            VaiTro = vaiTro;
            TrangThai = trangThai;

            this.Text = $"Sửa tài khoản: {tenDN}";
            this.Size = new Size(300, 180);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 3, Padding = new Padding(10) };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            cboVT.Items.AddRange(new[] { "SinhVien", "GiangVien", "Admin" });
            cboVT.SelectedItem = vaiTro;
            if (cboVT.SelectedIndex < 0) cboVT.SelectedIndex = 0;
            cboVT.DropDownStyle = ComboBoxStyle.DropDownList;

            cboTT.Items.AddRange(new[] { "Hoạt Động", "Bị Khóa" });
            cboTT.SelectedIndex = trangThai == 1 ? 0 : 1;
            cboTT.DropDownStyle = ComboBoxStyle.DropDownList;

            layout.Controls.Add(new Label { Text = "Vai trò:", Anchor = AnchorStyles.Right }, 0, 0);
            layout.Controls.Add(cboVT, 1, 0);
            layout.Controls.Add(new Label { Text = "Trạng thái:", Anchor = AnchorStyles.Right }, 0, 1);
            layout.Controls.Add(cboTT, 1, 1);

            btnOK.Text = "Lưu"; btnOK.DialogResult = DialogResult.OK;
            btnHuy.Text = "Hủy"; btnHuy.DialogResult = DialogResult.Cancel;
            var panBtn = new FlowLayoutPanel { FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Fill };
            panBtn.Controls.Add(btnHuy);
            panBtn.Controls.Add(btnOK);
            layout.Controls.Add(panBtn, 1, 2);

            this.Controls.Add(layout);
            this.AcceptButton = btnOK;
            this.CancelButton = btnHuy;

            btnOK.Click += (s, e) =>
            {
                VaiTro = cboVT.SelectedItem?.ToString() ?? "SinhVien";
                TrangThai = cboTT.SelectedIndex == 0 ? 1 : 0;
            };
        }
    }
}
