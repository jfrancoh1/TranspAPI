using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain
{
    public class Branch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required string Schedule { get; set; }
        public required TypeExchange Exchange { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
