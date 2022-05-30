using System.Collections.Generic;
using System.Threading.Tasks;
using WDC_project.Core.Models;

namespace WDC_project.Core.Interfaces
{
    public interface IPolicyService
    {
        Task<UserPolicy> GetByIdAsync(int polId);
        Task<IEnumerable<UserPolicy>> GetAllAsync();
    }
}