#nullable disable
using Microsoft.Data.SqlClient;
using System.Data;
using qldsv.Service;

namespace qldsv
{
    public partial class MenuGV : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;

        public MenuGV()
        {
            InitializeComponent();
            // Không gọi ThemeApplier — form tự custom-paint
            this.Load += MenuGV_Load;

            // Sidebar
            button5.Click  += (s, e) => MoForm(new LopHoc());                          // Lớp Học Phần
            button6.Click  += (s, e) => MoForm(new SinhVien());                        // Danh Sách SV
            button7.Click  += (s, e) => MoFormNhapDiem();                              // Nhập Điểm
            button8.Click  += (s, e) => MoFormBangDiem();                              // Xem Bảng Điểm
            button10.Click += (s, e) => new ThongKeLopGVDialog(_db, SessionInfo.MaGV).ShowDialog();   // Thống Kê Lớp
            button4.Click  += (s, e) => { TaiLichSuNhapDiem(); };                                     // Lịch sử (refresh)
            button2.Click  += (s, e) => new HoSoCaNhanGVDialog(_db, SessionInfo.MaGV).ShowDialog();   // Hồ Sơ Cá Nhân
            button3.Click  += (s, e) => new quản_lí_điểm_sinh_viên.DoiMK().ShowDialog();

            button11.Click += (s, e) => this.Close();
            button12.Click += (s, e) => new quản_lí_điểm_sinh_viên.DoiMK().ShowDialog();

            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
        }

        private void MenuGV_Load(object sender, EventArgs e)
        {
            label29.Text = SessionInfo.HoTen;
            label31.Text = "Mã GV: " + SessionInfo.MaGV;
            label30.Text = $"Xin chào, {SessionInfo.HoTen}! 👋";
            label4.Text  = "Giảng Viên";
            TaiDashboardGV();
        }

        private void TaiDashboardGV()
        {
            TaiThongKeGV();
            TaiDanhSachLopHP();
            TaiLichSuNhapDiem();
        }

