using CSVHandler.UI.Models;

namespace CSVHandler.UI.Services.Abstract
{
    public interface ICSVParserService
    {
        public IAsyncEnumerable<IEnumerable<Person>> ParsePeopleCSV(string csvFileName);
    }
}
