using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NetCore3._0Learn.WebApp.Data.Model;

namespace NetCore3._0Learn.WebApp.Data.Repository
{
    public class RepositoryBase<T, K> : IRepository<T, K> where T : class, EntityBase<K>
    {
       
        private readonly DbContext _dbContext;
        private IDbContextTransaction _transaction;

        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void BeginTran()
        {
            _transaction = _dbContext.Database.BeginTransaction(); 
        }

        public void Commit()
        {
            try
            {
                if (_transaction == null)
                    _dbContext.SaveChanges();
                else
                    _transaction.Commit();
            }
            catch (Exception ex)
            {
                _transaction?.Rollback();
            }
        }

        public void AddEntity(T t)
        {
            Console.WriteLine(_transaction.TransactionId);
            _dbContext.Entry(t).State = EntityState.Added;
            _dbContext.SaveChanges();
        }

        public void Update(T t)
        {
            _dbContext.Entry(t).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Remove(K k)
        {
            var type = typeof(T);
            var name = type.Name + "s";
            var attrs = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            if (attrs != null && string.IsNullOrEmpty(attrs.Name)) name = attrs.Name;
            _dbContext.Database.ExecuteSqlRaw("delete from " + name + " where Id={0}",
                k.ToString());
        }

        public void ExecuteSql(string sql, params object[] parameters)
        {
            _dbContext.Database.ExecuteSqlRaw(sql, parameters);
        }
    }
}