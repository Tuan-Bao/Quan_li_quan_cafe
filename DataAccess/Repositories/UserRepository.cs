using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quan_li_quan_cafe.Models;
using Quan_li_quan_cafe.Utils;

namespace Quan_li_quan_cafe.DataAccess.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly CoffeeShopDbContext _context;
        public UserRepository(CoffeeShopDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            return user;
        }
        public async Task AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, User entity)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            user.Username = entity.Username;
            user.PasswordHash = entity.PasswordHash;
            user.FullName = entity.FullName;
            user.Role = entity.Role;
            user.Position = entity.Position;
            user.HireDate = entity.HireDate;
            user.UpdatedAt = DateTime.Now;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async  void CreateSampleUser()
        {
            var sampleUser = new User
            {
                Username = "khoa",
                PasswordHash = PasswordHasher.HashPassword("456"),
                Role = "Employee",  
                FullName = "Anh Khoa",
                Position = "Nhân Viên"
            };

            await AddAsync(sampleUser);
        }
    }
}
