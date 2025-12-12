using Npgsql;

namespace _Responsi2_Junpro.Repositories
{
    public class DatabaseConnection
    {
        // ENCAPSULATION: Private field untuk connection string
        private static string connectionString =
            "Host=localhost;Port=5432;Database=responsi;Username=postgres;Password=informatika";

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}
