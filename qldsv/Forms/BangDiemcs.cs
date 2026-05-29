#nullable disable
using Microsoft.Data.SqlClient;
using System.Data;
using qldsv;
using qldsv.Service;

namespace quản_lí_điểm_sinh_viên
{
    public partial class BangDiemcs : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;
        private readonly string _maSV;
        private DataTable _dtDiem = new DataTable();

        public BangDiemcs(string maSV)
        {
            InitializeComponent();
            _maSV = maSV;

            this.Load += BangDiem_Load;

            comboBox1.SelectedIndexChanged += (s, e) => TaiBangDiem();
            button1.Click += btnInBangDiem_Click;  // In Bảng Điểm
            button2.Click += btnXuatPDF_Click;     // Xuất PDF
            // button3 (Quay Lại Menu) đã được wired trong Designer
        }

        private void BangDiem_Load(object sender, EventArgs e)
        {
            NapThongTinSV();
            NapComboHocKy();
            TaiBangDiem();
        }

        private void NapThongTinSV()
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT sv.MaSV, sv.HoTen, sv.Lop, sv.Khoa,
                           ISNULL(g.GPATichLuy, 0) AS GPA,
                           ISNULL(g.TongTinChi,  0) AS TongTC
                    FROM SinhVien sv
                    LEFT JOIN vw_GPATichLuy g ON sv.MaSV = g.MaSV
                    WHERE sv.MaSV = @MaSV", conn);
                cmd.Parameters.AddWithValue("@MaSV", _maSV);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // groupBox1 – label1: MSV, label2: HoTen, label3: Lop, label4: Khoa
                    label1.Text = "MSV: " + reader["MaSV"].ToString();
                    label2.Text = "Họ Tên: " + reader["HoTen"].ToString();
                    label3.Text = "Lớp: " + reader["Lop"]?.ToString();
                    label4.Text = "Khoa: " + reader["Khoa"]?.ToString();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải thông tin sinh viên:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapComboHocKy()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("-- Tất cả học kỳ --");
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                // Chỉ lấy HK mà SV có đăng ký
                var cmd = new SqlCommand(@"
                    SELECT DISTINCT hk.MaHK, hk.TenHK
                    FROM HocKy hk
                    JOIN LopHocPhan lhp ON lhp.MaHK = hk.MaHK
                    JOIN DangKyHocPhan dk ON dk.MaLHP = lhp.MaLHP
                    WHERE dk.MaSV = @MaSV
                    ORDER BY hk.MaHK DESC", conn);
                cmd.Parameters.AddWithValue("@MaSV", _maSV);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    comboBox1.Items.Add(new BDComboHKItem(
                        reader["MaHK"].ToString()!,
                        reader["TenHK"].ToString()!));
            }
            catch { }

            comboBox1.SelectedIndex = 0;
        }

        private void TaiBangDiem()
        {
            string? maHK = comboBox1.SelectedItem is BDComboHKItem hk ? hk.MaHK : null;

            try
            {
                _dtDiem = _db.ExecuteSP("sp_LayBangDiem",
                    DatabaseHelper.Param("@MaSV", _maSV),
                    DatabaseHelper.Param("@MaHK", (object?)maHK ?? DBNull.Value)
                );

                // Xóa cột Designer, dùng auto-generate từ DataTable
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = _dtDiem;

                // Header thân thiện
                SetHeader("MaMon", "Mã MH");
                SetHeader("TenMon", "Môn Học");
                SetHeader("SoTinChi", "TC");
                SetHeader("TenHK", "Học Kỳ");
                SetHeader("DiemCC", "CC");
                SetHeader("DiemGK", "GK");
                SetHeader("DiemCK", "CK");
                SetHeader("DiemTB", "DTB");
                SetHeader("DiemGPA", "GPA");
                SetHeader("XepLoai", "Xếp Loại");
                SetHeader("KetQua", "Kết Quả");

                // Ẩn cột phụ
                HideCol("MaHK");

                // Tô màu kết quả
                dataGridView1.CellFormatting += DataGridView1_CellFormatting;

                // Cập nhật thống kê phía dưới (nếu cần)
                CapNhatThongKeTichLuy();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải bảng điểm:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count) return;

            var row = dataGridView1.Rows[e.RowIndex];
            string kq = row.Cells["KetQua"]?.Value?.ToString() ?? "";

            if (kq == "KhongDat")
            {
                row.DefaultCellStyle.ForeColor = Color.Red;
            }
            else if (kq == "Dat")
            {
                row.DefaultCellStyle.ForeColor = Color.FromArgb(0, 128, 0); // Dark green
            }
        }

        private void CapNhatThongKeTichLuy()
        {
            // Nếu Designer có label hiển thị GPA tích lũy, cập nhật ở đây
            // (Dùng vw_GPATichLuy)
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT ISNULL(GPATichLuy,0) AS GPA, ISNULL(TongTinChi,0) AS TC FROM vw_GPATichLuy WHERE MaSV=@MaSV",
                    conn);
                cmd.Parameters.AddWithValue("@MaSV", _maSV);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Cập nhật label tích lũy nếu có trong Designer
                    // label5.Text = "GPA Tích Lũy: " + reader["GPA"].ToString();
                    // label6.Text = "TC Tích Lũy: "  + reader["TC"].ToString();
                }
            }
            catch { }
        }

        private void btnInBangDiem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng In Bảng Điểm đang phát triển.",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXuatPDF_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng Xuất PDF đang phát triển.",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // button3 – Quay Lại Menu (wired in Designer)
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }

    internal class BDComboHKItem
    {
        public string MaHK { get; }
        public string TenHK { get; }
        public BDComboHKItem(string maHK, string tenHK) { MaHK = maHK; TenHK = tenHK; }
        public override string ToString() => TenHK;
    }
}