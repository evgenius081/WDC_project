using System.ComponentModel.DataAnnotations;

namespace WDC_project.Web.Dtos.UserDtos
{
    public class UserRegisterDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required] 
        public int Age { get; set; }
    }
}