using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WDC_project.Core.Interfaces;
using WDC_project.Core.Models;
using WDC_project.Data.Context;

namespace WDC_project.Data.Repositories
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PolicyRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<UserPolicy> GetByIdAsync(int polId)
        {
            return await _dbContext.UserPolicies.FirstOrDefaultAsync(p => p.Id == polId);
        }

        public async Task<IEnumerable<UserPolicy>> GetAllAsync()
        {
            return await _dbContext.UserPolicies.ToListAsync();
        }
    }
}