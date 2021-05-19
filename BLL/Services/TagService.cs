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
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TagService(IUnitOfWork unitOfWork)
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
        public async Task Create(TagDTO tagDTO)
        {
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<TagDTO, Tag>());
            //var mapper = new Mapper(config);
            await _unitOfWork.Tags.Create(_mapper.Map<Tag>(tagDTO));
        }

        public async Task Delete(Guid id)
        {
            if (await _unitOfWork.Tags.Get(id) == null)
                throw new TagException("Tag doesn't exist.You don't delete this tag");
            await _unitOfWork.Tags.Delete(id);
        }

        public async Task<TagDTO> Get(Guid id)
        {
            var tag = _mapper.Map<TagDTO>(await _unitOfWork.Tags.Get(id));
            //if (await _unitOfWork.Tags.Get(id) == null)
            //    throw new TagException("Tag doesn't exist.You don't get this tag");

            //var config = new MapperConfiguration(cfg => cfg.CreateMap<Tag, TagDTO>());
            //var mapper = new Mapper(config);

            //return _mapper.Map<TagDTO>(await _unitOfWork.Tags.Get(id));
            return tag ?? throw new TagException("Tag doesn't exist.You don't get this tag");
        }

        public async Task<ICollection<TagDTO>> GetAll()
        {
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<Tag, TagDTO>());
            //var mapper = new Mapper(config);
            var tags = _mapper.Map<ICollection<TagDTO>>(await _unitOfWork.Tags.GetAll());
            //if (_mapper.Map<ICollection<TagDTO>>(await _unitOfWork.Tags.GetAll()).Count == 0)
            //    throw new TagException("List of tags is empty");

            //return _mapper.Map<ICollection<TagDTO>>(await _unitOfWork.Tags.GetAll());

            return tags ?? throw new TagException("List of tags is empty");
        }

        public async Task Update(Guid id, TagDTO tagDTO)
        {
            if (await _unitOfWork.Tags.Get(id) == null)
                throw new TagException("Tag doesn't exist.You don't update this tag");
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<TagDTO, Tag>());
            //var mapper = new Mapper(config);
            var updateTag = _mapper.Map<Tag>(tagDTO);
            updateTag.Id = id;
            await _unitOfWork.Tags.Update(updateTag);
        }
    }
}
