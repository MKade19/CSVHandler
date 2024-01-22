using CSVHandler.UI.Models;

namespace CSVHandler.UI.Services.Abstract
{
    public interface ICSVParserService
    {
        public Task<IEnumerable<Person>> ParsePeopleCSV(string csvFileName);
    }
}
