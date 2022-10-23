using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [DisplayName("Slika")]
        public string? ImageUrl { get; set; }

        [DisplayName("Količina")]
        public int Quantity { get; set; }
    }
}
