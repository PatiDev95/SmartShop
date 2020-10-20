using HappyShop.Core.Domain;
using System.Collections.Generic;

namespace WebApplication3.DTO
{
    public class SearchResult
    {
        public List<Product> Result { get; set; }

        public List<GroupDetail> CategoryDetails { get; set; }
        public List<GroupDetail> BrandDetails { get; set; }
        public List<GroupDetail> PriceRangeDetails { get; set; }

    }        
}
