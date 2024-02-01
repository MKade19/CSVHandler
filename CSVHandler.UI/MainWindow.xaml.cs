using CSVHandler.UI.Services.Abstract;
using CSVHandler.UI.ViewModels;
using MahApps.Metro.Controls;

namespace CSVHandler.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow(ICSVParserService parserService, IPersonService personService, IExcelService excelService, IXmlService xmlService)
        {
            InitializeComponent();
            DataContext = new MainViewModel(parserService, excelService, xmlService, personService);
        }
    }
}