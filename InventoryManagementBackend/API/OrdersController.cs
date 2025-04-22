using Microsoft.AspNetCore.Mvc;
using InventoryManagementBackend.Interfaces;
using InventoryManagementBackend.Entities;

namespace InventoryManagementBackend.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IDatabaseConnector dbConnector) : ControllerBase
    {
        
        
        [HttpGet]
        public IActionResult GetOrders()
        {

            // Retrieve orders from the database
            List<Order> orders = dbConnector.RetrieveOrders();

            // Check if the list is empty or nulls
            if (orders == null || orders.Count == 0)
            {
                return Ok(new { message = "No orders found.", data = new List<Order>() });
            };


            var response = new
            {
                message = "Orders received successfully.",
                data = orders
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            Order? order = dbConnector.RetrieveOrderById(id); 
            if (order == null)
            {
                return NotFound(new { message = "Order not found." });
            }

            var response = new
            {
                message = "Order received successfully.",
                data = order
            };

            return Ok(response);
        }

        // Endpoint for creating a new order
        [HttpPost]
        public IActionResult PostOrder([FromBody] Order order)
        {
            float price = 0;

            // Validate that the order and its product list are not null or empty
            if (order == null || order.Products == null || order.Products.Count == 0)
            {
                return BadRequest(new { message = "Invalid order data." });
            }

            // Check each product in the order for valid ID and quantity
            foreach (var product in order.Products)
            {
                if (product.ProductId <= 0 || product.Quantity <= 0) // Item1 = ProductId, Item2 = Quantity
                {
                    return BadRequest(new { message = "Invalid product ID or quantity in order." });
                }
                Product? productById = dbConnector.RetrieveProductById(product.ProductId);
                if (productById == null){
                    continue;
                }
                price += productById.Price * product.Quantity;

            }
            order.Price = price;

            // Attempt to store the order in the database
            bool success = dbConnector.PostOrder(order);
            if (!success)
            {
                return StatusCode(500, new { message = "Failed to create order." });
            }

            // Respond with confirmation and created order
            var response = new
            {
                message = "Order created successfully.",
                data = order
            };

            return CreatedAtAction(nameof(PostOrder), new { id = order.Id }, response);
        }

        // Endpoint for updating an existing order
        [HttpPut("{id}")]
        public IActionResult PutOrder(int id, [FromBody] Order order)
        {
            // Validate that the order data matches the given ID and contains valid product entries
            if (order == null || order.Id != id || order.Products == null || order.Products.Count == 0)
            {
                return BadRequest(new { message = "Invalid order data." });
            }

            // Check if the order to update exists
            var existingOrder = dbConnector.RetrieveOrderById(id);
            if (existingOrder == null)
            {
                return NotFound(new { message = "Order not found." });
            }

            // Validate product IDs and quantities
            foreach (var product in order.Products)
            {
                if (product.ProductId <= 0 || product.Quantity <= 0)
                {
                    return BadRequest(new { message = "Invalid product ID or quantity in order." });
                }
            }

            // Attempt to update the order in the database
            bool success = dbConnector.UpdateOrder(order);
            if (!success)
            {
                return StatusCode(500, new { message = "Failed to update order." });
            }

            var response = new
            {
                message = "Order updated successfully.",
                data = order
            };

            return Ok(response);
        }

        // Endpoint for deleting an existing order
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            // Check if the order exists
            var order = dbConnector.RetrieveOrderById(id);
            if (order == null)
            {
                return NotFound(new { message = "Order not found." });
            }

            // Attempt to delete the order
            bool success = dbConnector.DeleteOrder(id);
            if (!success)
            {
                return StatusCode(500, new { message = "Failed to delete order." });
            }

            var response = new
            {
                message = "Order deleted successfully."
            };

            return Ok(response);
        }
    }
}
