using HappyShop.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication3.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        public static List<Category> CategoryRepository = new List<Category>
        {
            new Category("Clothes"),
            new Category("Zabawki"),
            new Category("Elektronika"),
            new Category("Dla dorosłych")
        };

        [HttpPost("{name}")]
        public Category Create([FromRoute]string name)
        {
            var category = CategoryRepository.SingleOrDefault(x => x.Name.ToLower() == name.ToLower().Trim());
            if(category != null)
            {
                throw new Exception($"Category with name: {category.Name} already exists.");
            }
            var newCategory = new Category(name);
            CategoryRepository.Add(newCategory);
            return newCategory;
        }

        [HttpGet]
        public List<Category> GetAllListOfCategry()
        {
            return CategoryRepository;
        }

    }
}