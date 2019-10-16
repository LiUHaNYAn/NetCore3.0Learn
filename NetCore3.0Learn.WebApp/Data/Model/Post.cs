using System;

namespace NetCore3._0Learn.WebApp.Data.Model
{
    public class Post:EntityBase<Guid>
    {
       
        public string Title { get; set; }
        public  DateTime CreateTime { get; set; }
        public  Guid BlogId { get; set; }
        public  Blog Blog { get; set; }
        public Guid Id { get; set; }
    }
}