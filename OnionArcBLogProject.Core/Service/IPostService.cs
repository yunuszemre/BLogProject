using OnionArcBLogProject.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArcBLogProject.Core.Service
{
    public interface IPostService : ICoreService<Post>
    {
        List<Post> GetPosts();
    }
}
