using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IArticleService
    {
        Task Create(ArticleDTO articleDTO);
        Task Delete(Guid id);
        //Task AddTag(string title, TagDTO tagDTO);
        Task<ArticleDTO> Get(Guid id);
        Task<ICollection<ArticleDTO>> GetAll();
        Task<ICollection<ArticleDTO>> GetArticlesByTag(Guid id);
        Task Update(Guid id, ArticleDTO articleDTO);
    }
}
