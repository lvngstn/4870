using Library.eCommerce.DTO;
using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Library.eCommerce.Services;

namespace Library.eCommerce.Models
{
    public class Item
    {
        public int Id { get; set; }
        public ProductDTO? Product { get; set; }
        public int? Quantity { get; set; }

        public ICommand? AddCommand { get; set; }
        public ICommand? RemoveCommand { get; set; }

        public override string ToString()
        {
            return $"{Product} Quantity:{Quantity}";
        }

        public void IncrementQuantity()
        {
            Quantity++;
        }
        public void DecrementQuantity()
        {
            if (Quantity > 0)
            {
                Quantity--;
            }
        }

        public string Display { 
            get
            {
                return Product?.Display ?? string.Empty;
            }
        }

        public Item()
        {
            Product = new ProductDTO();
            Quantity = 0;

            AddCommand = new Command(DoAdd);
            RemoveCommand = new Command(DoRemove);
        }

        public Item(Item i)
        {
            Product = new ProductDTO(i.Product);
            Quantity = i.Quantity;
            Id = i.Id;

            AddCommand = new Command(DoAdd);
            RemoveCommand = new Command(DoRemove);
        }

        private void DoAdd()
        {
            ShoppingCartService.Current.AddOrUpdate(this);
        }

        private void DoRemove()
        {
            ShoppingCartService.Current.DecrementQuantity(this);
        }

    }
}
