using Microsoft.EntityFrameworkCore.Storage;
using NetCore3._0Learn.WebApp.Data.Model;

namespace NetCore3._0Learn.WebApp.Data.Repository
{
    public interface IRepository<T,K> where T:EntityBase<K>
    {
       
        void BeginTran();
        void Commit();
        void AddEntity(T t);
        void Update(T t);
       
        void Remove(K k);
        void ExecuteSql(string sql,params  object[] paramList);
    }
}