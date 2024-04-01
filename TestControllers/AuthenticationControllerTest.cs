using Application.Dto;
using Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;

namespace TestControllers
{
    public class AuthenticationControllerTest
    {
        private readonly Mock<IAuthentication> _mockAuthentication;

        public AuthenticationControllerTest()
        {
            _mockAuthentication = new Mock<IAuthentication>();
        }

        [Fact]
        public async Task Login_Returns_Ok_When_Successful()
        {
            // Arrange
            LoginDto loginDto = new LoginDto { Id = 2, Password = "12345678a" };
            UserDto userDto = new UserDto
            {
                Id = 1,
                Document = "1088027397",
                Name = "Jorge Enrique",
                LastName = "Franco Herrera",
                PhoneNumber = "3148434889",
                TypeUser = Domain.Enums.TypeUser.Administrator
            };
            GetInfoUserDto getInfoUserDto = new GetInfoUserDto
            {
                User = userDto,
                token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJyb2xlIjoiQWRtaW5pc3RyYXRvciIsIm5iZiI6MTcxMTkwOTY0NiwiZXhwIjoxNzExOTA5NzY2LCJpYXQiOjE3MTE5MDk2NDYsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwMTEvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzAxMS8ifQ.goxDXj5_KPh6KWZXwcz4qKVwUOdiAmuW7yLrwvsO6OM"
            };
            var expectedResponse = new ResultRequestDto<GetInfoUserDto> { IsSuccess = true, Response = StatusCodes.Status200OK.ToString(), Data = getInfoUserDto };
            _mockAuthentication.Setup(x => x.GetToken(loginDto)).ReturnsAsync(expectedResponse);
            var controller = new AuthenticationController(_mockAuthentication.Object);

            // Act
            var result = await controller.Login(loginDto) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task Login_Returns_Unauthorized_When_Credentials_Are_Invalid()
        {
            // Arrange
            LoginDto loginDto = new LoginDto { Id = 2, Password = "12345678" };
            var expectedResponse = new ResultRequestDto<GetInfoUserDto> { IsSuccess = false, Response = StatusCodes.Status401Unauthorized.ToString() };
            _mockAuthentication.Setup(x => x.GetToken(loginDto)).ReturnsAsync(expectedResponse);
            var controller = new AuthenticationController(_mockAuthentication.Object);

            // Act
            var result = await controller.Login(loginDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status401Unauthorized, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task Login_Returns_InternalServerError_When_Service_Fails()
        {
            // Arrange
            LoginDto loginDto = new LoginDto { Id = 2, Password = "12345678" };
            var expectedResponse = new ResultRequestDto<GetInfoUserDto> { IsSuccess = false, Response = StatusCodes.Status500InternalServerError.ToString() };
            _mockAuthentication.Setup(x => x.GetToken(loginDto)).ReturnsAsync(expectedResponse);
            var controller = new AuthenticationController(_mockAuthentication.Object);

            // Act
            var result = await controller.Login(loginDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }
    }
}