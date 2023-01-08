using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnionArcBLogProject.Core.Entity;

namespace OnionArcBLogProject.Entities.Entities
{
    public class Post : CoreEntity
    {
        public Post()
        {
            this.Comments = new List<Comment>();
            
        }
        public string Title { get; set; }

        public string PostDetail { get; set; }

        public string Tags { get; set; }

        public string? ImagePath { get; set; }

        public int ViewCount { get; set; }

        [ForeignKey("Category")]
        public Guid CategroyId { get; set; }

        public virtual Category? Category { get; set; }

        public virtual List<Comment> Comments { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public virtual User? User { get; set; }


    }
}
