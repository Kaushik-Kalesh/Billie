using Microsoft.UI.Xaml;

namespace Billie
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            UserPasswordAccess.InitializeDatabase();
        }

        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        private Window m_window;
    }
}
