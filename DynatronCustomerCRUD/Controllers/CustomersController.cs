using Microsoft.AspNetCore.Mvc;
using DynatronCustomerCRUD.Models;

namespace DynatronCustomerCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly List<Customer> _customers;

        public CustomersController(List<Customer> customers)
        {
            //_customers = customers ?? new List<Customer>();
            _customers = customers;
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            try
            {
                if (_customers == null)
                {
                    return NotFound();
                }

                return _customers;
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
                var customer = _customers.Find(c => c.Id == id);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // NOTE: temporary solution for Id += 1 until Entity Framework ORM / MySQL DB implementation
                var customer = new Customer
                {
                    Id = _customers.Max(c => c.Id) + 1,
                    FirstName = customerRequest.FirstName,
                    LastName = customerRequest.LastName,
                    Email = customerRequest.Email,
                    LastUpdatedDate = DateTime.Now,
                };
                _customers.Add(customer);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var customerToUpdate = _customers.FirstOrDefault(c => c.Id == id);
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
                var customerToRemove = _customers.Find(c => c.Id == id);
                if (customerToRemove == null)
                {
                    return NotFound();
                }
                _customers.Remove(customerToRemove);

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
