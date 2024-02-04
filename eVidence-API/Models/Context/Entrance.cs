using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eVidence_API.Models.Context
{
    public class Entrance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        public Account Account { get; set; }

        [Required]
        public DateTime Enter { get; set; } = DateTime.Now;

        public DateTime? Exit { get; set; } = null;
    }
}
