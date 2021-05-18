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
            //_mapper = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<Tag, TagDTO>().ReverseMap();
            //    cfg.CreateMap<Article, ArticleDTO>().ReverseMap();
            //    cfg.CreateMap<User, UserDTO>().ReverseMap();
            //    cfg.CreateMap<Comment, CommentDTO>().ReverseMap();
            //}).CreateMapper();
            _mapper = AutoMapperProfile.InitializeAutoMapper().CreateMapper();
        }
        //public async Task Create(CommentDTO commentDTO)
        //{
        //    var config = new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, Comment>());
        //    var mapper = new Mapper(config);
        //    var comment = mapper.Map<CommentDTO, Comment>(commentDTO);
        //    await _unitOfWork.Comments.Create(comment);
        //}
        public async Task Create(string title, CommentDTO commentDTO)
        {
            IEnumerable<Article> articles = await _unitOfWork.Articles.GetAll();
            IEnumerator<Article> articlesEnumerator = articles.GetEnumerator();
            Article article = null;
            while (articlesEnumerator.MoveNext())
            {
                if (articlesEnumerator.Current.Title == title)
                {
                    article = articlesEnumerator.Current;
                    break;
                }
            }
            if (article == null)
                throw new CommentException("Article doesn't exist.You don't create commentary because of that!");
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, Comment>());
            //var mapper = new Mapper(config);
            var comment = _mapper.Map<Comment>(commentDTO);
            comment.Article = article;
            //var article = await unitOfWork.Articles.Get(commentDTO.ArticleId);
            //comment.Article = article;
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
            if (await _unitOfWork.Comments.Get(id) == null)
                throw new CommentException("Commentar doesn't exist");
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<Comment,CommentDTO>());
            //var mapper = new Mapper(config);
            return _mapper.Map<CommentDTO>(await _unitOfWork.Comments.Get(id));
        }

        public async Task<ICollection<CommentDTO>> GetAll()
        {
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<Comment,CommentDTO>());
            //var mapper = new Mapper(config);
            if (_mapper.Map<ICollection<CommentDTO>>(await _unitOfWork.Comments.GetAll()).Count == 0)
                throw new CommentException("List of commentaries is empty");
            return _mapper.Map<ICollection<CommentDTO>>(await _unitOfWork.Comments.GetAll());
        }

        public async Task Update(Guid id, CommentDTO commentDTO)
        {
            if (await _unitOfWork.Comments.Get(id) == null)
                throw new CommentException("You can't update this comment.Because commentary doen't exist!");
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, Comment>());
            //var mapper = new Mapper(config);
            var updateComment = _mapper.Map<Comment>(commentDTO);
            updateComment.Id = id;
            await _unitOfWork.Comments.Update(updateComment);
        }
    }
}
