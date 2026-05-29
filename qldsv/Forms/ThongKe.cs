#nullable disable
using Microsoft.Data.SqlClient;
using System.Data;
using qldsv;
using qldsv.Service;

namespace quản_lí_điểm_sinh_viên
{
    public partial class ThongKe : Form
    {
        private readonly DatabaseHelper _db = DatabaseHelper.Instance;

        public ThongKe()
        {
            InitializeComponent();
            this.Load += ThongKe_Load;

            btnDangnhap.Click += btnXemThongKe_Click;  // Xem Thống Kê
            button1.Click += btnXuatBaoCao_Click;  // Xuất Báo Cáo
            button2.Click += btnQuayLai_Click;     // Quay Lại
            button3.Click += btnThoat_Click;       // Thoát
        }
        private void ThongKe_Load(object sender, EventArgs e)
        {
            NapComboHocKy();
            NapComboKhoa();
            XemThongKe();
        }

        private void NapComboHocKy()
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("-- Tất cả học kỳ --");
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT MaHK, TenHK FROM HocKy ORDER BY MaHK DESC", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    comboBox2.Items.Add(new ComboHKItem(
                        reader["MaHK"].ToString()!,
                        reader["TenHK"].ToString()!));
            }
            catch { }
            comboBox2.SelectedIndex = 0;
        }

        private void NapComboKhoa()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("-- Tất cả khoa --");
            comboBox1.Items.AddRange(new[] {
                "Công Nghệ Thông Tin", "Kinh Tế", "Kỹ Thuật", "Ngoại Ngữ",
                "Khoa Học Tự Nhiên", "Y Dược", "Luật", "Sư Phạm"
            });
            comboBox1.SelectedIndex = 0;
        }
        private void btnXemThongKe_Click(object sender, EventArgs e)
        {
            XemThongKe();
        }

        private void XemThongKe()
        {
            string? maHK = comboBox2.SelectedItem is ComboHKItem hk ? hk.MaHK : null;
            string? khoa = comboBox1.SelectedIndex > 0 ? comboBox1.SelectedItem?.ToString() : null;

            try
            {
                var ds = _db.ExecuteSPMultiResult("sp_ThongKe",
                    DatabaseHelper.Param("@MaHK", (object?)maHK ?? DBNull.Value),
                    DatabaseHelper.Param("@MaKhoa", (object?)khoa ?? DBNull.Value)
                );

                // --- Table[0]: Tổng quan ---
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var row = ds.Tables[0].Rows[0];
                    label4.Text = row["TongSV"]?.ToString() ?? "0";   // Tổng SV
                    label5.Text = row["TongLHP"]?.ToString() ?? "0";   // Lớp HP
                    label6.Text = row["TiLeDat"]?.ToString() ?? "0%";  // Tỉ lệ đạt
                    label7.Text = row["DiemTBChung"]?.ToString() ?? "0"; // DTB chung
                }
                else
                {
                    // Fallback query đơn giản
                    ThongKeDonGian(maHK, khoa);
                }

                // --- Table[1]: Phân bố điểm ---
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    HienThiPhanBoDiem(ds.Tables[1].Rows[0]);
                }

                // --- Table[2] hoặc Table[1]: Tỉ lệ Đạt/Rớt theo môn ---
                var tblMon = ds.Tables.Count > 2 ? ds.Tables[2]
                           : ds.Tables.Count > 1 ? ds.Tables[1]
                           : null;
                if (tblMon != null)
                    HienThiBangMon(tblMon);
            }
            catch
            {
                ThongKeDonGian(maHK, khoa);
            }
        }

        private void ThongKeDonGian(string? maHK, string? khoa)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                string where = "WHERE 1=1";
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                if (!string.IsNullOrEmpty(maHK))
                {
                    where += " AND lhp.MaHK = @MaHK";
                    cmd.Parameters.AddWithValue("@MaHK", maHK);
                }
                if (!string.IsNullOrEmpty(khoa))
                {
                    where += " AND sv.Khoa = @Khoa";
                    cmd.Parameters.AddWithValue("@Khoa", khoa);
                }

                cmd.CommandText = $@"
                    SELECT
                        COUNT(DISTINCT d.MaDiem) AS TongSV,
                        COUNT(DISTINCT lhp.MaLHP) AS TongLHP,
                        CAST(ISNULL(AVG(CAST(d.DiemTB AS FLOAT)),0) AS DECIMAL(4,2)) AS DiemTBChung,
                        CAST(
                            CASE WHEN COUNT(d.MaDiem) = 0 THEN 0
                            ELSE COUNT(CASE WHEN d.KetQua = 'Dat' THEN 1 END) * 100.0 / COUNT(d.MaDiem)
                            END AS DECIMAL(5,1)) AS TiLeDat
                    FROM Diem d
                    JOIN DangKyHocPhan dk ON d.MaDK = dk.MaDK
                    JOIN LopHocPhan lhp   ON dk.MaLHP = lhp.MaLHP
                    JOIN SinhVien sv      ON dk.MaSV = sv.MaSV
                    {where}";

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label4.Text = reader["TongSV"].ToString() ?? "0";
                    label5.Text = reader["TongLHP"].ToString() ?? "0";
                    label6.Text = reader["TiLeDat"].ToString() + "%";
                    label7.Text = reader["DiemTBChung"].ToString() ?? "0";
                }

                reader.Close();

                // Phân bố xếp loại
                cmd.CommandText = $@"
                    SELECT
                        COUNT(CASE WHEN d.XepLoai = 'A'  THEN 1 END) AS SoA,
                        COUNT(CASE WHEN d.XepLoai = 'B+' THEN 1 END) AS SoBPlus,
                        COUNT(CASE WHEN d.XepLoai = 'B'  THEN 1 END) AS SoB,
                        COUNT(CASE WHEN d.XepLoai = 'C+' THEN 1 END) AS SoCPlus,
                        COUNT(CASE WHEN d.XepLoai = 'C'  THEN 1 END) AS SoC,
                        COUNT(CASE WHEN d.XepLoai IN ('D','F') THEN 1 END) AS SoDF,
                        COUNT(*) AS Tong
                    FROM Diem d
                    JOIN DangKyHocPhan dk ON d.MaDK = dk.MaDK
                    JOIN LopHocPhan lhp   ON dk.MaLHP = lhp.MaLHP
                    JOIN SinhVien sv      ON dk.MaSV = sv.MaSV
                    {where}";

                using var reader2 = cmd.ExecuteReader();
                if (reader2.Read())
                {
                    int tong = Convert.ToInt32(reader2["Tong"]);
                    if (tong > 0)
                    {
                        int a = Convert.ToInt32(reader2["SoA"]);
                        int bPlus = Convert.ToInt32(reader2["SoBPlus"]);
                        int b = Convert.ToInt32(reader2["SoB"]);
                        int cPlus = Convert.ToInt32(reader2["SoCPlus"]);
                        int c = Convert.ToInt32(reader2["SoC"]);
                        int df = Convert.ToInt32(reader2["SoDF"]);

                        label8.Text = a.ToString();
                        label9.Text = bPlus.ToString();
                        label10.Text = b.ToString();
                        label11.Text = cPlus.ToString();
                        label12.Text = c.ToString();
                        label13.Text = df.ToString();

                        label14.Text = $"{a * 100 / tong}%";
                        label15.Text = $"{bPlus * 100 / tong}%";
                        label16.Text = $"{b * 100 / tong}%";
                        label17.Text = $"{cPlus * 100 / tong}%";
                        label18.Text = $"{c * 100 / tong}%";
                        label19.Text = $"{df * 100 / tong}%";

                        // Cập nhật chiều cao thanh (panel2-7)
                        CapNhatBars(a, bPlus, b, cPlus, c, df, tong);
                    }
                }
            }
            catch { }

            // Nạp bảng tỉ lệ đạt/rớt theo môn
            NapBangTheoMon(maHK, khoa);
        }

        private void HienThiPhanBoDiem(DataRow row)
        {
            int a = ToInt(row, "SoLuongA");
            int bPlus = ToInt(row, "SoLuongBPlus");
            int b = ToInt(row, "SoLuongB");
            int cPlus = ToInt(row, "SoLuongCPlus");
            int c = ToInt(row, "SoLuongC");
            int df = ToInt(row, "SoLuongDF");
            int tong = a + bPlus + b + cPlus + c + df;

            label8.Text = a.ToString();
            label9.Text = bPlus.ToString();
            label10.Text = b.ToString();
            label11.Text = cPlus.ToString();
            label12.Text = c.ToString();
            label13.Text = df.ToString();

            if (tong > 0)
            {
                label14.Text = $"{a * 100 / tong}%";
                label15.Text = $"{bPlus * 100 / tong}%";
                label16.Text = $"{b * 100 / tong}%";
                label17.Text = $"{cPlus * 100 / tong}%";
                label18.Text = $"{c * 100 / tong}%";
                label19.Text = $"{df * 100 / tong}%";

                CapNhatBars(a, bPlus, b, cPlus, c, df, tong);
            }
        }

        private void CapNhatBars(int a, int bPlus, int b, int cPlus, int c, int df, int tong)
        {
            int maxH = 80; // chiều cao tối đa panel
            var panels = new[] { panel2, panel3, panel4, panel5, panel6, panel7 };
            var vals = new[] { a, bPlus, b, cPlus, c, df };

            for (int i = 0; i < panels.Length; i++)
            {
                int h = tong > 0 ? (int)(vals[i] * maxH / (double)tong) : 0;
                if (h < 4 && vals[i] > 0) h = 4;
                panels[i].Height = h;
            }
        }

        private void HienThiBangMon(DataTable dt)
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = dt;

            SetHeader("TenMon", "Môn Học");
            SetHeader("SoDat", "Đạt");
            SetHeader("SoRot", "Rớt");
            SetHeader("DiemTB", "DTB Lớp");
        }

        private void NapBangTheoMon(string? maHK, string? khoa)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                string where = "WHERE d.DiemCK IS NOT NULL";
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                if (!string.IsNullOrEmpty(maHK))
                {
                    where += " AND lhp.MaHK = @MaHK";
                    cmd.Parameters.AddWithValue("@MaHK", maHK);
                }
                if (!string.IsNullOrEmpty(khoa))
                {
                    where += " AND sv.Khoa = @Khoa";
                    cmd.Parameters.AddWithValue("@Khoa", khoa);
                }

                cmd.CommandText = $@"
                    SELECT
                        mh.TenMon,
                        COUNT(CASE WHEN d.KetQua = 'Dat' THEN 1 END)      AS SoDat,
                        COUNT(CASE WHEN d.KetQua = 'KhongDat' THEN 1 END) AS SoRot,
                        CAST(ISNULL(AVG(CAST(d.DiemTB AS FLOAT)),0) AS DECIMAL(4,2)) AS DiemTB
                    FROM Diem d
                    JOIN DangKyHocPhan dk ON d.MaDK = dk.MaDK
                    JOIN LopHocPhan lhp   ON dk.MaLHP = lhp.MaLHP
                    JOIN MonHoc mh        ON lhp.MaMon = mh.MaMon
                    JOIN SinhVien sv      ON dk.MaSV = sv.MaSV
                    {where}
                    GROUP BY mh.MaMon, mh.TenMon
                    ORDER BY mh.TenMon";

                using var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);

                HienThiBangMon(dt);
            }
            catch { }
        }

        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng Xuất Báo Cáo đang phát triển.",
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

        private void SetHeader(string colName, string header)
        {
            if (dataGridView1.Columns[colName] != null)
                dataGridView1.Columns[colName].HeaderText = header;
        }

        private int ToInt(DataRow row, string col)
        {
            try { return row.Table.Columns.Contains(col) ? Convert.ToInt32(row[col]) : 0; }
            catch { return 0; }
        }
    }

    internal class ComboHKItem
    {
        public string MaHK { get; }
        public string TenHK { get; }
        public ComboHKItem(string maHK, string tenHK) { MaHK = maHK; TenHK = tenHK; }
        public override string ToString() => TenHK;
    }
}