using CSVHandler.UI.Data.Abstract;
using CSVHandler.UI.Models;
using CSVHandler.UI.Services.Abstract;

namespace CSVHandler.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ICSVParserService ParserService { get; }
        private IPeopleRepository PeopleRepository { get; }
        private IXmlService XmlService { get; }

        public MainViewModel(ICSVParserService parserService, IPeopleRepository peopleRepository, IXmlService xmlService)
        {
            ParserService = parserService;
            PeopleRepository = peopleRepository;
            XmlService = xmlService;
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

        public async Task SavePeopleFromFileAsync()
        {
            List<Person> people = new List<Person>(ParserService.ParseCSV(InputFileName));
            await PeopleRepository.SaveManyAsync(people);
        }

        public void ExportToXml()
        {
            List<Person> people = new List<Person>(PeopleRepository.GetAll());
            XmlService.SaveToFile(people, "D:\\people.xml");
        }
    }
}
