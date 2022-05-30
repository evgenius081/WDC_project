using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WDC_project.Core.Entities;
using WDC_project.Core.Helpers;
using WDC_project.Core.Interfaces;
using WDC_project.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace WDC_project.Services.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository policyRepository;
        private readonly JWTAuthSettings _JWTAuthSettings;

        public PolicyService(IPolicyRepository userRepository)
        {
            policyRepository = userRepository;

        }
        
        public async Task<IEnumerable<UserPolicy>> GetAllAsync()
        {
            return await policyRepository.GetAllAsync();
        }

        public async Task<UserPolicy> GetByIdAsync(int polId)
        {
            return await policyRepository.GetByIdAsync(polId);
        }
    }
}