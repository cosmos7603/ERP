using System;
using System.Data.Entity;
using DAL.Repository.Abstract;

namespace DAL
{
    public class UnitOfWork<T> : IDisposable where T : class
    {
        private DbContext _context;
        public UnitOfWork()
        {
            _context = new ERPEntities();
        }
        private RepositoryBase<T> _repository;
        public RepositoryBase<T> Repository
        {
            get
            {
                if (this._repository == null)
                {
                    this._repository = new RepositoryBase<T>(_context);
                }
                return _repository;
            }
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
