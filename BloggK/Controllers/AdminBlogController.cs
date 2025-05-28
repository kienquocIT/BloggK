using System.Linq;
using System.Threading.Tasks;
using Azure;
using BloggK.Data;
using BloggK.Models.Domain;
using BloggK.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggK.Controllers
{
    public class AdminBlogController : Controller
    {
        private readonly BloggKDbContext _context;
        public AdminBlogController(BloggKDbContext context)
        {
            _context = context;
        }

        // GET: AdminBlog/Index
        public IActionResult Index()
        {
            var blogPosts = _context.BlogPosts.ToList();
            return View(blogPosts);
        }

        // GET: AdminBlog/Add
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Tags = _context.Tags.ToList();
            return View();
        }

        // POST: AdminBlog/Add
        [HttpPost]
        public IActionResult Add(AddBlogRequest addBlogRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(addBlogRequest);
            }

            var blog = new BlogPost
            {
                Id = Guid.NewGuid(),
                Heading = addBlogRequest.Heading,
                PageTitle = addBlogRequest.PageTitle,
                Content = addBlogRequest.Content,
                ShortDescription = addBlogRequest.ShortDescription,
                FeaturedImageUrl = addBlogRequest.FeaturedImageUrl,
                UrlHandle = addBlogRequest.UrlHandle,
                PublishedDate = addBlogRequest.PublishedDate,
                Author = addBlogRequest.Author,
                Visible = addBlogRequest.Visible,
            };

            if (addBlogRequest.SelectedTags.Any())
            {
                var tags = _context.Tags
                    .Where(t => addBlogRequest.SelectedTags.Contains(t.Name))
                    .ToList();
                blog.Tags = tags;
            }

            _context.BlogPosts.Add(blog);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: AdminBlog/Edit/{id}
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            ViewBag.Tags = _context.Tags.ToList();
            var blogPost = _context.BlogPosts
                .Include(b => b.Tags)
                .FirstOrDefault(b => b.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }
            var editBlogRequest = new AddBlogRequest
            {
                Id = blogPost.Id,
                Heading = blogPost.Heading,
                PageTitle = blogPost.PageTitle,
                Content = blogPost.Content,
                ShortDescription = blogPost.ShortDescription,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                Visible = blogPost.Visible,
                SelectedTags = blogPost.Tags.Select(t => t.Name).ToList()
            };
            return View(editBlogRequest);
        }

        // POST: AdminBlog/Edit/{id}
        [HttpPost]
        public IActionResult Edit(AddBlogRequest editBlogRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(editBlogRequest);
            }
            var blogPost = _context.BlogPosts
                .Include(b => b.Tags)
                .FirstOrDefault(b => b.Id == editBlogRequest.Id);
            if (blogPost == null)
            {
                return NotFound();
            }
            blogPost.Heading = editBlogRequest.Heading;
            blogPost.PageTitle = editBlogRequest.PageTitle;
            blogPost.Content = editBlogRequest.Content;
            blogPost.ShortDescription = editBlogRequest.ShortDescription;
            blogPost.FeaturedImageUrl = editBlogRequest.FeaturedImageUrl;
            blogPost.UrlHandle = editBlogRequest.UrlHandle;
            blogPost.PublishedDate = editBlogRequest.PublishedDate;
            blogPost.Author = editBlogRequest.Author;
            blogPost.Visible = editBlogRequest.Visible;

            blogPost.Tags.Clear();

            if (editBlogRequest.SelectedTags != null && editBlogRequest.SelectedTags.Any())
            {
                var tags = _context.Tags
                    .Where(t => editBlogRequest.SelectedTags.Contains(t.Name))
                    .ToList();
                foreach (var tag in tags)
                {
                    blogPost.Tags.Add(tag);
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: AdminBlog/Delete/{id}
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var blogPost = _context.BlogPosts
                .Include(b => b.Tags)
                .FirstOrDefault(b => b.Id == id);

            if (blogPost == null)
            {
                return NotFound();
            }
            if (blogPost.Tags != null && blogPost.Tags.Any())
            {
                blogPost.Tags.Clear();
            }

            _context.BlogPosts.Remove(blogPost);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
