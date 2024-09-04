using System.ComponentModel.DataAnnotations;
using WebApplication1.Validations;

namespace WebApplication1.DomainModels
{
    public class Person
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "{0} length must be between {2} to {1}")]
        public string Email { get; set; }

        [AddressValidation]
        public string? Address { get; set; }

        [Required]
        [Compare(nameof(ConfirmPassword))]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
