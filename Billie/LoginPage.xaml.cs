using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.System;
using System;

namespace Billie
{
    public enum UserType
    {
        Cashier,
        Owner
    }

    public sealed partial class LoginPage : Page
    {
        private UserType currentUserType;

        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void ShowMessageDialog(string message)
        {
            await new ContentDialog
            {
                Title = "Message",
                Content = message,
                PrimaryButtonText = "Ok",
                XamlRoot = MainGrid.XamlRoot
            }.ShowAsync();
        }

        private void VerifyPassword(string password)
        {
            string actualPassword = UserPasswordAccess.GetPassword(currentUserType);
            // Handle First Time Login
            if (actualPassword == null)
            {
                UserPasswordAccess.UpdatePassword(currentUserType, password);
                ShowMessageDialog("Welcome Aboard!");
                return;
            }

            if (actualPassword != password)
            {
                ForgotPasswordStackPanel.Visibility = Visibility.Visible;
            }
            else
            {
                ForgotPasswordStackPanel.Visibility = Visibility.Collapsed;
                if (currentUserType == UserType.Cashier)
                {
                    Frame.Navigate(typeof(CashierDashboardPage));
                }
                else
                {
                    Frame.Navigate(typeof(OwnerDashboardPage));
                }
            }
        }

        private void GetPassword(UserType userType)
        {
            currentUserType = userType;

            if (userType == UserType.Cashier)
            {
                OwnerButton.Visibility = Visibility.Collapsed;
                CashierButton.Margin = new Thickness(0);
            }
            else
            {
                CashierButton.Visibility = Visibility.Collapsed;
            }

            UserPasswordBox.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
        }


        private void CashierButton_Click(object sender, RoutedEventArgs e)
        {
            GetPassword(UserType.Cashier);
        }

        private void OwnerButton_Click(object sender, RoutedEventArgs e)
        {
            GetPassword(UserType.Owner);
        }

        private void UserPasswordBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                VerifyPassword(UserPasswordBox.Password);
            }
        }

        private void ForgotPasswordPasswordBox_Click(object sender, RoutedEventArgs e)
        {
            // Setup Email Recovery
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            UserPasswordBox.Password = "";
            UserPasswordBox.Visibility = Visibility.Collapsed;
            ForgotPasswordStackPanel.Visibility = Visibility.Collapsed;
            BackButton.Visibility = Visibility.Collapsed;
            if (currentUserType == UserType.Cashier)
            {
                OwnerButton.Visibility = Visibility.Visible;
            }
            else
            {
                CashierButton.Visibility = Visibility.Visible;
            }
            CashierButton.Margin = new Thickness(0, 0, 8, 0);
        }
    }
}
