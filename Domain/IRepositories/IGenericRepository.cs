using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(long id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<long> AddAsync(T entity);
        Task<long> UpdateAsync(T entity);
        Task<long> DeleteAsync(long id);
        Task<dynamic?> AddMultiAsync(T[] entity);
    }
}
