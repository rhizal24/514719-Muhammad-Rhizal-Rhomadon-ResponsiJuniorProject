namespace Responsi2_Junpro.Models
{
    /// <summary>
    /// ABSTRACTION: Abstract class untuk perhitungan Skor
    /// Menyembunyikan detail implementasi perhitungan skor
    /// </summary>
    public abstract class Skor
    {
        // ENCAPSULATION: Protected fields untuk diakses child class
        protected int _fiturSelesai;
        protected int _jumlahBug;
        protected double _nilaiSkor;

        // Properties
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

        public double NilaiSkor
        {
            get { return _nilaiSkor; }
            protected set { _nilaiSkor = value < 0 ? 0 : value; }
        }

        // Constructor
        protected Skor(int fiturSelesai, int jumlahBug)
        {
            FiturSelesai = fiturSelesai;
            JumlahBug = jumlahBug;
        }

        /// <summary>
        /// ABSTRACTION: Method abstract untuk menghitung skor
        /// Implementasi berbeda di setiap child class (Polymorphism)
        /// </summary>
        public abstract double HitungSkor();
    }

    /// <summary>
    /// INHERITANCE: SkorFullTime mewarisi dari Skor
    /// POLYMORPHISM: Override method HitungSkor() dengan rumus Full Time
    /// Rumus: Skor = 10 × Fitur - 5 × Bug
    /// </summary>
    public class SkorFullTime : Skor
    {
        public SkorFullTime(int fiturSelesai, int jumlahBug) : base(fiturSelesai, jumlahBug)
        {
        }

        /// <summary>
        /// POLYMORPHISM: Implementasi perhitungan skor untuk Full Time
        /// Rumus: Skor = 10 × Fitur - 5 × Bug (min 0)
        /// </summary>
        public override double HitungSkor()
        {
            double skor = (10 * FiturSelesai) - (5 * JumlahBug);
            NilaiSkor = skor < 0 ? 0 : skor;
            return NilaiSkor;
        }
    }

    /// <summary>
    /// INHERITANCE: SkorFreelance mewarisi dari Skor
    /// POLYMORPHISM: Override method HitungSkor() dengan rumus Freelance
    /// Rumus: Skor = 100 × (1 - ((2×Bug)/(3×Fitur)))
    /// </summary>
    public class SkorFreelance : Skor
    {
        public SkorFreelance(int fiturSelesai, int jumlahBug) : base(fiturSelesai, jumlahBug)
        {
        }

        /// <summary>
        /// POLYMORPHISM: Implementasi perhitungan skor untuk Freelance
        /// Rumus: Skor = 100 × (1 - ((2×Bug)/(3×Fitur)))
        /// Skor bisa lebih dari 100, tapi tidak kurang dari 0
        /// </summary>
        public override double HitungSkor()
        {
            // Jika fitur = 0, skor = 0 (hindari division by zero)
            if (FiturSelesai == 0)
            {
                NilaiSkor = 0;
                return NilaiSkor;
            }

            double skor = 100 * (1 - ((2.0 * JumlahBug) / (3.0 * FiturSelesai)));
            NilaiSkor = skor < 0 ? 0 : skor;
            return NilaiSkor;
        }
    }

    /// <summary>
    /// Factory class untuk membuat instance Skor berdasarkan status kontrak
    /// </summary>
    public static class SkorFactory
    {
        public static Skor Create(string statusKontrak, int fiturSelesai, int jumlahBug)
        {
            return statusKontrak switch
            {
                "Full Time" => new SkorFullTime(fiturSelesai, jumlahBug),
                "Freelance" => new SkorFreelance(fiturSelesai, jumlahBug),
                _ => new SkorFullTime(fiturSelesai, jumlahBug) // Default Full Time
            };
        }
    }
}
