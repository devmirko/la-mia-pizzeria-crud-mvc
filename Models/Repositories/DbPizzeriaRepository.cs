﻿using la_mia_pizzeria_razor_layout.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_razor_layout.Models.Repositories
{
    public class DbPizzeriaRepository : IDbPizzaRepository
    {
        private PizzaDbContext db;

        public DbPizzeriaRepository()
        {
            db = new PizzaDbContext();
        }

        public List<Pizza> All()
        {
            return db.Pizza.Include(pizza => pizza.Category).Include(pizza => pizza.Tags).ToList();
        }

        public Pizza GetById(int id)
        {
            return db.Pizza.Where(p => p.Id == id).Include("Category").Include("Tags").FirstOrDefault();
        }

        public void Create(Pizza pizza, List<int> selectedTags)
        {
            //associazione dei tag selezionato dall'utente al modello

            pizza.Tags = new List<Tag>();
            foreach (int tagId in selectedTags)
            {
                //todo: implementare repository tag?
                Tag tag = db.Tags.Where(t => t.Id == tagId).FirstOrDefault();
                pizza.Tags.Add(tag);
            }

            db.Pizza.Add(pizza);
            db.SaveChanges();
        }


        public void Update(Pizza pizza, Pizza pizzaData, List<int>? selectedTags)
        {

            if (selectedTags == null)
            {
                selectedTags = new List<int>();
            }


            pizza.Name = pizzaData.Name;
            pizza.Description = pizzaData.Description;
            pizza.Image = pizzaData.Image;
            pizza.Price = pizzaData.Price;
            pizza.CategoryId = pizzaData.CategoryId;

            pizza.Tags.Clear();


            foreach (int tagId in selectedTags)
            {
                Tag tag = db.Tags.Where(t => t.Id == tagId).FirstOrDefault();
                pizza.Tags.Add(tag);
            }

            
            db.SaveChanges();
        }

        public void Delete(Pizza pizza)
        {
            db.Pizza.Remove(pizza);
            db.SaveChanges();
        }







    }
}
