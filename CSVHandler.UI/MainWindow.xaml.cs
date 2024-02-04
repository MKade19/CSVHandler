using CSVHandler.UI.Services.Abstract;
using CSVHandler.UI.ViewModels;
using MahApps.Metro.Controls;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Windows.Controls;

namespace CSVHandler.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        //private ILogger Logger { get; set; }

        public MainWindow(ICSVParserService parserService, IPersonService personService, IExcelService excelService, IXmlService xmlService)
        {
            InitializeComponent();
            DataContext = new MainViewModel(parserService, excelService, xmlService, personService);
            Loaded += MainWindow_Loaded;

            App.LanguageChanged += LanguageChanged;

            CultureInfo currLang = App.Language;

            //Заполняем меню смены языка:
            LanguageMenu.Items.Clear();
            foreach (var lang in App.Languages)
            {
                MenuItem menuLang = new MenuItem();
                menuLang.Header = lang.DisplayName;
                menuLang.Tag = lang;
                menuLang.IsChecked = lang.Equals(currLang);
                menuLang.Click += ChangeLanguageClick;
                LanguageMenu.Items.Add(menuLang);
            }
        }

        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;

            //Отмечаем нужный пункт смены языка как выбранный язык
            foreach (MenuItem i in LanguageMenu.Items)
            {
                CultureInfo ci = i.Tag as CultureInfo;
                i.IsChecked = ci != null && ci.Equals(currLang);
            }
        }

        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                CultureInfo lang = mi.Tag as CultureInfo;
                if (lang != null)
                {
                    App.Language = lang;
                }
            }
        }

        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //Logger.LogInformation("MainWindow has loaded.");
        }
    }
}