using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnionArcBLogProject.Core.Entity;

namespace OnionArcBLogProject.Entities.Entities
{
    public class Comment : CoreEntity
    {
        public string CommentText { get; set; }


        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Post")]
        public Guid PostId { get; set; }

        public virtual Post Post { get; set; }
        
    }
}
