using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSystem.Models
{
    public class AddedProduct
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public double PriceCome { get; set; }
        public double NewPriceCome { get; set; }
        public double PriceGo { get; set; }
        public double NewPriceGo { get; set; }
        public DateTime Added_time { get; set; }
        public int Count { get; set; }
        public int AddedCount { get; set; }
    }
}
