namespace NetCore3._0Learn.WebApp.Data.Service
{
    public abstract class ServiceBase:IServiceBase
    {
         public IDataBase DataBase { get; }

        protected ServiceBase(IDataBase dataBase)
        {
            this.DataBase = dataBase;
        }
    }
}