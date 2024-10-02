using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_li_quan_cafe.Models;
using Quan_li_quan_cafe.DataAccess.Repositories;
using Quan_li_quan_cafe.Utils;

namespace Quan_li_quan_cafe.Controllers
{
    public class UserController : IController<User>
    {
        private readonly UserRepository _userRepository;
        

        public UserController(UserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> Create(User entity)
        {
            await _userRepository.AddAsync(entity);
            return entity;
        }

        public async Task<User> Update(int id, User entity)
        {
            await _userRepository.UpdateAsync(id, entity);
            var existingUser = await _userRepository.GetByIdAsync(id); 
            return existingUser;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception($"User with ID {id} not found.");
            }

            await _userRepository.DeleteAsync(id);
            return true;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
            {
                throw new Exception("Username not found.");
            }

            if (string.IsNullOrEmpty(user.PasswordHash))
            {
                throw new Exception("Password hash is missing.");
            }

            var isPasswordValid = PasswordHasher.VerifyPassword(password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new Exception("Password is incorrect.");
            }
            return user;
        }
    }
}
