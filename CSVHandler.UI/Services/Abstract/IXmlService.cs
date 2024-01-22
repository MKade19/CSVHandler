using CSVHandler.UI.Models;

namespace CSVHandler.UI.Services.Abstract
{
    public interface IXmlService
    {
        public Task SavePeopleToFile(List<Person> people, string filepath);
    }
}
