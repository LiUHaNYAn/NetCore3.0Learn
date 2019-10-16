using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace NetCore3._0Learn.WebApp.Data
{
    public class DataBase
    {
        private DbContext _dbContext;
        private IDbContextTransaction _transaction;

        public DataBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void BeginTran()
        {
            _transaction=_dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}