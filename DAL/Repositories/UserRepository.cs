using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly BlogContext _db;
        public UserRepository(BlogContext db)
        {
            _db = db;
        }
        public async Task Create(User item)
        {
            await _db.Users.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            _db.Users.Remove(await _db.Users.FirstAsync(c => c.Id == id));
            await _db.SaveChangesAsync();
        }

        public async Task<User> Get(Guid id)
        {
            if (await _db.Users.CountAsync(a => a.Id == id) == 0)
                return null;
            return await _db.Users.FirstAsync(a => a.Id == id);
        }

        public async Task<ICollection<User>> GetAll()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task Update(User item)
        {
            _db.Users.Remove(await _db.Users.FirstAsync(a => a.Id == item.Id));
            await _db.Users.AddAsync(item);
            await _db.SaveChangesAsync();
        }
    }
}
