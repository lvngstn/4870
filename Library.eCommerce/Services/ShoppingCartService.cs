using Library.eCommerce.Models;
using Library.eCommerce.Util;
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

            var response = new WebRequestHandler().Put($"/Inventory/decrement/{item.Id}", item.Id).Result;
            return existingInvItem;
        }
        public Item? DecrementQuantity(Item item)
        {
            var existingInvItem = _prodSvc.GetById(item.Id);
            if (existingInvItem == null)
            {
                return null;
            } else
            {
                existingInvItem.Quantity++;
            }

            var existingItem = CartItems.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null && existingItem.Quantity <= 0)
            {
                CartItems.Remove(existingItem);
            } else if (existingItem != null)
            {
                existingItem.Quantity--;
            }

            var response = new WebRequestHandler().Put($"/Inventory/increment/{item.Id}", item.Id).Result;
            return existingItem;
        }

        public void ClearCart()
        {
            CartItems.Clear();
        }
    }
}
