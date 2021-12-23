using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.AutoMapper;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Sevices
{
    public class TagService : ITagService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _mapper = AutoMapperProfile.InitializeAutoMapper().CreateMapper();
        }

        public async Task Create(TagDTO tagDTO)
        {
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

            return tag ?? throw new TagException("Tag doesn't exist.You don't get this tag");
        }

        public async Task<ICollection<TagDTO>> GetAll()
        {
            var tags = _mapper.Map<ICollection<TagDTO>>(await _unitOfWork.Tags.GetAll());

            return tags ?? throw new TagException("List of tags is empty");
        }

        public async Task Update(Guid id, TagDTO tagDTO)
        {
            if (await _unitOfWork.Tags.Get(id) == null)
                throw new TagException("Tag doesn't exist.You don't update this tag");

            var updateTag = _mapper.Map<Tag>(tagDTO,
                t => t.AfterMap(((o,
                    tag) => tag.Id = id)));
            await _unitOfWork.Tags.Update(updateTag);
        }
    }
}