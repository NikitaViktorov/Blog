using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITagRepository
    {
        Task Create(Tag item);
        Task Update(Tag item);
        Task Delete(Guid id);
        Task<ICollection<Tag>> GetAll();
        Task<Tag> Get(Guid id);
        Task<Tag> GetByText(string text);
    }
}
