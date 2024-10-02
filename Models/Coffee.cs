using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_li_quan_cafe.Models
{
    public class Coffee
    {
        public int CoffeeId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public Coffee() 
        { 
            OrderItems = new HashSet<OrderItem>();
        }
    }
}
