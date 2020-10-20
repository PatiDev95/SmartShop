using HappyShop.Core.Domain;

namespace WebApplication3.Commands
{
    public class CreateProduct
    {
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductCondition ProductCondition { get; set; }
    }
}
