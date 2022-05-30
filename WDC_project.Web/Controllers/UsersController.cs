using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WDC_project.Core.Interfaces;
using WDC_project.Core.Models;
using WDC_project.Services.Services;
using WDC_project.Web.Dtos.UserDtos;
using WDC_project.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WDC_project.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(UserRegisterDto userRegisterDto)
        {
            var user = _mapper.Map<User>(userRegisterDto);
            var userId = await _userService.AddAsync(user);
            return StatusCode(201, userId);
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = HttpContextHelper.GetIdByContextUser(HttpContext.User);
            var user = await _userService.GetByIdAsync(userId);
            var userDetailsDto = _mapper.Map<UserDetailsDto>(user);
            return Ok(userDetailsDto);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByIdAsync(int userId)
        {
            var user = await _userService.GetByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { message = "User doesn't exist!" });
            }

            var userDetailsDto = _mapper.Map<UserDetailsDto>(user);
            return Ok(userDetailsDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();
            var userDetailsDtos = _mapper.Map<List<UserDetailsDto>>(users);
            return Ok(userDetailsDtos);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateAsync(int userId, UserUpdateDto userUpdateDto)
        {
            var updatedUser = _mapper.Map<User>(userUpdateDto);
            updatedUser.Id = userId;
            await _userService.UpdateAsync(updatedUser);
            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAsync(int userId)
        {
            await _userService.DeleteAsync(userId);
            return Ok();
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(UserLoginDto userLoginDto)
        {
            var user = await _userService.Authenticate(userLoginDto.Username, userLoginDto.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(user);
        }
    }
}
