using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_li_quan_cafe.DataAccess.Repositories;
using Quan_li_quan_cafe.Models;

namespace Quan_li_quan_cafe.Controllers
{
    public class OrderController : IController<Order>
    {
        private readonly OrderRepository _orderRepository;
        
        public OrderController(OrderRepository orderRepository) 
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Create(Order order)
        {
            await _orderRepository.AddAsync(order);
            return order;
        }

        public async Task<Order> Update(int id, Order order)
        {
            await _orderRepository.UpdateAsync(id, order);
            var existingOrder = await _orderRepository.GetByIdAsync(id);
            return existingOrder;
        }

        public async Task<bool> Delete(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order != null)
            {
                throw new Exception($"Order with ID {id} not found.");
            }

            await _orderRepository.DeleteAsync(id);
            return true;
        }

        public async Task<Order> GetById(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _orderRepository.GetAllAsync();
        }
    }
}
