using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface ICommentService
    {
        Task Create(Guid Articleid, CommentDTO commentDTO);
        Task Delete(Guid id);
        Task<CommentDTO> Get(Guid id);
        Task<ICollection<CommentDTO>> GetAll();
        Task Update(Guid id, CommentDTO commentDTO);
    }
}