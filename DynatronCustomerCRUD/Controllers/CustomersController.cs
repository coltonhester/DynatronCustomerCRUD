using Microsoft.AspNetCore.Mvc;
using DynatronCustomerCRUD.Models;

namespace DynatronCustomerCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private static readonly List<Customer> Customers = new List<Customer>()
        {
            new Customer { Id = 1, FirstName = "Bob", LastName = "Dillon", Email = "bobdillon@example.com", LastUpdatedDate = DateTime.Now },
            new Customer { Id = 2, FirstName = "Elroy", LastName = "Smith", Email = "elroysmith@example.com", LastUpdatedDate = DateTime.Now },
            new Customer { Id = 3, FirstName = "Tai", LastName = "Xiaoma", Email = "taixiaoma@example.com", LastUpdatedDate = DateTime.Now },
            new Customer { Id = 4, FirstName = "Kevin", LastName = "Bacon", Email = "kevinbacon@example.com", LastUpdatedDate = DateTime.Now },
            new Customer { Id = 5, FirstName = "Alena", LastName = "Zarcovia", Email = "alenazarcovia@example.com", LastUpdatedDate = DateTime.Now }
        };

        // GET: api/<CustomersController>
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            try
            {
                if (Customers == null)
                {
                    return NotFound();
                }

                return Customers;
            }
            catch (Exception ex)
            {
                // TODO: Log exception details once DB exists
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            try
            {
                var customer = Customers.Find(c => c.Id == id);
                if (customer == null)
                {
                    return NotFound();
                }

                return customer;
            }
            catch (Exception ex)
            {
                // TODO: Log exception details once DB exists
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // POST api/<CustomersController>
        [HttpPost]
        public ActionResult<Customer> Post([FromBody] CustomerCreateUpdateDto customerRequest)
        {
            try
            {
                // NOTE: temporary solution for Id += 1 until Entity Framework ORM / MySQL DB implementation
                var customer = new Customer
                {
                    Id = Customers.Max(c => c.Id) + 1,
                    FirstName = customerRequest.FirstName,
                    LastName = customerRequest.LastName,
                    Email = customerRequest.Email,
                    LastUpdatedDate = DateTime.Now,
                };
                Customers.Add(customer);
                return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
            }
            catch (Exception ex)
            {
                // TODO: Log exception details once DB exists
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public ActionResult<Customer> Put(int id, [FromBody] CustomerCreateUpdateDto customerRequest)
        {
            try
            {
                var customerToUpdate = Customers.FirstOrDefault(c => c.Id == id);
                if (customerToUpdate == null)
                {
                    return NotFound();
                }
                customerToUpdate.FirstName = customerRequest.FirstName;
                customerToUpdate.LastName = customerRequest.LastName;
                customerToUpdate.Email = customerRequest.Email;
                customerToUpdate.LastUpdatedDate = DateTime.Now;

                return customerToUpdate;
            }
            catch (Exception ex)
            {
                // TODO: Log exception details once DB exists
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public ActionResult<Customer> Delete(int id)
        {
            try
            {
                var customerToRemove = Customers.Find(c => c.Id == id);
                if (customerToRemove == null)
                {
                    return NotFound();
                }
                Customers.Remove(customerToRemove);

                return NoContent();
            }
            catch (Exception ex)
            {
                // TODO: Log exception details once DB exists
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
