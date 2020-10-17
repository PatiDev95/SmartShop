using System.Collections.Generic;
using WebApplication3.Models;

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
