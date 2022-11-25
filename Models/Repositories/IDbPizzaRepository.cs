using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_razor_layout.Models.Repositories
{
    public interface IDbPizzaRepository
    {
        List<Pizza> All();

        List<Category> AllCategory();

        List<Tag> AllTag();

        void Create(Pizza post, List<int> selectedTags);

        void Delete(Pizza post);

        Pizza GetById(int id);

        Category GetByIdCategory(int id);

        Tag GetByIdTag(int id);

        void Update(Pizza pizza, Pizza pizzaForm, List<int>? selectedTags);

        void AddCategory(Category category);

        void AddTag(Tag tag);

        void DeleteCategory(Category category);

        void DeleteTag(Tag tag);



    }
}
