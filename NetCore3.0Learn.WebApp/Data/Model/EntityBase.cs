using System;

namespace NetCore3._0Learn.WebApp.Data.Model
{
    public interface EntityBase<K> 
    {
           K Id { get; set; }
    }
}