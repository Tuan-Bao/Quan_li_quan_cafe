using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_li_quan_cafe.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int? UserId { get; set; } // Nullable in case User is deleted
        public string Status { get; set; } = "Pending";
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public virtual User? User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }
    }
}
