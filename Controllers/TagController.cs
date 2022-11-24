using la_mia_pizzeria_razor_layout.Data;
using la_mia_pizzeria_razor_layout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_razor_layout.Controllers
{
    public class TagController : Controller
    {

        PizzaDbContext db;

        public TagController() : base()
        {
            db = new PizzaDbContext();
        }
        public IActionResult Index()
        {
            List<Tag> tags = db.Tags.ToList();
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

            db.Tags.Add(tag);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Tag tag = db.Tags.Where(t => t.Id == id).Include(p => p.Pizzas).FirstOrDefault();
            if (tag == null)
                return NotFound();
            if (tag.Pizzas.Count() > 0)
            {
                return NotFound();
            }
            db.Tags.Remove(tag);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
