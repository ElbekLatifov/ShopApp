using System;
using System.ComponentModel.DataAnnotations;

namespace ShopSystem.Models
{
    public class Cashbox
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ShopId { get; set; }
    }
}
