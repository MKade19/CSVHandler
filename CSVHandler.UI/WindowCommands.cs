using System.Windows.Input;

namespace CSVHandler.UI
{
    public class WindowCommands
    {
        static WindowCommands()
        {
            SaveToDbCommand = new RoutedCommand(nameof(SaveToDbCommand), typeof(MainWindow));
            ExportToXmlCommand = new RoutedCommand(nameof(ExportToXmlCommand), typeof(MainWindow));
            ExportToExcelCommand = new RoutedCommand(nameof(ExportToExcelCommand), typeof(MainWindow));
        }

        public static RoutedCommand SaveToDbCommand { get; }
        public static RoutedCommand ExportToXmlCommand { get; }
        public static RoutedCommand ExportToExcelCommand { get; }
}
    }
