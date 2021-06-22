using CoursesPlatform.Models;
using CoursesPlatform.Models.Teacher.Course;
using CoursesPlatform.Models.Teacher.Course.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesPlatform.Controllers
{
    public class TradingAreaController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly int _pageSize = Constants.CountProductsOnPage;

        public TradingAreaController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(CourseFavorPropertiesViewModel properties)
        {
            IQueryable<Course> products = _context.Courses
                .Include(e => e.CourseCategory);

            //products = Filter(properties.CategoriesId, properties.CostFrom, properties.CostTo, products);
            //products = Sort(properties.sortOrder, products);

            //var count = await products.CountAsync();
            //var items = await products.Skip((properties.Page - 1) * _pageSize).Take(_pageSize).ToListAsync();

            //var viewModel = new ProductsViewModel
            //{
            //    PageViewModel = new PageViewModel(count, properties.Page, _pageSize),
            //    SortViewModel = new SortViewModel(properties.sortOrder),
            //    FilterViewModel = new FilterViewModel(_context.ProductCategories.ToList(),
            //        properties.CategoriesId, properties.CostFrom, properties.CostTo),
            //    Products = items
            //};
            //return View(viewModel);
            return null;
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

        private IQueryable<Course> Filter(int[] categoriesId, int? costFrom, int? costTo, IQueryable<Course> courses)
        {
            if (categoriesId != null && categoriesId.Length > 0)
                courses = courses
                    .Where(p =>
                        categoriesId.Any(x =>
                            x.Equals(p.CourseCategoryId)));
            if (costFrom != null && costTo != null)
                courses = courses.Where(x => x.Cost >= costFrom && x.Cost <= costTo);

            return courses;
        }
    }
}
