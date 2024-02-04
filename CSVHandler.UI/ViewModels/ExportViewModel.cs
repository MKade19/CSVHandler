using CSVHandler.UI.Models;
using CSVHandler.UI.Services.Abstract;
using CSVHandler.UI.Util;
using System.Collections.ObjectModel;
using Microsoft.WindowsAPICodePack.Dialogs;
using CSVHandler.UI.Views;
using System.Windows;
using System.Diagnostics;

namespace CSVHandler.UI.ViewModels
{
    public class ExportViewModel : ViewModelBase
    {
        private IXmlService XmlService { get; }
        private IPersonService PersonService { get; }
        private IExcelService ExcelService { get; }
        public DelegateCommand GetFilteredDataCommand { get; }
        public DelegateCommand ExportToXmlCommand { get; }
        public DelegateCommand ExportToExcelCommand { get; }
        public DelegateCommand RefreshCommand { get; }

        private bool _areDataLoading = false;
        private bool _isTableShown = false;
        private ObservableCollection<Person> _peopleToExport = new ObservableCollection<Person>();
        private PersonFilter _personFilter = new PersonFilter();

        public ExportViewModel(IXmlService xmlService, IPersonService personService, IExcelService excelService)
        {
            XmlService = xmlService;
            ExcelService = excelService;
            PersonService = personService;
            GetFilteredDataCommand = new DelegateCommand(GetFilteredDataCommand_Executed);
            ExportToXmlCommand = new DelegateCommand(ExportToXmlCommand_Executed);
            ExportToExcelCommand = new DelegateCommand(ExportToExcelCommand_Executed);
            RefreshCommand = new DelegateCommand(RefreshCommand_Executed);
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

        public bool IsTableShown
        {
            get => _isTableShown;
            set
            {
                _isTableShown = value;
                OnPropertyChanged(nameof(IsTableShown));
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

        public DateTime StartDate
        {
            get => _personFilter.StartDate;
            set
            {
                _personFilter.StartDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get => _personFilter.EndDate;
            set
            {
                _personFilter.EndDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public string FirstName
        {
            get => _personFilter.FirstName;
            set
            {
                _personFilter.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string MiddleName
        {
            get => _personFilter.MiddleName;
            set
            {
                _personFilter.MiddleName = value;
                OnPropertyChanged(nameof(MiddleName));
            }
        }

        public string LastName
        {
            get => _personFilter.LastName;
            set
            {
                _personFilter.LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Town
        {
            get => _personFilter.Town;
            set
            {
                _personFilter.Town = value;
                OnPropertyChanged(nameof(Town));
            }
        }

        public string Country
        {
            get => _personFilter.Country;
            set
            {
                _personFilter.Country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public async void GetFilteredDataCommand_Executed(object sender)
        {
            try
            {
                AreDataLoading = true;
                await GetPeopleFromDbAsync();
                IsTableShown = true;
            }
            finally
            {
                AreDataLoading = false;
            }
        }

        public async void ExportToXmlCommand_Executed(object sender)
        {
            try
            {
                AreDataLoading = true;

                var dlg = new CommonOpenFileDialog();
                dlg.Title = (string)Application.Current.FindResource("ChooseDirectoryDialog");
                dlg.IsFolderPicker = true;
                dlg.Multiselect = false;
                string folderName = string.Empty;
                string fileName = string.Empty;

                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    folderName = dlg.FileName ?? string.Empty;
                }

                var fileDialog = new XmlSaveDialogView();
                if (fileDialog.ShowDialog() == true)
                {
                    fileName = fileDialog.XmlFilename;
                }
                else
                {
                    return;
                }

                string fullPath = folderName + '\\' + fileName + ".xml";
                List<Person> people = new List<Person>(PeopleToExport);

                await XmlService.SavePeopleToFileAsync(people, fullPath);
                MessageBoxStore.Information((string)Application.Current.FindResource("ExportToXmlResultYes"));
                Process.Start("explorer.exe", folderName);
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
                await ExcelService.SavePeopleToExcelAsync(PeopleToExport);
            }
            finally
            {
                AreDataLoading = false;
            }
        }

        public void RefreshCommand_Executed(object obj)
        {
            if (MessageBoxStore.Confirmation((string)Application.Current.FindResource("RefreshTableDialogCaption")) == System.Windows.MessageBoxResult.No)
            {
                return;
            }

            try
            {
                AreDataLoading = true;
                PeopleToExport.Clear();
                IsTableShown = false;
            }
            finally
            {
                AreDataLoading = false;
            }
        }

        private async Task GetPeopleFromDbAsync()
        {
            PeopleToExport.Clear();

            await foreach (var chunk in PersonService.GetFilteredChunkAsync(_personFilter))
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
