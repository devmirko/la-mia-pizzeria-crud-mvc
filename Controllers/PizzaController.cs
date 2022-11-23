using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_razor_layout.Data;
using Microsoft.Extensions.Hosting;
using la_mia_pizzeria_razor_layout.Models;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using la_mia_pizzeria_razor_layout.Models.Form;
using Microsoft.SqlServer.Server;

namespace la_mia_pizzeria_razor_layout.Controllers
{
    public class PizzaController : Controller
    {
        PizzaDbContext db;

        public PizzaController() : base()
        {
            db = new PizzaDbContext();
        }

        public IActionResult Index()
        {
            //PizzaDbContext db = new PizzaDbContext();

            List<Pizza> listaPizza = db.Pizza.ToList();

            return View(listaPizza);
        }

        public IActionResult Detail(int id)
        {

            //PizzaDbContext db = new PizzaDbContext();

            Pizza pizza = db.Pizza.Where(p => p.Id == id).FirstOrDefault();

            return View(pizza);
        }

        public IActionResult Create()
        {
            //creiamo una nuova variabile con l'instanza di pizzaform
            PizzaForm pizzaData = new PizzaForm();

            //assegniamo alla propieta pizza di pizzaData una nuova istanza dell'oogetto pizza
            pizzaData.Pizza = new Pizza();

            //assegniamo alla lista Categories di pizzaData una lista con tutte le categorie del db
            pizzaData.Categories = db.Categories.ToList();

            return View(pizzaData);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaForm pizzaData)
        {
            if (!ModelState.IsValid)
            {
                //facciamo ritornare alla vista l'oggetto con tutte le categorie 
                pizzaData.Categories = db.Categories.ToList();
                return View(pizzaData);
            }

            db.Pizza.Add(pizzaData.Pizza);
            db.SaveChanges();

            return RedirectToAction("Index");



        }

        public IActionResult Update(int id)
        {
            Pizza pizza = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (pizza == null)
                return NotFound();

            //return View() --> non funziona perchè non ha la memoria della post
            return View(pizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id,Pizza formData)
        {
            if (!ModelState.IsValid)
            {

                return View("Update");
            }

            Pizza pizza = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (pizza == null) {

                return NotFound();

            }
               

            pizza.Name = formData.Name;
            pizza.Description = formData.Description;
            pizza.Image = formData.Image;
            pizza.Price = formData.Price;

            
            db.SaveChanges();

            return RedirectToAction("Index");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Pizza pizza = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (pizza == null)
            {

                return NotFound();

            }

            db.Pizza.Remove(pizza);
            db.SaveChanges();

            return RedirectToAction("Index");


        }











    }


}

