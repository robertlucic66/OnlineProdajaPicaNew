using OnlineProdajaPica.Models;

namespace OnlineProdajaPica.ViewModels
{
    public class OrderCheckViewModel
    {
        public List<Product> ProductList { get; set; }
        public CustomerInfo CustomerInfo { get; set; }
    }
}
