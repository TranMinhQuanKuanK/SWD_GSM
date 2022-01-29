using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ResponseModels.ErrorModels.StoreOwner
{
    public class ProductErrorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class DeleteProductErrorModel
    {
        public List<ProductErrorModel> InverseUnpackedProducts { get; set; }
    }
}
