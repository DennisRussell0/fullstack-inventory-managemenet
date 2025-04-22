using Microsoft.AspNetCore.Mvc;
using InventoryManagementBackend.Interfaces;
using InventoryManagementBackend.Entities;

namespace InventoryManagementBackend.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IDatabaseConnector dbConnector) : ControllerBase
    {
        
        
        [HttpGet]
        public IActionResult GetProducts()
        {

            // Retrieve products from the database
            List<Product> products = dbConnector.RetrieveProducts();

            // Check if the list is empty or nulls
            if (products == null || products.Count == 0)
            {
                return NotFound(new { message = "No products found." });
            };
            
            string imagesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "images");
            var productsWithImages = products.Select(product => {
                var productData = typeof(Product).GetProperties()
                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(product));

                // Add the image as Base64 if the ImagePath exists
            if (!string.IsNullOrEmpty(product.ImagePath) && System.IO.File.Exists(Path.Combine(imagesFolderPath, product.ImagePath)))
            {
                byte[] imageBytes = System.IO.File.ReadAllBytes(Path.Combine(imagesFolderPath, product.ImagePath));
                productData["Image"] = Convert.ToBase64String(imageBytes);
            }
            else
            {

                productData["Image"] = null;
          
            }
                return productData;
            });

            var response = new
            {
                message = "Products received successfully.",
                data = productsWithImages
            };



            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            Product? product = dbConnector.RetrieveProductById(id); // Replace with actual database call /!/ 
            if (product == null)
            {
                return NotFound(new { message = "Product not found." });
            }

            string imagesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "images"); 

            var productWithImage = typeof(Product).GetProperties()
                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(product));

                // Add the image as Base64 if the ImagePath exists
            if (!string.IsNullOrEmpty(product.ImagePath) && System.IO.File.Exists(Path.Combine(imagesFolderPath, product.ImagePath)))
            {
                byte[] imageBytes = System.IO.File.ReadAllBytes(Path.Combine(imagesFolderPath, product.ImagePath));
                productWithImage["Image"] = Convert.ToBase64String(imageBytes);
            }
            else
            {
                productWithImage["Image"] = null;
            }
 

            var response = new
            {
                message = "Product received successfully.",
                data = productWithImage
            };

            return Ok(response);
        }
        [HttpPost]
        public IActionResult PostProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest(new { message = "Invalid product data." });
            }

            dbConnector.PostProduct(product); // Replace with actual database call /!/ 

            var response = new
            {
                message = "Product added successfully.",
                data = product
            };

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, response);
        }
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, [FromBody] Product product)
        {
            if (product == null || product.Id != id)
            {
                return BadRequest(new { message = "Invalid product data." });
            }

            Product? existingProduct = dbConnector.RetrieveProductById(id); 
            if (existingProduct == null)
            {
                return NotFound(new { message = "Product not found." });
            }

            foreach (var prop in typeof(Product).GetProperties())
            {
                var newValue = prop.GetValue(product);
                if (newValue == null)
                {
                    prop.SetValue(product, prop.GetValue(existingProduct));
                }
            }
            
            bool success = dbConnector.UpdateProduct(product);
            if (!success)
            {
                return StatusCode(500, new { message = "Failed to update product." });
            }

            var response = new
            {
                message = "Product updated successfully.",
                data = product
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {

            // Check if the product exists in the database
            
            
            Product? product = dbConnector.RetrieveProductById(id); 
            if (product == null)
            {
                return NotFound(new { message = "Product not found." });
            }

            dbConnector.DeleteProduct(id); 

            var response = new
            {
                message = "Product deleted successfully."
            };

            return Ok(response);
        }

    }
}
