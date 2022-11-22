using la_mia_pizzeria_razor_layout.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_razor_layout.Data
{
    public class PizzaDbContext : DbContext
    {
        public DbSet<Pizza> Pizza { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db_pizza;Integrated Security=True;Encrypt=false;");

        }

    }
}
