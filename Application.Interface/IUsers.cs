using Application.Dto;

namespace Application.Interface
{
    public interface IUsers
    {
        Task<ResultRequestDto<UserDto>> Create(CreateUserDto input);
        Task<ResultRequestDto<UserDto>> Get(int id);
        Task<ResultRequestDto<List<UserDto>>> GetAll();
        Task<ResultRequestDto<UserDto>> GetUserByIdAndPassword(LoginDto input);
        Task<ResultRequestDto<UserDto>> Update(UpdateUserDto input);
        Task<ResultRequestDto<bool>> Delete(int id);

    }
}
