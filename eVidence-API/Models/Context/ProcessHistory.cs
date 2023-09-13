using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eVidence_API.Models.Context
{
    public class ProcessHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        public Account? Account { get; set; } = null;

        [Required]
        public TemporaryEntrance? TemporaryEntrance { get; set; } = null;

        [Required]
        public Department Department { get; set; }

        [Required]
        public Process Process { get; set; }

        [Required]
        public DateTime Start { get; set; } = DateTime.Now;

        public DateTime? Stop { get; set; } = null;
    }
}
