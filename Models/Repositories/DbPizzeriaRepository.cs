using la_mia_pizzeria_razor_layout.Data;
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

        public Category GetByIdCategory(int id)
        {
            return db.Categories.Where(cat => cat.Id == id).Include(p => p.Pizza).FirstOrDefault();
        }

        public Tag GetByIdTag(int id)
        {
            return db.Tags.Where(t => t.Id == id).Include(p => p.Pizzas).FirstOrDefault();
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

        public void DeleteCategory(Category category)
        {
            db.Categories.Remove(category);
            db.SaveChanges();
        }

        public void DeleteTag(Tag tag)
        {
            db.Tags.Remove(tag);
            db.SaveChanges();
        }


        public List<Category> AllCategory()
        {
            return db.Categories.ToList();
        }




        public List<Tag> AllTag()
        {
            return db.Tags.ToList();
        }

        public void AddCategory(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public void AddTag(Tag tag)
        {
            db.Tags.Add(tag);
            db.SaveChanges();


        }

       












    }
}
