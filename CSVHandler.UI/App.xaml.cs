using CSVHandler.UI.Data;
using CSVHandler.UI.Data.Abstract;
using CSVHandler.UI.Services;
using CSVHandler.UI.Services.Abstract;
using CSVHandler.UI.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CSVHandler.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler);

            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            MessageBoxStore.Error(ex.Message);
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<WindowCommands>();
            services.AddDbContextPool<ApplicationContext>(options => 
            {
                options.UseSqlServer("Server=.\\MKMSSQLSERVER;Database=PeopleDB;Trusted_Connection=True;TrustServerCertificate=True;");
            });
            services.AddTransient<ICSVParserService, CSVParserService>();
            services.AddTransient<IXmlService, XmlService>();
            services.AddTransient<IFileService, FileService>(); 
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IPersonRepository, PersonRepository>();
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBoxStore.Error(e.Exception.Message);
            e.Handled = true;
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }

}
