using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_li_quan_cafe.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? FullName { get; set; }
        public string? Role { get; set; }
        public string? Position { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public virtual ICollection<Order> Orders { get; set; }

        public User()
        {
            Orders = new HashSet<Order>();
        }
    }
}
