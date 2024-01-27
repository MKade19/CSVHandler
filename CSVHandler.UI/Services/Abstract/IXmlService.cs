using CSVHandler.UI.Models;

namespace CSVHandler.UI.Services.Abstract
{
    public interface IXmlService
    {
        public Task SavePeopleToFileAsync(List<Person> people, string filepath);
    }
}
