#nullable disable
using Microsoft.Data.SqlClient;
using System.Data;
using qldsv.Service;

namespace qldsv
{
    public partial class MonHoc : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;
        private DataTable _dtMonHoc = new DataTable();

        public MonHoc()
        {
            InitializeComponent();
            ThemeApplier.Apply(this);
            this.Load += MonHoc_Load;

            button7.Click += btnThemMon_Click;
            button1.Click += btnSua_Click;
            button3.Click += btnXoa_Click;
            button4.Click += btnExport_Click;
            button5.Click += btnQuayLai_Click;
            button2.Click += btnThoat_Click;
            button8.Click += btnTimKiem_Click;

            comboBox1.SelectedIndexChanged += (s, e) => btnTimKiem_Click(s, e);
        }

        private void MonHoc_Load(object sender, EventArgs e)
        {
            NapComboKhoa();
            TaiDanhSachMonHoc();
        }

        private void NapComboKhoa()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add(new MHKhoaItem("", "-- Tất cả --"));
            try
            {
                using var conn = _db.GetConnection(); conn.Open();
                var cmd = new SqlCommand("SELECT MaKhoa, TenKhoa FROM Khoa ORDER BY TenKhoa", conn);
                using var r = cmd.ExecuteReader();
                while (r.Read())
                    comboBox1.Items.Add(new MHKhoaItem(r["MaKhoa"].ToString(), r["TenKhoa"].ToString()));
            }
            catch { }
            comboBox1.SelectedIndex = 0;
        }

        private void TaiDanhSachMonHoc(string tuKhoa = "", string maKhoa = "")
        {
            try
            {
                string sql = @"
                    SELECT
                        mh.MaMH,
                        mh.TenMH,
                        mh.SoTinChi,
                        mh.LoaiMon,
                        k.TenKhoa AS Khoa,
                        mh.HKApDung,
                        mh.TrangThai
                    FROM MonHoc mh
                    JOIN Khoa k ON mh.MaKhoa = k.MaKhoa
                    WHERE 1=1
                      AND (@TuKhoa  = '' OR mh.MaMH  LIKE '%'+@TuKhoa+'%'
                                        OR mh.TenMH  LIKE '%'+@TuKhoa+'%')
                      AND (@MaKhoa  = '' OR mh.MaKhoa = @MaKhoa)
                    ORDER BY mh.MaMH";

                using var conn = _db.GetConnection();
                conn.Open();
                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TuKhoa", tuKhoa);
                cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);

                using var da = new SqlDataAdapter(cmd);
                _dtMonHoc = new DataTable();
                da.Fill(_dtMonHoc);

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = _dtMonHoc;

                SetHeader("MaMH", "Mã Môn");
                SetHeader("TenMH", "Môn Học");
                SetHeader("SoTinChi", "Tín Chỉ");
                SetHeader("LoaiMon", "Loại");
                SetHeader("Khoa", "Khoa");
                SetHeader("HKApDung", "HK Áp Dụng");
                SetHeader("TrangThai", "Trạng Thái");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải danh sách môn học:\n" + ex.Message,
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
            string tuKhoa = comboBox2.Text.Trim();
            string maKhoa = comboBox1.SelectedItem is MHKhoaItem ki && ki.MaKhoa != "" ? ki.MaKhoa : "";
            TaiDanhSachMonHoc(tuKhoa, maKhoa);
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            new ThemSuaMH(null).ShowDialog();
            TaiDanhSachMonHoc();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            string maMH = dataGridView1.CurrentRow.Cells["MaMH"].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(maMH)) return;

            new ThemSuaMH(maMH).ShowDialog();
            TaiDanhSachMonHoc();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            string maMH = dataGridView1.CurrentRow.Cells["MaMH"].Value?.ToString() ?? "";
            string tenMH = dataGridView1.CurrentRow.Cells["TenMH"].Value?.ToString() ?? "";

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa môn [{maMH}] {tenMH}?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                // Kiểm tra xem môn đã có lớp học phần chưa
                var chk = new SqlCommand("SELECT COUNT(*) FROM LopHocPhan WHERE MaMH=@MaMH", conn);
                chk.Parameters.AddWithValue("@MaMH", maMH);
                if ((int)chk.ExecuteScalar() > 0)
                {
                    MessageBox.Show("Không thể xóa môn học đã có lớp học phần.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xóa môn tiên quyết liên quan trước
                new SqlCommand("DELETE FROM MonHocTienQuyet WHERE MaMH=@MaMH OR MaMHTienQuyet=@MaMH", conn)
                { Parameters = { new("@MaMH", maMH) } }.ExecuteNonQuery();

                var cmd = new SqlCommand("DELETE FROM MonHoc WHERE MaMH=@MaMH", conn);
                cmd.Parameters.AddWithValue("@MaMH", maMH);
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachMonHoc();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không thể xóa môn học:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng Export đang phát triển.",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnQuayLai_Click(object sender, EventArgs e) => this.Close();
        private void btnThoat_Click(object sender, EventArgs e) => Application.Exit();
    }

    internal class MHKhoaItem
    {
        public string MaKhoa { get; }
        private readonly string _ten;
        public MHKhoaItem(string maKhoa, string ten) { MaKhoa = maKhoa; _ten = ten; }
        public override string ToString() => _ten;
    }
}
