using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_li_quan_cafe.Models;
using Quan_li_quan_cafe.DataAccess.Repositories;

namespace Quan_li_quan_cafe.Controllers
{
    public class CoffeeController : IController<Coffee>
    {
        private readonly CoffeeRepository _coffeeRepository;
        
        public CoffeeController(CoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;
        }
        
        public async Task<IEnumerable<Coffee>> GetAll()
        {
            return await _coffeeRepository.GetAllAsync();
        }
        
        public async Task<Coffee> GetById(int id)
        {
            return await _coffeeRepository.GetByIdAsync(id);
        }

        public async Task<Coffee> Create(Coffee entity)
        {
            await _coffeeRepository.AddAsync(entity);
            return entity;
        }

        public async Task<Coffee> Update(int id, Coffee entity)
        { 
            await _coffeeRepository.UpdateAsync(id, entity);
            var existingCoffee = await _coffeeRepository.GetByIdAsync(id);
            return existingCoffee;
        }

        public async Task<bool> Delete(int id)
        {
            var coffee = await _coffeeRepository.GetByIdAsync(id);
            if (coffee != null)
            {
                throw new Exception($"Coffee with ID {id} not found.");
            }

            await _coffeeRepository.DeleteAsync(id);
            return true;
        }
    }
}
