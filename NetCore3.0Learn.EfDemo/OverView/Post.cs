using System;

namespace NetCore3._0Learn.EfDemo.OverView
{
    public class Post
    {
        public  Guid Id { get; set; }
        public string Title { get; set; }
        public  string CreateTime { get; set; }
        public  Guid BlogId { get; set; }
        public  Blog Blog { get; set; }
    }
}