using Infrastructure.Interface;
using Domain.Enums;
using Domain;
using Application.Dto;
using Application.Main;
using AutoMapper;
using Moq;
using Microsoft.AspNetCore.Http;
using Application.Interface;

namespace TestsApplication
{
    public class UsersApplicationTest
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private IMapper _mapper;

        public UsersApplicationTest()
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

        #region Tests Create
        [Fact]
        public async Task Create_Returns_Ok_When_Successful()
        {
            CreateUserDto createUserDto = new CreateUserDto
            {
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator,
                Password = "12345678a",
            };
            User user = _mapper.Map<User>(createUserDto);
            UserDto userDto = _mapper.Map<UserDto>(user);
            _mockUserRepository.Setup(x => x.Create(user)).Returns(user.Id);
            var users = new Users(_mockUserRepository.Object, _mapper);

            // Act
            var result = await users.Create(createUserDto);
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("201", result.Response);
            Assert.True(result.IsSuccess);
        }

        
        [Fact]
        public async Task Create_Returns_InternalServerError_When_Service_Fails()
        {
            CreateUserDto createUserDto = new CreateUserDto
            {
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator,
                Password = "12345678a",
            };
            User user = _mapper.Map<User>(createUserDto);
            UserDto userDto = _mapper.Map<UserDto>(user);
            _mockUserRepository.Setup(x => x.Create(It.IsAny<User>())).Throws(new Exception("Test exception"));
            var users = new Users(_mockUserRepository.Object, _mapper);

            // Act
            var result = await users.Create(createUserDto);
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("500", result.Response);
        }
        #endregion
        
        #region Tests Update
        [Fact]
        public async Task Update_Returns_Ok_When_Successful()
        {
            int id = 1;
            UpdateUserDto updateUserDto = new UpdateUserDto
            {
                Id = id,
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator,
                Password = "12345678a",
            };
            User user = _mapper.Map<User>(updateUserDto);
            _mockUserRepository.Setup(x => x.Get(id)).Returns(user);
            _mockUserRepository.Setup(x => x.Update(user)).Returns(user.Id);
            var users = new Users(_mockUserRepository.Object, _mapper);

            // Act
            var result = await users.Update(updateUserDto);
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("201", result.Response);
        }

        
        
        [Fact]
        public async Task Update_Returns_NotFound_When_User_Isnt()
        {
            int id = 1;
            UpdateUserDto updateUserDto = new UpdateUserDto
            {
                Id = id,
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator,
                Password = "12345678a",
            };
            User user = _mapper.Map<User>(updateUserDto);
            _mockUserRepository.Setup(x => x.Get(id));
            _mockUserRepository.Setup(x => x.Update(user)).Returns(user.Id);
            var users = new Users(_mockUserRepository.Object, _mapper);

            // Act
            var result = await users.Update(updateUserDto);
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("404", result.Response);
            Assert.Equal("User Not Found.", result.Message);
            Assert.False(result.IsSuccess);
        }


        
        [Fact]
        public async Task Update_Returns_InternalServerError_When_Service_Fails()
        {
            int id = 1;
            UpdateUserDto updateUserDto = new UpdateUserDto
            {
                Id = id,
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator,
                Password = "12345678a",
            };
            User user = _mapper.Map<User>(updateUserDto);
            _mockUserRepository.Setup(x => x.Get(id)).Returns(user);
            _mockUserRepository.Setup(x => x.Update(It.IsAny<User>())).Throws(new Exception("Test exception"));
            var users = new Users(_mockUserRepository.Object, _mapper);

            // Act
            var result = await users.Update(updateUserDto);
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("500", result.Response);
        }
        #endregion

        #region Tests Get
        [Fact]
        public async Task Get_Returns_Ok_When_Successful()
        {
            int id = 1;
            User user = new User
            {
                Id = id,
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator,
                Password = "12345678a",
            };
            _mockUserRepository.Setup(x => x.Get(id)).Returns(user);
            var users = new Users(_mockUserRepository.Object, _mapper);

            // Act
            var result = await users.Get(id);
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("200", result.Response);
        }


        
        [Fact]
        public async Task Get_Returns_NotFound_When_User_Isnt()
        {
            int id = 1;
            _mockUserRepository.Setup(x => x.Get(id));
            var users = new Users(_mockUserRepository.Object, _mapper);

            // Act
            var result = await users.Get(id);
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("404", result.Response);
            Assert.Equal("User Not Found.", result.Message);
            Assert.False(result.IsSuccess);
        }


        
        [Fact]
        public async Task Get_Returns_InternalServerError_When_Service_Fails()
        {
            int id = 1;
            User user = new User
            {
                Id = id,
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator,
                Password = "12345678a",
            };
            _mockUserRepository.Setup(x => x.Get(It.IsAny<int>())).Throws(new Exception("Test exception"));
            var users = new Users(_mockUserRepository.Object, _mapper);

            // Act
            var result = await users.Get(id);

            
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("500", result.Response);
        }
        #endregion

