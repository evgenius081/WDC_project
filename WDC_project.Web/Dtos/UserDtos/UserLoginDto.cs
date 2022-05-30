using System.ComponentModel.DataAnnotations;

namespace WDC_project.Web.Dtos.UserDtos
{
    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}