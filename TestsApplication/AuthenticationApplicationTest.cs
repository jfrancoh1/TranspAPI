using Infrastructure.Interface;
using Domain;
using Application.Dto;
using Application.Main;
using AutoMapper;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace TestsApplication
{
    public class AuthenticationApplicationTest
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private IMapper _mapper;
        private IConfiguration _configuration;
        public AuthenticationApplicationTest()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            // Configurar AutoMapper si es necesario
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateUserDto, User>();
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<UpdateUserDto, UserDto>();
                cfg.CreateMap<UpdateUserDto, User>();
                cfg.CreateMap<LoginDto, Login>();
                cfg.CreateMap<Login, LoginDto>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Login_Returns_Ok_When_Successful()
        {

            int id = 1;
            // Arrange
            LoginDto loginDto = new LoginDto { Id = 2, Password = "12345678a" };
            User user = new User
            {
                Id = 2,
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator,
                Password = "12345678a"
            };
            UserDto userDto = _mapper.Map<UserDto>(user);
            GetInfoUserDto getInfoUserDto = new GetInfoUserDto
            {
                User = userDto,
                token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJyb2xlIjoiQWRtaW5pc3RyYXRvciIsIm5iZiI6MTcxMTkwOTY0NiwiZXhwIjoxNzExOTA5NzY2LCJpYXQiOjE3MTE5MDk2NDYsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwMTEvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzAxMS8ifQ.goxDXj5_KPh6KWZXwcz4qKVwUOdiAmuW7yLrwvsO6OM"
            };
            var expectedResponse = new ResultRequestDto<GetInfoUserDto> { IsSuccess = true, Response = StatusCodes.Status200OK.ToString(), Data = getInfoUserDto };
            _mockUserRepository.Setup(x => x.Get(id)).Returns(user);
            var auth = new Application.Main.Authentication(_configuration, _mockUserRepository.Object, _mapper);

            // Act
            var result = await auth.GetToken(loginDto);

            // Assert
            Assert.NotNull(result);
        }
        
    }
}