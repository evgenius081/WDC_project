using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WDC_project.Core.Entities;
using WDC_project.Core.Interfaces;
using WDC_project.Web.Dtos.UserDtos;
using WDC_project.Web.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WDC_project.Core.Models;

namespace WDC_project.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = RolesEntity.Admin + "," + RolesEntity.Manager)]
    public class ManagerController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPolicyService _policyService;
        private readonly IMapper _mapper;

        public ManagerController(IUserService userService, IPolicyService policyService, IMapper mapper)
        {
            _userService = userService;
            _policyService = policyService;
            _mapper = mapper;
        }

        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(int userId)
        {
            var user = await _userService.GetByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { message = "User doesn't exist!" });
            }

            

            var userDetailsDto = _mapper.Map<UserPreviewDto>(user);
            return Ok(userDetailsDto);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userService.GetAllAsync();
            
            var pol = await _policyService.GetAllAsync();
            var claim = pol.Where(p => p.Policy == PoliciesEntity.ManagerAgeClaimType);
            
            var sign = claim.First().Value[0];
            switch (sign)
            {
                case ('>'):
                {
                    users = users.Where(u => u.Age >= int.Parse(claim.First().Value.Substring(1)));
                    break;
                }
                case ('<'):
                {
                    users = users.Where(u => u.Age <= int.Parse(claim.First().Value.Substring(1)));
                    break;
                }
                default:
                {
                    users = users.Where(u => u.Age == int.Parse(claim.First().Value.Substring(1)));
                    break;
                }
            }
            // var loggedInUserMaxAccessLevelMax = int.Parse(loggedInUser.Policies.Where(p => p.Policy.Contains("AccessLevel")).Max(p => p.Value));
            // users = users.Where(u => int.Parse(u.Policies.Where(p => p.Policy
            //         .Contains("AccessLevel"))
            //     .Max(p => p.Value)) <= loggedInUserMaxAccessLevelMax);
            var userDetailsDtos = _mapper.Map<List<UserPreviewDto>>(users);
            return Ok(userDetailsDtos);
        }
    }
}