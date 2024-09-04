using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Validations
{
    public class AddressValidation : ValidationAttribute
    {
        private string _defualtErrMsq = "your alone!";

        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
            => value switch
            {
                null => null,
                "Gajil" => new ValidationResult(ErrorMessage ?? _defualtErrMsq),
                _ => ValidationResult.Success
            };
    }
}
