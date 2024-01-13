using System.ComponentModel.DataAnnotations;

namespace DynatronCustomerCRUD.Models
{
    public class Customer
    {
        public Customer()
        {
            CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        public DateTime CreatedDate { get; private set; }
        public DateTime LastUpdatedDate { get; set; }
    }

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
