using la_mia_pizzeria_razor_layout.Data;
using la_mia_pizzeria_razor_layout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_razor_layout.Controllers
{
    public class CategoryController : Controller
    {

        PizzaDbContext db;

        public CategoryController() : base()
        {
            db = new PizzaDbContext();
        }
        public IActionResult Index()
        {
            List<Category> categories = db.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            db.Categories.Add(category);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Category category = db.Categories.Where(cat => cat.Id == id).Include(p => p.Pizza).FirstOrDefault();
            if (category == null)
                return NotFound("Elemento non trovato");
            if (category.Pizza.Count() > 0)
            {
                return View("NotDelete");
            }
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
