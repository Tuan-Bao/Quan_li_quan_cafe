using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quan_li_quan_cafe.Models;

namespace Quan_li_quan_cafe.DataAccess.Repositories
{
    public class CoffeeRepository : IRepository<Coffee>
    {
        private readonly CoffeeShopDbContext _context;
        public CoffeeRepository(CoffeeShopDbContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<Coffee>> GetAllAsync()
        {
            return await _context.Coffees.ToListAsync();
        }
        public async Task<Coffee> GetByIdAsync(int id)
        {
            var coffee = await _context.Coffees.FindAsync(id);
            if (coffee == null)
            {
                throw new Exception($"Coffee with ID {id} not found.");
            }
            return coffee;
        }
        public async Task AddAsync(Coffee entity)
        {
            await _context.Coffees.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, Coffee entity)
        {
            var coffee = await _context.Coffees.FindAsync(id);
            if (coffee == null)
            {
                throw new KeyNotFoundException($"Coffee with ID {id} not found.");
            }

            coffee.Name = entity.Name;
            coffee.Price = entity.Price;
            coffee.Description = entity.Description;
            coffee.UpdatedAt = entity.UpdatedAt;

            _context.Coffees.Update(coffee);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var coffee = await _context.Coffees.FindAsync(id);
            if(coffee != null)
            {
                _context.Coffees.Remove(coffee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
