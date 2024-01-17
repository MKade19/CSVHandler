using CSVHandler.UI.Models;

namespace CSVHandler.UI.Services.Abstract
{
    public interface ICSVParserService
    {
        public IEnumerable<Person> ParseCSV(string csvFileName);
    }
}
