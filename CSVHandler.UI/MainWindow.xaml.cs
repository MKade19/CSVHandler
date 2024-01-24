using CSVHandler.UI.Data.Abstract;
using CSVHandler.UI.Services.Abstract;
using CSVHandler.UI.Util;
using CSVHandler.UI.ViewModels;
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
            EventAggregator.Instance.SaveToDbStarted += EventAggregator_SaveToDbStarted;
            EventAggregator.Instance.SaveToDbEnded += EventAggregator_SaveToDbEnded;
            EventAggregator.Instance.ShowFileDataStarted += EventAggregator_ShowFileDataStarted;
            EventAggregator.Instance.ShowFileDataEnded += EventAggregator_ShowFileDataEnded;
        }

        private void EventAggregator_ShowFileDataStarted(object? sender, EventArgs e)
        {
            ShowFileDataStatus.Visibility = Visibility.Visible;
            BrowseInputFileButton.IsEnabled = false;
        }

        private void EventAggregator_ShowFileDataEnded(object? sender, EventArgs e)
        {
            ShowFileDataStatus.Visibility = Visibility.Collapsed;
            BrowseInputFileButton.IsEnabled = true;
        }

        private void EventAggregator_SaveToDbStarted(object? sender, EventArgs e)
        {
            SaveToDbStatus.Visibility = Visibility.Visible;
            BrowseInputFileButton.IsEnabled = false;
            SaveToDBButton.IsEnabled = false;
        }

        private void EventAggregator_SaveToDbEnded(object? sender, EventArgs e)
        {
            SaveToDbStatus.Visibility = Visibility.Collapsed;
            BrowseInputFileButton.IsEnabled = true;
            SaveToDBButton.IsEnabled = true;
        }

        private void ExportToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxStore.Warning("iiiii");
        }
    }
}