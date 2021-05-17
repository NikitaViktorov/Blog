using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICommentService
    {
        Task Create(string title, CommentDTO commentDTO);
        //Task Create(CommentDTO commentDTO);
        Task Delete(Guid id);
        Task<CommentDTO> Get(Guid id);
        Task<ICollection<CommentDTO>> GetAll();
        Task Update(Guid id, CommentDTO commentDTO);
    }
}
