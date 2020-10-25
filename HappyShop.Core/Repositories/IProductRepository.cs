using HappyShop.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HappyShop.Core.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(Guid id);
        Task<List<Product>> GetProductsAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}
