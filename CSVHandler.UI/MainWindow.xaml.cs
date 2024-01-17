using CSVHandler.UI.Data;
using CSVHandler.UI.Data.Abstract;
using CSVHandler.UI.Services.Abstract;
using CSVHandler.UI.Util;
using CSVHandler.UI.ViewModels;
using Microsoft.Win32;
using System.Windows;

namespace CSVHandler.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow(ICSVParserService parserService, IPeopleRepository peopleRepository, IXmlService xmlService)
        {
            InitializeComponent();
            DataContext = new MainViewModel(parserService, peopleRepository, xmlService);
        }

        private void BrowseInputFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == true)
            {
                ((MainViewModel)DataContext).InputFileName = openFileDialog.FileName;
            }
        }

        private async void SaveToDBButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(InputFileBox.Text))
            {
                MessageBoxStore.Warning("Choose the existing file!");
                return;
            }

            //var SaveTask = ((MainViewModel)DataContext).SavePeopleFromFileAsync();
            await ((MainViewModel)DataContext).SavePeopleFromFileAsync();
        }

        private void ExportToXMLButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainViewModel)DataContext).ExportToXml();
        }

        private void ExportToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxStore.Warning("iiiii");
        }
    }
}