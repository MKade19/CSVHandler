using CSVHandler.UI.Data.Abstract;
using CSVHandler.UI.Services.Abstract;
using CSVHandler.UI.Util;
using CSVHandler.UI.ViewModels;
using System.Windows;

namespace CSVHandler.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(ICSVParserService parserService, IPersonService personService, IXmlService xmlService)
        {
            InitializeComponent();
            DataContext = new MainViewModel(parserService, personService, xmlService);
        }

        private void ExportToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxStore.Warning("iiiii");
        }
    }
}