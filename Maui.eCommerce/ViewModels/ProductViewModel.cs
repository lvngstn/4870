using Library.eCommerce.Models;
using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.eCommerce.ViewModels
{
    public class ProductViewModel
    {
        public string? Name
        {
            get
            {
                return Model?.Product?.Name ?? string.Empty;
            }

            set
            {
                if (Model != null && Model.Product?.Name != value && Model.Product?.Name != null)
                {
                    Model.Product.Name = value;
                }
            }
        }

        public int? Quantity
        {
            get
            {
                return Model?.Quantity;
            }

            set
            {
                if (Model != null && Model.Quantity != value)
                {
                    Model.Quantity = value ?? 0;
                }
            }
        }

        public int? Price
        {
            get => Model?.Product?.Price;
            set
            {
                if (Model?.Product?.Price != value)
                {
                    Model.Product.Price = value ?? 0;
                }
            }
        }

        public Item? Model { get; set; }

        public void AddOrUpdate()
        {
            if (Model != null)
            {
                ProductServiceProxy.Current.AddOrUpdate(Model);
            }
        }

        public ProductViewModel()
        {
            Model = new Item();
        }

        public ProductViewModel(Item? model)
        {
            Model = model;
        }
    }
}