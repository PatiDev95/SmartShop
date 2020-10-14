using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Controllers
{
    [Route("[controller]")]
    public partial class ProductsController : Controller
    {
        public static List<Product> ProductRepository = new List<Product>
        {
            new Product(Guid.NewGuid(), "Clothes", 18, "T-shirt"),
            new Product(Guid.NewGuid(), "Clothes", 100, "Jeans"),
            new Product(Guid.NewGuid(), "Clothes", 350, "Skirt"),
            new Product(Guid.NewGuid(), "Clothes", 250, "Jacket"),
            new Product(Guid.NewGuid(), "Clothes", 40, "Tights"),
            new Product(Guid.NewGuid(), "Clothes", 60, "T-shirt"),
            new Product(Guid.NewGuid(), "Clothes", 25, "Socks"),
            new Product(Guid.NewGuid(), "Clothes", 40, "Pants"),
            new Product(Guid.NewGuid(), "Clothes", 100, "Shirt"),
            new Product(Guid.NewGuid(), "Zabawki", 20, "Lalka"),
            new Product(Guid.NewGuid(), "Zabawki", 50, "Samochód"),
            new Product(Guid.NewGuid(), "Zabawki", 9, "Bańki Mydlane"),
            new Product(Guid.NewGuid(), "Zabawki", 2, "Kolorowanka"),
            new Product(Guid.NewGuid(), "Zabawki", 3, "Kreda"),
            new Product(Guid.NewGuid(), "Zabawki", 200, "Pociąg"),
            new Product(Guid.NewGuid(), "Elektronika", 5000, "Laptop"),
            new Product(Guid.NewGuid(), "Elektronika", 3500, "Telefon"),
            new Product(Guid.NewGuid(), "Elektronika", 10000, "Rzutnik"),
            new Product(Guid.NewGuid(), "Elektronika", 300, "Głośniki"),
            new Product(Guid.NewGuid(), "Elektronika", 300, "SmartWatch"),
        };

        [HttpGet]
        public List<Product> GetAllListOfProducts()
        {
            return ProductRepository;
        }

        [HttpGet("category/{category}")]
        public IActionResult GetProductsByCategory(string category)
        {
            var listOfProducts = ProductRepository.Where(x => x.Category.ToLower() == category.ToLower()).ToList();
            if (listOfProducts.Count() == 0)
            {
                return NotFound("Category not found.");
            }
            else
            {
                return Ok(listOfProducts);
            }
        }


    }
}
