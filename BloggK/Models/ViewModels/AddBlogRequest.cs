using System.ComponentModel.DataAnnotations;
using BloggK.Models.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BloggK.Models.ViewModels
{
    public class AddBlogRequest
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public Boolean Visible { get; set; }

        public List<string> SelectedTags { get; set; }
    }
}
