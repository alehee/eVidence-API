using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eVidence_API.Models.Context
{
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Required, Column(Order = 1)]
        public Department Department { get; set; }

        [Required, Column(Order = 2)]
        public string Name { get; set; }

        [Required, Column(Order = 3)]
        public string Surname { get; set; }

        [Required, Column(Order = 4)]
        public string Keycard { get; set; }

        [Required, Column(Order = 5)]
        public DateTime CreatedAt { get; } = DateTime.Now;

        [Column(Order = 6)]
        public DateTime? DeletedAt { get; set; } = null;
    }
}
