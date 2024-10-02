using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_li_quan_cafe.Controllers
{
    public interface IController<T>
    {
        Task<T> Create(T entity);
        Task<T> Update(int id, T entity);
        Task<bool> Delete(int id);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
    }
}
