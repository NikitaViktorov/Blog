using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TagRepository : IRepository<Tag>
    {
        private readonly BlogContext _db;
        public TagRepository(BlogContext db)
        {
            _db = db;
        }
        public async Task Create(Tag item)
        {
            await _db.Tags.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            _db.Tags.Remove(await _db.Tags.FirstAsync(c => c.Id == id));
            await _db.SaveChangesAsync();
        }

        public async Task<Tag> Get(Guid id)
        {
            if (await _db.Tags.CountAsync(a => a.Id == id) == 0)
                return null;
            return await _db.Tags.FirstAsync(a => a.Id == id);
        }

        public async Task<ICollection<Tag>> GetAll()
        {
            return await _db.Tags.Include(a => a.Articles).ThenInclude(a => a.User).ToListAsync();
        }

        public async Task Update(Tag item)
        {
            _db.Tags.Remove(await _db.Tags.FirstAsync(a => a.Id == item.Id));
            await _db.Tags.AddAsync(item);
            await _db.SaveChangesAsync();
        }
    }
}
