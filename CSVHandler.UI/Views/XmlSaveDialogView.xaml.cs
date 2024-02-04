using MahApps.Metro.Controls;
using System.Windows;

namespace CSVHandler.UI.Views
{
    /// <summary>
    /// Interaction logic for XmlSaveDialogView.xaml
    /// </summary>
    public partial class XmlSaveDialogView : MetroWindow
    {
        public XmlSaveDialogView()
        {
            InitializeComponent();
        }

        public string XmlFilename
        {
            get { return XmlFilenameBox.Text; }
            set { XmlFilenameBox.Text = value; }
        }

        private void SaveXmlFileButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
