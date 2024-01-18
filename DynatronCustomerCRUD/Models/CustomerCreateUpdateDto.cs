using System.ComponentModel.DataAnnotations;

namespace DynatronCustomerCRUD.Models
{
    public class CustomerCreateUpdateDto
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }
    }
}