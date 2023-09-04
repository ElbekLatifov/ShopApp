using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopSystem.Models;

public partial class User
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public bool? RememberMe { get; set; }
}
