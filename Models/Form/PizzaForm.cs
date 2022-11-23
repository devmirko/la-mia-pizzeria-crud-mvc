﻿using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_razor_layout.Models.Form
{
    public class PizzaForm
    {
        public Pizza Pizza { get; set; }

        public List<Category>? Categories { get; set; }
    }
}
