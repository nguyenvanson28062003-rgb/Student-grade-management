#nullable disable
using Microsoft.Data.SqlClient;
using System.Data;
using qldsv.Service;

namespace qldsv
{
    public partial class GiangVien : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;
        private DataTable _dtGV = new DataTable();

        public GiangVien()
        {
            InitializeComponent();
            this.Load += GiangVien_Load;

            button9.Click += btnThemMoi_Click;
            button5.Click += btnSua_Click;
            button2.Click += btnXoa_Click;
            button4.Click += btnXemHoSo_Click;
            button7.Click += btnExport_Click;
            button1.Click += btnPhanCong_Click;
            button3.Click += btnQuayVe_Click;
            button6.Click += btnThoat_Click;
            button10.Click += btnTimKiem_Click;
        }

        private void GiangVien_Load(object sender, EventArgs e)
        {
            NapComboBoxKhoa();
            NapComboBoxHocVi();
            TaiDanhSachGV();
        }

        private void NapComboBoxKhoa()
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("-- Tất cả --");
            try
            {
                using var conn = _db.GetConnection(); conn.Open();
                var cmd = new SqlCommand(
                    "SELECT MaKhoa, TenKhoa FROM Khoa ORDER BY TenKhoa", conn);
                using var r = cmd.ExecuteReader();
                while (r.Read())
                    comboBox2.Items.Add(new GVKhoaItem(r["MaKhoa"].ToString(), r["TenKhoa"].ToString()));
            }
            catch { }
            comboBox2.SelectedIndex = 0;
        }

        private void NapComboBoxHocVi()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("-- Tất cả --");
            comboBox1.Items.AddRange(new[] { "ThS", "TS", "PGS.TS", "GS.TS" });
            comboBox1.SelectedIndex = 0;
        }

        private void TaiDanhSachGV(string tuKhoa = "", string maKhoa = "", string hocVi = "")
        {
            try
            {
                string sql = @"
                    SELECT gv.MaGV, gv.HoTen, gv.HocVi, gv.ChuyenNganh,
                           k.TenKhoa AS Khoa, gv.Email, gv.SoDienThoai,
                           (SELECT COUNT(*) FROM LopHocPhan l WHERE l.MaGV = gv.MaGV) AS SoLopDay
                    FROM GiangVien gv
                    JOIN Khoa k ON gv.MaKhoa = k.MaKhoa
                    WHERE 1=1
                      AND (@TuKhoa = '' OR gv.HoTen LIKE '%'+@TuKhoa+'%' OR gv.MaGV LIKE '%'+@TuKhoa+'%')
                      AND (@MaKhoa = '' OR gv.MaKhoa = @MaKhoa)
                      AND (@HocVi  = '' OR gv.HocVi  = @HocVi)
                    ORDER BY gv.HoTen";

                using var conn = _db.GetConnection(); conn.Open();
                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TuKhoa", tuKhoa);
                cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);
                cmd.Parameters.AddWithValue("@HocVi", hocVi);

                using var da = new SqlDataAdapter(cmd);
                _dtGV = new DataTable();
                da.Fill(_dtGV);

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = _dtGV;

                SetHeader("MaGV", "MGV");
                SetHeader("HoTen", "Họ Tên");
                SetHeader("HocVi", "Học Vị");
                SetHeader("ChuyenNganh", "Chuyên Ngành");
                SetHeader("Khoa", "Khoa");
                SetHeader("Email", "Email");
                SetHeader("SoDienThoai", "SĐT");
                SetHeader("SoLopDay", "Số Lớp Dạy");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu giảng viên:\n" + ex.Message,
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
            string maKhoa = comboBox2.SelectedItem is GVKhoaItem ki ? ki.MaKhoa : "";
            string hocVi = comboBox1.SelectedIndex > 0 ? comboBox1.SelectedItem?.ToString() ?? "" : "";
            TaiDanhSachGV(tuKhoa, maKhoa, hocVi);
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            new ThemSuaGV(null).ShowDialog();
            TaiDanhSachGV();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            string maGV = dataGridView1.CurrentRow.Cells["MaGV"].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(maGV)) return;
            new ThemSuaGV(maGV).ShowDialog();
            TaiDanhSachGV();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            string maGV = dataGridView1.CurrentRow.Cells["MaGV"].Value?.ToString() ?? "";
            string hoTen = dataGridView1.CurrentRow.Cells["HoTen"].Value?.ToString() ?? "";

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa giảng viên [{maGV}] {hoTen}?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using var conn = _db.GetConnection(); conn.Open();

                var chk = new SqlCommand("SELECT COUNT(*) FROM LopHocPhan WHERE MaGV=@MaGV", conn);
                chk.Parameters.AddWithValue("@MaGV", maGV);
                int soLop = (int)chk.ExecuteScalar();

                if (soLop > 0)
                {
                    MessageBox.Show(
                        $"Giảng viên [{maGV}] đang phụ trách {soLop} lớp học phần.\n" +
                        "Hãy chuyển lớp sang GV khác trước khi xóa.",
                        "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy MaND để xóa NguoiDung sau
                var cmdND = new SqlCommand("SELECT MaND FROM GiangVien WHERE MaGV=@MaGV", conn);
                cmdND.Parameters.AddWithValue("@MaGV", maGV);
                int maND = Convert.ToInt32(cmdND.ExecuteScalar());

                new SqlCommand("DELETE FROM GiangVien WHERE MaGV=@MaGV", conn)
                { Parameters = { new("@MaGV", maGV) } }.ExecuteNonQuery();

                if (maND > 0)
                    new SqlCommand("DELETE FROM NguoiDung WHERE MaND=@MaND", conn)
                    { Parameters = { new("@MaND", maND) } }.ExecuteNonQuery();

                MessageBox.Show("Xóa thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSachGV();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi xóa giảng viên:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemHoSo_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            string maGV = dataGridView1.CurrentRow.Cells["MaGV"].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(maGV)) return;

            try
            {
                using var conn = _db.GetConnection(); conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT gv.MaGV, gv.HoTen, gv.HocVi, gv.ChuyenNganh,
                           k.TenKhoa AS Khoa, gv.Email, gv.SoDienThoai,
                           (SELECT COUNT(*) FROM LopHocPhan l WHERE l.MaGV = gv.MaGV) AS TongLop,
                           (SELECT COUNT(*) FROM LopHocPhan l
                            JOIN HocKy hk ON l.MaHK = hk.MaHK
                            WHERE l.MaGV = gv.MaGV AND hk.TrangThai = 'DangDienRa') AS LopHienTai
                    FROM GiangVien gv
                    JOIN Khoa k ON gv.MaKhoa = k.MaKhoa
                    WHERE gv.MaGV = @MaGV", conn);
                cmd.Parameters.AddWithValue("@MaGV", maGV);
                using var r = cmd.ExecuteReader();
                if (r.Read()) new HoSoGVDialog(r).ShowDialog();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải hồ sơ:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPhanCong_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn giảng viên cần phân công.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maGV = dataGridView1.CurrentRow.Cells["MaGV"].Value?.ToString() ?? "";
            string hoTen = dataGridView1.CurrentRow.Cells["HoTen"].Value?.ToString() ?? "";

            new PhanCongGVDialog(_db, maGV, hoTen).ShowDialog();
            TaiDanhSachGV();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng xuất Excel đang phát triển.",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button7_Click(object sender, EventArgs e) => btnExport_Click(sender, e);
        private void btnExport2_Click(object sender, EventArgs e) => btnExport_Click(sender, e);
        private void btnQuayVe_Click(object sender, EventArgs e) => this.Close();
        private void btnThoat_Click(object sender, EventArgs e) => Application.Exit();
    }

    internal class GVKhoaItem
    {
        public string MaKhoa { get; }
        private readonly string _ten;
        public GVKhoaItem(string maKhoa, string ten) { MaKhoa = maKhoa; _ten = ten; }
        public override string ToString() => _ten;
    }

    internal class HoSoGVDialog : Form
    {
        public HoSoGVDialog(SqlDataReader r)
        {
            string maGV = r["MaGV"]?.ToString() ?? "";
            string hoTen = r["HoTen"]?.ToString() ?? "";
            string hocVi = r["HocVi"]?.ToString() ?? "";
            string cnganh = r["ChuyenNganh"]?.ToString() ?? "";
            string khoa = r["Khoa"]?.ToString() ?? "";
            string email = r["Email"]?.ToString() ?? "";
            string sdt = r["SoDienThoai"]?.ToString() ?? "";
            string tongLop = r["TongLop"]?.ToString() ?? "0";
            string lopHT = r["LopHienTai"]?.ToString() ?? "0";

            this.Text = $"Hồ Sơ – {hoTen}";
            this.Size = new Size(440, 360);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            var tbl = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                Padding = new Padding(15, 10, 15, 10)
            };
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            void AddRow(string lbl, string val)
            {
                tbl.Controls.Add(new Label
                {
                    Text = lbl,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    AutoSize = true,
                    Anchor = AnchorStyles.Right
                });
                tbl.Controls.Add(new Label { Text = val, AutoSize = true, Anchor = AnchorStyles.Left });
            }

            AddRow("Mã GV:", maGV);
            AddRow("Họ tên:", hoTen);
            AddRow("Học vị:", hocVi);
            AddRow("Chuyên ngành:", cnganh);
            AddRow("Khoa:", khoa);
            AddRow("Email:", email);
            AddRow("Số điện thoại:", sdt);
            AddRow("Tổng lớp đã dạy:", $"{tongLop} lớp");
            AddRow("Lớp HK hiện tại:", $"{lopHT} lớp");

            var btnDong = new Button
            {
                Text = "Đóng",
                Dock = DockStyle.Bottom,
                Height = 35,
                DialogResult = DialogResult.Cancel
            };
            btnDong.Click += (s, ev) => this.Close();
            this.Controls.Add(tbl);
            this.Controls.Add(btnDong);
            this.CancelButton = btnDong;
        }
    }

    internal class PhanCongGVDialog : Form
    {
        private readonly DatabaseHelper _db;
        private readonly string _maGV;
        private readonly DataGridView _dgv = new DataGridView();
        private readonly ComboBox _cboHK = new ComboBox();

        public PhanCongGVDialog(DatabaseHelper db, string maGV, string hoTen)
        {
            _db = db;
            _maGV = maGV;

            this.Text = $"Phân Công Dạy – {hoTen} ({maGV})";
            this.Size = new Size(720, 500);
            this.StartPosition = FormStartPosition.CenterParent;

            var pnTop = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 42, Padding = new Padding(8, 6, 0, 0) };
            pnTop.Controls.Add(new Label { Text = "Học kỳ:", AutoSize = true, Anchor = AnchorStyles.Left });
            _cboHK.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboHK.Width = 220;
            pnTop.Controls.Add(_cboHK);
            var btnLoc = new Button { Text = "Lọc", Width = 60, Height = 28 };
            btnLoc.Click += (s, e) => TaiDanhSachLop();
            pnTop.Controls.Add(btnLoc);

            _dgv.Dock = DockStyle.Fill;
            _dgv.ReadOnly = true;
            _dgv.AllowUserToAddRows = false;
            _dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dgv.MultiSelect = false;

            var btnGan = new Button { Text = "Gán GV Này", Width = 120, Height = 35 };
            var btnDong = new Button { Text = "Đóng", Width = 80, Height = 35, DialogResult = DialogResult.OK };
            var pnBot = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 46,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(5, 5, 5, 0)
            };
            pnBot.Controls.Add(btnDong);
            pnBot.Controls.Add(btnGan);

            btnGan.Click += btnGan_Click;
            btnDong.Click += (s, e) => this.Close();

            this.Controls.Add(_dgv);
            this.Controls.Add(pnBot);
            this.Controls.Add(pnTop);

            NapComboHK();
            TaiDanhSachLop();
        }

        private void NapComboHK()
        {
            _cboHK.Items.Clear();
            _cboHK.Items.Add(new PCHKItem("", "-- Tất cả --"));
            try
            {
                using var conn = _db.GetConnection(); conn.Open();
                var cmd = new SqlCommand("SELECT MaHK, TenHK FROM HocKy ORDER BY MaHK DESC", conn);
                using var r = cmd.ExecuteReader();
                while (r.Read())
                    _cboHK.Items.Add(new PCHKItem(r["MaHK"].ToString(), r["TenHK"].ToString()));
            }
            catch { }
            _cboHK.SelectedIndex = 0;
        }

        private void TaiDanhSachLop()
        {
            string maHK = _cboHK.SelectedItem is PCHKItem hk ? hk.MaHK : "";
            try
            {
                using var conn = _db.GetConnection(); conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT lhp.MaLHP, mh.TenMH, hk.TenHK,
                           ISNULL(gv.HoTen,'(Chưa có GV)') AS GVHienTai,
                           lhp.Phong, lhp.LichHoc,
                           lhp.SiSoHienTai AS SV, lhp.TrangThai
                    FROM LopHocPhan lhp
                    JOIN MonHoc    mh  ON lhp.MaMH = mh.MaMH
                    JOIN HocKy     hk  ON lhp.MaHK  = hk.MaHK
                    LEFT JOIN GiangVien gv ON lhp.MaGV = gv.MaGV
                    WHERE (@MaHK = '' OR lhp.MaHK = @MaHK)
                    ORDER BY lhp.MaLHP", conn);
                cmd.Parameters.AddWithValue("@MaHK", maHK);

                using var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);

                _dgv.AutoGenerateColumns = true;
                _dgv.Columns.Clear();
                _dgv.DataSource = dt;

                if (_dgv.Columns["MaLHP"] != null) _dgv.Columns["MaLHP"].HeaderText = "Mã Lớp";
                if (_dgv.Columns["TenMH"] != null) _dgv.Columns["TenMH"].HeaderText = "Môn Học";
                if (_dgv.Columns["TenHK"] != null) _dgv.Columns["TenHK"].HeaderText = "Học Kỳ";
                if (_dgv.Columns["GVHienTai"] != null) _dgv.Columns["GVHienTai"].HeaderText = "GV Hiện Tại";
                if (_dgv.Columns["Phong"] != null) _dgv.Columns["Phong"].HeaderText = "Phòng";
                if (_dgv.Columns["LichHoc"] != null) _dgv.Columns["LichHoc"].HeaderText = "Lịch";
                if (_dgv.Columns["SV"] != null) _dgv.Columns["SV"].HeaderText = "SV";
                if (_dgv.Columns["TrangThai"] != null) _dgv.Columns["TrangThai"].HeaderText = "Trạng Thái";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải danh sách lớp:\n" + ex.Message);
            }
        }

        private void btnGan_Click(object sender, EventArgs e)
        {
            if (_dgv.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn lớp học phần cần phân công.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maLHP = _dgv.CurrentRow.Cells["MaLHP"].Value?.ToString() ?? "";
            string gvHienTai = _dgv.CurrentRow.Cells["GVHienTai"].Value?.ToString() ?? "";

            string msg = gvHienTai == "(Chưa có GV)"
                ? $"Gán GV [{_maGV}] dạy lớp [{maLHP}]?"
                : $"Lớp [{maLHP}] hiện do {gvHienTai} dạy.\nBạn có muốn thay bằng GV [{_maGV}] không?";

            if (MessageBox.Show(msg, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            try
            {
                using var conn = _db.GetConnection(); conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE LopHocPhan SET MaGV = @MaGV WHERE MaLHP = @MaLHP", conn);
                cmd.Parameters.AddWithValue("@MaGV", _maGV);
                cmd.Parameters.AddWithValue("@MaLHP", maLHP);
                cmd.ExecuteNonQuery();

                MessageBox.Show($"Đã phân công [{_maGV}] dạy lớp [{maLHP}].",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSachLop();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi phân công:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    internal class PCHKItem
    {
        public string MaHK { get; }
        private readonly string _ten;
        public PCHKItem(string maHK, string ten) { MaHK = maHK; _ten = ten; }
        public override string ToString() => _ten;
    }
}
