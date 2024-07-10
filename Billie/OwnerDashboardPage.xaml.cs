using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Billie
{    
    public sealed partial class OwnerDashboardPage : Page
    {
        public OwnerDashboardPage()
        {
            this.InitializeComponent();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }
    }
}
