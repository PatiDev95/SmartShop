using HappyShop.Core.Domain;
using HappyShop.Core.Repositories;
using HappyShop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HappyShop.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _productRepository.GetProductsAsync();
        }

        public Task<Product> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetAsync(string category)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(string category, decimal price, string name, string description, ProductCondition productCondition)
        {
            throw new NotImplementedException();
        }

        public Task Search(string productName, string brandName, decimal? from, decimal? to)
        {
            throw new NotImplementedException();
        }

        public Task SearchBrand()
        {
            throw new NotImplementedException();
        }

        public Task BuyProduct(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
