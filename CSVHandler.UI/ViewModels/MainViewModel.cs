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
        private IPersonService PersonService { get; }
        private IXmlService XmlService { get; }
        public DelegateCommand ShowFileDataCommand { get; }
        public DelegateCommand SaveToDbCommand { get; }
        public DelegateCommand ExportToXmlCommand { get; }
        public DelegateCommand ExportToExcelCommand { get; }

        public MainViewModel(ICSVParserService parserService, IPersonService personService, IXmlService xmlService)
        {
            ParserService = parserService;
            PersonService = personService;
            XmlService = xmlService;
            ShowFileDataCommand = new DelegateCommand(ShowFileDataCommand_Executed);
            SaveToDbCommand = new DelegateCommand(SaveToDbCommand_Executed);
            ExportToXmlCommand = new DelegateCommand(ExportToXmlCommand_Executed);
            ExportToExcelCommand = new DelegateCommand(ExportToExcelCommand_Executed);
        }

        private string _inputFileName = string.Empty;
        private bool _isCsvParsing = false;
        private bool _isBrowsingAllowed = true;
        private bool _areDataSaving = false;
        private bool _isExportingToXml = false;
        private bool _isExportingToExcel = false;

        public string InputFileName 
        { 
            get => _inputFileName;
            set
            {
                _inputFileName = value;
                OnPropertyChanged(nameof(InputFileName));
            }
        }

        public bool IsCsvParsing
        {
            get => _isCsvParsing;
            set
            {
                _isCsvParsing = value;
                OnPropertyChanged(nameof(IsCsvParsing));
            }
        }

        public bool IsBrowsingAllowed
        {
            get => _isBrowsingAllowed;
            set
            {
                _isBrowsingAllowed = value;
                OnPropertyChanged(nameof(IsBrowsingAllowed));
            }
        }

        public bool AreDataSaving
        {
            get => _areDataSaving;
            set
            {
                _areDataSaving = value;
                OnPropertyChanged(nameof(AreDataSaving));
            }
        }

        public bool IsExportingToXml
        {
            get => _isExportingToXml;
            set
            {
                _isExportingToXml = value;
                OnPropertyChanged(nameof(IsExportingToXml));
            }
        }

        public bool IsExportingToExcel
        {
            get => _isExportingToExcel;
            set
            {
                _isExportingToExcel = value;
                OnPropertyChanged(nameof(IsExportingToExcel));
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
                IsCsvParsing = true;
                IsBrowsingAllowed = false;
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
                IsCsvParsing = false;
                IsBrowsingAllowed = true;
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
                AreDataSaving = true;
                IsBrowsingAllowed = false;
                await PersonService.SaveManyAsync(PeopleToSave);
            }
            finally
            {
                AreDataSaving = false;
                IsBrowsingAllowed = true;
            }
        }

        public async void ExportToXmlCommand_Executed(object sender)
        {
            List<Person> people = new List<Person>(await PersonService.GetAllAsync());

            try
            {
                IsExportingToXml = true;
                await XmlService.SavePeopleToFileAsync(people, "D:\\people.xml");
            }
            finally
            {
                IsExportingToXml = false;
            }
        }

        public async void ExportToExcelCommand_Executed(object sender)
        {
            await Task.Run(() => { });
        }
    }
}
