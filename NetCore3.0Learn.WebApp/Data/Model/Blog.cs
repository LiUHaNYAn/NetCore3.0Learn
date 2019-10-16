using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCore3._0Learn.WebApp.Data.Model
{
    [Table("Blogs")]
    public class Blog:EntityBase<Guid>
    {
        public  string Title { get; set; }
        public DateTime CreateTime { get; set; }
        public Guid Id { get; set; }
    }
}