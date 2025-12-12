using Npgsql;
using Responsi2_Junpro.Models;
using _514719_Rhizal_ResponsiJunpro.Data;

namespace Responsi2_Junpro.Repositories
{
    /// <summary>
    /// Implementasi IDatabaseRepository untuk PostgreSQL
    /// Menggunakan Npgsql untuk koneksi database
    /// </summary>
    public class DatabaseRepository : IDatabaseRepository
    {
        #region CREATE

        public bool Insert(Developer developer)
        {
            if (!developer.IsValid())
                return false;

            developer.HitungSkorDanGaji();

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // Ambil id_proyek berdasarkan nama_proyek
                    string getProyekQuery = "SELECT id_proyek FROM proyek WHERE nama_proyek = @namaProyek";
                    int idProyek = 0;
                    using (var cmdProyek = new NpgsqlCommand(getProyekQuery, conn))
                    {
                        cmdProyek.Parameters.AddWithValue("@namaProyek", developer.NamaProyek);
                        var result = cmdProyek.ExecuteScalar();
                        if (result != null)
                            idProyek = Convert.ToInt32(result);
                    }

                    if (idProyek == 0)
                    {
                        MessageBox.Show("Proyek tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    string query = @"INSERT INTO developer 
                                    (nama_dev, status_kontrak, fitur_selesai, jumlah_bug, id_proyek) 
                                    VALUES (@nama, @status, @fitur, @bug, @idProyek)";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", developer.NamaDeveloper);
                        cmd.Parameters.AddWithValue("@status", developer.StatusKontrak);
                        cmd.Parameters.AddWithValue("@fitur", developer.FiturSelesai);
                        cmd.Parameters.AddWithValue("@bug", developer.JumlahBug);
                        cmd.Parameters.AddWithValue("@idProyek", idProyek);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat insert data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region READ

        public List<Developer> GetAll()
        {
            var developers = new List<Developer>();

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT d.id_dev, d.nama_dev, p.nama_proyek, d.status_kontrak, d.fitur_selesai, d.jumlah_bug 
                                    FROM developer d 
                                    JOIN proyek p ON d.id_proyek = p.id_proyek 
                                    ORDER BY d.id_dev";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
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
                            developers.Add(dev);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat mengambil data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return developers;
        }

        public Developer? GetById(int id)
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT d.id_dev, d.nama_dev, p.nama_proyek, d.status_kontrak, d.fitur_selesai, d.jumlah_bug 
                                    FROM developer d 
                                    JOIN proyek p ON d.id_proyek = p.id_proyek 
                                    WHERE d.id_dev = @id";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (var reader = cmd.ExecuteReader())
                        {
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat mengambil data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public Developer? GetByNama(string namaDeveloper)
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT d.id_dev, d.nama_dev, p.nama_proyek, d.status_kontrak, d.fitur_selesai, d.jumlah_bug 
                                    FROM developer d 
                                    JOIN proyek p ON d.id_proyek = p.id_proyek 
                                    WHERE d.nama_dev = @nama";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", namaDeveloper);

                        using (var reader = cmd.ExecuteReader())
                        {
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat mengambil data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public List<Proyek> GetAllProyek()
        {
            var proyekList = new List<Proyek>();

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT id_proyek, nama_proyek FROM proyek ORDER BY nama_proyek";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            proyekList.Add(new Proyek(reader.GetInt32(0), reader.GetString(1)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat mengambil data proyek: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return proyekList;
        }

        #endregion

        #region UPDATE

        public bool Update(Developer developer)
        {
            if (!developer.IsValid() || developer.Id <= 0)
                return false;

            developer.HitungSkorDanGaji();

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    
                    // Ambil id_proyek berdasarkan nama_proyek
                    string getProyekQuery = "SELECT id_proyek FROM proyek WHERE nama_proyek = @namaProyek";
                    int idProyek = 0;
                    using (var cmdProyek = new NpgsqlCommand(getProyekQuery, conn))
                    {
                        cmdProyek.Parameters.AddWithValue("@namaProyek", developer.NamaProyek);
                        var result = cmdProyek.ExecuteScalar();
                        if (result != null)
                            idProyek = Convert.ToInt32(result);
                    }

                    if (idProyek == 0)
                    {
                        MessageBox.Show("Proyek tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    string query = @"UPDATE developer SET 
                                    nama_dev = @nama, 
                                    status_kontrak = @status, 
                                    fitur_selesai = @fitur, 
                                    jumlah_bug = @bug, 
                                    id_proyek = @idProyek 
                                    WHERE id_dev = @id";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", developer.Id);
                        cmd.Parameters.AddWithValue("@nama", developer.NamaDeveloper);
                        cmd.Parameters.AddWithValue("@status", developer.StatusKontrak);
                        cmd.Parameters.AddWithValue("@fitur", developer.FiturSelesai);
                        cmd.Parameters.AddWithValue("@bug", developer.JumlahBug);
                        cmd.Parameters.AddWithValue("@idProyek", idProyek);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat update data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region DELETE

        public bool Delete(int id)
        {
            if (id <= 0)
                return false;

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM developer WHERE id_dev = @id";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat hapus data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool DeleteByNama(string namaDeveloper)
        {
            if (string.IsNullOrEmpty(namaDeveloper))
                return false;

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM developer WHERE nama_dev = @nama";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", namaDeveloper);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat hapus data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion
    }
}
