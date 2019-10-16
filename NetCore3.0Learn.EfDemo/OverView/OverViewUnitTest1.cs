using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Bson;
using NUnit.Framework;
using Xunit;

namespace NetCore3._0Learn.EfDemo.OverView
{

    public static class DbContextExtension
    {
        public static List<T> SqlQuery<T>(this DbContext dbContext, string sql)
        {
            var conn=dbContext.Database.GetDbConnection();
            
            return null;
        }
    }
    
    public class OverViewUnitTest1
    {
        [Test]
        public void ConnectDb()
        {
            var db=new EfDbContext();
            Console.WriteLine(db.Database.GetDbConnection().State);
        }

        [Test]
        public void AddTest()
        {
            using (var db=new EfDbContext())
            {
                db.Blogs.Add(new Blog(){Id = Guid.NewGuid(),CreateTime = DateTime.Now,Title = "Python入门经典"});
                var result=db.SaveChanges(); 
                
            }
        }
        [Test]
        public void QueryTest()
        {
            using (var db=new EfDbContext())
            {
                var blogs = db.Blogs;
                var data = from blog in blogs select new {blog.Id, blog.Title};
                Assert.IsTrue(blogs.Any());
            }
        }

        [Test]
        public void RawQuery()
        {
            using (var db=new EfDbContext())
            {
                var data=db.Set<Blog>().FromSqlRaw("select * from blogs")
                    .Where(p => p.Id == Guid.Parse("41201FD0-6781-4435-9FBC-38310A9A9533"));
                foreach (var blog in data)
                {
                    Console.WriteLine(blog.Title);
                }
            }
        }
        [Test]
        public void LinqQuery()
        {
            using var db = new EfDbContext();
            var data= db.Posts.Join(db.Blogs, p => p.BlogId, p => p.Id, (a, b) =>
                new{
                    PostTitle= a.Title,b.Title,a.CreateTime 
                });
            var data1 = new object();
        }

        [Test]
        public void ExecuteRawSql()
        {
            using var db = new EfDbContext();
             
            var tran = db.Database.BeginTransaction();
            tran = db.Database.CurrentTransaction;
            
            db.Database.ExecuteSqlRaw(
                "insert into blogs(id,title,createtime)values({0},{1},{2})",
                Guid.NewGuid(),
                "fsdfasdfa",
                DateTime.Now);
               

        db.Database.ExecuteSqlRaw("insert into blogs(id,title,createtime)values({0},{1},{2})",  
            
                  Guid.NewGuid(),
                 "测试指南fsdfgasgasdgsdfgsdfgs599523" +
                       "29+59+52" +
                       "32656+56+514899" +
                       "484984998489" +
                       "5549" +
                       "+48489" +
                       "+489498++*8986+6++dfgsfsdfgasgasdgsdfgsdfgsdfgsfdger" +
                 "trewyweyetytetyerytyetyetyetyeyytytruku6638498915123154fdg" +
                 "ertrewyweyetytetyerytyetyetyetyeyytytruku6638498915123154",
                 DateTime.Now
             );
            tran.Commit();
        }

        [Test]
        public void DataTaleTest()
        {
            using (var db=new EfDbContext())
            {
                db.SqlQuery<Blog>("select * from blogs");
            }
        }
    }
}