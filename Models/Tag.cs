using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_razor_layout.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string Title { get; set; }

        //relazione molti a molti con Post

        public List<Pizza>? Pizzas { get; set; }
    }
}
