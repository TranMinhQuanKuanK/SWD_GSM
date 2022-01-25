using System;
using System.Collections.Generic;

#nullable disable

namespace DataAcessLayer.Models
{
    public partial class DailyRevenue
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int BrandId { get; set; }
        public int Revenue { get; set; }
        public DateTime Date { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Store Store { get; set; }
    }
}
