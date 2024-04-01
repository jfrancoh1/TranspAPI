using Application.Dto;

namespace Application.Interface
{
    public interface IAuthentication
    {
        Task<ResultRequestDto<GetInfoUserDto>> GetToken(LoginDto logindto);
    }
}
