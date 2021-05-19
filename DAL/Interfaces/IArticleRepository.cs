using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IArticleRepository
    {
        Task Create(Article item);
        Task Update(Article item);
        Task Delete(Guid id);
        Task<ICollection<Article>> GetAll();
        Task<Article> Get(Guid id);
        Task<ICollection<Article>> GetArticlesByTag(Guid id);
        Task<Article> GetArticleByText(string text);
    }
}
