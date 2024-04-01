using AutoMapper;
using Domain;
using Application.Dto;


namespace Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, CreateUserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<User, UpdateUserDto>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<UserDto, UpdateUserDto>();
            CreateMap<UpdateUserDto, UserDto>();

            CreateMap<Branch, BranchDto>();
            CreateMap<BranchDto, Branch>();
            CreateMap<Branch, CreateBranchDto>();
            CreateMap<CreateBranchDto, Branch>();
            CreateMap<Branch, UpdateBranchDto>();
            CreateMap<UpdateBranchDto, Branch>();
            CreateMap<BranchDto, UpdateBranchDto>();
            CreateMap<UpdateBranchDto, BranchDto>();

            CreateMap<Login, LoginDto>();
            CreateMap<LoginDto, Login>();
        }
    }
}
