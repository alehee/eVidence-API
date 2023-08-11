using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eVidence_API.Models.Context
{
    public class Department
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Required, Column(Order = 1)]
        public Group Group { get; set; }

        [Required, Column(Order = 2)]
        public string Name { get; set; }
    }
}
