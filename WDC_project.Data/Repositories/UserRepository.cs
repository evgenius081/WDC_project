using System.Collections.Generic;
using System.Threading.Tasks;
using WDC_project.Core.Interfaces;
using WDC_project.Core.Models;
using WDC_project.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace WDC_project.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(User user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user.Id;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users.Include(u => u.Roles).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await _dbContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task UpdateAsync(User updatedUser)
        {
            _dbContext.Entry(updatedUser).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int userId)
        {
            var userToDelete = await _dbContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == userId);
            _dbContext.Users.Remove(userToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}