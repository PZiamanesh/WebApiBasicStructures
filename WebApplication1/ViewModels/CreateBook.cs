using System.ComponentModel.DataAnnotations;
using WebApplication1.Validations;

namespace WebApplication1.ViewModels
{
    [BookValidation]
    public class CreateBook
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string? Description { get; set; }
    }
}
