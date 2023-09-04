using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopSystem.Models;

public partial class Category
{
    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; }
    public DateTime Created_time { get; set; }
    public Guid ShopId { get; set; }
}
