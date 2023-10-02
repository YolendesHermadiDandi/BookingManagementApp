using API.Models;

namespace API.Contracts
{
    /* 
     * Generic class digunakan untuk membuat interface untuk semua repository
     * 
     * TEntity => digunakan untuk menentukan tipe objek/class yang akan digunakan
     */
    public interface IGeneralRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity? GetByGuid(Guid guid);
        TEntity? Create(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
    }
}
