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
        private readonly List<Customer> _customers;

        public CustomersControllerTests()
        {
            // Setup mock data
            _customers = new List<Customer>
            {
                new Customer { Id = 1, FirstName = "Bob", LastName = "Dillon", Email = "bobdillon@example.com", LastUpdatedDate = DateTime.Now },
                new Customer { Id = 2, FirstName = "Elroy", LastName = "Smith", Email = "elroysmith@example.com", LastUpdatedDate = DateTime.Now },
                new Customer { Id = 3, FirstName = "Tai", LastName = "Xiaoma", Email = "taixiaoma@example.com", LastUpdatedDate = DateTime.Now },
                new Customer { Id = 4, FirstName = "Kevin", LastName = "Bacon", Email = "kevinbacon@example.com", LastUpdatedDate = DateTime.Now },
                new Customer { Id = 5, FirstName = "Alena", LastName = "Zarcovia", Email = "alenazarcovia@example.com", LastUpdatedDate = DateTime.Now }
            };

            // Initialize controller with mock data
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

        // Add more tests for Get(id), Post, Put, Delete...
    }
}

