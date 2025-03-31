using System.Collections.Generic;
using Library.eCommerce.Services;
using Library.eCommerce.Models;
using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maui.eCommerce.ViewModels
{
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        public Item? SelectedProduct { get; set; }
        public string? Query { get; set; }
        private readonly ProductServiceProxy _svc = ProductServiceProxy.Current;
        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is not null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                throw new ArgumentNullException(nameof(propertyName));
            }
        }

        public void RefreshProductList()
        {
            NotifyPropertyChanged(nameof(Products));
        }

        public ObservableCollection<Item?> Products
        {
            get
            {
                var filteredList = _svc.Products.Where(p => p?.Product?.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty, StringComparison.CurrentCultureIgnoreCase) ?? false);
                return new ObservableCollection<Item?>(filteredList);
            }
        }

        public Item? Delete()
        {
            var item = _svc.Delete(SelectedProduct?.Id ?? 0);
            NotifyPropertyChanged(nameof(Products));
            return item;
        }

        public Item? IncrementQuantity()
        {
            Console.WriteLine(SelectedProduct?.Id ?? -1);
            var item = _svc.IncrementQuantity(SelectedProduct?.Id ?? 0);
            NotifyPropertyChanged(nameof(Products));
            return item;
        }
        public Item? DecrementQuantity()
        {
            Console.WriteLine(SelectedProduct?.Id ?? -1);
            var item = _svc.DecrementQuantity(SelectedProduct?.Id ?? 0);
            NotifyPropertyChanged(nameof(Products));
            return item;
        }

    }
}