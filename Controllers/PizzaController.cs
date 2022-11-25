using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_razor_layout.Data;
using Microsoft.Extensions.Hosting;
using la_mia_pizzeria_razor_layout.Models;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using la_mia_pizzeria_razor_layout.Models.Form;
using la_mia_pizzeria_razor_layout.Models.Repositories;
using Microsoft.SqlServer.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace la_mia_pizzeria_razor_layout.Controllers
{
    public class PizzaController : Controller
    {
        PizzaDbContext db;

        IDbPizzaRepository pizzaRepository;



        public PizzaController(IDbPizzaRepository _pizzaRepository) : base()
        {
            db = new PizzaDbContext();

            pizzaRepository = _pizzaRepository;
        }

        public IActionResult Index()
        {
            //PizzaDbContext db = new PizzaDbContext();

            //inserimento metodo all
            List<Pizza> listaPizza = pizzaRepository.All();
            //fine

            return View(listaPizza);
        }

        public IActionResult Detail(int id)
        {

            //PizzaDbContext db = new PizzaDbContext();

            //inserimento metodo GetById
            Pizza pizza = pizzaRepository.GetById(id);
            //fine

            if (pizza == null)
            {
                return NotFound("l'elemento non è stato trovato");
            }


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

            //assegniamo alla propietà tags di pizzaData una nuova lista di tipo SelectListItem
            pizzaData.Tags = new List<SelectListItem>();

            //creiamo una nuova lista di tipo tag contente tutti i tag del db con una query
            List<Tag> tagList = db.Tags.ToList();

            //facciamo un for in taglist e andiamo ad aggiungere ogni iterazione alla lista Tags instaziando un oggetto SelectListItem
            foreach (Tag tag in tagList)
            {
                pizzaData.Tags.Add(new SelectListItem(tag.Title, tag.Id.ToString()));
            }



            return View(pizzaData);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaForm pizzaData)
        {
            if (!ModelState.IsValid)
            {
                //facciamo ritornare alla vista l'oggetto con tutte le categorie ed i tags
                pizzaData.Categories = db.Categories.ToList();
                pizzaData.Tags = new List<SelectListItem>();


                //creiamo una nuova lista di tipo tag contente tutti i tag del db con una query
                List<Tag> tagList = db.Tags.ToList();

                //facciamo un for in taglist e andiamo ad aggiungere ogni iterazione alla lista Tags instaziando un oggetto SelectListItem
                foreach (Tag tag in tagList)
                {
                    pizzaData.Tags.Add(new SelectListItem(tag.Title, tag.Id.ToString()));
                }

                return View(pizzaData);
            }

            //implementazione del metodo create
            //associazione dei tag selezionato dall'utente al modello
            //creando una nuova lista di tipo tag, dove poi andremo ad aggiungere i tag selezionati dall'utente
            pizzaRepository.Create(pizzaData.Pizza, pizzaData.SelectedTags);
            //fine


            return RedirectToAction("Index");



        }

        public IActionResult Update(int id)
        {
            Pizza pizza = pizzaRepository.GetById(id);

            if (pizza == null)
                return NotFound();

            //creiamo una nuova variabile con l'instanza di pizzaform
            PizzaForm pizzaData = new PizzaForm();

            pizzaData.Pizza = pizza;
            pizzaData.Categories = db.Categories.ToList();
            pizzaData.Tags = new List<SelectListItem>();

            List<Tag> tagsList = db.Tags.ToList();

            foreach (Tag tag in tagsList)
            {
                pizzaData.Tags.Add(new SelectListItem(
                    tag.Title,
                    tag.Id.ToString(),
                    //la funzione Any ritorna true sui tag presenti nell'oggetto pizza che sono uguali a quelli del db.
                    pizza.Tags.Any(t => t.Id == tag.Id)
                    ));
            }


            return View(pizzaData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaForm pizzaData)
        {

            if (!ModelState.IsValid)
            {
                pizzaData.Pizza.Id = id;
                pizzaData.Categories = db.Categories.ToList();
                pizzaData.Tags = new List<SelectListItem>();


                //creiamo una nuova lista di tipo tag contente tutti i tag del db con una query
                List<Tag> tagList = db.Tags.ToList();

                //facciamo un for in taglist e andiamo ad aggiungere ogni iterazione alla lista Tags instaziando un oggetto SelectListItem
                foreach (Tag tag in tagList)
                {
                    pizzaData.Tags.Add(new SelectListItem(tag.Title, tag.Id.ToString()));
                }

                return View(pizzaData);
            }

            Pizza pizza = pizzaRepository.GetById(id);

            if (pizza == null) {

                return NotFound();

            }

            pizzaRepository.Update(pizza, pizzaData.Pizza, pizzaData.SelectedTags);
            
            return RedirectToAction("Index");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Pizza pizza = pizzaRepository.GetById(id);

            if (pizza == null)
            {

                return NotFound();

            }

            pizzaRepository.Delete(pizza);
            return RedirectToAction("Index");


        }











    }


}

