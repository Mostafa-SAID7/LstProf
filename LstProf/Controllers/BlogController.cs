using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LstProf.Data;
using LstProf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LstProf.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Display all published blog posts (latest first)
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var posts = await GetPublishedPosts();
            return View(posts);
        }

        /// <summary>
        /// Display details of a single blog post
        /// </summary>
        public async Task<IActionResult> Details(int id)
        {
            var post = await _context.BlogPosts
                                     .Include(p => p.Category)
                                     .Include(p => p.Tags)
                                     .FirstOrDefaultAsync(p => p.Id == id && p.IsPublished);

            if (post == null) return NotFound();
            return View(post);
        }

        /// <summary>
        /// Display blog posts under a specific category
        /// </summary>
        public async Task<IActionResult> Category(string category)
        {
            if (string.IsNullOrWhiteSpace(category)) return NotFound();

            var posts = await _context.BlogPosts
                                      .Include(p => p.Category)
                                      .Where(p => p.IsPublished &&
                                                  p.Category != null &&
                                                  p.Category.Name.ToLowerInvariant() == category.ToLowerInvariant())
                                      .OrderByDescending(p => p.CreatedAt)
                                      .ToListAsync();

            ViewBag.CategoryName = category;
            return View(posts);
        }

        /// <summary>
        /// Display blog archive (all published posts)
        /// </summary>
        public async Task<IActionResult> Archive()
        {
            var posts = await GetPublishedPosts();
            return View(posts);
        }

        /// <summary>
        /// Search blog posts by title or content
        /// </summary>
        public async Task<IActionResult> Search(string q)
        {
            if (string.IsNullOrWhiteSpace(q)) return View(new List<BlogPost>());

            var posts = await _context.BlogPosts
                                      .Where(p => p.IsPublished &&
                                                  (p.Title.Contains(q) || p.Content.Contains(q)))
                                      .OrderByDescending(p => p.CreatedAt)
                                      .ToListAsync();

            ViewBag.Query = q;
            return View(posts);
        }

        /// <summary>
        /// Display blog posts with a specific tag
        /// </summary>
        public async Task<IActionResult> Tags(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag)) return NotFound();

            var posts = await _context.BlogPosts
                                      .Include(p => p.Tags)
                                      .Where(p => p.IsPublished &&
                                                  p.Tags.Any(t => t.Name.ToLowerInvariant() == tag.ToLowerInvariant()))
                                      .OrderByDescending(p => p.CreatedAt)
                                      .ToListAsync();

            ViewBag.Tag = tag;
            return View(posts);
        }

        /// <summary>
        /// Helper method to get all published posts ordered by creation date
        /// </summary>
        private async Task<List<BlogPost>> GetPublishedPosts()
        {
            return await _context.BlogPosts
                                 .Where(p => p.IsPublished)
                                 .OrderByDescending(p => p.CreatedAt)
                                 .ToListAsync();
        }
    }
}
