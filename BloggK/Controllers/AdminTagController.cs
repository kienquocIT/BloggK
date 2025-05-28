using BloggK.Data;
using BloggK.Models.Domain;
using BloggK.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggK.Controllers
{
    public class AdminTagController : Controller
    {
        public readonly BloggKDbContext _context;
        public AdminTagController(BloggKDbContext context)
        {
            _context = context;
        }

        // GET: AdminTag
        [HttpGet]
        public IActionResult Index()
        {
            var tags = _context.Tags.ToList();
            return View(tags);
        }

        // GET: AdminTag/Add
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // POST: AdminTag/Add
        [HttpPost]
        public IActionResult Add(AddTagRequest request)
        {
            if (ModelState.IsValid)
            {
                var tag = new Tag
                {
                    Name = request.Name,
                    DisplayName = request.DisplayName
                };
                _context.Tags.Add(tag);
                _context.SaveChanges();
                return RedirectToAction("Index", "AdminTag");
            }
            return View(request);
        }

        // GET: AdminTag/Edit/{id}
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var tag = _context.Tags.Find(id);
            if (tag != null)
            {
                var viewModel = new AddTagRequest
                {
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                ViewBag.Id = tag.Id;
                return View(viewModel);
            }
            return View();
        }

        // POST: AdminTag/Edit/{id}
        [HttpPost]
        public IActionResult Edit(Guid id, AddTagRequest request)
        {
            if (ModelState.IsValid)
            {
                var tag = _context.Tags.Find(id);
                if (tag != null)
                {
                    tag.Name = request.Name;
                    tag.DisplayName = request.DisplayName;
                    _context.SaveChanges();
                    return RedirectToAction("Index", "AdminTag");
                }
            }
            ViewBag.Id = id;
            return View(request);
        }

        // GET: AdminTag/Delete/{id}
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var tag = _context.Tags
                .Include(t => t.BlogPosts)
                .FirstOrDefault(t => t.Id == id);

            if (tag == null)
                return NotFound();

            if (tag.BlogPosts != null && tag.BlogPosts.Any())
            {
                tag.BlogPosts.Clear();
                _context.SaveChanges(); // Ghi lại sự thay đổi quan hệ
            }

            _context.Tags.Remove(tag);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
