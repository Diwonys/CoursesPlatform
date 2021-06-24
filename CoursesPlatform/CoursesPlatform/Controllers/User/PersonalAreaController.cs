using CoursesPlatform.Models;
using CoursesPlatform.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesPlatform.Controllers
{

    [Authorize]
    public class PersonalAreaController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;

        public PersonalAreaController(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index() => View();
        public IActionResult PersonalData() => View();

        public async Task<IActionResult> SubscribedCourses()
        {
            var userId = _userManager.GetUserAsync(User).Result.Id;
            var usersCourses = await _context.Courses
                .Include(e=>e.Image)
                .Where(e => e.UserStudiedCourses.Id.Equals(userId))
                .ToListAsync();

            return View(usersCourses);
        }

        public async Task<IActionResult> CourseDetails(int? id)
        {
            var course = await _context.Courses
                .Include(e => e.Image)
                .Include(e => e.CourseCategory)
                .Include(e => e.Lessons)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (course == null)
                return NotFound();

            return View(course);
        }

        public async Task<IActionResult> LessonDetails(int? id)
        {
            var lesson = await _context.Lessons
                .Include(e=>e.Content)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (lesson == null)
                return NotFound();

            return View(lesson);
        }

    }
}
