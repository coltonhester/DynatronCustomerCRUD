using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynatronCustomerCRUD;
using DynatronCustomerCRUD.Controllers;
using DynatronCustomerCRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace DynatronCustomerCRUD.Tests
{

    public class CustomersControllerTests
    {
        private readonly CustomersController _controller;
        private readonly CustomerCreateUpdateDto _customerRequest;
        private readonly List<Customer> _customers;

        public CustomersControllerTests()
        {
            // Setup mock data
            _customers = new List<Customer>
            {
                new Customer { Id = 1, FirstName = "Bob", LastName = "Sclansky", Email = "bobdillon@example.com", LastUpdatedDate = DateTime.Now },
                new Customer { Id = 2, FirstName = "Elroy", LastName = "Smith", Email = "elroysmith@example.com", LastUpdatedDate = DateTime.Now },
                new Customer { Id = 3, FirstName = "Tai", LastName = "Xiaoma", Email = "taixiaoma@example.com", LastUpdatedDate = DateTime.Now },
                new Customer { Id = 4, FirstName = "Kevin", LastName = "Bacon", Email = "kevinbacon@example.com", LastUpdatedDate = DateTime.Now },
                new Customer { Id = 5, FirstName = "Alena", LastName = "Zarcovia", Email = "alenazarcovia@example.com", LastUpdatedDate = DateTime.Now }
            };
            _customerRequest = new CustomerCreateUpdateDto
            {
                FirstName = "First",
                LastName = "Last",
                Email = "customerrequest@example.com"
            };
            _controller = new CustomersController(_customers);
        }

        [Fact]
        public void Get_ReturnsAllCustomers()
        {
            // Act
            var result = _controller.Get();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Customer>>>(result);
            var returnValue = Assert.IsType<List<Customer>>(actionResult.Value);
            Assert.Equal(_customers.Count, returnValue.Count);
        }

        [Fact]
        public void Get_WithValidId_ReturnsCustomer()
        {
            // Act
            var result = _controller.Get(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Customer>>(result);
            var returnValue = Assert.IsType<Customer>(actionResult.Value);
            Assert.Equal(_customers[0], returnValue);
        }

        [Fact]
        public void Get_WithInvalidId_ReturnsNotFound()
        {
            var result = _controller.Get(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Post_ValidCustomer_ReturnsCreatedAtAction()
        {
            var newCustomer = _customerRequest;

            var result = _controller.Post(newCustomer);

            var actionResult = Assert.IsType<ActionResult<Customer>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.NotNull(createdAtActionResult.Value);
        }

        [Fact]
        public void Post_InvalidCustomer_ReturnsBadRequest()
        {
            var invalidCustomer = _customerRequest;
            invalidCustomer.FirstName = null;

            _controller.ModelState.AddModelError("FirstName", "Required");

            var result = _controller.Post(invalidCustomer);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void Put_ValidCustomer_ReturnsUpdatedCustomer()
        {
            var updateCustomer = _customerRequest;

            var result = _controller.Put(1, updateCustomer);

            var actionResult = Assert.IsType<ActionResult<Customer>>(result);
            var returnValue = Assert.IsType<Customer>(actionResult.Value);
            Assert.Equal(_customers[0], returnValue);
        }

        [Fact]
        public void Put_InvalidId_ReturnsNotFound()
        {
            var updateCustomer = _customerRequest;

            var result = _controller.Put(999, updateCustomer);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Put_InvalidCustomer_ReturnsBadRequest()
        {
            var invalidCustomer = _customerRequest;
            invalidCustomer.FirstName = null;

            _controller.ModelState.AddModelError("FirstName", "Required");

            var result = _controller.Put(1, invalidCustomer);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void Delete_ExistingCustomer_ReturnsNoContent()
        {
            var result = _controller.Delete(1);

            var actionResult = Assert.IsType<ActionResult<Customer>>(result);
            Assert.IsType<NoContentResult>(actionResult.Result);
        }

        [Fact]
        public void Delete_NonExistingCustomer_ReturnsNotFound()
        {
            var result = _controller.Delete(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}