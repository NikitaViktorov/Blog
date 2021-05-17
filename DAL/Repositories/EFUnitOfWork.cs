using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly BlogContext _db;
        private IArticleRepository _articleRepository;
        private CommentRepository _commentRepository;
        private TagRepository _tagRepository;
        private UserRepository _userRepository;
        public EFUnitOfWork(BlogContext db)
        {
            _db = db;
            //db = new BlogContext();
        }
        public IArticleRepository Articles
        {
            get
            {
                if (_articleRepository == null)
                    _articleRepository = new ArticleRepository(_db);
                return _articleRepository;
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new CommentRepository(_db);
                return _commentRepository;
            }
        }

        public IRepository<Tag> Tags
        {
            get
            {
                if (_tagRepository == null)
                    _tagRepository = new TagRepository(_db);
                return _tagRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_db);
                return _userRepository;
            }
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
