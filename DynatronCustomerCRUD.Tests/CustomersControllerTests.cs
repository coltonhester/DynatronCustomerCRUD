using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynatronCustomerCRUD;
using DynatronCustomerCRUD.Controllers;
using DynatronCustomerCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DynatronCustomerCRUD.Tests
{

    public class CustomersControllerTests
    {
        private readonly CustomersController _controller;
        private readonly CustomerCreateUpdateDto _customerRequest;
        private readonly DynatronDbContext _context;

        public CustomersControllerTests()
        {
            var options = new DbContextOptionsBuilder<DynatronDbContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Ensure unique DB for each test run
                        .Options;

            _context = new DynatronDbContext(options);
            // Arrange step: Seed the database with test data
            _context.Customers.AddRange(
                new Customer { Id = 1, FirstName = "Bob", LastName = "Schlansky", Email = "bobshclansky@example.com", LastUpdatedDate = DateTime.Now },
                new Customer { Id = 2, FirstName = "Elroy", LastName = "Smith", Email = "elroysmith@example.com", LastUpdatedDate = DateTime.Now }
            );
            _context.SaveChanges();

            _controller = new CustomersController(_context);

            _customerRequest = new CustomerCreateUpdateDto
            {
                FirstName = "First",
                LastName = "Last",
                Email = "customerrequest@example.com"
            };
        }

        [Fact]
        public async Task Get_ReturnsAllCustomers()
        {
            // Act
            var result = await _controller.Get();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Customer>>>(result);
            Assert.IsType<List<Customer>>(actionResult.Value);
            Assert.Equal(_context.Customers.ToList().Count, actionResult.Value.ToList().Count);
        }

        [Fact]
        public async Task Get_WithValidId_ReturnsCustomer()
        {
            // Act
            var result = await _controller.Get(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Customer>>(result);
            Assert.IsType<Customer>(actionResult.Value);
            Assert.Equal(await _context.Customers.FindAsync(1), actionResult.Value);
        }

        [Fact]
        public async Task Get_WithInvalidId_ReturnsNotFound()
        {
            var result = await _controller.Get(999);
                
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Post_ValidCustomer_ReturnsCreatedAtAction()
        {
            var newCustomer = _customerRequest;

            var result = await _controller.Post(newCustomer);

            var actionResult = Assert.IsType<ActionResult<Customer>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.NotNull(createdAtActionResult.Value);
            var customer = Assert.IsType<Customer>(createdAtActionResult.Value);

            // Verify that the customer was added
            var addedCustomer = await _context.Customers.FindAsync(customer.Id);
            Assert.NotNull(addedCustomer);
        }

        [Fact]
        public async Task Post_InvalidCustomer_ReturnsBadRequest()
        {
            var invalidCustomer = _customerRequest;
            invalidCustomer.FirstName = null;

            _controller.ModelState.AddModelError("FirstName", "Required");

            var result = await _controller.Post(invalidCustomer);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Put_ValidCustomer_ReturnsUpdatedCustomer()
        {
            var updateCustomer = _customerRequest;

            var result = await _controller.Put(1, updateCustomer);

            var actionResult = Assert.IsType<ActionResult<Customer>>(result);
            Assert.IsType<Customer>(actionResult.Value);
            Assert.Equal(await _context.Customers.FindAsync(1), actionResult.Value);
        }

        [Fact]
        public async Task Put_InvalidId_ReturnsNotFound()
        {
            var updateCustomer = _customerRequest;

            var result = await _controller.Put(999, updateCustomer);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Put_InvalidCustomer_ReturnsBadRequest()
        {
            var invalidCustomer = _customerRequest;
            invalidCustomer.FirstName = null;

            _controller.ModelState.AddModelError("FirstName", "Required");

            var result = await _controller.Put(1, invalidCustomer);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Delete_ExistingCustomer_ReturnsNoContent()
        {
            var result = await _controller.Delete(1);

            Assert.IsType<NoContentResult>(result.Result);

            // Verify that the customer was deleted
            var deletedCustomer = await _context.Customers.FindAsync(1);
            Assert.Null(deletedCustomer);
        }

        [Fact]
        public async Task Delete_NonExistingCustomer_ReturnsNotFound()
        {
            var result = await _controller.Delete(999);
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}