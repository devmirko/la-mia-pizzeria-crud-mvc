using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using la_mia_pizzeria_razor_layout.Validator;

namespace la_mia_pizzeria_razor_layout.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(20, ErrorMessage = "Il titolo non può essere oltre i 20 caratteri")]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [AlmenoCinqueIngredienti]
        [StringLength(100, ErrorMessage = "Il titolo non può essere oltre i 100 caratteri")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [ImageRequired]
        public string Image { get; set; }
        [Range(0,100)]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [PrezzoPizza]
        public double Price { get; set; }

        public Pizza()
        {

        }

        public Pizza(string name, string description, string image, double price)
        {
            this.Name = name;
            this.Description = description;
            this.Image = image;
            this.Price = price;

        }
    }
}
