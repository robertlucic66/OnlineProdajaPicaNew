using OnlineProdajaPica.Models;

namespace OnlineProdajaPica.ViewModels
{
    public class OrdersViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public List<Product> Products { get; set; }
        public DateTime? DatumNarudzbe { get; set; }

        public OrdersViewModel(Order order)
        {
            Id = order.Id;
            DatumNarudzbe = order.DatumNarudzbe;
        }
    }
}
