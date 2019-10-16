using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
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

        public void AddEntity(List<T> list)
        {
             dataBase.DbContext.Set<T>().AddRange(list);
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

        public T GetModel(K key)
        {
            var type = typeof(T);
            var name = type.Name + "s";
            var attrs = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            if (attrs != null && string.IsNullOrEmpty(attrs.Name)) name = attrs.Name;
           return dataBase.DbContext.Set<T>().FromSqlRaw("select * from " + name + " where Id={0}",
                key.ToString()).FirstOrDefault();
        }

        public IQueryable<T> GetList(Expression<Func<T, bool>> expression)
        {
            return dataBase.DbContext.Set<T>().Where(expression);
        }

     

        public List<T> PagerList(Expression<Func<T,bool>> expression,Expression<Func<T,bool>> orderExpression,int limit,int offset,out int total)
        {
            var data = dataBase.DbContext.Set<T>().Where(expression).OrderByDescending(orderExpression)
                .Skip(offset).Take(limit);
              total = dataBase.DbContext.Set<T>().Count(expression);
              return data.ToList();
        }
    }
}