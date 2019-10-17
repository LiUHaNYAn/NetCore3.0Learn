using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using NetCore3._0Learn.WebApp.Data.Model;

namespace NetCore3._0Learn.WebApp.Data.Repository
{
    public interface IRepository<T, K> where T : EntityBase<K>
    {
        void AddEntity(T t);
        void AddEntity(List<T> list); 
        void Update(T t);
        void Remove(K k);
        void ExecuteSql(string sql, params object[] paramList);
        IQueryable<T> GetList(Expression<Func<T, bool>> expression);
        T GetModel(K key);

        List<T> PagerList(Expression<Func<T, bool>> expression, Expression<Func<T, bool>> orderExpression, int limit,
            int offset, out int total);

        DataTable FindTable(string sql, params IDbDataParameter[] parameters);
        DataSet FindDataSet(string sql, params IDbDataParameter[] parameters);
    }
}