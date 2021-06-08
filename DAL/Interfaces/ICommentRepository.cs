using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICommentRepository
    {
        Task Create(Comment item);
        Task Update(Comment item);
        Task Delete(Guid id);
        Task<ICollection<Comment>> GetAll();
        Task<Comment> Get(Guid id);
    }
}
