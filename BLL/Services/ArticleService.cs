using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.AutoMapper;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = AutoMapperProfile.InitializeAutoMapper().CreateMapper();
        }

        public async Task AddTag(Guid articleId, TagDto tagDto)
        {
            var article = await _unitOfWork.Articles.Get(articleId);

            if (article == null) throw new ArticleException("Article doesn't exist");

            article.Tags.Add(_mapper.Map<Tag>(tagDto));
        }

        public async Task Create(ArticleDto articleDto)
        {
            articleDto.Id = Guid.NewGuid();
            var article = _mapper.Map<Article>(articleDto);

            var articles = article.Tags.ToList();
            for (var i = 0; i < article.Tags.Count; i++)
            {
                var tag = await _unitOfWork.Tags.GetByText(articles[i].Text);
                if (tag == null) continue;
                article.Tags.Remove(articles[i]);
                article.Tags.Add(tag);
            }

            await _unitOfWork.Articles.Create(article);
        }

        public async Task Delete(Guid id)
        {
            if (await _unitOfWork.Articles.Get(id) == null)
                throw new ArticleException("Tag doesn't exist.You don't delete this tag!");

            await _unitOfWork.Articles.Delete(id);
        }

        public async Task<ArticleDto> Get(Guid id)
        {
            var article = await _unitOfWork.Articles.Get(id) ?? throw new ArticleException("Article doesn't exist");

            return _mapper.Map<ArticleDto>(article);
        }

        public async Task<ICollection<ArticleDto>> GetAll()
        {
            var articles = await _unitOfWork.Articles.GetAll() ??
                           throw new ArticleException("List of articles is empty!");

            return _mapper.Map<ICollection<ArticleDto>>(articles);
        }

        public async Task<ArticleDto> GetArticleByText(string text)
        {
            var article = await _unitOfWork.Articles.GetArticleByText(text) ??
                          throw new ArticleException("Article doesn't exist");

            return _mapper.Map<ArticleDto>(article);
        }

        public async Task<ICollection<ArticleDto>> GetArticlesByTag(Guid tagId)
        {
            var articles = await _unitOfWork.Articles.GetArticlesByTag(tagId);
            if (articles == null) throw new ArticleException("Articles don't exist");
            return _mapper.Map<List<ArticleDto>>(articles);
        }

        public async Task<ICollection<ArticleDto>> GetUserArticles(Guid userId)
        {
            var articles = await _unitOfWork.Articles.GetUserArticles(userId);
            if (articles == null) throw new ArticleException("Articles don't exist");
            return _mapper.Map<List<ArticleDto>>(articles);
        }

        public async Task Update(Guid id, ArticleDto articleDto)
        {
            var updateArticle = await _unitOfWork.Articles.Get(id) ??
                                throw new ArticleException("Article doesn't exist!");

            var searchArticle = _mapper.Map<Article>(articleDto);

            var tags = searchArticle.Tags.ToList();

            updateArticle.Text = searchArticle.Text;
            updateArticle.Title = searchArticle.Title;
            updateArticle.UserId = articleDto.UserId;

            updateArticle.Tags.Clear();

            for (var i = 0; i < searchArticle.Tags.Count; i++)
            {
                var tag = await _unitOfWork.Tags.GetByText(tags[i].Text);
                if (tag != null)
                {
                    updateArticle.Tags.Remove(tag);
                    updateArticle.Tags.Add(tag);
                }
                else
                {
                    updateArticle.Tags.Add(tags[i]);
                }
            }

            await _unitOfWork.Articles.Update(updateArticle);
        }
    }
}