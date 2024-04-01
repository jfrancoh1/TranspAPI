using Domain.Enums;
using Domain;

namespace Application.Dto
{
    public class CreateBranchDto
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required string Schedule { get; set; }
        public required TypeExchange Exchange { get; set; }
        public int UserId { get; set; }
    }
}
