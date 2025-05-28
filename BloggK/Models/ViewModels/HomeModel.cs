using BloggK.Models.Domain;

namespace BloggK.Models.ViewModels
{
    public class HomeModel
    {
        public List<Tag> Tags { get; set; } = [];
        public List<BlogPost> Posts { get; set; } = [];
    }
}