        private void TaiThongKeGV()
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                using var cmd = new SqlCommand(@"
                    SELECT
                        (SELECT COUNT(*) FROM LopHocPhan WHERE MaGV=@MaGV)            AS TongLop,
                        (SELECT COUNT(DISTINCT dk.MaSV)
                         FROM DangKyHocPhan dk
                         JOIN LopHocPhan lhp ON dk.MaLHP = lhp.MaLHP
                         WHERE lhp.MaGV = @MaGV)                                       AS TongSV,
                        (SELECT COUNT(*)
                         FROM DangKyHocPhan dk
                         JOIN LopHocPhan lhp ON dk.MaLHP = lhp.MaLHP
                         WHERE lhp.MaGV = @MaGV
                           AND NOT EXISTS (SELECT 1 FROM Diem d
                                           WHERE d.MaDK = dk.MaDK AND d.DiemCK IS NOT NULL)) AS ChuaNhap,
                        CAST(ISNULL((
                            SELECT COUNT(CASE WHEN d.KetQua='Dat' THEN 1 END)*100.0 / NULLIF(COUNT(*),0)
                            FROM Diem d
                            JOIN DangKyHocPhan dk ON d.MaDK = dk.MaDK
                            JOIN LopHocPhan lhp ON dk.MaLHP = lhp.MaLHP
                            WHERE lhp.MaGV = @MaGV AND dk.TrangThai='HoanThanh'
                        ), 0) AS DECIMAL(5,1))                                          AS TiLeDat", conn);
                cmd.Parameters.AddWithValue("@MaGV", SessionInfo.MaGV);

                using var r = cmd.ExecuteReader();
                if (r.Read())
                {
                    label45.Text = SafeInt(r, "TongLop").ToString();
                    label43.Text = SafeInt(r, "TongSV").ToString();
                    label41.Text = SafeInt(r, "ChuaNhap").ToString();
                    label39.Text = SafeDbl(r, "TiLeDat").ToString("F1") + "%";

                    foreach (var pn in new[] { panel28, panel27, panel26, panel25 })
                        pn.Invalidate();
                }
            }
            catch { }
        }

        private void TaiDanhSachLopHP()
        {
            try
            {
                string sql = @"
                    SELECT lhp.MaLHP, mh.TenMH AS TenMonHoc,
                           lhp.SiSoHienTai AS SiSo, lhp.TrangThai
                    FROM LopHocPhan lhp
                    JOIN MonHoc mh ON lhp.MaMH = mh.MaMH
                    WHERE lhp.MaGV = @MaGV
                    ORDER BY lhp.MaLHP";

                using var conn = _db.GetConnection(); conn.Open();
                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaGV", SessionInfo.MaGV);
                using var da = new SqlDataAdapter(cmd);
                var dt = new DataTable(); da.Fill(dt);

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = dt;

                H(dataGridView1, "MaLHP",     "Mã LHP");
                H(dataGridView1, "TenMonHoc", "Môn Học");
                H(dataGridView1, "SiSo",      "Sĩ Số");
                H(dataGridView1, "TrangThai", "Trạng Thái");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TaiLichSuNhapDiem()
        {
            try
            {
                using var conn = _db.GetConnection(); conn.Open();
                using var cmd = new SqlCommand(@"
                    SELECT TOP 30
                        mh.TenMH                                    AS MonHoc,
                        sv.HoTen                                    AS SinhVien,
                        ls.DiemCK_Cu                                AS CK_Cu,
                        d.DiemCK                                    AS CK_Moi,
                        FORMAT(ls.NgayThayDoi, N'dd/MM/yyyy HH:mm') AS ThoiGian
                    FROM LichSuDiem ls
                    JOIN Diem d           ON ls.MaDiem = d.MaDiem
                    JOIN DangKyHocPhan dk ON d.MaDK    = dk.MaDK
                    JOIN LopHocPhan lhp   ON dk.MaLHP  = lhp.MaLHP
                    JOIN MonHoc mh        ON lhp.MaMH  = mh.MaMH
                    JOIN SinhVien sv      ON dk.MaSV   = sv.MaSV
                    WHERE lhp.MaGV = @MaGV
                    ORDER BY ls.NgayThayDoi DESC", conn);
                cmd.Parameters.AddWithValue("@MaGV", SessionInfo.MaGV);

                using var da = new SqlDataAdapter(cmd);
                var dt = new DataTable(); da.Fill(dt);

                dgvLichSu.AutoGenerateColumns = true;
                dgvLichSu.Columns.Clear();
                dgvLichSu.DataSource = dt;

                H(dgvLichSu, "MonHoc",   "Môn Học");
                H(dgvLichSu, "SinhVien", "Sinh Viên");
                H(dgvLichSu, "CK_Cu",    "CK Cũ");
                H(dgvLichSu, "CK_Moi",   "CK Mới");
                H(dgvLichSu, "ThoiGian", "Thời Gian");
            }
            catch { }
        }

        private static void H(DataGridView dgv, string col, string header)
        {
            if (dgv.Columns[col] != null) dgv.Columns[col].HeaderText = header;
        }

        private static int    SafeInt(System.Data.IDataRecord r, string c)
            => r[c] == DBNull.Value ? 0 : Convert.ToInt32(r[c]);
        private static double SafeDbl(System.Data.IDataRecord r, string c)
            => r[c] == DBNull.Value ? 0.0 : Convert.ToDouble(r[c]);

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string maLHP = dataGridView1.Rows[e.RowIndex].Cells["MaLHP"].Value?.ToString() ?? "";
            if (!string.IsNullOrEmpty(maLHP))
            {
                new quản_lí_điểm_sinh_viên.NhapDiem(maLHP).ShowDialog();
                TaiDashboardGV();
            }
        }

        private void MoFormNhapDiem()
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn lớp học phần từ danh sách!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maLHP = dataGridView1.CurrentRow.Cells["MaLHP"].Value?.ToString() ?? "";
            if (!string.IsNullOrEmpty(maLHP))
            {
                new quản_lí_điểm_sinh_viên.NhapDiem(maLHP).ShowDialog();
                TaiDashboardGV();
            }
        }

        private void MoFormBangDiem()
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn lớp học phần để xem bảng điểm!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maLHP = dataGridView1.CurrentRow.Cells["MaLHP"].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(maLHP)) return;
            new DanhSachSVTrongLopDialog(_db, maLHP).ShowDialog();
        }

        private void MoForm(Form form)
        {
            this.Hide();
            form.FormClosed += (s, e) => { TaiDashboardGV(); this.Show(); };
            form.Show();
        }
    }
}
