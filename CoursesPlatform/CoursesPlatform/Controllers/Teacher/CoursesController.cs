using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoursesPlatform.Models;
using CoursesPlatform.Models.Teacher.Course;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.AspNetCore.Identity;
using CoursesPlatform.Models.Users;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.Contracts;
using CoursesPlatform.Models.Teacher.Course.Elements;

namespace CoursesPlatform.Controllers
{
    [Authorize(Roles = "teacher")]
    public class CoursesController : Controller
    {
        private readonly ApplicationContext _context;

        public UserManager<User> _userManager { get; }

        public CoursesController(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var applicationContext = _context.Courses.Include(c => c.CourseCategory)
                .Where(x => x.UserPublishedCoursesId.Equals(user.Id));
            return View(await applicationContext.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.CourseCategory)
                .Include(i => i.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["CourseCategoryId"] = new SelectList(_context.CourseCategories, "Id", "Name");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                course.CreationDate = DateTime.Now;
                course.UserPublishedCoursesId = user.Id;

                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseCategoryId"] = new SelectList(_context.CourseCategories, "Id", "Name", course.CourseCategoryId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(e => e.Image)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            if (course == null)
            {
                return NotFound();
            }
            ViewData["CourseCategoryId"] = new SelectList(_context.CourseCategories, "Id", "Name", course.CourseCategoryId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            if (id != course.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var sourseCourse = await _context.Courses.
                        FirstOrDefaultAsync(e => e.Id.Equals(course.Id));

                    sourseCourse.Name = course.Name;
                    sourseCourse.Description = course.Description;
                    sourseCourse.Cost = course.Cost;
                    sourseCourse.CourseCategoryId = course.CourseCategoryId;
                    
                    if (course.Image != null)
                        sourseCourse.Image = course.Image;
                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseCategoryId"] = new SelectList(_context.CourseCategories, "Id", "Name", course.CourseCategoryId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.CourseCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
