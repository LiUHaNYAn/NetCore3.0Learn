using System;
using NetCore3._0Learn.WebApp.Data.Model;
using NetCore3._0Learn.WebApp.Data.Repository;

namespace NetCore3._0Learn.WebApp.Data.Service
{
    public class BlogService : ServiceBase, IBlogService
    {
        private readonly IRepository<Blog, Guid> _blogRepository;
        private IRepository<Post, Guid> _postRepository;

        public BlogService(IDataBase dataBase, IRepository<Blog, Guid> blogRepository,
            IRepository<Post, Guid> postRepository) : base(dataBase)
        {
            _blogRepository = blogRepository;
            _postRepository = postRepository;
        }

        public void Add()
        {
            var data=_blogRepository.FindEntity(Guid.Parse("CABC53ED-91E5-4CD3-9BDE-038EDE9209B6"));
            _blogRepository.AddEntity(new Blog {Id = Guid.NewGuid(), CreateTime = DateTime.Now, Title = "2fsfsd"});
            var dataset=_blogRepository.FindDataSet("select * from blogs;select * from posts");
            DataBase.BeginTran();
            var blogid = Guid.NewGuid();
//            postRepository.AddEntity(new Post {Id = Guid.NewGuid(), Title = "demo176FF941-7 ",BlogId = blogid});
            _blogRepository.AddEntity(new Blog {Id = Guid.NewGuid(), CreateTime = DateTime.Now, Title = "2fsfsd"});
            _blogRepository.AddEntity(new Blog
                {Id = blogid, CreateTime = DateTime.Now, Title = "helTransactionId:Transa"});
            DataBase.Commit();
        }
    }
}