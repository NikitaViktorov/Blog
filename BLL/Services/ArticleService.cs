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
            //_mapper = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<Tag, TagDTO>().ReverseMap();
            //    cfg.CreateMap<Article, ArticleDTO>().ReverseMap();
            //    cfg.CreateMap<User, UserDTO>().ReverseMap();
            //    cfg.CreateMap<Comment, CommentDTO>().ReverseMap();
            //}).CreateMapper();
            _mapper = AutoMapperProfile.InitializeAutoMapper().CreateMapper();
        }

        public async Task Create(ArticleDTO articleDTO)
        {
            if (await _unitOfWork.Tags.Get(articleDTO.TagId) == null) throw new ArticleException("You don't create article,because the article doen't exist");
            else if(await _unitOfWork.Users.Get(articleDTO.UserId) == null) throw new ArticleException("You don't create article,because the user doen't exist");
            else await _unitOfWork.Articles.Create(_mapper.Map<Article>(articleDTO));
            //var configArticle = new MapperConfiguration(cfg => cfg.CreateMap<ArticleDTO, Article>()); 
            //var mapperArticle = new Mapper(configArticle);
            //await _unitOfWork.Tags.Create(_mapper.Map<Tag>(articleDTO.Tag));
            //var article = _mapper.Map<Article>(articleDTO);
            //article.TagId = _mapper.Map<Tag>(articleDTO.Tag).Id;
            //var configTag = new MapperConfiguration(cfg => cfg.CreateMap<TagDTO, Tag>());
            //var mapperTag = new Mapper(configTag);
            //article.Tags = mapperTag.Map<IEnumerable<TagDTO>, IEnumerable<Tag>>(tagDTOs);
            //await _unitOfWork.Articles.Create(_mapper.Map<Article>(articleDTO));
        }

        //public async Task AddTag(string title, TagDTO tagDTO)
        //{
        //    //var configArticle = new MapperConfiguration(cfg => cfg.CreateMap<ArticleDTO, Article>());
        //    //var mapperArticle = new Mapper(configArticle);
        //    //var article = mapperArticle.Map<ArticleDTO, Article>(articleDTO);
        //    IEnumerable<Article> articles = await _unitOfWork.Articles.GetAll();
        //    IEnumerator<Article> articlesEnumerator = articles.GetEnumerator();
        //    //while(articlesEnumerator.MoveNext())
        //    //{
        //    //    if(articlesEnumerator.Current.Title == articleDTO.Title && articlesEnumerator.Current.Text == articleDTO.Text)
        //    //    {
        //    //        var config = new MapperConfiguration(cfg => cfg.CreateMap<TagDTO, Tag>());
        //    //        var mapper = new Mapper(config);
        //    //        articlesEnumerator.Current.Tags.Add(mapper.Map<TagDTO, Tag>(tagDTO));
        //    //        await _unitOfWork.Save();
        //    //        break;
        //    //    }
        //    //}
        //    while(articlesEnumerator.MoveNext())
        //    {
        //        if(articlesEnumerator.Current.Title == title)
        //        {
        //            //var config = new MapperConfiguration(cfg => cfg.CreateMap<TagDTO, Tag>());
        //            //var mapper = new Mapper(config);
        //            articlesEnumerator.Current.Tags.Add(_mapper.Map<TagDTO, Tag>(tagDTO));
        //            await _unitOfWork.Save();
        //            break;
        //        }
        //    }
        //    //var article = await _unitOfWork.Articles.Get(articleId);
        //    //Tag tag = await _unitOfWork.Tags.Get(tagId);
        //    //article.Tags.Add(tag);
        //    //await _unitOfWork.Save();
        //}

        public async Task Delete(Guid id)
        {
            //var config = new MapperCo nfiguration(cfg => cfg.CreateMap<ArticleDTO, Article>());
            //var mapper = new Mapper(config);
            //var article = mapper.Map<ArticleDTO, Article>(articleDTO);
            var article = await _unitOfWork.Articles.Get(id) ?? throw new ArticleException("Tag doens't exist.You don't delete this tag!");
            //if (await _unitOfWork.Tags.Get(id) == null)
            //    throw new ArticleException("Tag doens't exist.You don't delete this tag!");
            await _unitOfWork.Articles.Delete(id);
        }

        public async Task<ArticleDTO> Get(Guid id)
        {
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<Article,ArticleDTO>());
            //var mapper = new Mapper(config);


            //if (_mapper.Map<ArticleDTO>(await _unitOfWork.Articles.Get(id)) == null)
            //    throw new ArticleException("Article doesn't exist");

            var article = await _unitOfWork.Articles.Get(id) ?? throw new ArticleException("Article doesn't exist");
            return _mapper.Map<ArticleDTO>(article);
        }

        public async Task<ICollection<ArticleDTO>> GetAll()
        {
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<Article, ArticleDTO>());
            //var mapper = new Mapper(config);
            var articles = await _unitOfWork.Articles.GetAll() ?? throw new ArticleException("List of articles is empty!");
            return _mapper.Map<ICollection<ArticleDTO>>(articles);
        }
        public async Task<ICollection<ArticleDTO>> GetArticlesByTag(Guid id)
        {
            var articles = await _unitOfWork.Articles.GetArticlesByTag(id) ?? throw new ArticleException("List of tags is empty!");
            return _mapper.Map<ICollection<ArticleDTO>>(articles);
            //if (_mapper.Map<ICollection<ArticleDTO>>(await _unitOfWork.Articles.GetAll()).Count == 0)
            //    throw new ArticleException("List of tags is empty!");
            //return _mapper.Map<ICollection<ArticleDTO>>(await _unitOfWork.Articles.GetArticlesByTag(id));
        }
        public async Task Update(Guid id, ArticleDTO articleDTO)
        {
            var article = await _unitOfWork.Articles.Get(id) ?? throw new ArticleException("Article doesn't exist!");
            //if (article == null)
            //    throw new ArticleException("Article doesn't exist!");
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<ArticleDTO, Article>());
            //var mapper = new Mapper(config);
            var updateArticle = _mapper.Map<Article>(articleDTO);
            updateArticle.Id = id;
            await _unitOfWork.Articles.Update(updateArticle);
        }
    }
}
