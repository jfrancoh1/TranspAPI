using Domain.Enums;

namespace Application.Dto
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public required string Document { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public TypeUser TypeUser { get; set; }
    }
}
