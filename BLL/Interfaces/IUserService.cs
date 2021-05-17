using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task Create(UserDTO userDTO);
        Task Delete(Guid id);
        Task<UserDTO> Get(Guid id);
        Task<ICollection<UserDTO>> GetAll();
        Task Update(Guid id, UserDTO userDTO);
    }
}
