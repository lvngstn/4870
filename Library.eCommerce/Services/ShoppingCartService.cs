using Library.eCommerce.Models;
using Spring2025_Samples.Models;
using System.Collections.Generic;
using System.Linq;

namespace Library.eCommerce.Services
{
    public class ShoppingCartService
    {
        private readonly ShoppingCartService _cartSvc;
        private readonly ProductServiceProxy _prodSvc = ProductServiceProxy.Current;
        private readonly List<Item> items;
        public List<Item> CartItems
        {
            get
            {
                return items;
            }
        }
        public static ShoppingCartService Current {  
            get
            {
                instance ??= new ShoppingCartService();
                return instance;
            } 
        }
        private static ShoppingCartService? instance;
        private ShoppingCartService()
        {
            items = new List<Item>();
        }

        private int LastKey
        {
            get
            {
                if (!CartItems.Any())
                {
                    return 0;
                }

                return CartItems.Select(p => p?.Id ?? 0).Max();
            }
        }
        public Item? AddOrUpdate(Item item)
        {
            var existingInvItem = _prodSvc.GetById(item.Id);
            if (existingInvItem == null || existingInvItem.Quantity == 0)
            {
                return null; // would return an exception in a completed project
            }

            if (existingInvItem != null)
            {
                existingInvItem.Quantity--;
            }
            
            var existingItem = CartItems.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem == null)
            {
                var newItem = new Item(item);
                newItem.Quantity = 1;
                CartItems.Add(newItem);
            } else
            {
                existingItem.Quantity++;
            }
            return existingInvItem;
        }
        public Item? DecrementQuantity(Item item)
        {

            var existingInvItem = _prodSvc.GetById(item.Id);
            if (existingInvItem == null)
            {
                return null; // would return an exception in a completed project
            }

            if (existingInvItem != null)
            {
                existingInvItem.Quantity++;
            }

            var existingItem = CartItems.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null && existingItem.Quantity == 0)
            {
                CartItems.Remove(existingItem);
            }
            else
            {
                existingItem.Quantity--;
            }
            return existingItem;
        }

        public Item? Delete(int id)
        {
            if (id == 0)
            {
                return null;
            }

            Item? product = CartItems.FirstOrDefault(p => p.Id == id);
            CartItems.Remove(product);

            return product;
        }
        public Item? GetById(int id)
        {
            return CartItems.FirstOrDefault(p => p.Id == id);
        }
        public void ClearCart()
        {
            CartItems.Clear();
        }

    }
}
