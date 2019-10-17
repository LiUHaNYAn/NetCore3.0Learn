using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using NetCore3._0Learn.WebApp.Data.Common;
using NetCore3._0Learn.WebApp.Data.Model;

namespace NetCore3._0Learn.WebApp.Data.Repository
{
    public class RepositoryBase<T, K> : IRepository<T, K> where T : class, EntityBase<K>
    {
        private readonly IDataBase dataBase;

        public RepositoryBase(IDataBase dataBase)
        {
            this.dataBase = dataBase;
            Console.WriteLine("database:" + dataBase.GetHashCode());
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
        public void BulkAddEntity(List<T> list)
        {
             dataBase.DbContext.BulkInsert(list); 
        }

        public void Update(T t)
        {
            dataBase.DbContext.Entry(t).State = EntityState.Modified;
            dataBase.DbContext.SaveChanges();
        }

        public void Remove(T t)
        {
            dataBase.DbContext.Set<T>().Attach(t);
            dataBase.DbContext.Set<T>().Remove(t);
            dataBase.DbContext.SaveChanges();
        }
        public void Remove(List<T> list)
        {
            dataBase.DbContext.Set<T>().AttachRange(list);
            dataBase.DbContext.Set<T>().RemoveRange(list);
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

        public void Remove(string tableName, string propName, string propVal)
        {
            var sql = "delete  from "+tableName+" where {propName}={0}";
            dataBase.DbContext.Database.ExecuteSqlRaw(sql, propVal);
        }

        public void Remove(string tableName, string propName, List<string> propVals)
        {
            var condition = propVals.Select(p => "'" + p + "'");
            var vals = string.Join(",", condition);
            var sql = "delete  from "+tableName+" where {propName} in ({0})";
            dataBase.DbContext.Database.ExecuteSqlRaw(sql, vals);
        }

        public void BulkRemove(Expression<Func<T, bool>> expression)
        {
             dataBase.DbContext.Set<T>().Where(expression).BatchDelete(); 
        }

        public void ExecuteSql(string sql, params object[] parameters)
        {
            dataBase.DbContext.Database.ExecuteSqlRaw(sql, parameters);
        }

        public T FindEntity(K key)
        { 
            var type = typeof(T);
            var name = type.Name + "s";
            var attrs = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            if (attrs != null && string.IsNullOrEmpty(attrs.Name)) name = attrs.Name;
            return dataBase.DbContext.Set<T>().FromSqlRaw("select * from " + name + " where Id={0}",
                key.ToString()).FirstOrDefault();
        }

        public T FindEntity(Expression<Func<T, bool>> expression)
        {
            return dataBase.DbContext.Set<T>().FirstOrDefault(expression);
        }

        public IQueryable<T> GetList(Expression<Func<T, bool>> expression)
        {
            return dataBase.DbContext.Set<T>().Where(expression);
        }


        public List<T> PagerList(Expression<Func<T, bool>> expression, Expression<Func<T, bool>> orderExpression,
            int limit, int offset, out int total)
        {
            var data = dataBase.DbContext.Set<T>().Where(expression).OrderByDescending(orderExpression)
                .Skip(offset).Take(limit);
            total = dataBase.DbContext.Set<T>().Count(expression);
            return data.ToList();
        }

        public DataTable FindTable(string sql, params IDbDataParameter[] parameters)
        {
            var conn = dataBase.DbContext.Database.GetDbConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(parameters);
            if (conn.State == ConnectionState.Closed) conn.Open();
            return DataConvert.ConvertDataReaderToDataTable(cmd.ExecuteReader());
        }

        public DataSet FindDataSet(string sql, params IDbDataParameter[] parameters)
        {
            var conn = dataBase.DbContext.Database.GetDbConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(parameters);
            if (conn.State == ConnectionState.Closed) conn.Open();
            return DataConvert.ConvertDataReaderToDataSet(cmd.ExecuteReader());
        }
    }
}