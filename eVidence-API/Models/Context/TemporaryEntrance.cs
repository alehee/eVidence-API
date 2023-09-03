using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eVidence_API.Models.Context
{
    public class TemporaryEntrance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        public TemporaryCard TemporaryCard { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public DateTime Enter { get; init; } = DateTime.Now;

        public DateTime? Exit { get; set; } = null;
    }
}
