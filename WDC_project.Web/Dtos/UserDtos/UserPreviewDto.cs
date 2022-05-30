using System.Collections.Generic;
using WDC_project.Web.Dtos.UserRoleDtos;

namespace WDC_project.Web.Dtos.UserDtos
{
    public class UserPreviewDto
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public List<UserRoleDto> Roles { get; set; }

    }
}