using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace NetCore3._0Learn.WebApp.Data
{
    public class DataBase:IDataBase
    {
        public DbContext DbContext { get; }
        private IDbContextTransaction _transaction;

        public DataBase(DbContext dbContext)
        {
            DbContext = dbContext;
        }
        public void BeginTran()
        {
            _transaction = DbContext.Database.BeginTransaction(); 
        }

        public void Commit()
        {
            try
            {
                if (_transaction == null)
                    DbContext.SaveChanges();
                else
                    _transaction.Commit();
            }
            catch (Exception ex)
            {
                _transaction?.Rollback();
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}