using Api.eCommerce.EC;
using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Spring2025_Samples.Models;
using System.Security.Cryptography.Xml;

namespace Api.eCommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {

        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Item?> Get(int id)
        {
            return new InventoryEC().GetAllProducts().Result;
        }

        [HttpGet("{id}")]
        public Item? GetById(int id)
        {
            return new InventoryEC().GetProduct(id).Result;
        }

        [HttpDelete("{id}")]
        public Item? Delete(int id)
        {
            return new InventoryEC().DeleteProduct(id).Result;
        }

        [HttpPost("add")]
        public async Task<Item?> AddProduct([FromBody] Item item)
        {
            var newItem = await new InventoryEC().AddProduct(item);
            return newItem;
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTO product)
        {
            var inventoryEC = new InventoryEC();
            var existingProduct = await inventoryEC.GetProduct(id);

            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            await inventoryEC.UpdateProduct(id, product);
            return Ok($"Product with ID {id} updated successfully.");
        }

        [HttpPut("increment/{id}")]
        public async Task<IActionResult> IncrementProductQuantity(int id)
        {
            var inventoryEC = new InventoryEC();
            var product = await inventoryEC.IncrementProductQuantity(id);

            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return Ok(product);
        }

        [HttpPut("decrement/{id}")]
        public async Task<IActionResult> DecrementProductQuantity(int id)
        {
            var inventoryEC = new InventoryEC();
            var product = await inventoryEC.DecrementProductQuantity(id);

            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return Ok(product);
        }
    }
}