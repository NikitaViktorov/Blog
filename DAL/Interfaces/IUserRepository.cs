using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        Task Create(User item);
        Task Update(User item);
        Task Delete(Guid id);
        Task<ICollection<User>> GetAll();
        Task<User> Get(Guid id);
    }
}
