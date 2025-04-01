using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.eCommerce.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Price { get; set; }

        public string? Display
        {
            get
            {
                return $"{Id}. {Name} ${Price}";
            }
        }

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

        public override string ToString()
        {
            return Display ?? string.Empty;
        }
    }
}