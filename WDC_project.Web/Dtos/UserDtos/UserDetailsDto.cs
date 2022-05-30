using System.Collections.Generic;
using WDC_project.Core.Models;
using WDC_project.Web.Dtos.UserRoleDtos;

namespace WDC_project.Web.Dtos.UserDtos
{
    public class UserDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public List<UserRoleDto> Roles { get; set; }
        public int Age { get; set; }
    }
}