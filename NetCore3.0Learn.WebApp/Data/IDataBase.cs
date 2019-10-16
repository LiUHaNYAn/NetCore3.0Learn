using Microsoft.EntityFrameworkCore;

namespace NetCore3._0Learn.WebApp.Data
{
    public interface IDataBase
    {
          DbContext DbContext { get; }
          void BeginTran();
          void Commit();
          void Rollback();
    }
}