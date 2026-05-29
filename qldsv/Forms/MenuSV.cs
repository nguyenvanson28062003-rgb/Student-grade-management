#nullable disable
using Microsoft.Data.SqlClient;
using System.Data;
using qldsv.Service;

namespace qldsv
{
    public partial class MenuSV : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;

        public MenuSV()
        {
            InitializeComponent();
            ThemeApplier.Apply(this);
            this.Load += MenuSV_Load;

            // Sidebar
            button7.Click += (s, e) => TaiDashboard();                                           // Tổng quan (refresh)
            button8.Click += (s, e) => MoFormBangDiem();                                         // Bảng điểm
            button10.Click += (s, e) => MessageBox.Show("Đăng ký môn học: đang phát triển.");     // Đăng ký MH
            button2.Click += (s, e) => new XemHoSo(SessionInfo.MaSV).ShowDialog();              // Hồ Sơ
            button3.Click += (s, e) => new quản_lí_điểm_sinh_viên.DoiMK().ShowDialog();          // Đổi MK
            button1.Click += (s, e) => MessageBox.Show("TKB: đang phát triển.");                  // TKB

            // Tiêu đề
            button11.Click += (s, e) => this.Close();   // X – đóng Menu → app thoát
            button12.Click += (s, e) => new quản_lí_điểm_sinh_viên.DoiMK().ShowDialog();
        }

        private void MenuSV_Load(object sender, EventArgs e)
        {
            label4.Text = "Sinh Viên";
            label29.Text = SessionInfo.HoTen;
            label30.Text = $"Xin chào, {SessionInfo.HoTen}";
            label31.Text = SessionInfo.MaSV;
            TaiDashboard();
        }

        private void TaiDashboard()
        {
            TaiThongKe();
            TaiDiemGanNhat();
        }

        private void TaiThongKe()
        {
            try
            {
                string sql = @"
                    SELECT
                        ISNULL(g.GPATichLuy, 0)                                AS GPA,
                        ISNULL(g.TongTinChi, 0)                                AS TongTC,
                        (SELECT COUNT(*) FROM DangKyHocPhan dk
                         JOIN LopHocPhan lhp ON dk.MaLHP = lhp.MaLHP
                         JOIN HocKy hk ON lhp.MaHK = hk.MaHK
                         WHERE dk.MaSV = @MaSV AND hk.NamHoc = YEAR(GETDATE()))  AS MonKiNay,
                        (SELECT COUNT(*) FROM DangKyHocPhan dk
                         WHERE dk.MaSV = @MaSV AND NOT EXISTS (
                             SELECT 1 FROM Diem d WHERE d.MaDK = dk.MaDK AND d.DiemCK IS NOT NULL
                         ))                                                     AS ChuaCoĐiem
                    FROM SinhVien sv
                    LEFT JOIN vw_GPATichLuy g ON sv.MaSV = g.MaSV
                    WHERE sv.MaSV = @MaSV";

                using var conn = _db.GetConnection();
                conn.Open();
                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSV", SessionInfo.MaSV);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label45.Text = reader["GPA"].ToString();       // GPA tích lũy (giá trị)
                    label43.Text = reader["TongTC"].ToString();    // TC đã qua (giá trị)
                    label41.Text = reader["MonKiNay"].ToString();  // Môn kì này (giá trị)
                    label39.Text = reader["ChuaCoĐiem"].ToString(); // Chưa có điểm (giá trị)
                }
            }
            catch { }
        }

        private void TaiDiemGanNhat()
        {
            try
            {
                var dt = _db.ExecuteSP("sp_LayBangDiem",
                    DatabaseHelper.Param("@MaSV", SessionInfo.MaSV),
                    DatabaseHelper.Param("@MaHK", DBNull.Value)
                );

                // Xóa cột Designer, bind từ DataTable
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.Columns.Clear();

                // Lấy 5 môn gần nhất
                if (dt.Rows.Count > 0)
                {
                    var dtGanNhat = dt.AsEnumerable().Take(5).CopyToDataTable();
                    dataGridView1.DataSource = dtGanNhat;
                }
                else
                {
                    dataGridView1.DataSource = dt;
                }

                SetHeader("TenMH",   "Môn Học");
                SetHeader("DiemCC",  "CC");
                SetHeader("DiemGK",  "GK");
                SetHeader("DiemCK",  "CK");
                SetHeader("DiemTB",  "DTB");
                SetHeader("XepLoai", "Xếp Loại");

                // Ẩn cột không cần
                HideCol("MaMH"); HideCol("MaHK"); HideCol("TenHK");
                HideCol("SoTinChi"); HideCol("DiemGPA"); HideCol("KetQua");
                HideCol("NamHoc"); HideCol("DaChotDiem");
            }
            catch { }
        }

        private void SetHeader(string colName, string header)
        {
            if (dataGridView1.Columns[colName] != null)
                dataGridView1.Columns[colName].HeaderText = header;
        }

        private void HideCol(string colName)
        {
            if (dataGridView1.Columns[colName] != null)
                dataGridView1.Columns[colName].Visible = false;
        }

        private void MoFormBangDiem()
        {
            var frm = new quản_lí_điểm_sinh_viên.BangDiemcs(SessionInfo.MaSV);
            frm.ShowDialog();
        }

        private void panel16_Paint(object sender, PaintEventArgs e) { }
    }
}