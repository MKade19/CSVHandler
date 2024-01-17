using CSVHandler.UI.Models;

namespace CSVHandler.UI.Services.Abstract
{
    public interface IXmlService
    {
        public void SaveToFile(List<Person> people, string filepath);
    }
}
