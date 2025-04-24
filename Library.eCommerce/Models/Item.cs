using Google.Cloud.Firestore; // Add this
using Library.eCommerce.DTO;

namespace Library.eCommerce.Models
{
    [FirestoreData]
    public class Item
    {
        [FirestoreProperty]
        public int Id { get; set; }

        [FirestoreProperty]
        public ProductDTO? Product { get; set; }

        [FirestoreProperty]
        public int? Quantity { get; set; }

        public void IncrementQuantity() => Quantity++;

        public void DecrementQuantity()
        {
            if (Quantity > 0)
                Quantity--;
        }

        public Item()
        {
            Product = new ProductDTO();
            Quantity = 0;
        }

        public Item(Item i)
        {
            Product = new ProductDTO(i.Product);
            Quantity = i.Quantity;
            Id = i.Id;
        }
    }
}
