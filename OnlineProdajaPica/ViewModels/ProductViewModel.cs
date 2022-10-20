﻿using OnlineProdajaPica.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineProdajaPica.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Category>? Categories { get; set; }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int NumberInStock { get; set; }

        [Required]
        public int CategoryId { get; set; }

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
