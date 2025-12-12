namespace Responsi2_Junpro.Models
{
    /// <summary>
    /// ABSTRACTION: Abstract class untuk perhitungan Total Gaji
    /// Menyembunyikan detail implementasi perhitungan gaji
    /// </summary>
    public abstract class TotalGaji
    {
        // ENCAPSULATION: Protected fields untuk diakses child class
        protected int _fiturSelesai;
        protected double _skor;
        protected decimal _gajiPokok;
        protected decimal _nilaiGaji;

        // Properties
        public int FiturSelesai
        {
            get { return _fiturSelesai; }
            set { _fiturSelesai = value >= 0 ? value : 0; }
        }

        public double Skor
        {
            get { return _skor; }
            set { _skor = value >= 0 ? value : 0; }
        }

        public decimal GajiPokok
        {
            get { return _gajiPokok; }
            protected set { _gajiPokok = value; }
        }

        public decimal NilaiGaji
        {
            get { return _nilaiGaji; }
            protected set { _nilaiGaji = value; }
        }

        // Constructor
        protected TotalGaji(int fiturSelesai, double skor)
        {
            FiturSelesai = fiturSelesai;
            Skor = skor;
        }

        /// <summary>
        /// ABSTRACTION: Method abstract untuk menghitung total gaji
        /// Implementasi berbeda di setiap child class (Polymorphism)
        /// </summary>
        public abstract decimal HitungGaji();
    }

    /// <summary>
    /// INHERITANCE: TotalGajiFullTime mewarisi dari TotalGaji
    /// POLYMORPHISM: Override method HitungGaji() dengan rumus Full Time
    /// Rumus: Total Gaji = Gaji Pokok (5jt) + Skor × 20ribu
    /// </summary>
    public class TotalGajiFullTime : TotalGaji
    {
        private const decimal GAJI_POKOK_FULLTIME = 5000000m;
        private const decimal BONUS_PER_SKOR = 20000m;

        public TotalGajiFullTime(int fiturSelesai, double skor) : base(fiturSelesai, skor)
        {
            GajiPokok = GAJI_POKOK_FULLTIME;
        }

        /// <summary>
        /// POLYMORPHISM: Implementasi perhitungan gaji untuk Full Time
        /// Rumus: Total Gaji = Gaji Pokok (5jt) + Skor × 20ribu
        /// </summary>
        public override decimal HitungGaji()
        {
            decimal bonus = (decimal)Skor * BONUS_PER_SKOR;
            NilaiGaji = GajiPokok + bonus;
            return NilaiGaji;
        }
    }

    /// <summary>
    /// INHERITANCE: TotalGajiFreelance mewarisi dari TotalGaji
    /// POLYMORPHISM: Override method HitungGaji() dengan rumus Freelance
    /// Tidak ada gaji pokok, berdasarkan skor dan fitur
    /// </summary>
    public class TotalGajiFreelance : TotalGaji
    {
        private const decimal TARIF_SKOR_TINGGI = 500000m;   // Skor >= 80
        private const decimal TARIF_SKOR_SEDANG = 400000m;   // 50 <= Skor < 80
        private const decimal TARIF_SKOR_RENDAH = 350000m;   // Skor < 50

        public TotalGajiFreelance(int fiturSelesai, double skor) : base(fiturSelesai, skor)
        {
            GajiPokok = 0m; // Freelance tidak punya gaji pokok
        }

        /// <summary>
        /// POLYMORPHISM: Implementasi perhitungan gaji untuk Freelance
        /// - Skor >= 80: 500ribu × Fitur
        /// - 50 <= Skor < 80: 400ribu × Fitur
        /// - Skor < 50: 350ribu × Fitur
        /// </summary>
        public override decimal HitungGaji()
        {
            decimal tarifPerFitur;

            if (Skor >= 80)
            {
                tarifPerFitur = TARIF_SKOR_TINGGI;
            }
            else if (Skor >= 50)
            {
                tarifPerFitur = TARIF_SKOR_SEDANG;
            }
            else
            {
                tarifPerFitur = TARIF_SKOR_RENDAH;
            }

            NilaiGaji = tarifPerFitur * FiturSelesai;
            return NilaiGaji;
        }
    }

    /// <summary>
    /// Factory class untuk membuat instance TotalGaji berdasarkan status kontrak
    /// </summary>
    public static class TotalGajiFactory
    {
        public static TotalGaji Create(string statusKontrak, int fiturSelesai, double skor)
        {
            return statusKontrak switch
            {
                "Full Time" => new TotalGajiFullTime(fiturSelesai, skor),
                "Freelance" => new TotalGajiFreelance(fiturSelesai, skor),
                _ => new TotalGajiFullTime(fiturSelesai, skor) // Default Full Time
            };
        }
    }
}
