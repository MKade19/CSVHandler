using CSVHandler.UI.Models;
using CSVHandler.UI.Services.Abstract;
using CSVHandler.UI.Util;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace CSVHandler.UI.ViewModels
{
    public class SaveToDbViewModel : ViewModelBase
    {
        private ICSVParserService ParserService { get; }
        private IPersonService PersonService { get; }
        public DelegateCommand ShowFileDataCommand { get; }
        public DelegateCommand SaveToDbCommand { get; }
        public DelegateCommand RefreshCommand { get; }

        private string _inputFileName = string.Empty;
        private bool _areDataLoading = false;
        private ObservableCollection<Person> _peopleToSave = new ObservableCollection<Person>();

        public SaveToDbViewModel(ICSVParserService parserService, IPersonService personService)
        {
            ParserService = parserService;
            PersonService = personService; 
            ShowFileDataCommand = new DelegateCommand(ShowFileDataCommand_Executed);
            SaveToDbCommand = new DelegateCommand(SaveToDbCommand_Executed);
            RefreshCommand = new DelegateCommand(RefreshCommand_Executed);
        }

        public ObservableCollection<Person> PeopleToSave
        {
            get => _peopleToSave;
            set
            {
                _peopleToSave = value;
                OnPropertyChanged(nameof(PeopleToSave));
            }
        }

        public string InputFileName
        {
            get => _inputFileName;
            set
            {
                _inputFileName = value;
                OnPropertyChanged(nameof(InputFileName));
            }
        }

        public bool AreDataLoading
        {
            get => _areDataLoading;
            set
            {
                _areDataLoading = value;
                OnPropertyChanged(nameof(AreDataLoading));
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
            AreDataLoading = true;
            PeopleToSave.Clear();

            try
            {
                await foreach (var peopleChunk in ParserService.ParsePeopleCSV(InputFileName))
                {
                    List<Person> people = peopleChunk.ToList();

                    foreach (var person in people)
                    {
                        PeopleToSave.Add(person);
                    }
                }
            }
            finally
            {
                AreDataLoading = false;
            }
        }

        public async void SaveToDbCommand_Executed(object sender)
        {
            if (string.IsNullOrEmpty(InputFileName))
            {
                MessageBoxStore.Warning("Choose the existing file!");
                return;
            }

            if (MessageBoxStore.Confirmation("Do you want to save these data?") == System.Windows.MessageBoxResult.No)
            {
                return;
            }

            try
            {
                AreDataLoading = true;
                await PersonService.SaveManyAsync(PeopleToSave);
            }
            finally
            {
                AreDataLoading = false;
            }
        }

        public void RefreshCommand_Executed(object sender)
        {
            if (MessageBoxStore.Confirmation("Do you want to refresh the choice?") == System.Windows.MessageBoxResult.No)
            {
                return;
            }

            PeopleToSave.Clear();
            InputFileName = string.Empty;
        }

    }
}
