using HappyShop.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication3.Commands;
using WebApplication3.DTO;
using WebApplication3.EF;

namespace WebApplication3.Controllers
{
    [Route("[controller]")]
    public partial class ProductsController : Controller
    {
        private readonly ShopContext db = new ShopContext();

        [HttpGet]
        public List<Product> GetAllListOfProducts()
        {
            return db.Products.Where(x => x.IsArchived == false).ToList();
        }

        [HttpGet("category/{category}")]
        public IActionResult GetProductsByCategory(string category)
        {
            var listOfProducts = db.Products.Where(x => x.Category.ToLower() == category.ToLower() && x.IsArchived == false).ToList();
            if (listOfProducts.Count() == 0)
            {
                return NotFound("Category not found.");
            }
            else
            {
                return Ok(listOfProducts);
            }
        }

        [HttpGet("product/{id}")]
        public Product GetProductById(Guid id)
        {
            var product =  db.Products.SingleOrDefault(x => x.Id == id);
            if(product != null)
            {
                return product;
            }
            else
            {
                throw new Exception($"There is no product with this id: {id}");
            }
            

        }

        [HttpPost]
        public ActionResult<Product> Create([FromBody] CreateProduct command)
        {
            var newProduct = new Product(command.Category, command.Price, command.Name, command.Description, command.ProductCondition);
            db.Products.Add(newProduct);
            db.SaveChanges();
            return Created($"/product/{newProduct.Id}", newProduct);
        }

        [HttpGet("search")]
        public SearchResult Search([FromQuery]string productName, [FromQuery]string brandName, [FromQuery]decimal? from, [FromQuery]decimal? to)
        {
            var result = db.Products
                .Where(x =>
                    (x.IsArchived == false) &&
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
            return db.Products.Where(x => x.Brand != null).Select(x => x.Brand).Distinct().ToList();
        }

        [HttpPut("buyProduct")]
        public string BuyProduct(Guid id)
        {
            var result2 = db.Products.SingleOrDefault(x => x.Id == id);
            result2.BuyProduct();
            db.SaveChanges();
            return "You just bought the product. Your order is being processed.";
        }

        [HttpGet("main")]
        public object main()
        {
             return db.Products
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
