using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3
{
    public class Product
    {
        public Guid Id { get; protected set; }
        public string Category { get; protected set; }
        public decimal Price { get; protected set; }
        public string Name { get; protected set; }

        public Product(Guid id, string category, decimal price, string name)
        {
            Id = id;
            Category = category;
            Price = price;
            Name = name;
        }
    }
}
