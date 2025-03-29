using System.ComponentModel.DataAnnotations;

namespace SportShop2025.ViewModel
{
    public class CheckoutViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PaymentMethod { get; set; } 
        public CartViewModel Cart { get; set; } = new CartViewModel();



    }
}
