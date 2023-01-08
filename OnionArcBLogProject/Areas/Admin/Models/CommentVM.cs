using OnionArcBLogProject.Entities.Entities;

namespace OnionArcBLogProject.WebUI.Areas.Admin.Models
{
    public class CommentVM
    {
        public User User{ get; set; }
        public Post Post { get; set; }
    }
}
