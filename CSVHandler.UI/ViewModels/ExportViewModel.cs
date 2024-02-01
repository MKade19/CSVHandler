using CSVHandler.UI.Models;
using CSVHandler.UI.Services.Abstract;
using CSVHandler.UI.Util;
using System.Collections.ObjectModel;
using System.Windows;

namespace CSVHandler.UI.ViewModels
{
    public class ExportViewModel : ViewModelBase
    {
        private IXmlService XmlService { get; }
        private IPersonService PersonService { get; }
        private IExcelService ExcelService { get; }
        public DelegateCommand ExportToXmlCommand { get; }
        public DelegateCommand ExportToExcelCommand { get; }

        private bool _areDataLoading = false;
        private ObservableCollection<Person> _peopleToExport = new ObservableCollection<Person>();

        public ExportViewModel(IXmlService xmlService, IPersonService personService, IExcelService excelService)
        {
            XmlService = xmlService;
            ExcelService = excelService;
            PersonService = personService;
            ExportToXmlCommand = new DelegateCommand(ExportToXmlCommand_Executed);
            ExportToExcelCommand = new DelegateCommand(ExportToExcelCommand_Executed);
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

        public ObservableCollection<Person> PeopleToExport
        {
            get => _peopleToExport;
            set
            {
                _peopleToExport = value;
                OnPropertyChanged(nameof(PeopleToExport));
            }
        }

        public async void ExportToXmlCommand_Executed(object sender)
        {
            try
            {
                AreDataLoading = true;
                List<Person> people = new List<Person>(PeopleToExport);
                await XmlService.SavePeopleToFileAsync(people, "D:\\people.xml");
                MessageBoxStore.Information("Data were exported to xml!");
            }
            finally
            {
                AreDataLoading = false;
            }
        }

        public async void ExportToExcelCommand_Executed(object sender)
        {
            try
            {
                AreDataLoading = true;
                await GetPeopleFromDb();
                ExcelService.SavePeopleToExcelAsync(PeopleToExport);
                MessageBoxStore.Information("Data were exported to excel!");
            }
            finally
            {
                AreDataLoading = false;
            }
        }

        public async Task LoadPeopleToGrid()
        {
            try
            {
                AreDataLoading = true;
                await GetPeopleFromDb();
            }
            finally
            {
                AreDataLoading = false;
            }
        }

        private async Task GetPeopleFromDb()
        {
            PeopleToExport.Clear();

            await foreach (var chunk in PersonService.GetChunkAsync())
            {
                List<Person> people = chunk.ToList();
                
                foreach(var person in people)
                {
                    PeopleToExport.Add(person);
                }
            }
        }
    }
}
