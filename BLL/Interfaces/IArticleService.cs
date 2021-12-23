using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IArticleService
    {
        Task Create(ArticleDTO articleDTO);
        Task Delete(Guid id);
        Task<ArticleDTO> Get(Guid id);
        Task<ICollection<ArticleDTO>> GetAll();
        Task<ICollection<ArticleDTO>> GetArticlesByTag(Guid tagId);
        Task<ICollection<ArticleDTO>> GetUserArticles(Guid userId);
        Task Update(Guid id, ArticleDTO articleDTO);
        Task AddTag(Guid Article, TagDTO tagDTO);
        Task<ArticleDTO> GetArticleByText(string text);
    }
}