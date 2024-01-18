using Microsoft.AspNetCore.Mvc;
using DynatronCustomerCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace DynatronCustomerCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DynatronDbContext _context;

        public CustomersController(DynatronDbContext context)
        {
            _context = context;
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            try
            {
                var customers = await _context.Customers.ToListAsync();
                if (customers == null)
                {
                    return NotFound();
                }

                return customers;
            }
            catch (Exception ex)
            {
                // TODO: Log exception details
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }
                return customer;
            }
            catch (Exception ex)
            {
                // TODO: Log exception details
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<ActionResult<Customer>> Post([FromBody] CustomerCreateUpdateDto customerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var customer = new Customer
                {
                    FirstName = customerRequest.FirstName,
                    LastName = customerRequest.LastName,
                    Email = customerRequest.Email,
                    LastUpdatedDate = DateTime.Now,
                };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
            }
            catch (Exception ex)
            {
                // TODO: Log exception details
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> Put(int id, [FromBody] CustomerCreateUpdateDto customerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var customer = await _context.Customers.FindAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }
                customer.FirstName = customerRequest.FirstName;
                customer.LastName = customerRequest.LastName;
                customer.Email = customerRequest.Email;
                customer.LastUpdatedDate = DateTime.Now;

                return customer;
                //return NoContent();  //prefer returning more visible results...
            }
            catch (Exception ex)
            {
                // TODO: Log exception details
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete(int id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // TODO: Log exception details
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
