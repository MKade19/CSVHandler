using CSVHandler.UI.Data;
using CSVHandler.UI.Data.Abstract;
using CSVHandler.UI.Services;
using CSVHandler.UI.Services.Abstract;
using CSVHandler.UI.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Globalization;
using System.Windows;

namespace CSVHandler.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        private static List<CultureInfo> _languages = new List<CultureInfo>();

        public App()
        {
            _languages.Clear();
            _languages.Add(new CultureInfo("en-US"));
            _languages.Add(new CultureInfo("ru-RU"));
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler);

            _host = Host.CreateDefaultBuilder()
                .UseSerilog((host, loggerConfiguration) =>
                {
                    loggerConfiguration
                    .WriteTo.File("log.txt")
                    .MinimumLevel.Debug();
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<WindowCommands>();
                    services.AddDbContextPool<ApplicationContext>(options =>
                    {
                        options.UseSqlServer("Server=.\\MKMSSQLSERVER;Database=PeopleDB;Trusted_Connection=True;TrustServerCertificate=True;");
                    });
                    services.AddTransient<ICSVParserService, CSVParserService>();
                    services.AddTransient<IXmlService, XmlService>();
                    services.AddTransient<IExcelService, ExcelService>();
                    services.AddTransient<IFileService, FileService>();
                    services.AddTransient<IPersonService, PersonService>();
                    services.AddTransient<IPersonRepository, PersonRepository>();
                })
                .Build();
        }

        private void ExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            MessageBoxStore.Error(ex.Message);
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBoxStore.Error(e.Exception.Message);
            e.Handled = true;
        }
        
        public static List<CultureInfo> Languages
        {
            get
            {
                return _languages;
            }
        }

        public static event EventHandler? LanguageChanged;

        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value==null) throw new ArgumentNullException("value");
                if (value==System.Threading.Thread.CurrentThread.CurrentUICulture) return;

                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                ResourceDictionary dict = new ResourceDictionary();
                switch (value.Name)
                {
                    case "ru-RU":
                        dict.Source = new Uri(String.Format("Resources/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri("Resources/lang.xaml", UriKind.Relative);
                        break;
                }

                ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                                              where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
                                              select d).First();
                if (oldDict != null)
                {
                    int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }

                LanguageChanged(Application.Current, new EventArgs());
            }
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _host.Services.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
 