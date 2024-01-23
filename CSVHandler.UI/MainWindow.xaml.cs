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
            EventAggregator.Instance.SaveToDbStarted += Instance_SaveToDbStarted;
            EventAggregator.Instance.SaveToDbEnded += Instance_SaveToDbEnded;
            EventAggregator.Instance.ShowFileDataStarted += Instance_ShowFileDataStarted;
            EventAggregator.Instance.ShowFileDataEnded += Instance_ShowFileDataEnded;
        }

        private void Instance_ShowFileDataEnded(object? sender, EventArgs e)
        {
            ShowFileDataStatus.Visibility = Visibility.Collapsed;
        }

        private void Instance_ShowFileDataStarted(object? sender, EventArgs e)
        {
            ShowFileDataStatus.Visibility = Visibility.Visible;
        }

        private void Instance_SaveToDbEnded(object? sender, EventArgs e)
        {
            SaveToDbStatus.Visibility = Visibility.Collapsed;
        }

        private void Instance_SaveToDbStarted(object? sender, EventArgs e)
        {
            SaveToDbStatus.Visibility = Visibility.Visible;
        }

        private void ExportToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxStore.Warning("iiiii");
        }
    }
}