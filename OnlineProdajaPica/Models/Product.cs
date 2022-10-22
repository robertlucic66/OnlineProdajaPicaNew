using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineProdajaPica.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("Naziv")]
        public string Name { get; set; }

        [DisplayName("Opis")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Na stanju")]
        public int NumberInStock { get; set; }

        [DisplayName("Kategorija")]
        public Category? Category { get; set; }

        [Required]
        [DisplayName("Kategorija")]
        public int CategoryId { get; set; }

        [DisplayName("Slika")]
        public string? ImageUrl { get; set; }

        [DisplayName("Količina")]
        public int Quantity { get; set; }
    }
}
