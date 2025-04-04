using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryManagementBackend.Data;
using InventoryManagementBackend.Entities;

namespace InventoryManagementBackend.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        DatabaseConnector dbConnector = new DatabaseConnector(); // Initialize the database connector

        [HttpGet]
        public IActionResult GetProducts()
        {

            List<Product> products = dbConnector.RetrieveProducts(); // Replace with actual database call /!/
            if (products == null || products.Count == 0)
            {
                return NotFound(new { message = "No products found." });
            };

            var response = new
            {
                message = "Products received successfully.",
                data = products
            };

            return Ok(response);
        }
    }
}
