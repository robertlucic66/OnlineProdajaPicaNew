using OnlineProdajaPica.Models;

namespace OnlineProdajaPica.ViewModels
{
    public class OrderDetailsViewModel
    {
        public Order Order { get; set; }
        public CustomerInfo CustomerInfo { get; set; }
        public string Username { get; set; }
        public List<Product> Products { get; set; }
    }
}
