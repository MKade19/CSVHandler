using CSVHandler.UI.Data.Abstract;
using CSVHandler.UI.Models;
using CSVHandler.UI.Services.Abstract;
using CSVHandler.UI.Util;
using System.Windows;
using System.Windows.Input;

namespace CSVHandler.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ICSVParserService ParserService { get; }
        private IPeopleRepository PeopleRepository { get; }
        private IXmlService XmlService { get; }
        public RoutedCommand SaveToDbCommand { get; }
        public RoutedCommand ExportToXmlCommand { get; }
        public RoutedCommand ExportToExcelCommand { get; }

        public MainViewModel(ICSVParserService parserService, IPeopleRepository peopleRepository, IXmlService xmlService)
        {
            ParserService = parserService;
            PeopleRepository = peopleRepository;
            XmlService = xmlService;
            SaveToDbCommand = new RoutedCommand(nameof(SaveToDbCommand), typeof(MainWindow));
            ExportToXmlCommand = new RoutedCommand(nameof(ExportToXmlCommand), typeof(MainWindow));
            ExportToExcelCommand = new RoutedCommand(nameof(ExportToExcelCommand), typeof(MainWindow));
        }

        private string _inputFileName = string.Empty;

        public string InputFileName 
        { 
            get => _inputFileName;
            set
            {
                _inputFileName = value;
                OnPropertyChanged(nameof(InputFileName));
            }
        }

        //public async void SaveToDbCommand_Executed(object sender, RoutedEventArgs e)
        public async Task SaveToDbCommand_Executed()
        {
            if (string.IsNullOrEmpty(InputFileName))
            {
                MessageBoxStore.Warning("Choose the existing file!");
                return;
            }

            List<Person> people = new List<Person>(await ParserService.ParsePeopleCSV(InputFileName));
            await PeopleRepository.SaveManyAsync(people);
        }

        public async void ExportToXmlCommand_Executed(object sender, RoutedEventArgs e)
        {
            List<Person> people = new List<Person>(await PeopleRepository.GetAllAsync());
            await XmlService.SavePeopleToFile(people, "D:\\people.xml");
        }

        public async void ExportToExcelCommand_Executed(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => { });
        }
    }
}
