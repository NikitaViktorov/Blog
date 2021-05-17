using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private BlogContext _db;
        public ArticleRepository(BlogContext db)
        {
            _db = db;
        }

        public async Task Create(Article item)
        {
            await _db.Articles.AddAsync(item);
            await _db.SaveChangesAsync();

        }
        public async Task Delete(Guid id)
        {
            _db.Articles.Remove(await _db.Articles.FirstAsync(a => a.Id == id));
            await _db.SaveChangesAsync();
        }
        public async Task<Article> Get(Guid id)
        {

            if (await _db.Articles.CountAsync(a => a.Id == id) == 0)
                return null;
            return await _db.Articles.Include(c => c.Tags).ThenInclude(c => c.Articles).Include(c => c.Comments).FirstAsync(a => a.Id == id);
        }

        public async Task<ICollection<Article>> GetAll()
        {
            //return await _db.Articles.ToListAsync();
            return await _db.Articles.Include(c => c.User).Include(c => c.Comments).Include(s => s.Tags).ThenInclude(c => c.Articles).ToListAsync();
        }

        public async Task<ICollection<Article>> GetArticlesByTag(Guid id)
        {
            var tag = await _db.Tags.FindAsync(id);
            var articles = await _db.Articles.Include(x => x.Tags).ToListAsync();
            var result = articles.Where(x => x.Tags.Contains(tag));

            return result.ToList();

            //var result = await _db.Articles.Include(x => x.Tags)
            //    .Where(x => x.Tags.Select(x=>x.Id)
            //    .Contains(id)).ToListAsync();
        }

        public async Task Update(Article item)
        {
            _db.Articles.Remove(await _db.Articles.FirstAsync(a => a.Id == item.Id));
            await _db.Articles.AddAsync(item);
            await _db.SaveChangesAsync();
        }
    }
}
