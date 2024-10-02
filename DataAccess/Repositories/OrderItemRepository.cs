using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quan_li_quan_cafe.Models;

namespace Quan_li_quan_cafe.DataAccess.Repositories
{
    public class OrderItemRepository : IRepository<OrderItem>
    {   
        private readonly CoffeeShopDbContext _context;
        public OrderItemRepository(CoffeeShopDbContext context) 
        { 
            _context = context;
        }
        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItems
                .Include(oi => oi.Order)
                .Include(oi => oi.Order)
                .ToListAsync();
        }
        public async Task<OrderItem> GetByIdAsync(int id)
        {
            var orderItem = await _context.OrderItems
                .Include(oi => oi.Order)
                .Include(oi => oi.Coffee)
                .FirstOrDefaultAsync(oi => oi.OrderItemId == id);
            if (orderItem == null)
            {
                throw new Exception($"OrderItem with ID {id} not found.");
            }
            return orderItem;
        }
        public async Task AddAsync(OrderItem entity) 
        {
            await _context.OrderItems.AddAsync(entity);
            await _context.SaveChangesAsync(); 
        }
        public async Task UpdateAsync(int id, OrderItem entity)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                throw new KeyNotFoundException($"OrderItem with ID {id} not found.");
            }

            orderItem.CoffeeId = entity.CoffeeId;
            orderItem.Quantity = entity.Quantity;
            orderItem.UpdatedAt = DateTime.Now;

            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id) 
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
