using CSVHandler.UI.Data.Abstract;
using CSVHandler.UI.Models;
using CSVHandler.UI.Services.Abstract;
using CSVHandler.UI.Util;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CSVHandler.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ICSVParserService ParserService { get; }
        private IPeopleRepository PeopleRepository { get; }
        private IXmlService XmlService { get; }
        public DelegateCommand ShowFileDataCommand { get; }
        public DelegateCommand SaveToDbCommand { get; }
        public RoutedCommand ExportToXmlCommand { get; }
        public RoutedCommand ExportToExcelCommand { get; }

        public MainViewModel(ICSVParserService parserService, IPeopleRepository peopleRepository, IXmlService xmlService)
        {
            ParserService = parserService;
            PeopleRepository = peopleRepository;
            XmlService = xmlService;
            ShowFileDataCommand = new DelegateCommand(ShowFileDataCommand_Executed);
            SaveToDbCommand = new DelegateCommand(SaveToDbCommand_Executed);
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


        private ObservableCollection<Person> _peopleToSave = new ObservableCollection<Person>();

        public ObservableCollection<Person> PeopleToSave
        {
            get => _peopleToSave;
            set
            {
                _peopleToSave = value;
                OnPropertyChanged(nameof(PeopleToSave));
            }
        }

        public async void ShowFileDataCommand_Executed(object sender)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == false)
            {
                return;
            }

            InputFileName = openFileDialog.FileName;

            EventAggregator.Instance.RaiseShowFileDataStartedEvent();
            PeopleToSave.Clear();

            await foreach (var peopleChunk in ParserService.ParsePeopleCSV(InputFileName))
            {
                List<Person> people = new List<Person>(peopleChunk);

                foreach (var person in people) 
                {
                    PeopleToSave.Add(person);
                }
            }

            EventAggregator.Instance.RaiseShowFileDataEndedEvent();
        }

        public async void SaveToDbCommand_Executed(object sender)
        {
            if (string.IsNullOrEmpty(InputFileName))
            { 
                MessageBoxStore.Warning("Choose the existing file!");
                return;
            }

            EventAggregator.Instance.RaiseSaveToDbStartedEvent();

            await PeopleRepository.SaveManyAsync(new List<Person>(PeopleToSave));

            EventAggregator.Instance.RaiseSaveToDbEndedEvent();
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
