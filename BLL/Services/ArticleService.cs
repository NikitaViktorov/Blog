using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Exceptions;
using BLL.AutoMapper;

namespace BLL.Sevices
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = AutoMapperProfile.InitializeAutoMapper().CreateMapper();
        }

        public async Task AddTag(Guid ArticleId,TagDTO tagDTO)
        {
            /*Tag tag = null*/;
            var article = await _unitOfWork.Articles.Get(ArticleId);
            //var tag = await _unitOfWork.Tags.Get(tagDTO.Id);
            if (article == null) throw new ArticleException("Article doesn't exist");

            article.Tags.Add(_mapper.Map<Tag>(tagDTO));
            //if (await _unitOfWork.Tags.Get(tagDTO.Id) == null)
            //{
            //    tag = _mapper.Map<Tag>(tagDTO); 
            //    await _unitOfWork.Tags.Create(tag);
            //}
            //else
            //{
            //    tag = _mapper.Map<Tag>(tagDTO);
            //}
                
            //var article = await _unitOfWork.Articles.Get(ArticleId);
            //article.Tags.Add(_mapper.Map<Tag>(tag));

            
        }

        public async Task Create(ArticleDTO articleDTO)
        {
            //if (await _unitOfWork.Tags.Get(articleDTO.TagId) == null) throw new ArticleException("You don't create article,because the article doen't exist");
            //else if(await _unitOfWork.Users.Get(articleDTO.UserId) == null) throw new ArticleException("You don't create article,because the user doen't exist");
            //else
            await _unitOfWork.Articles.Create(_mapper.Map<Article>(articleDTO));
        }
        public async Task Delete(Guid id)
        {
            var article = await _unitOfWork.Articles.Get(id) ?? throw new ArticleException("Tag doens't exist.You don't delete this tag!");
            
            await _unitOfWork.Articles.Delete(id);
        }

        public async Task<ArticleDTO> Get(Guid id)
        {
            var article = await _unitOfWork.Articles.Get(id) ?? throw new ArticleException("Article doesn't exist");

            return _mapper.Map<ArticleDTO>(article);
        }

        public async Task<ICollection<ArticleDTO>> GetAll()
        {
            var articles = await _unitOfWork.Articles.GetAll() ?? throw new ArticleException("List of articles is empty!");

            return _mapper.Map<ICollection<ArticleDTO>>(articles);
        }

        public async Task<ArticleDTO> GetArticleByText(string text)
        {
            var article = await _unitOfWork.Articles.GetArticleByText(text) ?? throw new ArticleException("Article doesn't exist");

            return _mapper.Map<ArticleDTO>(article);
        }

        public async Task<ICollection<ArticleDTO>> GetArticlesByTag(Guid id)
        {
            var articles = await _unitOfWork.Articles.GetArticlesByTag(id) ?? throw new ArticleException("List of tags is empty!");

            return _mapper.Map<ICollection<ArticleDTO>>(articles);
        }
        public async Task Update(Guid id, ArticleDTO articleDTO)
        {
            var article = await _unitOfWork.Articles.Get(id) ?? throw new ArticleException("Article doesn't exist!");
            
            var updateArticle = _mapper.Map<Article>(articleDTO);
            updateArticle.Id = id;

            await _unitOfWork.Articles.Update(updateArticle);
        }
    }
}
