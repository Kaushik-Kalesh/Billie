using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Billie
{
    public sealed partial class CashierDashboardPage : Page
    {
        public CashierDashboardPage()
        {
            this.InitializeComponent();            
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }
    }
}
