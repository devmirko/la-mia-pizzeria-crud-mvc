using la_mia_pizzeria_razor_layout.Data;
using la_mia_pizzeria_razor_layout.Models;
using la_mia_pizzeria_razor_layout.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_razor_layout.Controllers
{
    public class TagController : Controller
    {

        //PizzaDbContext db;

        IDbPizzaRepository pizzaRepository;

        public TagController(IDbPizzaRepository _pizzaRepository) : base()
        {
            //db = new PizzaDbContext();

            pizzaRepository = _pizzaRepository;
        }
        public IActionResult Index()
        {
            List<Tag> tags = pizzaRepository.AllTag();
            return View(tags);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View(tag);
            }

            pizzaRepository.AddTag(tag);

            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Tag tag = pizzaRepository.GetByIdTag(id);
            if (tag == null)
                return NotFound();
            if (tag.Pizzas.Count() > 0)
            {
                return View("NotDelete");
            }

            pizzaRepository.DeleteTag(tag);
            return RedirectToAction("Index");
        }
    }
}
