using Library.eCommerce.Models;
using Library.eCommerce.Services;
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
    public class ShoppingManagementViewModel : INotifyPropertyChanged
    {
        private readonly ProductServiceProxy _invSvc = ProductServiceProxy.Current;
        private readonly ShoppingCartService _cartSvc = ShoppingCartService.Current;
        public event PropertyChangedEventHandler? PropertyChanged;

        public Item? SelectedItem { get; set; }
        public Item? SelectedCartItem { get; set; }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int? _cost;
        public int? Cost
        {
            get => _cost;
            private set
            {
                _cost = value;
                NotifyPropertyChanged(nameof(Cost));
            }
        }

        private ObservableCollection<Item?> _inventory;
        public ObservableCollection<Item?> Inventory
        {
            get => _inventory;
            private set
            {
                _inventory = value;
                NotifyPropertyChanged(nameof(Inventory));
            }
        }

        private ObservableCollection<Item?> _shoppingCart;
        public ObservableCollection<Item?> ShoppingCart
        {
            get => _shoppingCart;
            private set
            {
                _shoppingCart = value;
                NotifyPropertyChanged(nameof(ShoppingCart));
            }
        }

        public ShoppingManagementViewModel()
        {
            _inventory = new ObservableCollection<Item?>();
            _shoppingCart = new ObservableCollection<Item?>();
            RefreshInventory();
        }

        public void RefreshInventory()
        {
            Inventory = new ObservableCollection<Item?>(_invSvc.Products
                .Where(i => i?.Quantity > 0));

            ShoppingCart = new ObservableCollection<Item?>(_cartSvc.CartItems
                .Where(i => i?.Quantity > 0));

            NotifyPropertyChanged(nameof(Inventory));
            NotifyPropertyChanged(nameof(ShoppingCart));
            calculateCost();
        }

        public void PurchaseItem()
        {
            if (SelectedItem != null)
            {
                var shouldRefresh = SelectedItem.Quantity > 0;
                var updatedItem = _cartSvc.AddOrUpdate(SelectedItem);

                if (updatedItem != null && shouldRefresh)
                {
                    RefreshInventory();
                }
            }
        }

        public void DecrementQuantity()
        {
            if (SelectedCartItem != null)
            {
                var shouldRefresh = SelectedCartItem.Quantity > 0;
                var updatedItem = _cartSvc.DecrementQuantity(SelectedCartItem);

                if (updatedItem != null && shouldRefresh)
                {
                    RefreshInventory();
                }
            }
        }

        public void calculateCost()
        {
            int totalCost = 0;

            foreach (Item? i in ShoppingCart)
            {
                totalCost += (i?.Product?.Price ?? 0) * (i?.Quantity ?? 0);
            }

            Cost = totalCost;
        }
    }
}
