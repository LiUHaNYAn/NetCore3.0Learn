using Microsoft.EntityFrameworkCore;

namespace NetCore3._0Learn.EfDemo.OverView
{
    public class EfDbContext:DbContext
    {
        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
        {
            
        }

        public EfDbContext()
        {
            
        }
        public  DbSet<Blog> Blogs { get; set; }
        public  DbSet<Post> Posts { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=localhost;initial catalog=overviewdb;uid=sa;pwd=123456", p =>
            {
                
            });
            
        }
    }
}