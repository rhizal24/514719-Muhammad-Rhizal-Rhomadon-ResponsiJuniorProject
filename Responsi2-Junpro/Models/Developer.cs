namespace Responsi2_Junpro.Models
{
    // Developer mewarisi dari BaseModel (Inheritance)
    public class Developer : BaseModel
    {
        // Private fields (Encapsulation)
        private string _namaDeveloper = string.Empty;
        private string _namaProyek = string.Empty;
        private string _statusKontrak = string.Empty;
        private int _fiturSelesai;
        private int _jumlahBug;
        private double _skorTotal;
        private decimal _totalGaji;

        // Properties
        public string NamaDeveloper
        {
            get { return _namaDeveloper; }
            set { _namaDeveloper = value ?? string.Empty; }
        }

        public string NamaProyek
        {
            get { return _namaProyek; }
            set { _namaProyek = value ?? string.Empty; }
        }

        public string StatusKontrak
        {
            get { return _statusKontrak; }
            set { _statusKontrak = value ?? string.Empty; }
        }

        public int FiturSelesai
        {
            get { return _fiturSelesai; }
            set { _fiturSelesai = value >= 0 ? value : 0; }
        }

        public int JumlahBug
        {
            get { return _jumlahBug; }
            set { _jumlahBug = value >= 0 ? value : 0; }
        }

        public double SkorTotal
        {
            get { return _skorTotal; }
            private set { _skorTotal = value; }
        }

        public decimal TotalGaji
        {
            get { return _totalGaji; }
            private set { _totalGaji = value; }
        }

        public Developer() { }

        public Developer(string nama, string proyek, string status, int fitur, int bug)
        {
            NamaDeveloper = nama;
            NamaProyek = proyek;
            StatusKontrak = status;
            FiturSelesai = fitur;
            JumlahBug = bug;
            HitungSkorDanGaji();
        }

        // Hitung skor dan gaji menggunakan class Skor dan TotalGaji (Polymorphism)
        public void HitungSkorDanGaji()
        {
            // Pakai factory untuk buat calculator sesuai status kontrak
            Skor skorCalc = SkorFactory.Create(StatusKontrak, FiturSelesai, JumlahBug);
            _skorTotal = skorCalc.HitungSkor();

            TotalGaji gajiCalc = TotalGajiFactory.Create(StatusKontrak, FiturSelesai, _skorTotal);
            _totalGaji = gajiCalc.HitungGaji();
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(NamaDeveloper) &&
                   !string.IsNullOrWhiteSpace(NamaProyek) &&
                   !string.IsNullOrWhiteSpace(StatusKontrak);
        }
    }
}
