using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnionArcBLogProject.Core.Entity;

namespace OnionArcBLogProject.Entities.Entities
{
    public class User : CoreEntity
    {
        public User()
        {
            this.Comments = new List<Comment>();
            this.Posts = new List<Post>();
        }
        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

        public string? FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public DateTime? LastLogin { get; set; }

        public string LastIpAdress { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<Post> Posts { get; set; }

    }
}
