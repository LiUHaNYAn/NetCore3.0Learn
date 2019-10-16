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
       
        private readonly IDataBase dataBase;

        public RepositoryBase(IDataBase dataBase)
        {
            this.dataBase = dataBase; 
            Console.WriteLine("database:"+dataBase.GetHashCode());
        }
        public void AddEntity(T t)
        { 
            dataBase.DbContext.Entry(t).State = EntityState.Added;
            dataBase.DbContext.SaveChanges();
        }

        public void Update(T t)
        {
            dataBase.DbContext.Entry(t).State = EntityState.Modified;
            dataBase.DbContext.SaveChanges();
        }

        public void Remove(K k)
        {
            var type = typeof(T);
            var name = type.Name + "s";
            var attrs = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            if (attrs != null && string.IsNullOrEmpty(attrs.Name)) name = attrs.Name;
            dataBase.DbContext.Database.ExecuteSqlRaw("delete from " + name + " where Id={0}",
                k.ToString());
        }

        public void ExecuteSql(string sql, params object[] parameters)
        {
            dataBase.DbContext.Database.ExecuteSqlRaw(sql, parameters);
        }
    }
}