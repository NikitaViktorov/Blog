using AutoMapper;
using BLL.AutoMapper;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Sevices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = AutoMapperProfile.InitializeAutoMapper().CreateMapper();
        }
        public async Task Create(UserDTO userDTO)
        {
            await _unitOfWork.Users.Create(_mapper.Map<User>(userDTO));
        }

        public async Task Delete(Guid id)
        {
            if (await _unitOfWork.Users.Get(id) == null)
                throw new UserException("You don't delete this user.User doesn't exist");
            await _unitOfWork.Users.Delete(id);
        }

        public async Task<UserDTO> Get(Guid id)
        {
            var user = await _unitOfWork.Users.Get(id);
            
            return _mapper.Map<UserDTO>(user) ?? throw new UserException("You don't get this user.User doesn't exist");
        }

        public async Task<ICollection<UserDTO>> GetAll()
        {
            var users = await _unitOfWork.Users.GetAll(); 
            
            return _mapper.Map<ICollection<UserDTO>>(users) ?? throw new UserException("List of Users is empty");
        }

        public async Task Update(Guid id, UserDTO userDTO)
        {
            if (await _unitOfWork.Users.Get(id) == null)
                throw new UserException("You don't update this user.User doesn't exist");
            
            var updateUser = _mapper.Map<User>(userDTO);
            updateUser.Id = id;

            await _unitOfWork.Users.Update(updateUser);
        }
    }
}
