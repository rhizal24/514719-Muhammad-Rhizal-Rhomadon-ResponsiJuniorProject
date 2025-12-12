using Npgsql;
using Responsi2_Junpro.Models;
using _Responsi2_Junpro.Repositories;

namespace Responsi2_Junpro.Repositories
{
    public class DatabaseRepository : IDatabaseRepository
    {
        // INSERT developer baru
        public bool Insert(Developer developer)
        {
            if (!developer.IsValid()) return false;

            developer.HitungSkorDanGaji();

            // Cek budget dulu
            if (!ValidateBudget(developer.NamaProyek, developer.TotalGaji))
            {
                MessageBox.Show($"Budget proyek tidak cukup untuk gaji Rp {developer.TotalGaji:N0}", 
                    "Budget Tidak Cukup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using var conn = DatabaseConnection.GetConnection();
                conn.Open();
                
                string query = "SELECT insert_developer(@nama, @status, @fitur, @bug, @proyek)";
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nama", developer.NamaDeveloper);
                cmd.Parameters.AddWithValue("@status", developer.StatusKontrak);
                cmd.Parameters.AddWithValue("@fitur", developer.FiturSelesai);
                cmd.Parameters.AddWithValue("@bug", developer.JumlahBug);
                cmd.Parameters.AddWithValue("@proyek", developer.NamaProyek);

                var result = cmd.ExecuteScalar();
                return result != null && Convert.ToInt32(result) > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error insert: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // GET semua developer
        public List<Developer> GetAll()
        {
            var list = new List<Developer>();

            try
            {
                using var conn = DatabaseConnection.GetConnection();
                conn.Open();
                
                string query = "SELECT * FROM get_all_developers()";
                using var cmd = new NpgsqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var dev = new Developer
                    {
                        Id = reader.GetInt32(0),
                        NamaDeveloper = reader.GetString(1),
                        NamaProyek = reader.GetString(2),
                        StatusKontrak = reader.GetString(3),
                        FiturSelesai = reader.GetInt32(4),
                        JumlahBug = reader.GetInt32(5)
                    };
                    dev.HitungSkorDanGaji();
                    list.Add(dev);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error get data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return list;
        }

        // GET developer by id
        public Developer? GetById(int id)
        {
            try
            {
                using var conn = DatabaseConnection.GetConnection();
                conn.Open();
                
                string query = "SELECT * FROM get_developer_by_id(@id)";
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    var dev = new Developer
                    {
                        Id = reader.GetInt32(0),
                        NamaDeveloper = reader.GetString(1),
                        NamaProyek = reader.GetString(2),
                        StatusKontrak = reader.GetString(3),
                        FiturSelesai = reader.GetInt32(4),
                        JumlahBug = reader.GetInt32(5)
                    };
                    dev.HitungSkorDanGaji();
                    return dev;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error get data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        // GET semua proyek
        public List<Proyek> GetAllProyek()
        {
            var list = new List<Proyek>();

            try
            {
                using var conn = DatabaseConnection.GetConnection();
                conn.Open();
                
                string query = "SELECT * FROM get_all_proyek()";
                using var cmd = new NpgsqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Proyek(
                        reader.GetInt32(0), 
                        reader.GetString(1),
                        reader.GetDecimal(2)
                    ));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error get proyek: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return list;
        }

        // UPDATE developer
        public bool Update(Developer developer)
        {
            if (!developer.IsValid() || developer.Id <= 0) return false;

            developer.HitungSkorDanGaji();

            if (!ValidateBudget(developer.NamaProyek, developer.TotalGaji, developer.Id))
            {
                MessageBox.Show($"Budget proyek tidak cukup untuk gaji Rp {developer.TotalGaji:N0}", 
                    "Budget Tidak Cukup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using var conn = DatabaseConnection.GetConnection();
                conn.Open();
                
                string query = "SELECT update_developer(@id, @nama, @status, @fitur, @bug, @proyek)";
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", developer.Id);
                cmd.Parameters.AddWithValue("@nama", developer.NamaDeveloper);
                cmd.Parameters.AddWithValue("@status", developer.StatusKontrak);
                cmd.Parameters.AddWithValue("@fitur", developer.FiturSelesai);
                cmd.Parameters.AddWithValue("@bug", developer.JumlahBug);
                cmd.Parameters.AddWithValue("@proyek", developer.NamaProyek);

                var result = cmd.ExecuteScalar();
                return result != null && Convert.ToBoolean(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error update: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // DELETE developer
        public bool Delete(int id)
        {
            if (id <= 0) return false;

            try
            {
                using var conn = DatabaseConnection.GetConnection();
                conn.Open();
                
                string query = "SELECT delete_developer(@id)";
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                var result = cmd.ExecuteScalar();
                return result != null && Convert.ToBoolean(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error delete: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Ambil budget proyek
        public decimal GetBudgetProyek(string namaProyek)
        {
            try
            {
                using var conn = DatabaseConnection.GetConnection();
                conn.Open();
                
                string query = "SELECT get_budget_proyek(@nama)";
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nama", namaProyek);

                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToDecimal(result) : 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error get budget: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        // Hitung total pengeluaran proyek
        public decimal GetTotalPengeluaranProyek(string namaProyek, int? excludeDevId = null)
        {
            decimal total = 0;

            try
            {
                using var conn = DatabaseConnection.GetConnection();
                conn.Open();
                
                string query = "SELECT * FROM get_total_pengeluaran_proyek(@nama)";
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nama", namaProyek);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string status = reader.GetString(1);
                    int fitur = reader.GetInt32(2);
                    int bug = reader.GetInt32(3);

                    // Hitung skor dan gaji pakai class polymorphism
                    var skorCalc = SkorFactory.Create(status, fitur, bug);
                    double skor = skorCalc.HitungSkor();

                    var gajiCalc = TotalGajiFactory.Create(status, fitur, skor);
                    total += gajiCalc.HitungGaji();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error hitung pengeluaran: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Exclude dev tertentu kalo lagi update
            if (excludeDevId.HasValue)
            {
                var dev = GetById(excludeDevId.Value);
                if (dev != null && dev.NamaProyek == namaProyek)
                    total -= dev.TotalGaji;
            }

            return total;
        }

        public decimal GetTotalPengeluaranProyek(string namaProyek)
        {
            return GetTotalPengeluaranProyek(namaProyek, null);
        }

        // Validasi budget cukup atau tidak
        public bool ValidateBudget(string namaProyek, decimal gajiBaru, int? excludeDevId = null)
        {
            decimal budget = GetBudgetProyek(namaProyek);
            decimal totalPengeluaran = GetTotalPengeluaranProyek(namaProyek, excludeDevId);
            return (budget - totalPengeluaran) >= gajiBaru;
        }
    }
}
