using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopSystem.Models;

public partial class Product
{
    [Key]
    public Guid Id { get; set; }

    public Guid? Categoryid { get; set; }

    public string Title { get; set; }
    public string? Barcode { get; set; }
    public double PriceCome { get; set; }
    public double PriceGo { get; set; }
    public DateTime Created_time { get; set; }
    public Guid? ShopId { get; set; }
    public int Count { get; set; }
}
