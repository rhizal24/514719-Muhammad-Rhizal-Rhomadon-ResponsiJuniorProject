namespace Responsi2_Junpro.Models
{
    /// <summary>
    /// INHERITANCE: Developer mewarisi dari BaseModel
    /// ENCAPSULATION: Semua field private dengan getter/setter
    /// Menggunakan Skor dan TotalGaji class untuk perhitungan (Abstraction, Polymorphism)
    /// </summary>
    public class Developer : BaseModel
    {
        // ENCAPSULATION: Private fields
        private string _namaDeveloper = string.Empty;
        private string _namaProyek = string.Empty;
        private string _statusKontrak = string.Empty;
        private int _fiturSelesai;
        private int _jumlahBug;
        private double _skorTotal;
        private decimal _totalGaji;

        // ENCAPSULATION: Public properties dengan validasi
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

        // Constructor default
        public Developer()
        {
            _namaDeveloper = string.Empty;
            _namaProyek = string.Empty;
            _statusKontrak = string.Empty;
            _fiturSelesai = 0;
            _jumlahBug = 0;
            _skorTotal = 0;
            _totalGaji = 0;
        }

        // Constructor dengan parameter
        public Developer(string namaDeveloper, string namaProyek, string statusKontrak, int fiturSelesai, int jumlahBug)
        {
            NamaDeveloper = namaDeveloper;
            NamaProyek = namaProyek;
            StatusKontrak = statusKontrak;
            FiturSelesai = fiturSelesai;
            JumlahBug = jumlahBug;
            HitungSkorDanGaji();
        }

        /// <summary>
        /// Menghitung skor dan gaji menggunakan class Skor dan TotalGaji
        /// POLYMORPHISM: Menggunakan factory pattern untuk membuat instance yang sesuai
        /// berdasarkan status kontrak (Full Time atau Freelance)
        /// </summary>
        public void HitungSkorDanGaji()
        {
            // POLYMORPHISM: Menggunakan SkorFactory untuk membuat instance Skor yang sesuai
            // Full Time: Skor = 10 × Fitur - 5 × Bug
            // Freelance: Skor = 100 × (1 - ((2×Bug)/(3×Fitur)))
            Skor skorCalculator = SkorFactory.Create(StatusKontrak, FiturSelesai, JumlahBug);
            _skorTotal = skorCalculator.HitungSkor();

            // POLYMORPHISM: Menggunakan TotalGajiFactory untuk membuat instance TotalGaji yang sesuai
            // Full Time: Gaji Pokok (5jt) + Skor × 20ribu
            // Freelance: Berdasarkan skor (>=80: 500rb, >=50: 400rb, <50: 350rb) × Fitur
            TotalGaji gajiCalculator = TotalGajiFactory.Create(StatusKontrak, FiturSelesai, _skorTotal);
            _totalGaji = gajiCalculator.HitungGaji();
        }

        // INHERITANCE: Override method abstract dari BaseModel
        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(NamaDeveloper) &&
                   !string.IsNullOrWhiteSpace(NamaProyek) &&
                   !string.IsNullOrWhiteSpace(StatusKontrak);
        }
    }
}
