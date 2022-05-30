using AutoMapper;
using WDC_project.Core.Models;
using WDC_project.Web.Dtos.UserDtos;
using WDC_project.Web.Dtos.UserPoliciesDtos;
using WDC_project.Web.Dtos.UserRoleDtos;

namespace WDC_project.Web.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDetailsDto>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<UserLoginDto, User>();
            CreateMap<User, UserDetailsDto>();
            CreateMap<User, UserPreviewDto>();

            CreateMap<UserRole, UserRoleDto>();
            CreateMap<UserRoleDto, UserRole>();
            
            CreateMap<UserPolicy, UserPolicyDto>();
            CreateMap<UserPolicyDto, UserPolicy>();
        }
    }
}