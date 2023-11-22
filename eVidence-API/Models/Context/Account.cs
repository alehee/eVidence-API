using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eVidence_API.Models.Context
{
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        public Department Department { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string? Keycard { get; set; }

        [Required]
        public DateTime CreatedAt { get; init; } = DateTime.Now;

        public DateTime? DeletedAt { get; set; } = null;
    }
}
