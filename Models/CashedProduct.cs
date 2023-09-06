using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSystem.Models
{
    public class CashedProduct
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? Categoryid { get; set; }

        public string Title { get; set; }
        public string? Barcode { get; set; }
        public double PriceCome { get; set; }
        public double PriceGo { get; set; }
        public DateTime Created_time { get; set; }
        public DateTime CashedTime { get; set; }
        public Guid? ShopId { get; set; }
        public int Count { get; set; }
        public int Totalcount { get; set; }
    }
}
