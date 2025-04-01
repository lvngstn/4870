using Maui.eCommerce.ViewModels;
using Library.eCommerce.Models;

namespace Maui.eCommerce.Views;

public partial class ShoppingManagementView : ContentPage
{
	public ShoppingManagementView()
	{
		InitializeComponent();
		BindingContext = new ShoppingManagementViewModel();
	}

    private void AddToCartClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is Item item)
        {
            (BindingContext as ShoppingManagementViewModel)!.SelectedItem = item;
            (BindingContext as ShoppingManagementViewModel)!.PurchaseItem();
        }
    }

    private void RemoveFromCartClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is Item item)
        {
            (BindingContext as ShoppingManagementViewModel)!.SelectedCartItem = item;
            (BindingContext as ShoppingManagementViewModel)!.DecrementQuantity();
        }
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (BindingContext is ShoppingManagementViewModel viewModel)
        {
            viewModel.RefreshInventory();
        }
    }

    private void CheckoutClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Checkout");
    }

    private void InlineClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).RefreshInventory();
    }
}