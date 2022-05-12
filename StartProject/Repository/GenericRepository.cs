using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using StartProject.Config;
using StartProject.Interfaces;
using System.Runtime.InteropServices;

namespace StartProject.Repository
{
    public class GenericRepository<T> : IGeneric<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<ContextBase> _db;

        public GenericRepository()
        {
            _db = new DbContextOptions<ContextBase>();
        }

        public async Task Add(T Object)
        {
            using ContextBase? data = new(_db);
            await data.Set<T>().AddAsync(Object);
            await data.SaveChangesAsync();
        }

        public async Task Delete(T Object)
        {
            using ContextBase? data = new(_db);
            data.Set<T>().Remove(Object);
            await data.SaveChangesAsync();
        }
        public async Task<T> GetEntityById(int Id)
        {
            using ContextBase? data = new(_db);
#pragma warning disable CS8603 // Possible null reference return.
            return await data.Set<T>().FindAsync(Id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<List<T>> GetAll()
        {
            using ContextBase? data = new(_db);
            return await data.Set<T>().AsNoTracking().ToListAsync();
        }



        public async Task Update(T Object)
        {
            using ContextBase? data = new(_db);
            data.Set<T>().Update(Object);
            await data.SaveChangesAsync();
        }


        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Instantiate a SafeHandle instance.
        readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);



        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
        #endregion

    }
}
