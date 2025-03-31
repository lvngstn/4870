using Maui.eCommerce.ViewModels;
using Library.eCommerce.Models;

namespace Maui.eCommerce.Views;

public partial class CheckoutView : ContentPage
{
    public CheckoutView()
    {
        InitializeComponent();
        BindingContext = new CheckoutViewModel();
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (BindingContext is CheckoutViewModel viewModel)
        {
            viewModel.RefreshInventory();
        }
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ShoppingManagement");
    }

    private void PurchaseClicked(object sender, EventArgs e)
    {
        (BindingContext as CheckoutViewModel)!.PurchaseCart();
    }
}