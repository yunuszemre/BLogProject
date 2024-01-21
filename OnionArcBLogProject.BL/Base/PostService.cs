using OnionArcBLogProject.Core.Service;
using OnionArcBLogProject.Entities.Context;
using OnionArcBLogProject.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnionArcBLogProject.Service.Base
{
    public class PostService : BaseService<Post>, IPostService
    {
        private readonly BlogContext _posts;
        public PostService(BlogContext blogContext) : base(blogContext)
        {
            _posts = blogContext;
        }

        public List<Post> GetPosts()
        {
            throw new NotImplementedException();
        }

        //public List<Post> GetPosts()
        //{
        //    //var query = (from post in _posts.Posts join _)

        //}
    }
}
