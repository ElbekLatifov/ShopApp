using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSystem.Models
{
    public class Subcategory
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }
        public DateTime Created_time { get; set; }
        public Guid? ShopId { get; set; }
        public Guid? ParentId { get; set; }
    }
}
