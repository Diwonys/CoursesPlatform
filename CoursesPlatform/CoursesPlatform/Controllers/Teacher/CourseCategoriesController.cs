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

namespace CoursesPlatform.Controllers
{
    [Authorize(Roles = "teacher")]
    public class CourseCategoriesController : Controller
    {
        private readonly ApplicationContext _context;

        public CourseCategoriesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: CourseCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.CourseCategories.ToListAsync());
        }

        // GET: CourseCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseCategory = await _context.CourseCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseCategory == null)
            {
                return NotFound();
            }

            return View(courseCategory);
        }

        // GET: CourseCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CourseCategory courseCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseCategory);
        }

        // GET: CourseCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseCategory = await _context.CourseCategories.FindAsync(id);
            if (courseCategory == null)
            {
                return NotFound();
            }
            return View(courseCategory);
        }

        // POST: CourseCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CourseCategory courseCategory)
        {
            if (id != courseCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseCategoryExists(courseCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(courseCategory);
        }

        // GET: CourseCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseCategory = await _context.CourseCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseCategory == null)
            {
                return NotFound();
            }

            return View(courseCategory);
        }

        // POST: CourseCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseCategory = await _context.CourseCategories.FindAsync(id);
            _context.CourseCategories.Remove(courseCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseCategoryExists(int id)
        {
            return _context.CourseCategories.Any(e => e.Id == id);
        }
    }
}
