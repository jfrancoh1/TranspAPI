using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain
{
    [Table("Users")]
    public class User : ClassBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }
        public required string Document { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public required string Password { get; set; }
        public required TypeUser TypeUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public virtual Branch Office {  get; set; }
    }
}

