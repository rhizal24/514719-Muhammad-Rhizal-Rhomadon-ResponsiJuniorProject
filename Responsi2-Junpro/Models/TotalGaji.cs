namespace Responsi2_Junpro.Models
{
    // Abstract class untuk hitung gaji (Abstraction)
    public abstract class TotalGaji
    {
        protected int Fitur;
        protected double Skor;

        public TotalGaji(int fitur, double skor)
        {
            Fitur = fitur;
            Skor = skor;
        }

        public abstract decimal HitungGaji();
    }

    // Child class untuk Full Time (Polymorphism)
    public class TotalGajiFullTime : TotalGaji
    {
        public TotalGajiFullTime(int fitur, double skor) : base(fitur, skor) { }

        public override decimal HitungGaji()
        {
            // Rumus: Gaji Pokok (5jt) + Skor x 20ribu
            return 5000000 + (decimal)(Skor * 20000);
        }
    }

    // Child class untuk Freelance (Polymorphism)
    public class TotalGajiFreelance : TotalGaji
    {
        public TotalGajiFreelance(int fitur, double skor) : base(fitur, skor) { }

        public override decimal HitungGaji()
        {
            // Tarif berdasarkan skor
            decimal tarif;
            if (Skor >= 80)
                tarif = 500000;
            else if (Skor >= 50)
                tarif = 400000;
            else
                tarif = 350000;

            return tarif * Fitur;
        }
    }

    // Factory untuk create instance sesuai status
    public static class TotalGajiFactory
    {
        public static TotalGaji Create(string status, int fitur, double skor)
        {
            if (status == "Full Time")
                return new TotalGajiFullTime(fitur, skor);
            else
                return new TotalGajiFreelance(fitur, skor);
        }
    }
}
