using Microsoft.Extensions.DependencyInjection;
using Skillbox.CustomersApp.Data;
using Skillbox.CustomersApp.Model;
using Skillbox.CustomersApp.ViewModel;
using System.Windows;

namespace Skillbox.CustomersApp
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Настраиваем dependency injection 
        /// Внедряем зависимости сразу, чтобы потом по всему коду конструкторы не писать.
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<ManagerViewModel>();
            services.AddTransient<ConsultantViewModel>();
            services.AddTransient<Consultant>();
            services.AddTransient<Manager>();

            services.AddTransient<ICustomersDataProvider, CustomerDataProvider>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
