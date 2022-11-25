using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_razor_layout.Models.Repositories
{
    public class InMemoryPizzaRepository : IDbPizzaRepository
    {
        public static List<Pizza> Pizzas = new List<Pizza>();

        public InMemoryPizzaRepository()
        {
            
        }

        //ritorna gli oggetti presenti nella lista
        public List<Pizza> All()
        {
            return Pizzas;
        }


        private static void TagToPost(Pizza pizza, List<int> selectedTags)
        {
            pizza.Category = new Category() { Id = 1, Title = "Fake cateogry" };

            foreach (int tagId in selectedTags)
            {
                pizza.Tags.Add(new Tag() { Id = tagId, Title = "Fake tag " + tagId });
            }
        }


        public void Create(Pizza pizza, List<int> selectedTags)
        {
            
            pizza.Id = Pizzas.Count;
            pizza.Category = new Category() { Id = 1, Title = "Fake cateogry" };

            
            pizza.Tags = new List<Tag>();

            TagToPost(pizza, selectedTags);
            

            Pizzas.Add(pizza);
        }

        public void Delete(Pizza pizza)
        {
            Pizzas.Remove(pizza);
        }

        public Pizza GetById(int id)
        {
            Pizza pizza = Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            pizza.Category = new Category() { Id = 1, Title = "Fake cateogry" };

            return pizza;
        }


        public void Update(Pizza pizza, Pizza pizzaData, List<int>? selectedTags)
        {
            pizza = pizzaData;
            pizza.Category = new Category() { Id = 1, Title = "Fake cateogry" };

            pizza.Tags = new List<Tag>();

          

            TagToPost(pizza, selectedTags);
            


        }



    }
}
