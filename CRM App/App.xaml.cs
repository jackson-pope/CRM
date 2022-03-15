using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

using Backend.Models;
using Backend.ViewModels;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CRM_App
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary> 
        /// Gets the current <see cref="App"/> instance in use 
        /// </summary> 
        public new static App Current => (App)Application.Current;
        /// <summary> 
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services. 
        /// </summary> 
        public IServiceProvider Services { get; }
        /// <summary> 
        /// Configures the services for the application. 
        /// </summary> 
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            var connectionString = ConfigurationManager.ConnectionStrings["CRM-DB"].ConnectionString;
            services.AddDbContext<CrmContext>(options => { options.UseSqlServer(connectionString); });
            services.AddSingleton<MainViewModel>();
            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Services = ConfigureServices();
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        private Window m_window;
    }
}
