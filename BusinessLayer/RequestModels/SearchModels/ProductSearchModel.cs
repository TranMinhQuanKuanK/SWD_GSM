using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModels.SearchModels
{
    public class ProductSearchModel
    {
        public enum ProductSearchStatus
        {
            Selling,
            Disabled
        }
        public string SearchTerm { get; set; }
        public int? MinimumSellingPrice { get; set; }
        public int? MaximumSellingPrice { get; set; }
        public int? MinimumBuyingPrice { get; set; }
        public int? MaximumBuyingPrice { get; set; }
        public ProductSearchStatus? Status { get; set; }
    }
}
