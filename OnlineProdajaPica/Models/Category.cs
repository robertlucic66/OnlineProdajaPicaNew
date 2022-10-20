using System.ComponentModel.DataAnnotations;

namespace OnlineProdajaPica.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
