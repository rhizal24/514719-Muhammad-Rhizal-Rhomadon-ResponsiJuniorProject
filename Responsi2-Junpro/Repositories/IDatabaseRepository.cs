using Responsi2_Junpro.Models;

namespace Responsi2_Junpro.Repositories
{
    /// <summary>
    /// Interface untuk Repository Pattern
    /// Mendefinisikan kontrak CRUD operations
    /// </summary>
    public interface IDatabaseRepository
    {
        // CREATE
        bool Insert(Developer developer);

        // READ
        List<Developer> GetAll();
        Developer? GetById(int id);
        List<Proyek> GetAllProyek();

        // UPDATE
        bool Update(Developer developer);

        // DELETE
        bool Delete(int id);
    }
}
