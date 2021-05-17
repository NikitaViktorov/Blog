using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Create(T item);
        Task Update(T item);
        Task Delete(Guid id);
        Task<ICollection<T>> GetAll();
        Task<T> Get(Guid id);
    }
}
