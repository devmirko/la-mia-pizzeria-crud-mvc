using la_mia_pizzeria_razor_layout.Data;
using la_mia_pizzeria_razor_layout.Models;
using la_mia_pizzeria_razor_layout.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_razor_layout.Controllers
{
    public class CategoryController : Controller
    {

        //PizzaDbContext db;

        IDbPizzaRepository pizzaRepository;

        public CategoryController(IDbPizzaRepository _pizzaRepository) : base()
        {
            //db = new PizzaDbContext();

            pizzaRepository = _pizzaRepository;
        }
        public IActionResult Index()
        {
            List<Category> categories = pizzaRepository.AllCategory();
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

            pizzaRepository.AddCategory(category);

            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Category category = pizzaRepository.GetByIdCategory(id);
            if (category == null)
                return NotFound("Elemento non trovato");
            if (category.Pizza.Count() > 0)
            {
                return View("NotDelete");
            }

            pizzaRepository.DeleteCategory(category);

            return RedirectToAction("Index");
        }
    }
}
