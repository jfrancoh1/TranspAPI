using Application.Dto;
using Application.Interface;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;

namespace TestControllers
{
    public class BranchesControllerTest
    {
        private readonly Mock<IBranches> _mockBranch;


        public BranchesControllerTest()
        {
            _mockBranch = new Mock<IBranches>();
        }
        
        #region Tests Create
        [Fact]
        public async Task Create_Returns_Ok_When_Successful()
        {

            CreateBranchDto createBranchDto = new CreateBranchDto
            {
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            BranchDto branchDto = new BranchDto
            {
                Id = 6,
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            var expectedResponse = new ResultRequestDto<BranchDto> { IsSuccess = true, Response = StatusCodes.Status201Created.ToString(), Data = branchDto };
            _mockBranch.Setup(x => x.Create(createBranchDto)).ReturnsAsync(expectedResponse);
            var controller = new BranchesController(_mockBranch.Object);

            // Act
            var result = await controller.Create(createBranchDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        
        [Fact]
        public async Task Create_Returns_InternalServerError_When_Service_Fails()
        {

            CreateBranchDto createBranchDto = new CreateBranchDto
            {
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            var expectedResponse = new ResultRequestDto<BranchDto> { IsSuccess = true, Response = StatusCodes.Status500InternalServerError.ToString() };
            _mockBranch.Setup(x => x.Create(createBranchDto)).ReturnsAsync(expectedResponse);
            var controller = new BranchesController(_mockBranch.Object);

            // Act
            var result = await controller.Create(createBranchDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task Create_Returns_BadRequest_When_Object_Is_Wrong()
        {

            CreateBranchDto createBranchDto = new CreateBranchDto
            {
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            var expectedResponse = new ResultRequestDto<BranchDto> { IsSuccess = true, Response = StatusCodes.Status400BadRequest.ToString() };
            _mockBranch.Setup(x => x.Create(createBranchDto)).ReturnsAsync(expectedResponse);
            var controller = new BranchesController(_mockBranch.Object);

            // Act
            var result = await controller.Create(createBranchDto) as ObjectResult;

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
            BranchDto branchDto = new BranchDto
            {
                Id = 6,
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            var expectedResponse = new ResultRequestDto<BranchDto> { IsSuccess = true, Response = StatusCodes.Status200OK.ToString(), Data = branchDto };
            _mockBranch.Setup(x => x.Get(id)).ReturnsAsync(expectedResponse);
            var controller = new BranchesController(_mockBranch.Object);

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
            var expectedResponse = new ResultRequestDto<BranchDto> { IsSuccess = true, Response = StatusCodes.Status500InternalServerError.ToString() };
            _mockBranch.Setup(x => x.Get(id)).ReturnsAsync(expectedResponse);
            var controller = new BranchesController(_mockBranch.Object);

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
            var expectedResponse = new ResultRequestDto<BranchDto> { IsSuccess = true, Response = StatusCodes.Status404NotFound.ToString() };
            _mockBranch.Setup(x => x.Get(id)).ReturnsAsync(expectedResponse);
            var controller = new BranchesController(_mockBranch.Object);

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
            var expectedResponse = new ResultRequestDto<BranchDto> { IsSuccess = true, Response = StatusCodes.Status400BadRequest.ToString() };
            _mockBranch.Setup(x => x.Get(id)).ReturnsAsync(expectedResponse);
            var controller = new BranchesController(_mockBranch.Object);

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
            List<BranchDto> lsbranchesDto = new();
            BranchDto branchDto = new BranchDto
            {
                Id = 6,
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            lsbranchesDto.Add(branchDto);
            var expectedResponse = new ResultRequestDto<List<BranchDto>> { IsSuccess = true, Response = StatusCodes.Status200OK.ToString(), Data = lsbranchesDto };
            _mockBranch.Setup(x => x.GetAll()).ReturnsAsync(expectedResponse);
            var controller = new BranchesController(_mockBranch.Object);

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
            var expectedResponse = new ResultRequestDto<List<BranchDto>> { IsSuccess = true, Response = StatusCodes.Status500InternalServerError.ToString() };
            _mockBranch.Setup(x => x.GetAll()).ReturnsAsync(expectedResponse);
            var controller = new BranchesController(_mockBranch.Object);

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
            var expectedResponse = new ResultRequestDto<List<BranchDto>> { IsSuccess = true, Response = StatusCodes.Status404NotFound.ToString() };
            _mockBranch.Setup(x => x.GetAll()).ReturnsAsync(expectedResponse);
            var controller = new BranchesController(_mockBranch.Object);

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
            var expectedResponse = new ResultRequestDto<List<BranchDto>> { IsSuccess = true, Response = StatusCodes.Status400BadRequest.ToString() };
            _mockBranch.Setup(x => x.GetAll()).ReturnsAsync(expectedResponse);
            var controller = new BranchesController(_mockBranch.Object);

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
            UpdateBranchDto updateBranchDto = new UpdateBranchDto
            {
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            BranchDto branchDto = new BranchDto
            {
                Id = 6,
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            var expectedResponse = new ResultRequestDto<BranchDto> { IsSuccess = true, Response = StatusCodes.Status201Created.ToString(), Data = branchDto };
            _mockBranch.Setup(x => x.Update(updateBranchDto)).ReturnsAsync(expectedResponse);
            var controller = new BranchesController(_mockBranch.Object);

            // Act
            var result = await controller.Update(updateBranchDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task Update_Returns_NotFound()
        {
            UpdateBranchDto updateBranchDto = new UpdateBranchDto
            {
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            var expectedResponse = new ResultRequestDto<BranchDto> { IsSuccess = true, Response = StatusCodes.Status404NotFound.ToString() };
            _mockBranch.Setup(x => x.Update(updateBranchDto)).ReturnsAsync(expectedResponse);
            var controller = new BranchesController(_mockBranch.Object);

            // Act
            var result = await controller.Update(updateBranchDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task Update_Returns_InternalServerError_When_Service_Fails()
        {

            UpdateBranchDto updateBranchDto = new UpdateBranchDto
            {
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            var expectedResponse = new ResultRequestDto<BranchDto> { IsSuccess = true, Response = StatusCodes.Status500InternalServerError.ToString() };
            _mockBranch.Setup(x => x.Update(updateBranchDto)).ReturnsAsync(expectedResponse);
            var controller = new BranchesController(_mockBranch.Object);

            // Act
            var result = await controller.Update(updateBranchDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public async Task Update_Returns_BadRequest_When_Object_Is_Wrong()
        {

            UpdateBranchDto updateBranchDto = new UpdateBranchDto
            {
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            var expectedResponse = new ResultRequestDto<BranchDto> { IsSuccess = true, Response = StatusCodes.Status400BadRequest.ToString() };
            _mockBranch.Setup(x => x.Update(updateBranchDto)).ReturnsAsync(expectedResponse);
            var controller = new BranchesController(_mockBranch.Object);

            // Act
            var result = await controller.Update(updateBranchDto) as ObjectResult;

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
            _mockBranch.Setup(x => x.Delete(id)).ReturnsAsync(expectedResponse);
            var controller = new BranchesController(_mockBranch.Object);

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
            _mockBranch.Setup(x => x.Delete(id)).ReturnsAsync(expectedResponse);
            var controller = new BranchesController(_mockBranch.Object);

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
            _mockBranch.Setup(x => x.Delete(id)).ReturnsAsync(expectedResponse);
            var controller = new BranchesController(_mockBranch.Object);

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
            _mockBranch.Setup(x => x.Delete(id)).ReturnsAsync(expectedResponse);
            var controller = new BranchesController(_mockBranch.Object);

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