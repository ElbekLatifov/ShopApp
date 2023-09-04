using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopSystem.Models;

public partial class Shop
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Owner { get; set; }
    public DateTime Created_time { get; set; }
}
