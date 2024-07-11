using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace Billie
{
    public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public float Price { get; set; }
        public int Barcode { get; set; }
        public int StockQuantity { get; set; }

        public Product(int id, string productName, string category, float price, int barcode, int stockQuantity)
        {
            ID = id;
            ProductName = productName;
            Category = category;
            Price = price;
            Barcode = barcode;
            StockQuantity = stockQuantity;
        }
    }

    public sealed partial class StockingPage : Page
    {
        public ObservableCollection<Product> Products { get; set; }

        public StockingPage()
        {
            this.InitializeComponent();
            Products = new ObservableCollection<Product>
            {
                new Product(1, "Lays", "Food", 25, 567890, 50)
            };
            CustomerDataGrid.ItemsSource = Products;
        }

        private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            Products.Add(new Product(0, null, null, 0, 0, 0));
        }
    }
}
