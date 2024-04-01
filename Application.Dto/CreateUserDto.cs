using Domain.Enums;
    
namespace Application.Dto
{
    public class CreateUserDto
    {
        public required string Document { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public TypeUser? TypeUser { get; set; }
    }
}
