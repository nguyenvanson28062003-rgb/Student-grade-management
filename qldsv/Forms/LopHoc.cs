#nullable disable
using Microsoft.Data.SqlClient;
using System.Data;
using qldsv.Service;

namespace qldsv
{
    public partial class LopHoc : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;
        private DataTable _dtLop = new DataTable();

        public LopHoc()
        {
            InitializeComponent();
            ThemeApplier.Apply(this);
            this.Load += LopHoc_Load;

            button1.Click += btnTaoLop_Click;       // Tạo lớp HP
            button2.Click += btnSua_Click;           // Sửa
            button3.Click += btnXoa_Click;           // Xóa
            button4.Click += btnNhapDiem_Click;      // Nhập điểm
            // button5 (Danh sách SV) đã có trong Designer
            button6.Click += btnExportTKB_Click;     // Export TKB
            button7.Click += btnQuayLai_Click;       // Quay lại Menu
            button8.Click += btnThoat_Click;         // Thoát

            comboBox1.SelectedIndexChanged += comboBox1_Changed;
        }

        private void LopHoc_Load(object sender, EventArgs e)
        {
            NapComboBoxHocKy();
            NapComboBoxMonHoc();
            NapComboBoxGiangVien();
            TaiDanhSachLopHP();
        }

        // --------------------------------------------------------
        //  Nạp ComboBox Học Kỳ (comboBox1 – lọc)
        // --------------------------------------------------------
        private void NapComboBoxHocKy()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("-- Tất cả học kỳ --");
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand("SELECT MaHK, TenHK FROM HocKy ORDER BY MaHK DESC", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    comboBox1.Items.Add(new LopHocHKItem(
                        reader["MaHK"].ToString(), reader["TenHK"].ToString()));
            }
            catch { }
            comboBox1.SelectedIndex = 0;
        }

        // --------------------------------------------------------
        //  Nạp ComboBox Môn Học (comboBox2 – lọc)
        // --------------------------------------------------------
        private void NapComboBoxMonHoc()
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("-- Tất cả --");
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT MaMH, TenMH FROM MonHoc WHERE TrangThai = 'DangMoLop' ORDER BY TenMH", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    comboBox2.Items.Add(new LopHocMonItem(
                        reader["MaMH"].ToString(), reader["TenMH"].ToString()));
            }
            catch { }
            comboBox2.SelectedIndex = 0;
        }

        // --------------------------------------------------------
        //  Nạp ComboBox Giảng Viên (comboBox3 – lọc)
        // --------------------------------------------------------
        private void NapComboBoxGiangVien()
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("-- Tất cả --");
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT MaGV, HoTen FROM GiangVien WHERE TrangThai = 'DangDay' ORDER BY HoTen", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    comboBox3.Items.Add(new LopHocGVItem(
                        reader["MaGV"].ToString(), reader["HoTen"].ToString()));
            }
            catch { }
            comboBox3.SelectedIndex = 0;
        }

        // --------------------------------------------------------
        //  Tải danh sách lớp học phần
        // --------------------------------------------------------
        private void TaiDanhSachLopHP(string maHK = "", string maMH = "", string maGV = "")
        {
            try
            {
                string sql = @"
                    SELECT
                        lhp.MaLHP,
                        mh.TenMH        AS TenMonHoc,
                        gv.HoTen         AS GiangVien,
                        lhp.Phong,
                        lhp.LichHoc,
                        lhp.SiSoHienTai  AS SVDangKy,
                        lhp.SiSoToiDa,
                        hk.TenHK,
                        lhp.TrangThai
                    FROM LopHocPhan lhp
                    JOIN MonHoc    mh  ON lhp.MaMH = mh.MaMH
                    JOIN GiangVien gv  ON lhp.MaGV  = gv.MaGV
                    JOIN HocKy     hk  ON lhp.MaHK  = hk.MaHK
                    WHERE 1=1
                      AND (@MaHK  = '' OR lhp.MaHK  = @MaHK)
                      AND (@MaMH  = '' OR lhp.MaMH  = @MaMH)
                      AND (@MaGV  = '' OR lhp.MaGV  = @MaGV)
                    ORDER BY lhp.MaLHP";

                using var conn = _db.GetConnection();
                conn.Open();
                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaHK", maHK);
                cmd.Parameters.AddWithValue("@MaMH", maMH);
                cmd.Parameters.AddWithValue("@MaGV", maGV);

                using var da = new SqlDataAdapter(cmd);
                _dtLop = new DataTable();
                da.Fill(_dtLop);

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = _dtLop;

                SetHeader("MaLHP", "Mã Lớp");
                SetHeader("TenMonHoc", "Môn Học");
                SetHeader("GiangVien", "GV Phụ Trách");
                SetHeader("Phong", "Phòng");
                SetHeader("LichHoc", "Lịch Học");
                SetHeader("SVDangKy", "SV Đăng Ký");
                SetHeader("SiSoToiDa", "Sĩ Số TĐ");
                SetHeader("TenHK", "Học Kỳ");
                SetHeader("TrangThai", "Trạng Thái");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải danh sách lớp học phần:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetHeader(string colName, string header)
        {
            if (dataGridView1.Columns[colName] != null)
                dataGridView1.Columns[colName].HeaderText = header;
        }

        private void comboBox1_Changed(object sender, EventArgs e)
        {
            string maHK = comboBox1.SelectedItem is LopHocHKItem hk ? hk.MaHK : "";
            string maMH = comboBox2.SelectedItem is LopHocMonItem mn ? mn.MaMH : "";
            string maGV = comboBox3.SelectedItem is LopHocGVItem gv ? gv.MaGV : "";
            TaiDanhSachLopHP(maHK, maMH, maGV);
        }

        // --------------------------------------------------------
        //  Nút Tạo lớp HP
        // --------------------------------------------------------
        private void btnTaoLop_Click(object sender, EventArgs e)
        {
            using var frm = new TaoLopDialog(_db);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using var conn = _db.GetConnection();
                conn.Open();

                // Sinh MaLHP tự động
                var cmdMax = new SqlCommand(
                    "SELECT ISNULL(MAX(CAST(SUBSTRING(MaLHP,4,LEN(MaLHP)) AS INT)),0)+1 FROM LopHocPhan WHERE MaLHP LIKE 'LHP%'",
                    conn);
                int next = Convert.ToInt32(cmdMax.ExecuteScalar());
                string maLHP = "LHP" + next.ToString("D3");

                var cmd = new SqlCommand(@"
                    INSERT INTO LopHocPhan
                        (MaLHP, MaMH, MaGV, MaHK, Phong, LichHoc, SiSoToiDa, SiSoHienTai, TrangThai)
                    VALUES
                        (@MaLHP, @MaMH, @MaGV, @MaHK, @Phong, @LichHoc, @SiSoToiDa, 0, 'DangMo')",
                    conn);
                cmd.Parameters.AddWithValue("@MaLHP", maLHP);
                cmd.Parameters.AddWithValue("@MaMH", frm.MaMH);
                cmd.Parameters.AddWithValue("@MaGV", frm.MaGV);
                cmd.Parameters.AddWithValue("@MaHK", frm.MaHK);
                cmd.Parameters.AddWithValue("@Phong", frm.Phong);
                cmd.Parameters.AddWithValue("@LichHoc", frm.LichHoc);
                cmd.Parameters.AddWithValue("@SiSoToiDa", frm.SiSoToiDa);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show($"Tạo lớp học phần [{maLHP}] thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachLopHP();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi tạo lớp học phần:\n" + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --------------------------------------------------------
        //  Nút Sửa lớp HP
        // --------------------------------------------------------
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            string maLHP = dataGridView1.CurrentRow.Cells["MaLHP"].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(maLHP)) return;

            using var frm = new SuaLopDialog(_db, maLHP);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using var conn = _db.GetConnection();
                    conn.Open();
                    var cmd = new SqlCommand(@"
                        UPDATE LopHocPhan SET
                            MaGV       = @MaGV,
                            Phong      = @Phong,
                            LichHoc    = @LichHoc,
                            SiSoToiDa  = @SiSoToiDa,
                            TrangThai  = @TrangThai
                        WHERE MaLHP = @MaLHP", conn);
                    cmd.Parameters.AddWithValue("@MaGV", frm.MaGV);
                    cmd.Parameters.AddWithValue("@Phong", frm.Phong);
                    cmd.Parameters.AddWithValue("@LichHoc", frm.LichHoc);
                    cmd.Parameters.AddWithValue("@SiSoToiDa", frm.SiSoToiDa);
                    cmd.Parameters.AddWithValue("@TrangThai", frm.TrangThai);
                    cmd.Parameters.AddWithValue("@MaLHP", maLHP);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Cập nhật lớp học phần thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachLopHP();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi cập nhật:\n" + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --------------------------------------------------------
        //  Nút Xóa lớp HP
        // --------------------------------------------------------
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            string maLHP = dataGridView1.CurrentRow.Cells["MaLHP"].Value?.ToString() ?? "";

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa lớp học phần [{maLHP}]?\n" +
                "Dữ liệu đăng ký và điểm liên quan cũng sẽ bị xóa!",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM LopHocPhan WHERE MaLHP = @MaLHP", conn);
                cmd.Parameters.AddWithValue("@MaLHP", maLHP);
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachLopHP();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không thể xóa lớp học phần:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --------------------------------------------------------
        //  Nút Nhập điểm
        // --------------------------------------------------------
        private void btnNhapDiem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn lớp học phần!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maLHP = dataGridView1.CurrentRow.Cells["MaLHP"].Value?.ToString() ?? "";
            if (!string.IsNullOrEmpty(maLHP))
            {
                var frm = new quản_lí_điểm_sinh_viên.NhapDiem(maLHP);
                frm.ShowDialog();
                TaiDanhSachLopHP();
            }
        }

        // --------------------------------------------------------
        //  Nút Danh sách SV (button5 – wired in Designer)
        // --------------------------------------------------------
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn lớp học phần!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maLHP = dataGridView1.CurrentRow.Cells["MaLHP"].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(maLHP)) return;

            // Hiển thị danh sách SV trong lớp
            var frmDS = new DanhSachSVTrongLopDialog(_db, maLHP);
            frmDS.ShowDialog();
        }

        // --------------------------------------------------------
        //  Nút Export TKB
        // --------------------------------------------------------
        private void btnExportTKB_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng Export TKB đang phát triển.",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

    // ============================================================
    //  ComboBox helper items
    // ============================================================
    internal class LopHocHKItem
    {
        public string MaHK { get; }
        public string TenHK { get; }
        public LopHocHKItem(string maHK, string tenHK) { MaHK = maHK; TenHK = tenHK; }
        public override string ToString() => TenHK;
    }

    internal class LopHocMonItem
    {
        public string MaMH { get; }
        public string TenMH { get; }
        public LopHocMonItem(string maMH, string tenMH) { MaMH = maMH; TenMH = tenMH; }
        public override string ToString() => TenMH;
    }

    internal class LopHocGVItem
    {
        public string MaGV { get; }
        public string HoTen { get; }
        public LopHocGVItem(string maGV, string hoTen) { MaGV = maGV; HoTen = hoTen; }
        public override string ToString() => HoTen;
    }

    // ============================================================
    //  Dialog: Tạo lớp học phần mới
    // ============================================================
    internal class TaoLopDialog : Form
    {
        public string MaMH { get; private set; } = "";
        public string MaGV { get; private set; } = "";
        public string MaHK { get; private set; } = "";
        public string Phong { get; private set; } = "";
        public string LichHoc { get; private set; } = "";
        public int SiSoToiDa { get; private set; } = 40;

        private readonly ComboBox cboMon = new ComboBox();
        private readonly ComboBox cboGV = new ComboBox();
        private readonly ComboBox cboHK = new ComboBox();
        private readonly TextBox txtPhong = new TextBox();
        private readonly TextBox txtLich = new TextBox();
        private readonly NumericUpDown numSiSo = new NumericUpDown();

        public TaoLopDialog(DatabaseHelper db)
        {
            this.Text = "Tạo Lớp Học Phần Mới";
            this.Size = new Size(420, 320);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            var tbl = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 7, Padding = new Padding(10) };
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            numSiSo.Minimum = 1; numSiSo.Maximum = 200; numSiSo.Value = 40;
            cboMon.DropDownStyle = cboGV.DropDownStyle = cboHK.DropDownStyle = ComboBoxStyle.DropDownList;

            // Nạp dữ liệu vào combo
            try
            {
                using var conn = db.GetConnection(); conn.Open();
                Fill(conn, cboMon, "SELECT MaMH, TenMH FROM MonHoc WHERE TrangThai='DangMoLop' ORDER BY TenMH", "MaMH", "TenMH");
                Fill(conn, cboGV, "SELECT MaGV, HoTen FROM GiangVien ORDER BY HoTen", "MaGV", "HoTen");
                Fill(conn, cboHK, "SELECT MaHK, TenHK FROM HocKy ORDER BY MaHK DESC", "MaHK", "TenHK");
            }
            catch { }

            tbl.Controls.Add(new Label { Text = "Môn học:", Anchor = AnchorStyles.Right }, 0, 0); tbl.Controls.Add(cboMon, 1, 0);
            tbl.Controls.Add(new Label { Text = "Giảng viên:", Anchor = AnchorStyles.Right }, 0, 1); tbl.Controls.Add(cboGV, 1, 1);
            tbl.Controls.Add(new Label { Text = "Học kỳ:", Anchor = AnchorStyles.Right }, 0, 2); tbl.Controls.Add(cboHK, 1, 2);
            tbl.Controls.Add(new Label { Text = "Phòng học:", Anchor = AnchorStyles.Right }, 0, 3); tbl.Controls.Add(txtPhong, 1, 3);
            tbl.Controls.Add(new Label { Text = "Lịch học:", Anchor = AnchorStyles.Right }, 0, 4); tbl.Controls.Add(txtLich, 1, 4);
            tbl.Controls.Add(new Label { Text = "Sĩ số tối đa:", Anchor = AnchorStyles.Right }, 0, 5); tbl.Controls.Add(numSiSo, 1, 5);

            var btnOK = new Button { Text = "Tạo", DialogResult = DialogResult.OK };
            var btnHuy = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel };
            var pan = new FlowLayoutPanel { FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Fill };
            pan.Controls.Add(btnHuy); pan.Controls.Add(btnOK);
            tbl.Controls.Add(pan, 1, 6);

            this.Controls.Add(tbl);
            this.AcceptButton = btnOK;
            this.CancelButton = btnHuy;

            btnOK.Click += (s, e) =>
            {
                if (cboMon.SelectedIndex < 0 || cboGV.SelectedIndex < 0 || cboHK.SelectedIndex < 0)
                { MessageBox.Show("Vui lòng chọn đầy đủ Môn học, GV, Học kỳ.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning); this.DialogResult = DialogResult.None; return; }

                MaMH = ((LopHocMonItem)cboMon.SelectedItem).MaMH;
                MaGV = ((LopHocGVItem)cboGV.SelectedItem).MaGV;
                MaHK = ((LopHocHKItem)cboHK.SelectedItem).MaHK;
                Phong = txtPhong.Text.Trim();
                LichHoc = txtLich.Text.Trim();
                SiSoToiDa = (int)numSiSo.Value;
            };
        }

        private void Fill(SqlConnection conn, ComboBox cb, string sql, string keyCol, string valCol)
        {
            var cmd = new SqlCommand(sql, conn);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                if (keyCol == "MaMH") cb.Items.Add(new LopHocMonItem(r[keyCol].ToString(), r[valCol].ToString()));
                else if (keyCol == "MaGV") cb.Items.Add(new LopHocGVItem(r[keyCol].ToString(), r[valCol].ToString()));
                else cb.Items.Add(new LopHocHKItem(r[keyCol].ToString(), r[valCol].ToString()));
            }
            if (cb.Items.Count > 0) cb.SelectedIndex = 0;
        }
    }

    // ============================================================
    //  Dialog: Sửa lớp học phần
    // ============================================================
    internal class SuaLopDialog : Form
    {
        public string MaGV { get; private set; } = "";
        public string Phong { get; private set; } = "";
        public string LichHoc { get; private set; } = "";
        public int SiSoToiDa { get; private set; } = 40;
        public string TrangThai { get; private set; } = "DangMo";

        private readonly ComboBox cboGV = new ComboBox();
        private readonly ComboBox cboTT = new ComboBox();
        private readonly TextBox txtPhong = new TextBox();
        private readonly TextBox txtLich = new TextBox();
        private readonly NumericUpDown numSiSo = new NumericUpDown();

        public SuaLopDialog(DatabaseHelper db, string maLHP)
        {
            this.Text = $"Sửa Lớp: {maLHP}";
            this.Size = new Size(400, 270);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            var tbl = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 6, Padding = new Padding(10) };
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            numSiSo.Minimum = 1; numSiSo.Maximum = 200; numSiSo.Value = 40;
            cboGV.DropDownStyle = cboTT.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTT.Items.AddRange(new[] { "DangMo", "DaKhoa", "Huy" });
            cboTT.SelectedIndex = 0;

            // Nạp GV
            try
            {
                using var conn = db.GetConnection(); conn.Open();
                var cmd = new SqlCommand("SELECT MaGV, HoTen FROM GiangVien ORDER BY HoTen", conn);
                using var r = cmd.ExecuteReader();
                while (r.Read()) cboGV.Items.Add(new LopHocGVItem(r["MaGV"].ToString(), r["HoTen"].ToString()));
            }
            catch { }

            // Nạp thông tin lớp hiện tại
            try
            {
                using var conn = db.GetConnection(); conn.Open();
                var cmd = new SqlCommand("SELECT MaGV, Phong, LichHoc, SiSoToiDa, TrangThai FROM LopHocPhan WHERE MaLHP=@MaLHP", conn);
                cmd.Parameters.AddWithValue("@MaLHP", maLHP);
                using var r = cmd.ExecuteReader();
                if (r.Read())
                {
                    txtPhong.Text = r["Phong"]?.ToString() ?? "";
                    txtLich.Text = r["LichHoc"]?.ToString() ?? "";
                    numSiSo.Value = r["SiSoToiDa"] != DBNull.Value ? Convert.ToDecimal(r["SiSoToiDa"]) : 40;
                    string tthai = r["TrangThai"]?.ToString() ?? "DangMo";
                    int idx = cboTT.Items.IndexOf(tthai);
                    cboTT.SelectedIndex = idx >= 0 ? idx : 0;

                    string maGV = r["MaGV"]?.ToString() ?? "";
                    for (int i = 0; i < cboGV.Items.Count; i++)
                        if (((LopHocGVItem)cboGV.Items[i]).MaGV == maGV) { cboGV.SelectedIndex = i; break; }
                }
            }
            catch { }

            tbl.Controls.Add(new Label { Text = "Giảng viên:", Anchor = AnchorStyles.Right }, 0, 0); tbl.Controls.Add(cboGV, 1, 0);
            tbl.Controls.Add(new Label { Text = "Phòng học:", Anchor = AnchorStyles.Right }, 0, 1); tbl.Controls.Add(txtPhong, 1, 1);
            tbl.Controls.Add(new Label { Text = "Lịch học:", Anchor = AnchorStyles.Right }, 0, 2); tbl.Controls.Add(txtLich, 1, 2);
            tbl.Controls.Add(new Label { Text = "Sĩ số tối đa:", Anchor = AnchorStyles.Right }, 0, 3); tbl.Controls.Add(numSiSo, 1, 3);
            tbl.Controls.Add(new Label { Text = "Trạng thái:", Anchor = AnchorStyles.Right }, 0, 4); tbl.Controls.Add(cboTT, 1, 4);

            var btnOK = new Button { Text = "Lưu", DialogResult = DialogResult.OK };
            var btnHuy = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel };
            var pan = new FlowLayoutPanel { FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Fill };
            pan.Controls.Add(btnHuy); pan.Controls.Add(btnOK);
            tbl.Controls.Add(pan, 1, 5);

            this.Controls.Add(tbl);
            this.AcceptButton = btnOK;
            this.CancelButton = btnHuy;

            btnOK.Click += (s, e) =>
            {
                MaGV = cboGV.SelectedItem is LopHocGVItem gv ? gv.MaGV : "";
                Phong = txtPhong.Text.Trim();
                LichHoc = txtLich.Text.Trim();
                SiSoToiDa = (int)numSiSo.Value;
                TrangThai = cboTT.SelectedItem?.ToString() ?? "DangMo";
            };
        }
    }

    // ============================================================
    //  Dialog: Danh sách SV trong lớp (button5)
    // ============================================================
    internal class DanhSachSVTrongLopDialog : Form
    {
        public DanhSachSVTrongLopDialog(DatabaseHelper db, string maLHP)
        {
            this.Text = $"Danh Sách SV – Lớp {maLHP}";
            this.Size = new Size(600, 420);
            this.StartPosition = FormStartPosition.CenterParent;

            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            var btnDong = new Button { Text = "Đóng", Dock = DockStyle.Bottom, Height = 35 };
            btnDong.Click += (s, e) => this.Close();

            this.Controls.Add(dgv);
            this.Controls.Add(btnDong);

            // Nạp dữ liệu
            try
            {
                using var conn = db.GetConnection(); conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT sv.MaSV, sv.HoTen, sv.MaLop AS Lop,
                           d.DiemCC, d.DiemGK, d.DiemCK, d.DiemTB, d.XepLoai, d.KetQua
                    FROM DangKyHocPhan dk
                    JOIN SinhVien sv ON dk.MaSV = sv.MaSV
                    LEFT JOIN Diem d  ON d.MaDK  = dk.MaDK
                    WHERE dk.MaLHP = @MaLHP
                    ORDER BY sv.HoTen", conn);
                cmd.Parameters.AddWithValue("@MaLHP", maLHP);

                using var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);

                dgv.AutoGenerateColumns = true;
                dgv.Columns.Clear();
                dgv.DataSource = dt;

                if (dgv.Columns["MaSV"] != null) dgv.Columns["MaSV"].HeaderText = "MSV";
                if (dgv.Columns["HoTen"] != null) dgv.Columns["HoTen"].HeaderText = "Họ Tên";
                if (dgv.Columns["Lop"] != null) dgv.Columns["Lop"].HeaderText = "Lớp";
                if (dgv.Columns["DiemCC"] != null) dgv.Columns["DiemCC"].HeaderText = "CC";
                if (dgv.Columns["DiemGK"] != null) dgv.Columns["DiemGK"].HeaderText = "GK";
                if (dgv.Columns["DiemCK"] != null) dgv.Columns["DiemCK"].HeaderText = "CK";
                if (dgv.Columns["DiemTB"] != null) dgv.Columns["DiemTB"].HeaderText = "DTB";
                if (dgv.Columns["XepLoai"] != null) dgv.Columns["XepLoai"].HeaderText = "Xếp Loại";
                if (dgv.Columns["KetQua"] != null) dgv.Columns["KetQua"].HeaderText = "Kết Quả";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải danh sách:\n" + ex.Message);
            }
        }
    }
}
