using CSVHandler.UI.Services.Abstract;
using CSVHandler.UI.ViewModels;
using System.Windows.Controls;

namespace CSVHandler.UI.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class ExportView : UserControl
    {
        public ExportView(IXmlService xmlService, IPersonService personService, IExcelService excelService)
        {
            InitializeComponent();
            DataContext = new ExportViewModel(xmlService, personService, excelService);
            Loaded += ExportView_Loaded;
        }

        private async void ExportView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await ((ExportViewModel)DataContext).LoadPeopleToGrid();
        }
    }
}
