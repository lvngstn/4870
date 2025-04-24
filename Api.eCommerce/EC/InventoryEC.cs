using Api.eCommerce.Database;
using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using Newtonsoft.Json;
using Spring2025_Samples.Models;

namespace Api.eCommerce.EC
{

    public class InventoryEC
    {
        public FirebaseService db;
        public InventoryEC()
        {
            db = new FirebaseService();
        }

        public async Task<List<Item?>> GetAllProducts()
        {
            return await db.GetAllProducts();
        }

        public async Task<Item?> GetProduct(int id)
        {
            return await db.GetProduct(id.ToString());
        }

        public async Task<Item?> AddProduct(Item item)
        {
            return await db.AddProduct(item);
        }

        public async Task UpdateProduct(int id, ProductDTO product)
        {
            await db.UpdateProduct(id, product);
        }
        public async Task<Item?> DeleteProduct(int id)
        {
            return await db.DeleteProduct(id);
        }

        public async Task<Item?> IncrementProductQuantity(int id)
        {
            return await db.IncrementProductQuantity(id);
        }

        public async Task<Item?> DecrementProductQuantity(int id)
        {
            return await db.DecrementProductQuantity(id);
        }

    }
}