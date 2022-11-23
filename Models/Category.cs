using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_razor_layout.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }


        public List<Pizza>? Pizza { get; set; }

    }
}
