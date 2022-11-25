using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_razor_layout.Models.Repositories
{
    public interface IDbPizzaRepository
    {
        List<Pizza> All();
        void Create(Pizza post, List<int> selectedTags);

        void Delete(Pizza post);

        Pizza GetById(int id);

        void Update(Pizza pizza, Pizza pizzaForm, List<int>? selectedTags);

    }
}
