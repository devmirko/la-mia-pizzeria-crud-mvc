﻿using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_razor_layout.Data;
using Microsoft.Extensions.Hosting;
using la_mia_pizzeria_razor_layout.Models;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using la_mia_pizzeria_razor_layout.Models.Form;
using Microsoft.SqlServer.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

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

            //inserimento metodo all
            List<Pizza> listaPizza = db.Pizza.Include(pizza => pizza.Category).ToList();
            //fine

            return View(listaPizza);
        }

        public IActionResult Detail(int id)
        {

            //PizzaDbContext db = new PizzaDbContext();

            //inserimento metodo GetById
            Pizza pizza = db.Pizza.Where(p => p.Id == id).Include("Category").Include("Tags").FirstOrDefault();
            //fine

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
            pizzaData.Pizza.Tags = new List<Tag>();

            foreach (int tagId in pizzaData.SelectedTags)
            {
                //ad ogni iterazione vai nella tabella tags e trova i tags uguali a quelli che a selezionato l'utente
                Tag tag = db.Tags.Where(t => t.Id == tagId).FirstOrDefault();
                //le iterazioni uguali vengono aggiunte al database
                pizzaData.Pizza.Tags.Add(tag);


            }


            //salviamo l'instanza pizza che si trova in pizzadata nel db
            db.Pizza.Add(pizzaData.Pizza);
            db.SaveChanges();
            //fine
            return RedirectToAction("Index");



        }

        public IActionResult Update(int id)
        {
            Pizza pizza = db.Pizza.Where(pizza => pizza.Id == id).Include(p => p.Tags).FirstOrDefault();

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
            //assegniamo al ID di pizza presente in pizzaData l'id passato dal form
            //pizzaData.Pizza.Id = id;

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

            Pizza pizza = db.Pizza.Where(pizza => pizza.Id == id).Include(p => p.Tags).FirstOrDefault();

            if (pizza == null) {

                return NotFound();

            }
               
            //inserimento funzione update
            pizza.Name = pizzaData.Pizza.Name;
            pizza.Description = pizzaData.Pizza.Description;
            pizza.Image = pizzaData.Pizza.Image;
            pizza.Price = pizzaData.Pizza.Price;
            pizza.CategoryId = pizzaData.Pizza.CategoryId;

            //eliminiamo i tags presenti nel oggetto presente sul db
            pizza.Tags.Clear();

            //se la lista SelectedTags ci arriva nulla la instanziamo con una lista vuota per farli passare la validazione
            if (pizzaData.SelectedTags == null)
            {
                pizzaData.SelectedTags = new List<int>();
            }

            foreach (int tagId in pizzaData.SelectedTags)
            {
                //ad ogni iterazione vai nella tabella tags e trova i tags uguali a quelli che a selezionato l'utente
                Tag tag = db.Tags.Where(t => t.Id == tagId).FirstOrDefault();
                //le iterazioni uguali vengono aggiunte al database
                pizza.Tags.Add(tag);


            }



            db.SaveChanges();
            //fine
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

            //inserimento funzione delete
            db.Pizza.Remove(pizza);
            db.SaveChanges();
            //fine
            return RedirectToAction("Index");


        }











    }


}

