using API.Contracts;
using API.Data;
using API.Models;
using API.Utilities.Handler;

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
        protected readonly BookingManagementDbContext _context;

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
            var entity = _context.Set<TEntity>().Find(guid);
            _context.ChangeTracker.Clear();
            return entity;
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
            catch(Exception ex)
            {
                if (ex.InnerException is not null && ex.InnerException.Message.Contains("IX_tb_m_employees_nik"))
                {
                    throw new ExceptionHandler("NIK already exists");
                }
                if (ex.InnerException is not null && ex.InnerException.Message.Contains("IX_tb_m_employees_email"))
                {
                    throw new ExceptionHandler("Email already exists");
                }
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_tb_m_employees_phone_number"))
                {
                    throw new ExceptionHandler("Phone number already exists");
                }
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
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

        private void Clear()
        {
            _context.ChangeTracker.Clear();
        }

    }
}
