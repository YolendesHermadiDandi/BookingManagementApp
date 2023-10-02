using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    /* 
 * Generic class digunakan untuk membuat repository untuk semua class/object
 * class ini juga merupakan class parent dari semua class repository
 * 
 * TEntity => digunakan untuk menentukan tipe objek/class yang akan digunakan
 */
    public class GeneralRepository<TEntity> : IGeneralRepository<TEntity> where TEntity : class
    {
        private readonly BookingManagementDbContext _context;

        public GeneralRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        /*
         * mengambil semua data didalam sebuah tabel dan mengembalikannya dalam bentuk list
         */
        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        /*
         * mencari/mengambil 1 baris data berdasarkan Guid
         * 
         * PHARAM:
         * - guid : primary key data pada suatu tabel
         */
        public TEntity? GetByGuid(Guid guid)
        {
            return _context.Set<TEntity>().Find(guid);
        }

        /*
         * Membuat/memasukan(instert) 1 data kedalam databse
         * 
         * PHARAM:
         * - entity :   merupakan kumpulan data (object/class/model) dari suatu data
         *              yang nantinya akan diinput oleh user
         */
        public TEntity? Create(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }
        /*
        * Mengedit/mengubah(update) 1 data yang sudah ada dan memasukan perubahan kedalam databse
        * 
        * PHARAM:
        * - entity :   merupakan kumpulan data (object/class/model) dari suatu data
        *              yang nantinya akan diinput oleh user
        */
        public bool Update(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        /*
     * Menghapus(delte) 1 data yang ada didalam database
     * 
     * PHARAM:
     * - entity :   merupakan kumpulan data (object/class/model) dari suatu data
     *              yang ingin dihapus
     */
        public bool Delete(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
