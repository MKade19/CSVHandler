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
        private const int RecordsLimit = 1000;
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

            try
            {
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
            }
            finally
            {
                EventAggregator.Instance.RaiseShowFileDataEndedEvent();
            }
        }

        public async void SaveToDbCommand_Executed(object sender)
        {
            if (string.IsNullOrEmpty(InputFileName))
            { 
                MessageBoxStore.Warning("Choose the existing file!");
                return;
            }

            try
            {
                EventAggregator.Instance.RaiseSaveToDbStartedEvent();

                foreach (var peopleChunk in PeopleToSaveEnumerator())
                {
                    await PeopleRepository.SaveManyAsync(peopleChunk);
                }
            }
            finally
            {
                EventAggregator.Instance.RaiseSaveToDbEndedEvent();
            }
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

        private IEnumerable<List<Person>> PeopleToSaveEnumerator()
        {
            for(int i = 0; i < PeopleToSave.Count; i += RecordsLimit)
            {
                List<Person> peopleChunk = new List<Person>();

                for (int j = i; j < i + RecordsLimit && j < PeopleToSave.Count; j++)
                {
                    peopleChunk.Add(PeopleToSave[j]);
                }

                yield return peopleChunk;
            }
        }
    }
}
