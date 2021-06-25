using CoursesPlatform.Models;
using CoursesPlatform.Models.Teacher.Course;
using CoursesPlatform.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;

        public HomeController(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserAsync(User).Result.Id;
                var usersLesons = await _context.Lessons
                    .Include(e => e.Course)
                    .Include(e => e.Content)
                    .Where(e => e.Course.UserStudiedCourses.Id.Equals(userId))
                    .ToListAsync();

                for (int i = 0; i < usersLesons.Count(); i++)
                {
                    var lesson = usersLesons[i];
                    lesson.Content = lesson.Content.Take(3).ToList();
                }

                usersLesons.Reverse();

                return View(usersLesons);
            }

            return View(new List<Lesson>());
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
