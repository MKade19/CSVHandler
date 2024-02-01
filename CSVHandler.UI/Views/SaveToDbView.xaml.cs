using CSVHandler.UI.Services.Abstract;
using CSVHandler.UI.ViewModels;
using System.Windows.Controls;

namespace CSVHandler.UI.Views
{
    /// <summary>
    /// Interaction logic for CSVReaderView.xaml
    /// </summary>
    public partial class SaveToDbView : UserControl
    {
        public SaveToDbView(ICSVParserService parserService, IPersonService personService)
        {
            InitializeComponent();
            DataContext = new SaveToDbViewModel(parserService, personService);
        }
    }
}
