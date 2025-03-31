using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;
using Spring2025_Samples.Models;

namespace Maui.eCommerce.Views;

[QueryProperty(nameof(ProductId), "productId")]
public partial class ProductDetails : ContentPage
{
	public ProductDetails()
	{
		InitializeComponent();
		
	}

    private int _productId { get; set; }
    public int ProductId
    {
        get => _productId;
        set
        {
            _productId = value;
            OnPropertyChanged(nameof(ProductId));
            LoadProduct();
        }
    }

    private void GoBackClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//InventoryManagement");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as ProductViewModel)?.AddOrUpdate();
        
        Shell.Current.GoToAsync("//InventoryManagement");
    }

    private void LoadProduct()
    {
        if (ProductId > 0)
        {
            BindingContext = new ProductViewModel(ProductServiceProxy.Current.GetById(ProductId));
        } else
        {
            BindingContext = new ProductViewModel();
        }
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if(ProductId == 0)
        {
            BindingContext = new ProductViewModel();
        } else
        {
            BindingContext = new ProductViewModel(ProductServiceProxy.Current.GetById(ProductId));
        }
        
    }
}