namespace Responsi2_Junpro.Models
{
    /// <summary>
    /// INHERITANCE: Proyek mewarisi dari BaseModel
    /// Model untuk data proyek yang digunakan di dropdown
    /// </summary>
    public class Proyek : BaseModel
    {
        // ENCAPSULATION: Private fields
        private string _namaProyek = string.Empty;
        private decimal _budget;

        public string NamaProyek
        {
            get { return _namaProyek; }
            set { _namaProyek = value ?? string.Empty; }
        }

        public decimal Budget
        {
            get { return _budget; }
            set { _budget = value >= 0 ? value : 0; }
        }

        public Proyek()
        {
            _namaProyek = string.Empty;
            _budget = 0;
        }

        public Proyek(int id, string namaProyek)
        {
            Id = id;
            NamaProyek = namaProyek;
            Budget = 0;
        }

        public Proyek(int id, string namaProyek, decimal budget)
        {
            Id = id;
            NamaProyek = namaProyek;
            Budget = budget;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(NamaProyek);
        }

        public override string ToString()
        {
            return NamaProyek;
        }
    }
}
