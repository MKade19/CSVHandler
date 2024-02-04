using CSVHandler.UI.Data;
using CSVHandler.UI.Data.Abstract;
using CSVHandler.UI.Services;
using CSVHandler.UI.Services.Abstract;
using CSVHandler.UI.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        private ServiceProvider serviceProvider;
        private static List<CultureInfo> _languages = new List<CultureInfo>();

        public static List<CultureInfo> Languages
        {
            get
            {
                return _languages;
            }
        }

        public static event EventHandler LanguageChanged;

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

                //1. Меняем язык приложения:
                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                //2. Создаём ResourceDictionary для новой культуры
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

                //3. Находим старую ResourceDictionary и удаляем его и добавляем новую ResourceDictionary
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

                //4. Вызываем евент для оповещения всех окон.
                LanguageChanged(Application.Current, new EventArgs());
            }
        }

        public App()
        {
            _languages.Clear();
            _languages.Add(new CultureInfo("en-US")); //Нейтральная культура для этого проекта
            _languages.Add(new CultureInfo("ru-RU"));
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler);

            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
            //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
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
            services.AddTransient<IExcelService, ExcelService>();
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
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                LoggerConfiguration loggerConfiguration = new LoggerConfiguration().WriteTo.File("log.txt");

                builder.AddSerilog(loggerConfiguration.CreateLogger());
            });

            ILogger<MainWindow> mainWindowLogger = loggerFactory.CreateLogger<MainWindow>();
            
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }

}
 