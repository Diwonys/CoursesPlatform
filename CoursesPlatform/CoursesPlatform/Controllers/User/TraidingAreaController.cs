using CoursesPlatform.Models;
using CoursesPlatform.Models.Teacher.Course;
using CoursesPlatform.Models.Teacher.Course.ViewModel;
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
    public class TraidingAreaController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly int _pageSize = Constants.CountProductsOnPage;

        public TraidingAreaController(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(CourseFavorPropertiesViewModel properties)
        {
            IQueryable<Course> products = _context.Courses
                .Include(e => e.Image)
                .Include(e => e.UserPublishedCourses)
                .Include(e => e.CourseCategory);

            products = Filter(properties, products);
            products = Sort(properties.sortOrder, products);

            var count = await products.CountAsync();

            var courses = await products.Skip((properties.Page - 1) * _pageSize)
                .Take(_pageSize).ToListAsync();

            var viewModel = new CourseViewModel
            {
                PageViewModel = new PageViewModel(count, properties.Page, _pageSize),
                SortViewModel = new SortViewModel(properties.sortOrder),
                FilterViewModel = new FilterViewModel(_context.CourseCategories.ToList(), properties),
                Courses = courses
            };

            return View(viewModel);
        }

        private IQueryable<Course> Sort(SortState sortOrder, IQueryable<Course> products) 
            => sortOrder switch
        {
            SortState.NameAsc => products.OrderBy(s => s.Name),
            SortState.NameDesc => products.OrderByDescending(s => s.Name),
            SortState.CostAsc => products.OrderBy(s => s.Cost),
            SortState.CostDesc => products.OrderByDescending(s => s.Cost),
            _ => products,
        };

        private IQueryable<Course> Filter(CourseFavorPropertiesViewModel properties, IQueryable<Course> courses)
        {
            if (properties.CategoriesId != null && properties.CategoriesId.Length > 0)
                courses = courses.Where(c => properties.CategoriesId.Any(x => x.Equals(c.CourseCategoryId)));

            if (properties.CostFrom != null)
                courses = courses.Where(c => c.Cost >= properties.CostFrom);
            if (properties.CostTo != null)
                courses = courses.Where(c => c.Cost <= properties.CostTo);

            return courses;
        }

        public async Task<IActionResult> Details(int? id)
        {
            var course = await _context.Courses
                .Include(e=>e.Image)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            
            if (course == null)
                return NotFound();

            return View(course);
        }
        
        [Authorize]
        public async Task<IActionResult> Subscribe(int? id)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            var userId = _userManager.GetUserAsync(User).Result.Id;

            var user = await _context.Users
                .Include(e => e.StudiedCourses)
                .FirstOrDefaultAsync(e => e.Id.Equals(userId));

            if (course == null)
                return NotFound();

            if (user.StudiedCourses.Contains(course))
                return RedirectToAction(nameof(AlreadySubscribed));

            if (course.Cost > 0)
                return RedirectToAction(nameof(Pay), new { id = course.Id });

            user.StudiedCourses.Add(course);
            await _context.SaveChangesAsync();

            return RedirectToAction("SubscribedCourses", "PersonalArea");
        }

        [Authorize]
        public async Task<IActionResult> Pay(int? id)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (course == null)
                return NotFound();

            return View(course);
        }

        [Authorize]
        [HttpPost, ActionName("Pay")]
        public async Task<IActionResult> PayConfirmation(int? id)
        {
            var course = await _context.Courses
               .FirstOrDefaultAsync(e => e.Id.Equals(id));

            var userId = _userManager.GetUserAsync(User).Result.Id;
            var user = await _context.Users
                .Include(e => e.StudiedCourses)
                .FirstOrDefaultAsync(e => e.Id.Equals(userId));

            if (course == null)
                return NotFound();

            user.StudiedCourses.Add(course);
            await _context.SaveChangesAsync();

            return RedirectToAction("SubscribedCourses", "PersonalArea");
        }
        [Authorize]
        public IActionResult AlreadySubscribed() => View();
    }
}
