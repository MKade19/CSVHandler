namespace CSVHandler.UI.ViewModels
{
    public class XmlSaveDialogViewModel : ViewModelBase
    {
        private string _filename = string.Empty;

        public string Filename
        {
            get => _filename;
            set
            {
                _filename = value;
                OnPropertyChanged(nameof(Filename));
            }
        }
    }
}
