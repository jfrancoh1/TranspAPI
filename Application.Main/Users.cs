using Application.Dto;
using Application.Interface;
using AutoMapper;
using Domain;
using Infrastructure.Interface;

namespace Application.Main
{

    public class Users : IUsers
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public Users(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ResultRequestDto<UserDto>> Create(CreateUserDto input)
        {
            User user = _mapper.Map<User>(input);
            UserDto userDto = _mapper.Map<UserDto>(user);
            try
            {
                user.CreateDate = DateTime.Now;
                userDto.CreateDate = user.CreateDate;
                userDto.Id = _userRepository.Create(user);
                return new ResultRequestDto<UserDto> { Data = userDto, IsSuccess = true, Message = "User Created.", Response = "201" };
            }
            catch (Exception)
            {
                return new ResultRequestDto<UserDto> { Data = userDto, IsSuccess = true, Message = "Internal Server Error! Try again later.", Response = "500" };
            }
        }

        public async Task<ResultRequestDto<UserDto>> Get(int id)
        {
            try
            {
                User? user = _userRepository.Get(id);
                UserDto userDto = _mapper.Map<UserDto>(user);

                if (user != null)
                {
                    return new ResultRequestDto<UserDto> { Data = userDto, IsSuccess = true, Message = "OK", Response = "200" };
                }
                else
                {
                    return new ResultRequestDto<UserDto> { Data = new UserDto(), IsSuccess = false, Message = "User Not Found.", Response = "404" };
                }
            }
            catch (Exception)
            {
                return new ResultRequestDto<UserDto> { Data = new UserDto(), IsSuccess = false, Message = "Internal Server Error! Try again later.", Response = "500" };
            }
        }

        public async Task<ResultRequestDto<List<UserDto>>> GetAll()
        {
            try
            {
                List<User> users = _userRepository.GetAll();
                List<UserDto> lsusersDto = new();

                foreach (User user in users)
                {
                    var userDto = _mapper.Map<UserDto>(user);
                    lsusersDto.Add(userDto);
                }

                return new ResultRequestDto<List<UserDto>> { Data = lsusersDto, IsSuccess = true, Message = "OK", Response = "200" };
            }
            catch (Exception)
            {
                return new ResultRequestDto<List<UserDto>> { Data = new List<UserDto>(), IsSuccess = false, Message = "Internal Server Error! Try again later.", Response = "500" };
            }
        }

        public async Task<ResultRequestDto<UserDto>> GetUserByIdAndPassword(LoginDto input)
        {
            Login login = _mapper.Map<Login>(input);
            try
            {
                User? user = _userRepository.GetUserByIdAndPassword(login);
                UserDto userDto = _mapper.Map<UserDto>(user);

                if (user != null)
                {
                    return new ResultRequestDto<UserDto> { Data = userDto, IsSuccess = true, Message = "OK", Response = "200" };
                }
                else
                {
                    return new ResultRequestDto<UserDto> { Data = new UserDto(), IsSuccess = false, Message = "User Not Found.", Response = "404" };
                }
            }
            catch (Exception)
            {
                return new ResultRequestDto<UserDto> { Data = new UserDto(), IsSuccess = false, Message = "Internal Server Error! Try again later.", Response = "500" };
            }
        }

        public async Task<ResultRequestDto<UserDto>> Update(UpdateUserDto input)
        {
            UserDto userDto = _mapper.Map<UserDto>(input);
            try
            {
                User? user = _userRepository.Get(input.Id);
                if (user == null)
                {
                    return new ResultRequestDto<UserDto> { Data = userDto, IsSuccess = false, Message = "User Not Found.", Response = "404" };
                }
                if (!string.IsNullOrEmpty(input.Document) && user.Document != input.Document)
                    user.Document = input.Document;
                if (!string.IsNullOrEmpty(input.Name) && user.Name != input.Name)
                    user.Name = input.Name;
                if (!string.IsNullOrEmpty(input.LastName) && user.LastName != input.LastName)
                    user.LastName = input.LastName;
                if (!string.IsNullOrEmpty(input.PhoneNumber) && user.PhoneNumber != input.PhoneNumber)
                    user.PhoneNumber = input.PhoneNumber;
                if (!string.IsNullOrEmpty(input.Password) && user.Password != input.Password)
                    user.Password = input.Password;
                if (user.TypeUser != input.TypeUser)
                    user.TypeUser = input.TypeUser;

                UserDto userDtoOk = _mapper.Map<UserDto>(user);
                user.UpdateDate = DateTime.Now;
                userDto.UpdateDate = user.UpdateDate;
                _userRepository.Update(user);

                return new ResultRequestDto<UserDto> { Data = userDtoOk, IsSuccess = true, Message = "User Updated", Response = "201" };
            }
            catch (Exception)
            {
                return new ResultRequestDto<UserDto> { Data = userDto, IsSuccess = false, Message = "Internal Server Error! Try again later.", Response = "500" };
            }
        }

        public async Task<ResultRequestDto<bool>> Delete(int id)
        {
            try
            {
                User? user = _userRepository.Get(id);
                UserDto userDto = _mapper.Map<UserDto>(user);

                if (user != null)
                {
                    _userRepository.Delete(user);
                    return new ResultRequestDto<bool> { Data = true, IsSuccess = true, Message = "OK", Response = "200" };
                }
                else
                {
                    return new ResultRequestDto<bool> { Data = false, IsSuccess = false, Message = "User Not Found.", Response = "404" };
                }
            }
            catch (Exception)
            {
                return new ResultRequestDto<bool> { Data = false, IsSuccess = false, Message = "Internal Server Error! Try again later.", Response = "500" };
            }
        }
    }
}
