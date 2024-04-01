using Application.Dto;
using Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;

namespace TestControllers
{
    public class UsersControllerTest
    {
        private readonly Mock<IUsers> _mockUser;


        public UsersControllerTest()
        {
            _mockUser = new Mock<IUsers>();
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
            UserDto userDto = new UserDto
            {
                Id = 1,
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator
            };
            var expectedResponse = new ResultRequestDto<UserDto> { IsSuccess = true, Response = StatusCodes.Status201Created.ToString(), Data = userDto };
            _mockUser.Setup(x => x.Create(createUserDto)).ReturnsAsync(expectedResponse);
            var controller = new UsersController(_mockUser.Object);

            // Act
            var result = await controller.Create(createUserDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
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
            var expectedResponse = new ResultRequestDto<UserDto> { IsSuccess = true, Response = StatusCodes.Status500InternalServerError.ToString() };
            _mockUser.Setup(x => x.Create(createUserDto)).ReturnsAsync(expectedResponse);
            var controller = new UsersController(_mockUser.Object);

            // Act
            var result = await controller.Create(createUserDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task Create_Returns_BadRequest_When_Object_Is_Wrong()
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
            var expectedResponse = new ResultRequestDto<UserDto> { IsSuccess = true, Response = StatusCodes.Status400BadRequest.ToString() };
            _mockUser.Setup(x => x.Create(createUserDto)).ReturnsAsync(expectedResponse);
            var controller = new UsersController(_mockUser.Object);

            // Act
            var result = await controller.Create(createUserDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }
        #endregion

        #region Tests Get

        [Fact]
        public async Task Get_Returns_Ok_When_Successful()
        {

            int id = 1;
            UserDto userDto = new UserDto
            {
                Id = 1,
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator
            };
            var expectedResponse = new ResultRequestDto<UserDto> { IsSuccess = true, Response = StatusCodes.Status200OK.ToString(), Data = userDto };
            _mockUser.Setup(x => x.Get(id)).ReturnsAsync(expectedResponse);
            var controller = new UsersController(_mockUser.Object);

            // Act
            var result = await controller.Get(id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }


        [Fact]
        public async Task Get_Returns_InternalServerError_When_Service_Fails()
        {

            int id = 1;
            var expectedResponse = new ResultRequestDto<UserDto> { IsSuccess = true, Response = StatusCodes.Status500InternalServerError.ToString() };
            _mockUser.Setup(x => x.Get(id)).ReturnsAsync(expectedResponse);
            var controller = new UsersController(_mockUser.Object);

            // Act
            var result = await controller.Get(id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task Get_Returns_NotFound()
        {

            int id = 1;
            var expectedResponse = new ResultRequestDto<UserDto> { IsSuccess = true, Response = StatusCodes.Status404NotFound.ToString() };
            _mockUser.Setup(x => x.Get(id)).ReturnsAsync(expectedResponse);
            var controller = new UsersController(_mockUser.Object);

            // Act
            var result = await controller.Get(id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task Get_Returns_BadRequest_When_Object_Is_Wrong()
        {

            int id = 1;
            var expectedResponse = new ResultRequestDto<UserDto> { IsSuccess = true, Response = StatusCodes.Status400BadRequest.ToString() };
            _mockUser.Setup(x => x.Get(id)).ReturnsAsync(expectedResponse);
            var controller = new UsersController(_mockUser.Object);

            // Act
            var result = await controller.Get(id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }
        #endregion

        #region Tests GetAll

        [Fact]
        public async Task GetAll_Returns_Ok_When_Successful()
        {
            List<UserDto> lsusersDto = new();
            UserDto userDto = new UserDto
            {
                Id = 1,
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator
            };
            lsusersDto.Add(userDto);
            var expectedResponse = new ResultRequestDto<List<UserDto>> { IsSuccess = true, Response = StatusCodes.Status200OK.ToString(), Data = lsusersDto };
            _mockUser.Setup(x => x.GetAll()).ReturnsAsync(expectedResponse);
            var controller = new UsersController(_mockUser.Object);

            // Act
            var result = await controller.GetAll() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        
        [Fact]
        public async Task GetAll_Returns_InternalServerError_When_Service_Fails()
        {
            var expectedResponse = new ResultRequestDto<List<UserDto>> { IsSuccess = true, Response = StatusCodes.Status500InternalServerError.ToString()};
            _mockUser.Setup(x => x.GetAll()).ReturnsAsync(expectedResponse);
            var controller = new UsersController(_mockUser.Object);

            // Act
            var result = await controller.GetAll() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task GetAll_Returns_NotFound()
        {
            var expectedResponse = new ResultRequestDto<List<UserDto>> { IsSuccess = true, Response = StatusCodes.Status404NotFound.ToString() };
            _mockUser.Setup(x => x.GetAll()).ReturnsAsync(expectedResponse);
            var controller = new UsersController(_mockUser.Object);

            // Act
            var result = await controller.GetAll() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task GetAll_Returns_BadRequest_When_Object_Is_Wrong()
        {
            var expectedResponse = new ResultRequestDto<List<UserDto>> { IsSuccess = true, Response = StatusCodes.Status400BadRequest.ToString() };
            _mockUser.Setup(x => x.GetAll()).ReturnsAsync(expectedResponse);
            var controller = new UsersController(_mockUser.Object);

            // Act
            var result = await controller.GetAll() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        #endregion

        #region Tests Update
        [Fact]
        public async Task Update_Returns_Ok_When_Successful()
        {
            UpdateUserDto updateUserDto = new UpdateUserDto
            {
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator,
                Password = "12345678a",
            };
            UserDto userDto = new UserDto
            {
                Id = 1,
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator
            };
            var expectedResponse = new ResultRequestDto<UserDto> { IsSuccess = true, Response = StatusCodes.Status201Created.ToString(), Data = userDto };
            _mockUser.Setup(x => x.Update(updateUserDto)).ReturnsAsync(expectedResponse);
            var controller = new UsersController(_mockUser.Object);

            // Act
            var result = await controller.Update(updateUserDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task Update_Returns_NotFound()
        {
            UpdateUserDto updateUserDto = new UpdateUserDto
            {
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator,
                Password = "12345678a",
            };
            var expectedResponse = new ResultRequestDto<UserDto> { IsSuccess = true, Response = StatusCodes.Status404NotFound.ToString() };
            _mockUser.Setup(x => x.Update(updateUserDto)).ReturnsAsync(expectedResponse);
            var controller = new UsersController(_mockUser.Object);

            // Act
            var result = await controller.Update(updateUserDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task Update_Returns_InternalServerError_When_Service_Fails()
        {

            UpdateUserDto updateUserDto = new UpdateUserDto
            {
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator,
                Password = "12345678a",
            };
            var expectedResponse = new ResultRequestDto<UserDto> { IsSuccess = true, Response = StatusCodes.Status500InternalServerError.ToString() };
            _mockUser.Setup(x => x.Update(updateUserDto)).ReturnsAsync(expectedResponse);
            var controller = new UsersController(_mockUser.Object);

            // Act
            var result = await controller.Update(updateUserDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task Update_Returns_BadRequest_When_Object_Is_Wrong()
        {

            UpdateUserDto updateUserDto = new UpdateUserDto
            {
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator,
                Password = "12345678a",
            };
            var expectedResponse = new ResultRequestDto<UserDto> { IsSuccess = true, Response = StatusCodes.Status400BadRequest.ToString() };
            _mockUser.Setup(x => x.Update(updateUserDto)).ReturnsAsync(expectedResponse);
            var controller = new UsersController(_mockUser.Object);

            // Act
            var result = await controller.Update(updateUserDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }
        #endregion

        #region Tests Delete

        [Fact]
        public async Task Delete_Returns_Ok_When_Successful()
        {

            int id = 1;
            var expectedResponse = new ResultRequestDto<bool> { IsSuccess = true, Response = StatusCodes.Status200OK.ToString(), Data = true };
            _mockUser.Setup(x => x.Delete(id)).ReturnsAsync(expectedResponse);
            var controller = new UsersController(_mockUser.Object);

            // Act
            var result = await controller.Delete(id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }


        [Fact]
        public async Task Delete_Returns_InternalServerError_When_Service_Fails()
        {

            int id = 1;
            var expectedResponse = new ResultRequestDto<bool> { IsSuccess = true, Response = StatusCodes.Status500InternalServerError.ToString()};
            _mockUser.Setup(x => x.Delete(id)).ReturnsAsync(expectedResponse);
            var controller = new UsersController(_mockUser.Object);

            // Act
            var result = await controller.Delete(id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task Delete_Returns_NotFound()
        {

            int id = 1;
            var expectedResponse = new ResultRequestDto<bool> { IsSuccess = true, Response = StatusCodes.Status404NotFound.ToString() };
            _mockUser.Setup(x => x.Delete(id)).ReturnsAsync(expectedResponse);
            var controller = new UsersController(_mockUser.Object);

            // Act
            var result = await controller.Delete(id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task Delete_Returns_BadRequest_When_Object_Is_Wrong()
        {

            int id = 1;
            var expectedResponse = new ResultRequestDto<bool> { IsSuccess = true, Response = StatusCodes.Status400BadRequest.ToString() };
            _mockUser.Setup(x => x.Delete(id)).ReturnsAsync(expectedResponse);
            var controller = new UsersController(_mockUser.Object);

            // Act
            var result = await controller.Delete(id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        #endregion
    }
}