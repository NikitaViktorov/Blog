using DAL.EF;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly BlogContext _db;
        private IArticleRepository _articleRepository;
        private ICommentRepository _commentRepository;
        private ITagRepository _tagRepository;
        private IUserRepository _userRepository;
        public EFUnitOfWork(BlogContext db)
        {
            _db = db;
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

        public ICommentRepository Comments
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new CommentRepository(_db);

                return _commentRepository;
            }
        }
        public ITagRepository Tags
        {
            get
            {
                if (_tagRepository == null)
                    _tagRepository = new TagRepository(_db);

                return _tagRepository;
            }
        }
        public IUserRepository Users
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_db);

                return _userRepository;
            }
        }
    }
}
