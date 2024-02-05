using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;

namespace StoreFront.UI.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly StardewContext _context;

        public CategoriesController(StardewContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
              return _context.Categories != null ? 
                          View(await _context.Categories.ToListAsync()) :
                          Problem("Entity set 'StardewContext.Categories'  is null.");
        }

        [AcceptVerbs("POST")]
        public JsonResult Delete(int id)
        {
            var cat = _context.Categories.Find(id);
            _context.Categories.Remove(cat);
            _context.SaveChanges();
            string message = $"Deleted the category \"{cat.CategoryName}\" from the database!";
            return Json(new { id = id, message = message });
        }

        public PartialViewResult Details(int id)
        {
            return PartialView(_context.Categories.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Json(category);
        }

        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            return PartialView(_context.Categories.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Category category)
        {
            _context.Update(category);
            _context.SaveChanges();
            return Json(category);
        }
    }
}
