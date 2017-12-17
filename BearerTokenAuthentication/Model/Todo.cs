using System.ComponentModel.DataAnnotations;

namespace BearerTokenAuthentication.Model
{
    public class Todo
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public bool IsComplete { get; set; }
    }
}
