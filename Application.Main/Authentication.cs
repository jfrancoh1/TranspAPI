using Application.Dto;
using Application.Interface;
using Domain;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Infrastructure.Interface;
using AutoMapper;

namespace Application.Main
{
    public class Authentication : IAuthentication
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public Authentication(IConfiguration config, IUserRepository userRepository, IMapper mapper)
        {
            _config = config;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ResultRequestDto<GetInfoUserDto>> GetToken(LoginDto logindto)
        {
            try
            {
                Login login = _mapper.Map<Login>(logindto);

                User? user = _userRepository.GetUserByIdAndPassword(login);
                UserDto userDto = _mapper.Map<UserDto>(user);

                if (user != null)
                {
                    var secret = _config.GetSection("Jwt").GetSection("secretkey").ToString();
                    var issuer = _config.GetSection("Jwt:Issuer").Value;
                    var audience = _config.GetSection("Jwt:Audience").Value;
                    var keyBytes = Encoding.ASCII.GetBytes(secret);
                    var claims = new ClaimsIdentity();

                    claims.AddClaim(new Claim("id", login.Id.ToString()));
                    claims.AddClaim(new Claim("role", user.TypeUser.ToString()));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddMinutes(10),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                    string tokencreado = tokenHandler.WriteToken(tokenConfig);

                    GetInfoUserDto getInfoUserDto = new GetInfoUserDto
                    {
                        User = userDto,
                        token = tokencreado
                    };
                    return new ResultRequestDto<GetInfoUserDto> { Data = getInfoUserDto, IsSuccess = true, Message = "OK", Response = "200" };
                }

                return new ResultRequestDto<GetInfoUserDto> { Data = new GetInfoUserDto(), IsSuccess = false, Message = "User or Password Invalid", Response = "401" };

            }
            catch (Exception)
            {
                return new ResultRequestDto<GetInfoUserDto> { Data = new GetInfoUserDto(), IsSuccess = false, Message = "Internal Server Error! Try again later.", Response = "500" };
            }
        } 


    }
}
