using CSVHandler.UI.Services.Abstract;
using CSVHandler.UI.Views;
using System.Windows.Controls;

namespace CSVHandler.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ICSVParserService ParserService { get; }
        private IExcelService ExcelService { get; }
        private IXmlService XmlService { get; }
        private IPersonService PersonService { get; }
        public DelegateCommand GoToParserCommand { get; }
        public DelegateCommand GoToExportCommand { get; }

        private UserControl _activeView;

        public MainViewModel(ICSVParserService parserService, IExcelService excelService, IXmlService xmlService, IPersonService personService)
        {
            ParserService = parserService;
            ExcelService = excelService;
            XmlService = xmlService;
            PersonService = personService;
            GoToParserCommand = new DelegateCommand(GoToParserCommand_Executed);
            GoToExportCommand = new DelegateCommand(GoToExportCommand_Executed);
            ActiveView = new SaveToDbView(ParserService, PersonService);
        }

        public UserControl ActiveView
        {
            get => _activeView;
            set
            {
                _activeView = value;
                OnPropertyChanged(nameof(ActiveView));
            }
        }
        
        public void GoToParserCommand_Executed(object sender)
        {
            ActiveView = new SaveToDbView(ParserService, PersonService);
        }
        
        public void GoToExportCommand_Executed(object sender)
        {
            ActiveView = new ExportView(XmlService, PersonService, ExcelService);
        } 
    }
}
