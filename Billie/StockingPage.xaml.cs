using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;

namespace Billie
{
    public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public float Price { get; set; }
        public long Barcode { get; set; }
        public int StockQuantity { get; set; }
    }

    public sealed partial class StockingPage : Page
    {
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        public StockingPage()
        {
            this.InitializeComponent();            
            CustomerDataGrid.ItemsSource = Products;
        }        

        private async void AddProductButton_Click(object sender, RoutedEventArgs e)
        {            
            if (NewBarcodeTextBox.Text.Length == 13 && Int64.TryParse(NewBarcodeTextBox.Text, out long barcode) == true)
            {                
                var product = await ApiService.FetchProductDetailsFromApi(barcode);
                if (product != null)
                {
                    Products.Add(new Product
                    {
                        ID = Products.Count + 1,
                        ProductName = product.product_name,
                        Brand = product.brands,
                        Price = 0,
                        Barcode = barcode,
                        StockQuantity = 0
                    });
                }
            }             
        }
    }
}
