using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WDC_project.Core.Entities;
using WDC_project.Core.Interfaces;
using WDC_project.Web.Dtos.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WDC_project.Web.Helpers;

namespace WDC_project.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = RolesEntity.Admin)]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPolicyService _policyService;
        private readonly IMapper _mapper;

        public AdminController(IUserService userService, IPolicyService policyService, IMapper mapper)    
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

            var userDetailsDto = _mapper.Map<UserDetailsDto>(user);
            return Ok(userDetailsDto);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var loggedInUserId = HttpContextHelper.GetIdByContextUser(HttpContext.User);
            var loggedInUser = await _userService.GetByIdAsync(loggedInUserId);
            var pol = await _policyService.GetAllAsync();
            var claim = pol.Where(p => p.Policy == PoliciesEntity.AdminUsernameClaimType);
            if (loggedInUser.Name != claim.First().Value)
            {
                return Forbid();
            }
            var users = await _userService.GetAllAsync();
            var userDetailsDtos = _mapper.Map<List<UserDetailsDto>>(users);
            return Ok(userDetailsDtos);
        }
    }
}