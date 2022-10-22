using OnlineProdajaPica.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineProdajaPica.ViewModels
{
    public class ProductViewModel
    {
        [DisplayName("Kategorija")]
        public IEnumerable<Category>? Categories { get; set; }

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

        [Required]
        [DisplayName("Kategorija")]
        public int CategoryId { get; set; }

        [DisplayName("Slika")]
        public string? ImageUrl { get; set; }

        public ProductViewModel()
        {
            Id = 0;
        }

        public ProductViewModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            NumberInStock = product.NumberInStock;
            CategoryId = product.CategoryId;
            ImageUrl = product.ImageUrl;
        }
    }
}
