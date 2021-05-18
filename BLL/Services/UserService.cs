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
            //_mapper = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<Tag, TagDTO>().ReverseMap();
            //    cfg.CreateMap<Article, ArticleDTO>().ReverseMap();
            //    cfg.CreateMap<User, UserDTO>().ReverseMap();
            //    cfg.CreateMap<Comment, CommentDTO>().ReverseMap();
            //}).CreateMapper();
            _mapper = AutoMapperProfile.InitializeAutoMapper().CreateMapper();
        }
        public async Task Create(UserDTO userDTO)
        {
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO,User>());
            //var mapper = new Mapper(config);
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
            if (await _unitOfWork.Users.Get(id) == null)
                throw new UserException("You don't get this user.User doesn't exist");
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>());
            //var mapper = new Mapper(config);
            return _mapper.Map<UserDTO>(await _unitOfWork.Users.Get(id));
        }

        public async Task<ICollection<UserDTO>> GetAll()
        {
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>());
            //var mapper = new Mapper(config);
            if (_mapper.Map<ICollection<UserDTO>>(await _unitOfWork.Users.GetAll()).Count == 0)
                throw new UserException("List of Users is empty");
            return _mapper.Map<ICollection<UserDTO>>(await _unitOfWork.Users.GetAll());
        }

        public async Task Update(Guid id, UserDTO userDTO)
        {
            if (await _unitOfWork.Users.Get(id) == null)
                throw new UserException("You don't update this user.User doesn't exist");
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>());
            //var mapper = new Mapper(config);
            var updateUser = _mapper.Map<User>(userDTO);
            updateUser.Id = id;
            await _unitOfWork.Users.Update(updateUser);
        }
    }
}
