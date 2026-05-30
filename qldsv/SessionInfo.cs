namespace qldsv
{
    public static class SessionInfo
    {
        public static int MaND { get; set; }
        public static string TenDangNhap { get; set; } = "";
        public static string VaiTro { get; set; } = "";

        // Dữ liệu mở rộng
        public static string MaSV { get; set; } = "";   // nếu là sinh viên
        public static string MaGV { get; set; } = "";   // nếu là giảng viên
        public static string HoTen { get; set; } = "";

        public static void XoaSession()
        {
            MaND = 0;
            TenDangNhap = "";
            VaiTro = "";
            MaSV = "";
            MaGV = "";
            HoTen = "";
        }
    }
}
