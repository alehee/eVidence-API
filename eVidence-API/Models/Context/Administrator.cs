using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eVidence_API.Models.Context
{
    public class Administrator
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool PermissionAdministrator { get; set; } = false;

        [Required]
        public bool PermissionUser { get; set; } = false;

        [Required]
        public bool PermissionProcess { get; set; } = false;

        [Required]
        public bool PermissionReport { get; set; } = false;

        [Required]
        public DateTime CreatedAt { get; init; } = DateTime.Now;
    }
}
