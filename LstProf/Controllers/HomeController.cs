using LstProf.Data;
using LstProf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LstProf.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Main single-page portfolio
        public async Task<IActionResult> Index()
        {
            // Fetch latest 3 projects and blogs
            var latestProjects = await _context.Projects
                                    .OrderByDescending(p => p.CreatedAt)
                                    .Take(3)
                                    .ToListAsync();

            var latestBlogs = await _context.BlogPosts
                                    .Include(b => b.Category)
                                    .OrderByDescending(b => b.PublishedAt)
                                    .Take(3)
                                    .ToListAsync();

            // Pass to View using ViewModel
            var model = new HomeViewModel
            {
                LatestProjects = latestProjects,
                LatestBlogs = latestBlogs
            };

            return View(model);
        }

        // Optional section routes redirecting to Index with anchor
        public IActionResult About()
        {
            return RedirectToAction("Index", new { fragment = "about" });
        }

        public IActionResult Portfolio()
        {
            return RedirectToAction("Index", new { fragment = "portfolio" });
        }

        public IActionResult Resume()
        {
            return RedirectToAction("Index", new { fragment = "resume" });
        }

        public IActionResult Services()
        {
            return RedirectToAction("Index", new { fragment = "services" });
        }

        public IActionResult Privacy()
        {
            return RedirectToAction("Index", new { fragment = "privacy" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
