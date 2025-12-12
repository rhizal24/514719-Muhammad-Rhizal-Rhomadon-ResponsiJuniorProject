namespace Responsi2_Junpro.Models
{
    /// <summary>
    /// INHERITANCE: Base class untuk semua model
    /// Menyediakan properti umum yang diwarisi oleh class turunan
    /// </summary>
    public abstract class BaseModel
    {
        // ENCAPSULATION: Private field dengan public property
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        // Method abstract yang harus diimplementasikan oleh class turunan
        public abstract bool IsValid();
    }
}
