using DAL.Entities;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IArticleRepository Articles { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Tag> Tags { get; }
        IRepository<User> Users { get; }
        Task Save();
    }
}
