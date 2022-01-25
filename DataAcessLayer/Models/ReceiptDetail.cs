using System;
using System.Collections.Generic;

#nullable disable

namespace DataAcessLayer.Models
{
    public partial class ReceiptDetail
    {
        public int ReceiptId { get; set; }
        public int ProductId { get; set; }
        public int BuyPrice { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Receipt Receipt { get; set; }
    }
}
