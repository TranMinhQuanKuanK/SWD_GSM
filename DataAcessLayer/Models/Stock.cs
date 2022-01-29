using System;
using System.Collections.Generic;

#nullable disable

namespace DataAcessLayer.Models
{
    public partial class Stock
    {
        public enum StockDetail
        {
            Normal,
            NearlyOutOfStock
        }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public StockDetail Status { get; set; }

        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
