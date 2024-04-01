using Domain.Enums;
using Domain;

namespace Application.Dto
{
    public class BranchDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Schedule { get; set; }
        public TypeExchange Exchange { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int UserId { get; set; }
    }
}
