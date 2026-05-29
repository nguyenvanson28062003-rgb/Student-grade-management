#nullable disable
// ================================================================
//  NhapDiem.cs  –  Form Nhập Điểm
//  Namespace: quản_lí_điểm_sinh_viên  (khớp với Designer)
// ================================================================

using Microsoft.Data.SqlClient;
using System.Data;
using qldsv;
using qldsv.Service;

namespace quản_lí_điểm_sinh_viên
{
    public partial class NhapDiem : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;
        private string _maLHP = "";
        private DataTable _dtDiem = new DataTable();

        // Nhận MaLHP từ form gọi
        public NhapDiem(string maLHP)
        {
            InitializeComponent();
            _maLHP = maLHP;
            this.Load += NhapDiem_Load;

            button2.Click += btnLuuDiem_Click;    // Lưu điểm
            button3.Click += btnChotDiem_Click;   // Chốt điểm
            button1.Click += btnImport_Click;     // Import Excel
            button4.Click += btnExport_Click;     // Export
            button5.Click += btnThoat_Click;      // Thoát
            button6.Click += btnQuayLai_Click;    // Quay lại Menu
        }

        private void NhapDiem_Load(object sender, EventArgs e)
        {
            TaiThongTinLopVaDiem();
        }

        // --------------------------------------------------------
        //  Tải thông tin LHP + danh sách SV
        // --------------------------------------------------------
        private void TaiThongTinLopVaDiem()
        {
            try
            {
                var ds = _db.ExecuteSPMultiResult("sp_LayDanhSachSVTrongLHP",
                    DatabaseHelper.Param("@MaLHP", _maLHP)
                );

                // Thông tin lớp
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var infoRow = ds.Tables[0].Rows[0];
                    label1.Text = "Mã lớp HP: " + infoRow["MaLHP"];
                    label7.Text = infoRow["MaLHP"]?.ToString() ?? "";
                    label3.Text = "Môn học: " + infoRow["TenMH"];
                    label8.Text = infoRow["TenMH"]?.ToString() ?? "";
                    label4.Text = "Giảng viên: " + infoRow["HoTenGV"];
                    label9.Text = infoRow["HoTenGV"]?.ToString() ?? "";
                    label6.Text = "Học kỳ: " + infoRow["TenHK"];
                    label10.Text = infoRow["TenHK"]?.ToString() ?? "";
                }

                // Danh sách SV + điểm
                if (ds.Tables.Count > 1)
                {
                    _dtDiem = ds.Tables[1];

                    // Xóa các cột designer cũ, dùng auto-generate từ DataTable
                    dataGridView1.AutoGenerateColumns = true;
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = _dtDiem;

                    // Ẩn cột không cần hiển thị
                    HideCol("MaDK"); HideCol("MaDiem"); HideCol("KetQua"); HideCol("NgayNhap");

                    // Đặt ReadOnly cho cột tính toán
                    SetReadOnly("DiemTB", true);
                    SetReadOnly("DiemGPA", true);
                    SetReadOnly("XepLoai", true);

                    // Tên header thân thiện
                    SetHeader("MaSV", "MSV");
                    SetHeader("HoTen", "Họ Tên");
                    SetHeader("DiemCC", "Chuyên Cần");
                    SetHeader("DiemGK", "Giữa Kỳ");
                    SetHeader("DiemCK", "Cuối Kỳ");
                    SetHeader("DiemTB", "DTB");
                    SetHeader("DiemGPA", "GPA");
                    SetHeader("XepLoai", "Xếp Loại");
                    SetHeader("DaChotDiem", "Đã Chốt");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void SetReadOnly(string colName, bool readOnly)
        {
            if (dataGridView1.Columns[colName] != null)
                dataGridView1.Columns[colName].ReadOnly = readOnly;
        }

        // --------------------------------------------------------
        //  Lưu điểm
        // --------------------------------------------------------
        private void btnLuuDiem_Click(object sender, EventArgs e)
        {
            LuuDiem(chotDiem: false);
        }

        // --------------------------------------------------------
        //  Chốt điểm
        // --------------------------------------------------------
        private void btnChotDiem_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Sau khi chốt điểm sẽ KHÔNG thể sửa!\nBạn có chắc muốn chốt?",
                "Xác nhận chốt điểm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
                LuuDiem(chotDiem: true);
        }

        private void LuuDiem(bool chotDiem)
        {
            int loi = 0, thanhCong = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                if (!int.TryParse(row.Cells["MaDK"]?.Value?.ToString(), out int maDK)) continue;

                if (!decimal.TryParse(row.Cells["DiemCC"]?.Value?.ToString(), out decimal diemCC)) diemCC = 0;
                if (!decimal.TryParse(row.Cells["DiemGK"]?.Value?.ToString(), out decimal diemGK)) diemGK = 0;
                if (!decimal.TryParse(row.Cells["DiemCK"]?.Value?.ToString(), out decimal diemCK)) diemCK = 0;

                if (diemCC < 0 || diemCC > 10 || diemGK < 0 || diemGK > 10 || diemCK < 0 || diemCK > 10)
                {
                    MessageBox.Show($"Điểm phải từ 0–10 (dòng: {row.Cells["HoTen"]?.Value})",
                        "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    loi++;
                    continue;
                }

                try
                {
                    var ketQua = _db.ExecuteSPSingleRow("sp_NhapDiem",
                        DatabaseHelper.Param("@MaDK", maDK),
                        DatabaseHelper.Param("@DiemCC", diemCC),
                        DatabaseHelper.Param("@DiemGK", diemGK),
                        DatabaseHelper.Param("@DiemCK", diemCK),
                        DatabaseHelper.Param("@MaGV", SessionInfo.MaGV),
                        DatabaseHelper.Param("@ChotDiem", chotDiem ? 1 : 0),
                        DatabaseHelper.Param("@LyDo", (object?)null)
                    );

                    if (Convert.ToInt32(ketQua["KetQua"]) == 1)
                        thanhCong++;
                    else
                        loi++;
                }
                catch (SqlException)
                {
                    loi++;
                }
            }

            string thongBao = chotDiem
                ? $"Chốt điểm: {thanhCong} sinh viên thành công, {loi} lỗi."
                : $"Lưu điểm: {thanhCong} sinh viên thành công, {loi} lỗi.";

            MessageBox.Show(thongBao, "Kết quả",
                MessageBoxButtons.OK,
                loi > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

            TaiThongTinLopVaDiem();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng Import Excel đang phát triển.");
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng Export đang phát triển.");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}