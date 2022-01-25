using System;
using System.Collections.Generic;

#nullable disable

namespace DataAcessLayer.Models
{
    public partial class EventDetail
    {
        public int EventId { get; set; }
        public int ProductId { get; set; }
        public int NewPrice { get; set; }

        public virtual Event Event { get; set; }
        public virtual Product Product { get; set; }
    }
}
