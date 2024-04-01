using Domain.Enums;

namespace Application.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Document { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public TypeUser? TypeUser { get; set; }
    }
}
