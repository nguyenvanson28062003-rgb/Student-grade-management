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
            ThemeApplier.Apply(this);
            this.Load += MenuGV_Load;

            // Sidebar
            button5.Click += (s, e) => MoForm(new LopHoc());                           // Lớp Học Phần
            button6.Click += (s, e) => MoForm(new SinhVien());                         // Danh Sách SV
            button7.Click += (s, e) => MoFormNhapDiem();                               // Nhập Điểm
            button8.Click += (s, e) => MoFormBangDiem();                               // Xem Bảng Điểm
            button10.Click += (s, e) => MoForm(new quản_lí_điểm_sinh_viên.ThongKe());  // Thống Kê
            button1.Click += (s, e) => MessageBox.Show("Xuất Điểm: đang phát triển.");
            button2.Click += (s, e) => MessageBox.Show("Hồ Sơ: đang phát triển.");
            button3.Click += (s, e) => new quản_lí_điểm_sinh_viên.DoiMK().ShowDialog(); // Đổi MK

            // Tiêu đề
            button11.Click += (s, e) => this.Close();   // X – đóng Menu → app thoát
            button12.Click += (s, e) => new quản_lí_điểm_sinh_viên.DoiMK().ShowDialog();

            // Double-click lớp → nhập điểm trực tiếp từ dashboard
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
        }

        private void MenuGV_Load(object sender, EventArgs e)
        {
            label29.Text = $"GV: {SessionInfo.HoTen}";
            label30.Text = $"Xin chào, {SessionInfo.HoTen}";
            TaiDashboardGV();
        }

        private void TaiDashboardGV()
        {
            try
            {
                var ds = _db.ExecuteSPMultiResult("sp_LayLopHPCuaGV",
                    DatabaseHelper.Param("@MaGV", SessionInfo.MaGV)
                );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var row = ds.Tables[0].Rows[0];
                    label45.Text = row["TongLop"]?.ToString() ?? "0";
                    label43.Text = row["TongSV"]?.ToString() ?? "0";
                    label41.Text = row["ChuaNhapDiem"]?.ToString() ?? "0";
                    label39.Text = row["TiLeDat"]?.ToString() ?? "0%";
                }
            }
            catch { }

            TaiDanhSachLopHP();
        }

        private void TaiDanhSachLopHP()
        {
            try
            {
                string sql = @"
                    SELECT
                        lhp.MaLHP,
                        mh.TenMH        AS TenMonHoc,
                        lhp.Phong,
                        lhp.LichHoc,
                        lhp.SiSoHienTai  AS SiSo,
                        lhp.TrangThai
                    FROM LopHocPhan lhp
                    JOIN MonHoc mh ON lhp.MaMH = mh.MaMH
                    WHERE lhp.MaGV = @MaGV
                    ORDER BY lhp.MaLHP";

                using var conn = _db.GetConnection();
                conn.Open();
                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaGV", SessionInfo.MaGV);

                using var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = dt;

                if (dataGridView1.Columns["MaLHP"] != null) dataGridView1.Columns["MaLHP"].HeaderText = "Mã LHP";
                if (dataGridView1.Columns["TenMonHoc"] != null) dataGridView1.Columns["TenMonHoc"].HeaderText = "Môn Học";
                if (dataGridView1.Columns["Phong"] != null) dataGridView1.Columns["Phong"].HeaderText = "Phòng";
                if (dataGridView1.Columns["LichHoc"] != null) dataGridView1.Columns["LichHoc"].HeaderText = "Lịch";
                if (dataGridView1.Columns["SiSo"] != null) dataGridView1.Columns["SiSo"].HeaderText = "Sĩ Số";
                if (dataGridView1.Columns["TrangThai"] != null) dataGridView1.Columns["TrangThai"].HeaderText = "Trạng Thái";

                label45.Text = dt.Rows.Count.ToString(); // Tổng lớp phân công
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string maLHP = dataGridView1.Rows[e.RowIndex].Cells["MaLHP"].Value?.ToString() ?? "";
            if (!string.IsNullOrEmpty(maLHP))
            {
                var frm = new quản_lí_điểm_sinh_viên.NhapDiem(maLHP);
                frm.ShowDialog();
                TaiDanhSachLopHP();
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
                var frm = new quản_lí_điểm_sinh_viên.NhapDiem(maLHP);
                frm.ShowDialog();
                TaiDanhSachLopHP();
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

            // Lấy danh sách SV trong lớp và cho phép xem bảng điểm từng SV
            var frmDS = new DanhSachSVTrongLopDialog(_db, maLHP);
            frmDS.ShowDialog();
        }

        private void MoForm(Form form)
        {
            this.Hide();
            form.FormClosed += (s, e) =>
            {
                TaiDashboardGV();
                this.Show();
            };
            form.Show();
        }
    }
}