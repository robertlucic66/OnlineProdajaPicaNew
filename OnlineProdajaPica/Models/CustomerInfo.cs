using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineProdajaPica.Models
{
    public class CustomerInfo
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required]
        [DisplayName("Ime")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Prezime")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Broj telefona")]
        public string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Mjesto")]
        public string City { get; set; }
        [Required]
        [DisplayName("Poštanski broj")]
        public string PostalCode { get; set; }
        [Required]
        [DisplayName("Država")]
        public string Country { get; set; }
        [Required]
        [DisplayName("Adresa")]
        public string Address { get; set; }
        public int OrderId { get; set; }
    }
}
