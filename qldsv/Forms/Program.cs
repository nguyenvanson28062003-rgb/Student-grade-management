using quản_lí_điểm_sinh_viên;
namespace qldsv
{
    internal static class Program
    {

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var db = qldsv.Service.DatabaseHelper.Instance;
            if (! db.KiemTraKetNoi())
                MessageBox.Show("Kết nối thất bại! Kiểm tra kết nối sever");
            Application.Run(new DangNhap());
        }
    }
}