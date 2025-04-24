using Google.Cloud.Firestore;
using Spring2025_Samples.Models;

namespace Library.eCommerce.DTO
{
    [FirestoreData]
    public class ProductDTO
    {
        [FirestoreProperty]
        public int Id { get; set; }

        [FirestoreProperty]
        public string? Name { get; set; }

        [FirestoreProperty]
        public int? Price { get; set; }

        public ProductDTO()
        {
            Name = string.Empty;
        }

        public ProductDTO(Product? product)
        {
            if (product != null)
            {
                Id = product.Id;
                Name = product.Name;
                Price = product.Price;
            }
        }

        public ProductDTO(ProductDTO? product)
        {
            if (product != null)
            {
                Id = product.Id;
                Name = product.Name;
                Price = product.Price;
            }
        }
    }
}
