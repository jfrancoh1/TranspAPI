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
    public class BranchesApplicationTest
    {
        private readonly Mock<IBranchRepository> _mockBranchRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private IMapper _mapper;

        public BranchesApplicationTest()
        {
            _mockBranchRepository = new Mock<IBranchRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            // Configurar AutoMapper si es necesario
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateBranchDto, Branch>();
                cfg.CreateMap<Branch, BranchDto>();
                cfg.CreateMap<BranchDto, Branch>();
                cfg.CreateMap<UpdateBranchDto, BranchDto>();
            });
            _mapper = config.CreateMapper();
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
            Branch branch = _mapper.Map<Branch>(createBranchDto);
            BranchDto branchDto = _mapper.Map<BranchDto>(branch);
            _mockUserRepository.Setup(x => x.Get(createBranchDto.UserId)).Returns(user);
            _mockBranchRepository.Setup(x => x.Create(branch)).Returns(branch.Id);
            var branches = new Branches(_mockUserRepository.Object, _mockBranchRepository.Object, _mapper);

            // Act
            var result = await branches.Create(createBranchDto);
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("201", result.Response);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Create_Returns_NotFound_When_User_Isnt()
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
            Branch branch = _mapper.Map<Branch>(createBranchDto);
            BranchDto branchDto = _mapper.Map<BranchDto>(branch);
            _mockUserRepository.Setup(x => x.Get(createBranchDto.UserId));
            _mockBranchRepository.Setup(x => x.Create(branch)).Returns(branch.Id);
            var branches = new Branches(_mockUserRepository.Object, _mockBranchRepository.Object, _mapper);

            // Act
            var result = await branches.Create(createBranchDto);
            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.Equal("404", result.Response);
            Assert.Equal("User Not Found.", result.Message);
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
            Branch branch = _mapper.Map<Branch>(createBranchDto);
            _mockUserRepository.Setup(x => x.Get(createBranchDto.UserId)).Returns(user);
            _mockBranchRepository.Setup(repo => repo.Create(It.IsAny<Branch>())).Throws(new Exception("Test exception"));
            var branches = new Branches(_mockUserRepository.Object, _mockBranchRepository.Object, _mapper);

            // Act
            var result = await branches.Create(createBranchDto);
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
            UpdateBranchDto updateBranchDto = new UpdateBranchDto
            {
                Id = 1,
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
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
            BranchDto branchdto = _mapper.Map<BranchDto>(updateBranchDto);
            Branch branch = _mapper.Map<Branch>(branchdto);
            _mockBranchRepository.Setup(x => x.Get(updateBranchDto.Id)).Returns(branch);
            _mockBranchRepository.Setup(x => x.Update(branch)).Returns(branch.Id);
            var branches = new Branches(_mockUserRepository.Object, _mockBranchRepository.Object, _mapper);

            // Act
            var result = await branches.Update(updateBranchDto);
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("201", result.Response);
        }

        
        [Fact]
        public async Task Update_Returns_NotFound_When_Branch_Isnt()
        {
            UpdateBranchDto updateBranchDto = new UpdateBranchDto
            {
                Id = 1,
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
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
            BranchDto branchdto = _mapper.Map<BranchDto>(updateBranchDto);
            Branch branch = _mapper.Map<Branch>(branchdto);
            _mockBranchRepository.Setup(x => x.Get(updateBranchDto.Id));
            _mockBranchRepository.Setup(x => x.Update(branch)).Returns(branch.Id);
            var branches = new Branches(_mockUserRepository.Object, _mockBranchRepository.Object, _mapper);

            // Act
            var result = await branches.Update(updateBranchDto);
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("404", result.Response);
            Assert.Equal("Branch Not Found.", result.Message);
            Assert.False(result.IsSuccess);
        }


        
        [Fact]
        public async Task Update_Returns_InternalServerError_When_Service_Fails()
        {
            UpdateBranchDto updateBranchDto = new UpdateBranchDto
            {
                Id = 1,
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
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
            BranchDto branchdto = _mapper.Map<BranchDto>(updateBranchDto);
            Branch branch = _mapper.Map<Branch>(branchdto);
            _mockBranchRepository.Setup(x => x.Get(updateBranchDto.Id)).Returns(branch);
            _mockBranchRepository.Setup(x => x.Update(It.IsAny<Branch>())).Throws(new Exception("Test exception"));
            var branches = new Branches(_mockUserRepository.Object, _mockBranchRepository.Object, _mapper);

            // Act
            var result = await branches.Update(updateBranchDto);
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
            Branch branch = new Branch
            {
                Id = 1,
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            _mockBranchRepository.Setup(x => x.Get(id)).Returns(branch);
            var branches = new Branches(_mockUserRepository.Object, _mockBranchRepository.Object, _mapper);

            // Act
            var result = await branches.Get(id);
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("200", result.Response);
        }


        [Fact]
        public async Task Get_Returns_NotFound_When_Branch_Isnt()
        {
            int id = 1;
            Branch branch = new Branch
            {
                Id = 1,
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            _mockBranchRepository.Setup(x => x.Get(id));
            var branches = new Branches(_mockUserRepository.Object, _mockBranchRepository.Object, _mapper);

            // Act
            var result = await branches.Get(id);
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("404", result.Response);
            Assert.Equal("Branch Not Found.", result.Message);
            Assert.False(result.IsSuccess);
        }



        [Fact]
        public async Task Get_Returns_InternalServerError_When_Service_Fails()
        {
            int id = 1;
            Branch branch = new Branch
            {
                Id = 1,
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            _mockBranchRepository.Setup(x => x.Get(It.IsAny<int>())).Throws(new Exception("Test exception"));
            var branches = new Branches(_mockUserRepository.Object, _mockBranchRepository.Object, _mapper);

            // Act
            var result = await branches.Get(id);

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
            List<Branch> lsBranches = new ();
            Branch branch = new Branch
            {
                Id = 1,
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            lsBranches.Add(branch);
            _mockBranchRepository.Setup(x => x.GetAll()).Returns(lsBranches);
            var branches = new Branches(_mockUserRepository.Object, _mockBranchRepository.Object, _mapper);

            // Act
            var result = await branches.GetAll();
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("200", result.Response);
        }

        [Fact]
        public async Task GetAll_Returns_InternalServerError_When_Service_Fails()
        {
            List<Branch> lsBranches = new();
            Branch branch = new Branch
            {
                Id = 1,
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            lsBranches.Add(branch);
            _mockBranchRepository.Setup(x => x.GetAll()).Throws(new Exception("Test exception"));
            var branches = new Branches(_mockUserRepository.Object, _mockBranchRepository.Object, _mapper);

            // Act
            var result = await branches.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("500", result.Response);
        }
        #endregion

        #region Tests GetByUser
        [Fact]
        public async Task GetByUser_Returns_Ok_When_Successful()
        {
            int id = 1;
            List<Branch> lsBranches = new();
            Branch branch = new Branch
            {
                Id = 1,
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            lsBranches.Add(branch);
            _mockBranchRepository.Setup(x => x.GetByUser(id)).Returns(lsBranches);
            var branches = new Branches(_mockUserRepository.Object, _mockBranchRepository.Object, _mapper);

            // Act
            var result = await branches.GetByUser(id);
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("200", result.Response);
        }


        [Fact]
        public async Task GetByUser_Returns_NotFound_When_Branch_Isnt()
        {
            int id = 1;
            List<Branch> lsBranches = new();
            _mockBranchRepository.Setup(x => x.GetByUser(id)).Returns(lsBranches);
            var branches = new Branches(_mockUserRepository.Object, _mockBranchRepository.Object, _mapper);

            // Act
            var result = await branches.GetByUser(id);
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal("404", result.Response);
            Assert.Equal("Branch Not Found.", result.Message);
            Assert.False(result.IsSuccess);
        }



        [Fact]
        public async Task GetByUser_Returns_InternalServerError_When_Service_Fails()
        {
            int id = 1;
            List<Branch> lsBranches = new();
            Branch branch = new Branch
            {
                Id = 1,
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            lsBranches.Add(branch);
            _mockBranchRepository.Setup(x => x.GetByUser(It.IsAny<int>())).Throws(new Exception("Test exception"));
            var branches = new Branches(_mockUserRepository.Object, _mockBranchRepository.Object, _mapper);

            // Act
            var result = await branches.GetByUser(id);

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
            Branch branch = new Branch
            {
                Id = 1,
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            _mockBranchRepository.Setup(x => x.Get(id)).Returns(branch);
            _mockBranchRepository.Setup(x => x.Delete(branch)).Returns(id);
            var branches = new Branches(_mockUserRepository.Object, _mockBranchRepository.Object, _mapper);

            // Act
            var result = await branches.Delete(id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("200", result.Response);
            Assert.True(result.Data);
        }


        [Fact]
        public async Task Delete_Returns_NotFound_When_Branch_Isnt()
        {
            int id = 1;
            Branch branch = new Branch
            {
                Id = 1,
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            _mockBranchRepository.Setup(x => x.Get(id));
            _mockBranchRepository.Setup(x => x.Delete(branch)).Returns(id);
            var branches = new Branches(_mockUserRepository.Object, _mockBranchRepository.Object, _mapper);

            // Act
            var result = await branches.Delete(id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("404", result.Response);
            Assert.Equal("Branch Not Found.", result.Message);
            Assert.False(result.IsSuccess);
        }



        [Fact]
        public async Task Delete_Returns_InternalServerError_When_Service_Fails()
        {

            int id = 1;
            Branch branch = new Branch
            {
                Id = 1,
                Name = "Alpina",
                Address = "Calle 44 # 12 - 70 Barrio Las Camelias",
                PhoneNumber = "3154096906",
                Email = "gerencia@alpina.com",
                Schedule = "8:00am - 6:00pm",
                Exchange = TypeExchange.COP,
                UserId = 2
            };
            _mockBranchRepository.Setup(x => x.Get(It.IsAny<int>())).Throws(new Exception("Test exception"));
            _mockBranchRepository.Setup(x => x.Delete(branch)).Returns(id);
            var branches = new Branches(_mockUserRepository.Object, _mockBranchRepository.Object, _mapper);

            // Act
            var result = await branches.Delete(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("500", result.Response);
            Assert.False(result.IsSuccess);
        }
        #endregion
    }
}