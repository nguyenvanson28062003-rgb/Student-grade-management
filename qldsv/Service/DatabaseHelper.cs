
using Microsoft.Data.SqlClient;
using System.Data;

namespace qldsv.Service
{
    public class DatabaseHelper
    {
        private const string CONNECTION_STRING =
            "Server=localhost;" +
            "Database=QL_DSV;" +
            "Trusted_Connection=True;" +
            "TrustServerCertificate=True;";

        // Singleton: toàn bộ ứng dụng dùng chung 1 instance
        private static DatabaseHelper? _instance;
        public static DatabaseHelper Instance =>
            _instance ??= new DatabaseHelper();

        private DatabaseHelper() { }

        //  Tạo kết nối mới (gọi .Open() trước khi dùng)
        public SqlConnection GetConnection()
        {
            return new SqlConnection(CONNECTION_STRING);
        }

        //  Kiểm tra kết nối database có thông không
        //  Dùng khi khởi động app để báo lỗi sớm
        public bool KiemTraKetNoi()
        {
            try
            {
                using var conn = GetConnection();
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //  CÁC PHƯƠNG THỨC TIỆN ÍCH DÙNG CHUNG
        public DataTable ExecuteSP(string tenSP, params SqlParameter[] parameters)
        {
            var dt = new DataTable();
            using var conn = GetConnection();
            using var cmd = new SqlCommand(tenSP, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            conn.Open();
            using var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        public DataSet ExecuteSPMultiResult(string tenSP, params SqlParameter[] parameters)
        {
            var ds = new DataSet();
            using var conn = GetConnection();
            using var cmd = new SqlCommand(tenSP, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            conn.Open();
            using var da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }

        public Dictionary<string, object?> ExecuteSPSingleRow(string tenSP, params SqlParameter[] parameters)
        {
            var result = new Dictionary<string, object?>();
            using var conn = GetConnection();
            using var cmd = new SqlCommand(tenSP, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    result[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                }
            }
            return result;
        }
        //  Helper tạo SqlParameter nhanh
        public static SqlParameter Param(string name, object? value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }
    }
}
