using System;
using System.Collections.Generic;

#nullable disable

namespace DataAcessLayer.Models
{
    public partial class Stock
    {
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Status { get; set; }

        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
