#nullable disable
using Microsoft.Data.SqlClient;
using System.Data;
using qldsv.Service;

namespace qldsv
{
    public partial class MenuAdmin : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;

        public MenuAdmin()
        {
            InitializeComponent();
            this.Load += MenuAdmin_Load;

            // Thanh menu trái - Người dùng
            button9.Click += (s, e) => MoForm(new SinhVien());                         // Sinh Viên
            button1.Click += (s, e) => MoForm(new GiangVien());                        // Giảng Viên
            button2.Click += (s, e) => MoForm(new quản_lí_điểm_sinh_viên.QLTK());     // Tài Khoản
            button3.Click += (s, e) => MoForm(new quản_lí_điểm_sinh_viên.QLTK());     // Phân Quyền

            // Học Tập
            button4.Click += (s, e) => MoForm(new MonHoc());                           // Môn Học
            button5.Click += (s, e) => MoForm(new LopHoc());                           // Lớp Học Phần
            button6.Click += (s, e) => MoForm(new LopHoc());                           // Nhập Điểm (qua LopHoc)
            button7.Click += (s, e) => MoFormBangDiem();                               // Bảng Điểm

            // Báo Cáo
            button8.Click += (s, e) => MoForm(new quản_lí_điểm_sinh_viên.ThongKe()); // Thống Kê
            button10.Click += (s, e) => MoForm(new quản_lí_điểm_sinh_viên.ThongKe()); // Xuất Báo Cáo

            // Thanh tiêu đề
            button11.Click += (s, e) => this.Close();                                   // X – đóng Menu → app thoát
            button12.Click += (s, e) => new quản_lí_điểm_sinh_viên.DoiMK().ShowDialog(); // Settings
        }

        private void MenuAdmin_Load(object sender, EventArgs e)
        {
            label2.Text = $"Xin chào, Admin: {SessionInfo.TenDangNhap}";
            TaiThongKeDashboard();
        }

        // --------------------------------------------------------
        //  Tải thống kê dashboard
        // --------------------------------------------------------
        private void TaiThongKeDashboard()
        {
            try
            {
                var ds = _db.ExecuteSPMultiResult("sp_ThongKe",
                    DatabaseHelper.Param("@MaHK", DBNull.Value),
                    DatabaseHelper.Param("@MaKhoa", DBNull.Value)
                );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var row = ds.Tables[0].Rows[0];
                    label6.Text = row["TongSV"]?.ToString() ?? "0";
                    label8.Text = row["TongMH"]?.ToString() ?? "0";
                    label10.Text = row["TongLHP"]?.ToString() ?? "0";
                    label12.Text = row["SVCanChuY"]?.ToString() ?? "0";
                }

                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    var row = ds.Tables[1].Rows[0];
                    int tongSV = Convert.ToInt32(row["TongSV"]);
                    if (tongSV > 0)
                    {
                        label22.Text = row["SoLuongA"]?.ToString() ?? "0";
                        label23.Text = row["SoLuongBPlus"]?.ToString() ?? "0";
                        label24.Text = row["SoLuongB"]?.ToString() ?? "0";
                        label25.Text = row["SoLuongCPlus"]?.ToString() ?? "0";
                        label26.Text = row["SoLuongC"]?.ToString() ?? "0";
                        label27.Text = row["SoLuongDF"]?.ToString() ?? "0";
                    }
                }
            }
            catch
            {
                TaiThongKeDonGian();
            }
        }

        private void TaiThongKeDonGian()
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                using var cmd = new SqlCommand(@"
                    SELECT
                        (SELECT COUNT(*) FROM SinhVien)                               AS TongSV,
                        (SELECT COUNT(*) FROM MonHoc)                                 AS TongMH,
                        (SELECT COUNT(*) FROM LopHocPhan)                             AS TongLHP,
                        (SELECT COUNT(*) FROM SinhVien WHERE TinhTrang = 'DangHoc')   AS SVDangHoc
                    ", conn);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label6.Text = reader["TongSV"].ToString();
                    label8.Text = reader["TongMH"].ToString();
                    label10.Text = reader["TongLHP"].ToString();
                    label12.Text = "0";
                }
            }
            catch { }
        }

        // --------------------------------------------------------
        //  Helpers mở form con – ẩn Menu, khi con đóng → hiện lại Menu
        // --------------------------------------------------------
        private void MoForm(Form form)
        {
            this.Hide();
            form.FormClosed += (s, e) =>
            {
                TaiThongKeDashboard(); // refresh dashboard
                this.Show();
            };
            form.Show();
        }

        private void MoFormBangDiem()
        {
            // Admin vào danh sách SV → double-click để xem bảng điểm từ form SinhVien
            MoForm(new SinhVien());
        }
    }
}