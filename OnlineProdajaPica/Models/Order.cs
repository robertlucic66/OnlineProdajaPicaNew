namespace OnlineProdajaPica.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Proizvodi { get; set; }
        public string Kolicine { get; set; }
        public string UserId { get; set; }
        public DateTime? DatumNarudzbe { get; set; }
        public bool Dostavljeno { get; set; }
    }
}