        #region Tests GetAll
        [Fact]
        public async Task GetAll_Returns_Ok_When_Successful()
        {
            List<User> lsUsers = new ();
            User user = new User
            {
                Id = 1,
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator,
                Password = "12345678a",
            };
            lsUsers.Add(user);
            _mockUserRepository.Setup(x => x.GetAll()).Returns(lsUsers);
            var users = new Users(_mockUserRepository.Object, _mapper);

            // Act
            var result = await users.GetAll();
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("200", result.Response);
        }

        
        [Fact]
        public async Task GetAll_Returns_InternalServerError_When_Service_Fails()
        {
            List<User> lsUsers = new();
            User user = new User
            {
                Id = 1,
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator,
                Password = "12345678a",
            };
            lsUsers.Add(user);
            _mockUserRepository.Setup(x => x.GetAll()).Throws(new Exception("Test exception")); 
            var users = new Users(_mockUserRepository.Object, _mapper);

            // Act
            var result = await users.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("500", result.Response);
        }
        #endregion

        
        #region Tests GetUserByIdAndPassword
        [Fact]
        public async Task GetUserByIdAndPassword_Returns_Ok_When_Successful()
        {

            LoginDto input = new LoginDto
            {
                Id = 1,
                Password = "12345678a"
            };
            Login login = _mapper.Map<Login>(input);
            User user = new User
            {
                Id = 1,
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator,
                Password = "12345678a"
            };
            _mockUserRepository.Setup(x => x.GetUserByIdAndPassword(login)).Returns(user);
            var users = new Users(_mockUserRepository.Object, _mapper);

            // Act
            var result = await users.GetUserByIdAndPassword(input);
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("200", result.Response);
        }

        
        [Fact]
        public async Task GetUserByIdAndPassword_Returns_NotFound_When_User_Isnt()
        {
            LoginDto loginDto = new LoginDto
            {
                Id = 1,
                Password = "12345678a"
            };
            Login login = _mapper.Map<Login>(loginDto);
            User user = new User
            {
                Id = 1,
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator,
                Password = "12345678a",
            };
            _mockUserRepository.Setup(x => x.GetUserByIdAndPassword(login));
            var users = new Users(_mockUserRepository.Object, _mapper);

            // Act
            var result = await users.GetUserByIdAndPassword(loginDto);
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("404", result.Response);
            Assert.Equal("User Not Found.", result.Message);
            Assert.False(result.IsSuccess);
        }


        
        [Fact]
        public async Task GetUserByIdAndPassword_Returns_InternalServerError_When_Service_Fails()
        {
            LoginDto loginDto = new LoginDto
            {
                Id = 1,
                Password = "12345678a"
            };
            _mockUserRepository.Setup(x => x.GetUserByIdAndPassword(It.IsAny<Login>())).Throws(new Exception("Test exception"));
            var users = new Users(_mockUserRepository.Object, _mapper);

            // Act
            var result = await users.GetUserByIdAndPassword(loginDto);
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("500", result.Response);
        }
        #endregion

        
        #region Tests Delete
        [Fact]
        public async Task Delete_Returns_Ok_When_Successful()
        {
            int id = 1;
            User user = new User
            {
                Id = 1,
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator,
                Password = "12345678a",
            };
            _mockUserRepository.Setup(x => x.Get(id)).Returns(user);
            _mockUserRepository.Setup(x => x.Delete(user)).Returns(id);
            var users = new Users(_mockUserRepository.Object, _mapper);

            // Act
            var result = await users.Delete(id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("200", result.Response);
            Assert.True(result.Data);
        }

        
        [Fact]
        public async Task Delete_Returns_NotFound_When_User_Isnt()
        {
            int id = 1;
            User user = new User
            {
                Id = 1,
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator,
                Password = "12345678a",
            };
            _mockUserRepository.Setup(x => x.Get(id));
            _mockUserRepository.Setup(x => x.Delete(user)).Returns(id);
            var users = new Users(_mockUserRepository.Object, _mapper);

            // Act
            var result = await users.Delete(id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("404", result.Response);
            Assert.Equal("User Not Found.", result.Message);
            Assert.False(result.IsSuccess);
        }


        
        [Fact]
        public async Task Delete_Returns_InternalServerError_When_Service_Fails()
        {
            int id = 1;
            User user = new User
            {
                Id = 1,
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator,
                Password = "12345678a",
            };
            _mockUserRepository.Setup(x => x.Get(It.IsAny<int>())).Throws(new Exception("Test exception"));
            _mockUserRepository.Setup(x => x.Delete(user)).Returns(id);
            var users = new Users(_mockUserRepository.Object, _mapper);

            // Act
            var result = await users.Delete(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("500", result.Response);
            Assert.False(result.IsSuccess);
        }
        #endregion
        
    }
}