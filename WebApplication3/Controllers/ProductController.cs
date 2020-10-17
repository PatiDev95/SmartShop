using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using WebApplication3.Commands;
using WebApplication3.DTO;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("[controller]")]
    public partial class ProductsController : Controller
    {
        public static List<Product> ProductRepository = new List<Product>
        {
            new Product("Clothes", 18, "T-shirt", "Biały zwykły.", ProductCondition.New).WithDiscount(10),
            new Product("Clothes", 100, "Jeans", "Niebieskie, materiał sztywny", ProductCondition.New).WithBrand("Big Star").WithWeight(0.5),
            new Product("Clothes", 350, "Skirt", "czerwona mindi", ProductCondition.New),
            new Product("Clothes", 250, "Jacket", "jeansowa niebieska", ProductCondition.New),
            new Product("Clothes", 40, "Tights", "w grochy czarne R3", ProductCondition.New),
            new Product("Clothes", 60, "T-shirt", "Biały zwykły, Czas na kawę?", ProductCondition.New),
            new Product("Clothes", 25, "Socks", "białe w różowe grochy, damskie", ProductCondition.New),
            new Product("Clothes", 40, "Pants", "czerwone koronkowe stringi", ProductCondition.New),
            new Product("Clothes", 100, "Shirt", "czarna mindi", ProductCondition.New),
            new Product("Zabawki", 20, "Lalka", "Roszponka", ProductCondition.New),
            new Product("Zabawki", 50, "Samochód", "Audi czerwone", ProductCondition.New),
            new Product("Zabawki", 9, "Bańki Mydlane", "z Myszka Miki", ProductCondition.New),
            new Product("Zabawki", 2, "Kolorowanka", "Księżniczki Disnay - Arielka", ProductCondition.New),
            new Product("Zabawki", 3, "Kreda", "24 kolory, duża", ProductCondition.New).WithWeight(1),
            new Product("Zabawki", 200, "Pociąg", "drewniany, 100 elementów", ProductCondition.New),
            new Product("Elektronika", 1000, "Laptop", "Lenovo", ProductCondition.New).WithWeight(4.5),
            new Product("Elektronika", 1500, "Laptop", "jdjdjd", ProductCondition.New),
            new Product("Elektronika", 2000, "Laptop", "jdjdjd", ProductCondition.New),
            new Product("Elektronika", 2500, "Laptop", "jdjdjd", ProductCondition.New),
            new Product("Elektronika", 3000, "Laptop", "jdjdjd", ProductCondition.New).WithDiscount(10),
            new Product("Elektronika", 3500, "Laptop", "jdjdjd", ProductCondition.New),
            new Product("Elektronika", 4000, "Laptop", "jdjdjd", ProductCondition.New).WithDiscount(50),
            new Product("Elektronika", 2000, "Iphone 8", "jdjdjd", ProductCondition.New).WithBrand("Apple"),
            new Product("Elektronika", 2500, "Iphone X", "jdjdjd", ProductCondition.New).WithBrand("Apple"),
            new Product("Elektronika", 2300, "Iphone XS", "jdjdjd", ProductCondition.New).WithBrand("Apple"),
            new Product("Elektronika", 5000, "Iphone 12", "jdjdjd", ProductCondition.New).WithBrand("Apple"),
            new Product("Elektronika", 6000, "Iphone 12 Pro", "jdjdjd", ProductCondition.New).WithBrand("Apple"),
            new Product("Elektronika", 7000, "Iphone 12 Pro Max", "jdjdjd", ProductCondition.New).WithDiscount(89).WithBrand("Apple"),
            new Product("Elektronika", 4500, "Iphone 12 Mini", "jdjdjd", ProductCondition.New).WithBrand("Apple"),
            new Product("Elektronika", 10000, "Rzutnik", "jdjdjd", ProductCondition.Used).WithBrand("Panasonic"),
            new Product("Elektronika", 9500, "Rzutnik", "nowy opis", ProductCondition.Used).WithBrand("Panasonic"),
            new Product("Elektronika", 300, "Głośniki", "jdjdjd", ProductCondition.New),
            new Product("Elektronika", 300, "SmartWatch", "jdjdjd", ProductCondition.New),
            new Product("Dla dorosłych", 50, "Prezerwatywa", "Najlepsze uczucie bliskości.", ProductCondition.New).WithBrand("DurexX")
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

        [HttpPost]
        public Product Create([FromBody] CreateProduct command)
        {
            var newProduct = new Product(command.Category, command.Price, command.Name, command.Description, command.ProductCondition);
            ProductRepository.Add(newProduct);
            return newProduct;
        }

        [HttpGet("search")]
        public SearchResult Search([FromQuery]string productName, [FromQuery]string brandName, [FromQuery]decimal? from, [FromQuery]decimal? to)
        {
            var result = ProductRepository
                .Where(x =>
                    (productName != null ? x.Name.ToLower().Contains(productName.ToLower()) : true) &&
                    (brandName != null ? (x.Brand != null ? x.Brand.ToLower().Contains(brandName.ToLower()) : false) : true) &&
                    (from != null ? x.RealPrice > from.Value : true) &&
                    (to != null ? x.RealPrice < to.Value : true))
                .ToList();

            return new SearchResult
            {
                Result = result,
                CategoryDetails = result.GroupBy(x => x.Category).Select(x => new GroupDetail(x.Key, x.Count())).ToList(),
                BrandDetails = result.GroupBy(x => x.Brand).Select(x => new GroupDetail(x.Key ?? "Others", x.Count())).ToList(),
                PriceRangeDetails = result.GroupBy(x => x.RealPrice<100 ? "0-99" : x.RealPrice<1000 ? "100-999" : x.RealPrice<10000 ? "1000-9999" : "10000 - ").Select(x => new GroupDetail(x.Key, x.Count())).ToList()
                
            };
        }

        [HttpGet("searchBrand")]
        public List<string> SearchBrand()
        {
            return ProductRepository.Where(x => x.Brand != null).Select(x => x.Brand).Distinct().ToList();
        }

        [HttpGet("main")]
        public object main()
        {
             return ProductRepository
                .FirstOrDefault(x => 
                (x.Price > 5000 || (x.Price < 100 && x.Price>10)) && 
                (x.Brand != null ? (x.Brand.Length>5) : false) &&
                (x.Category != "Dla dorosłych") &&
                (x.ProductCondition == ProductCondition.New || x.ProductCondition == ProductCondition.Used) &&
                (x.Discount == null)
                );

        }
    }
}
