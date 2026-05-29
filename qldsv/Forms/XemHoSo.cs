#nullable disable
using Microsoft.Data.SqlClient;
using System.Data;
using qldsv.Service;

namespace qldsv
{
    public partial class XemHoSo : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;
        private readonly string _maSV;

        public XemHoSo(string maSV)
        {
            InitializeComponent();
            ThemeApplier.Apply(this);
            _maSV = maSV;
            this.Load += XemHoSo_Load;
            button1.Click += btnDong_Click;
        }

        private void XemHoSo_Load(object sender, EventArgs e)
        {
            NapThongTinSinhVien();
        }

        private void NapThongTinSinhVien()
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT
                        sv.MaSV,
                        sv.HoTen,
                        sv.NgaySinh,
                        sv.GioiTinh,
                        sv.DiaChi,
                        sv.SoDienThoai,
                        sv.Email,
                        sv.MaLop,
                        k.TenKhoa,
                        sv.TinhTrang,
                        sv.NamNhapHoc
                    FROM SinhVien sv
                    LEFT JOIN Lop l ON sv.MaLop = l.MaLop
                    LEFT JOIN Khoa k ON l.MaKhoa = k.MaKhoa
                    WHERE sv.MaSV = @MaSV", conn);
                cmd.Parameters.AddWithValue("@MaSV", _maSV);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label1.Text = "Mã Sinh Viên: " + reader["MaSV"]?.ToString();
                    label2.Text = "Họ Tên: " + reader["HoTen"]?.ToString();
                    label3.Text = "Ngày Sinh: " + (reader["NgaySinh"] != DBNull.Value 
                        ? Convert.ToDateTime(reader["NgaySinh"]).ToString("dd/MM/yyyy") 
                        : "");
                    string gioiTinh = reader["GioiTinh"]?.ToString() ?? "";
                    label4.Text = "Giới Tính: " + (gioiTinh == "Nam" ? "Nam" : gioiTinh == "Nu" ? "Nữ" : "");
                    label5.Text = "Địa Chỉ: " + reader["DiaChi"]?.ToString();
                    label6.Text = "Số Điện Thoại: " + reader["SoDienThoai"]?.ToString();
                    label7.Text = "Email: " + reader["Email"]?.ToString();
                    label8.Text = "Lớp: " + reader["MaLop"]?.ToString();
                    label9.Text = "Khoa: " + reader["TenKhoa"]?.ToString();
                    string tinhTrang = reader["TinhTrang"]?.ToString() ?? "";
                    label10.Text = "Tình Trạng: " + 
                        (tinhTrang == "DangHoc" ? "Đang Học" : 
                         tinhTrang == "TotNghiep" ? "Tốt Nghiệp" : 
                         tinhTrang == "BoHoc" ? "Bỏ Học" : 
                         tinhTrang == "BaoLuu" ? "Bảo Lưu" : "");
                    label11.Text = "Năm Nhập Học: " + (reader["NamNhapHoc"] != DBNull.Value 
                        ? reader["NamNhapHoc"].ToString() 
                        : "");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải thông tin hồ sơ:\n" + ex.Message, 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
