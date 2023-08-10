using System.ComponentModel.DataAnnotations;

namespace eVidence_API.Models
{
    public class Entity
    {
        [Key]
        public int Id { get; init; }
        [Required]
        public string Name { get; init; }
    }
}
