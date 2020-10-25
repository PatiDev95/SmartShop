using HappyShop.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HappyShop.Infrastructure.Services
{
    public interface IProductService
    {
        Task<Product> GetAsync(Guid id);
        Task<Product> GetAsync(string category);
        Task<List<Product>> GetAllAsync();
        Task CreateAsync(string category, decimal price, string name, string description, ProductCondition productCondition);
        Task BuyProduct(Guid id);
        Task SearchBrand();
        Task Search(string productName, string brandName, decimal? from, decimal? to);
    }
}
