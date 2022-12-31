using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnionArcBLogProject.Core.Entity;

namespace OnionArcBLogProject.Entities.Entities
{
    public class Category : CoreEntity
    {
        public Category()
        {
            this.Posts = new List<Post>();
        }
        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        public virtual List<Post> Posts { get; set; }
    }
}
