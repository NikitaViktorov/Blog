using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface ITagService
    {
        Task Create(TagDTO tagDTO);
        Task Delete(Guid id);
        Task<TagDTO> Get(Guid id);
        Task<ICollection<TagDTO>> GetAll();
        Task Update(Guid id, TagDTO tagDTO);
    }
}