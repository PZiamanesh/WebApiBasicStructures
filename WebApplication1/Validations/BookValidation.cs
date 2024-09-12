using System.ComponentModel.DataAnnotations;
using WebApplication1.ViewModels;

namespace WebApplication1.Validations
{
    public class BookValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var bookDto = validationContext.ObjectInstance as CreateBook;

            if (bookDto == null)
            {
                throw new ArgumentNullException(nameof(bookDto));
            }

            if (bookDto.Description != null &&
                bookDto.Title.Trim().ToLower() == bookDto.Description.Trim().ToLower())
            {
                return new ValidationResult("Title and Description must not be same.");
            }

            return ValidationResult.Success;
        }
    }
}
