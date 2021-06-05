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
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = AutoMapperProfile.InitializeAutoMapper().CreateMapper();
        }
        public async Task Create(Guid ArticleId, CommentDTO commentDTO)
        { 

            var article = await _unitOfWork.Articles.Get(ArticleId);

            if (article == null) throw new CommentException("Article doesn't exist.You don't create commentary because of that!");
            
            var comment = _mapper.Map<Comment>(commentDTO);

            comment.ArticleId = article.Id;

            await _unitOfWork.Comments.Create(comment);
        }

        public async Task Delete(Guid id)
        {
            if (await _unitOfWork.Comments.Get(id) == null)
                throw new CommentException("You don't delete commentary.Because the commentary doesn't exist");

            await _unitOfWork.Comments.Delete(id);
        }

        public async Task<CommentDTO> Get(Guid id)
        {
            var comment = await _unitOfWork.Comments.Get(id);
            
            return _mapper.Map<CommentDTO>(comment) ?? throw new CommentException("Commentar doesn't exist");
        }

        public async Task<ICollection<CommentDTO>> GetAll()
        {
            var comments = _mapper.Map<ICollection<CommentDTO>>(await _unitOfWork.Comments.GetAll());
            
            return comments ?? throw new CommentException("List of commentaries is empty");
        }

        public async Task Update(Guid id, CommentDTO commentDTO)
        {
            Comment comment = await _unitOfWork.Comments.Get(id);
            if (comment == null)
                throw new CommentException("You can't update this comment.Because commentary doen't exist!");
            
            var updateComment = _mapper.Map<Comment>(commentDTO);
            updateComment.Id = id;
            updateComment.ArticleId = comment.ArticleId;
            await _unitOfWork.Comments.Update(updateComment);
        }
    }
}
