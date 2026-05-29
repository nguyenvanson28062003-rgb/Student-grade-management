#nullable disable
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
            ThemeApplier.Apply(this);
            this.Load += QLTK_Load;

            button1.Click += btnThemTK_Click;    
            button4.Click += btnSuaTK_Click;     
            button3.Click += btnKhoaMoKhoa_Click;
            button6.Click += btnTimKiem_Click;   
            button5.Click += btnQuayLai_Click;   
            button2.Click += btnThoat_Click;     

            button7.Click += (s, e) => TaiDanhSachTK();
        }

        private void QLTK_Load(object sender, EventArgs e)
        {
            TaiDanhSachTK();
        }

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
                SetHeader("TenDangNhap",    "Tên Đăng Nhập");
                SetHeader("VaiTro",         "Vai Trò");
                SetHeader("TrangThai",      "Trạng Thái");
                SetHeader("NgayTao",        "Ngày Tạo");
                SetHeader("LanDangNhapCuoi","Đăng Nhập Cuối");

                // Re-fit cột theo cả header + data, sau đó dùng Fill để lấp đầy
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                // Giữ min-width bằng header, để Fill lấp khoảng dư
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                    col.MinimumWidth = col.Width;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = textBox2.Text.Trim();
            TaiDanhSachTK(tuKhoa);
        }


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


        private void btnKhoaMoKhoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenDN    = dataGridView1.CurrentRow.Cells["TenDangNhap"]?.Value?.ToString() ?? "";
            string trangThai= dataGridView1.CurrentRow.Cells["TrangThai"]?.Value?.ToString()   ?? "";
            if (string.IsNullOrEmpty(tenDN)) return;

            // Không cho khóa tài khoản đang đăng nhập
            if (tenDN == SessionInfo.TenDangNhap)
            {
                MessageBox.Show("Không thể khóa tài khoản đang đăng nhập!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool dangHoatDong = trangThai == "Hoạt Động";
            string hanhDong   = dangHoatDong ? "khóa" : "mở khóa";
            string icon       = dangHoatDong ? "🔒" : "🔓";

            var confirm = MessageBox.Show(
                $"{icon} Bạn có chắc muốn {hanhDong} tài khoản [{tenDN}]?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                int trangThaiMoi = dangHoatDong ? 0 : 1; // đảo trạng thái
                var cmd = new SqlCommand(
                    "UPDATE NguoiDung SET TrangThai = @TT WHERE TenDangNhap = @TenDN", conn);
                cmd.Parameters.AddWithValue("@TT",    trangThaiMoi);
                cmd.Parameters.AddWithValue("@TenDN", tenDN);
                cmd.ExecuteNonQuery();

                string ketQua = dangHoatDong ? "Đã khóa" : "Đã mở khóa";
                MessageBox.Show($"{ketQua} tài khoản [{tenDN}] thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSachTK(textBox2.Text.Trim());
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
