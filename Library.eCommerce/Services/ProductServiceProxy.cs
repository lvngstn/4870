using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using Library.eCommerce.Util;
using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Library.eCommerce.Services
{
    public class ProductServiceProxy
    {
        private ProductServiceProxy()
        {
            var productPayload = new WebRequestHandler().Get("/Inventory").Result;
            Products = JsonConvert.DeserializeObject<List<Item?>>(productPayload) ?? new List<Item?>();
        }
        private static ProductServiceProxy? instance;
        private static readonly object instanceLock = new object();
        public static ProductServiceProxy Current
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductServiceProxy();
                    }
                }

                return instance;
            }
        }

        public List<Item?> Products { get; private set; }

        private int GetNextHighestId()
        {
            if (Products == null || !Products.Any())
            {
                return 1;
            }

            return Products
                .Where(p => p != null)
                .Max(p => p!.Id) + 1;
        }

        public Item AddOrUpdate(Item item)
        {
            if (item.Id == 0)
            {
                item.Id = GetNextHighestId();
                Products.Add(item);
            }
            else
            {
                Delete(item.Id);
                var existingItem = Products.FirstOrDefault(p => p?.Id == item.Id);
                if (existingItem != null)
                {
                    var index = Products.IndexOf(existingItem);
                    Products.RemoveAt(index);
                    Products.Insert(index, new Item(item));
                }
                else
                {
                    Products.Add(item);
                }
            }

            var response = new WebRequestHandler().Post("/Inventory/add", item).Result;
            return item;
        }

        public Item? Delete(int id)
        {
            if(id == 0)
            {
                return null;
            }

            var result = new WebRequestHandler().Delete($"/Inventory/{id}").Result;

            Item? product = Products.FirstOrDefault(p => p?.Id == id);
            Products.Remove(product);

            return JsonConvert.DeserializeObject<Item>(result ?? "");
        }

        public Item? GetById(int id)
        {
            return Products.FirstOrDefault(p => p?.Id == id);
        }

        public Item? IncrementQuantity(int id)
        {
            var item = GetById(id);
            item?.IncrementQuantity();
            var response = new WebRequestHandler().Put($"/Inventory/increment/{id}", id).Result;
            return item;
        }
        public Item? DecrementQuantity(int id)
        {
            var item = GetById(id);
            item?.DecrementQuantity();
            var response = new WebRequestHandler().Put($"/Inventory/decrement/{id}", id).Result;
            return item;
        }
    }
}
