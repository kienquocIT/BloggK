using System.Diagnostics;
using BloggK.Data;
using BloggK.Models;
using BloggK.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggK.Controllers
{
    public class HomeController : Controller
    {
        private readonly BloggKDbContext _context;

        public HomeController(BloggKDbContext context)
        {
            _context = context;
        }

        // GET: privacy
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Index(Guid? tag)
        {
            var tags = _context.Tags.ToList();

            var postsQuery = _context.BlogPosts
                .Include(p => p.Tags)
                .AsQueryable();

            if (tag.HasValue)
            {
                postsQuery = postsQuery
                    .Where(p => p.Tags.Any(t => t.Id == tag.Value));
            }

            var posts = postsQuery.ToList();

            var model = new HomeModel
            {
                Tags = tags,
                Posts = posts
            };

            return View(model);
        }

        // GET: AdminBlog/Home/Details/{id}
        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var blogPost = _context.BlogPosts
                .Include(b => b.Tags)
                .FirstOrDefault(b => b.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return View(blogPost);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
