using HappyShop.Core.Domain;
using HappyShop.Core.Repositories;
using HappyShop.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyShop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopContext db = new ShopContext();
        public async Task<Product> GetAsync(Guid id)
            => await Task.FromResult(db.Products.SingleOrDefault(x => x.Id == id));

        public async Task<List<Product>> GetProductsAsync()
            => await Task.FromResult(db.Products.Where(x => x.IsArchived == false).ToList());
        public async Task AddAsync(Product product)
        {
            db.Add(product);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Product product)
        {
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Product product)
        {
            db.Remove(product);
            await Task.CompletedTask;
        }
    }
}
