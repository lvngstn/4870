using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;
using Library.eCommerce.Models;

namespace Maui.eCommerce.Views;

public partial class InventoryManagementView : ContentPage
{
	public InventoryManagementView()
	{
		InitializeComponent();
		BindingContext = new InventoryManagementViewModel();
	}

    private void DeleteClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is Item item)
        {
            (BindingContext as InventoryManagementViewModel)!.SelectedProduct = item;
            (BindingContext as InventoryManagementViewModel)!.Delete();
        }
    }

    private void IncrementQuantityClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is Item item)
        {
            (BindingContext as InventoryManagementViewModel)!.SelectedProduct = item;
            (BindingContext as InventoryManagementViewModel)!.IncrementQuantity();
        }
    }
    private void DecrementQuantityClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is Item item)
        {
            (BindingContext as InventoryManagementViewModel)!.SelectedProduct = item;
            (BindingContext as InventoryManagementViewModel)!.DecrementQuantity();
        }
    }

    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Product");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.RefreshProductList();
    }

    private void EditClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is Item item)
        {
            (BindingContext as InventoryManagementViewModel)!.SelectedProduct = item;
            Shell.Current.GoToAsync($"//Product?productId={item?.Id}");
        }
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.RefreshProductList();
    }
}