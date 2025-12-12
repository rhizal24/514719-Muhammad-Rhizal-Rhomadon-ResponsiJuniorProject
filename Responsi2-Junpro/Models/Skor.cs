namespace Responsi2_Junpro.Models
{
    // Abstract class untuk hitung skor (Abstraction)
    public abstract class Skor
    {
        protected int Fitur;
        protected int Bug;

        public Skor(int fitur, int bug)
        {
            Fitur = fitur;
            Bug = bug;
        }

        public abstract double HitungSkor();
    }

    // Child class untuk Full Time (Polymorphism)
    public class SkorFullTime : Skor
    {
        public SkorFullTime(int fitur, int bug) : base(fitur, bug) { }

        public override double HitungSkor()
        {
            // Rumus: 10 x Fitur - 5 x Bug
            return (10 * Fitur) - (5 * Bug);
        }
    }

    // Child class untuk Freelance (Polymorphism)
    public class SkorFreelance : Skor
    {
        public SkorFreelance(int fitur, int bug) : base(fitur, bug) { }

        public override double HitungSkor()
        {
            // Rumus: 100 x (1 - (2xBug)/(3xFitur))
            if (Fitur == 0) return 0;
            double ratio = (2.0 * Bug) / (3.0 * Fitur);
            return Math.Round(100 * (1 - ratio), 2);
        }
    }

    // Factory untuk create instance sesuai status
    public static class SkorFactory
    {
        public static Skor Create(string status, int fitur, int bug)
        {
            if (status == "Full Time")
                return new SkorFullTime(fitur, bug);
            else
                return new SkorFreelance(fitur, bug);
        }
    }
}
