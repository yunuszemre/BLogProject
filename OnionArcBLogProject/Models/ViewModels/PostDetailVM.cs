using OnionArcBLogProject.Entities.Entities;

namespace OnionArcBLogProject.WebUI.Models.ViewModels
{
    public class PostDetailVM
    {
        public Post Post{ get; set; }

        public Category Category { get; set; }

        public User User { get; set; }

        
    }
}
