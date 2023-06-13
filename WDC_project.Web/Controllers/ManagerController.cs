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
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
            var pol = await _policyService.GetAllAsync();
            var minimumAge = int.Parse(pol.Where(p => p.Policy == PoliciesEntity.MinimumManagerAgeClaimType).First().Value);
            var userAge = HttpContextHelper.GetAgeByContextUser(HttpContext.User);
            if (userAge < minimumAge)
            {
                return Forbid();
            }
            var users = await _userService.GetAllAsync();

            var userDetailsDtos = _mapper.Map<List<UserPreviewDto>>(users);
            return Ok(userDetailsDtos);
        }
    }
}