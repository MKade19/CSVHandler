using CSVHandler.UI.Models;
using CSVHandler.UI.Services.Abstract;
using FileHelpers;

namespace CSVHandler.UI.Services
{
    public class CSVParserService : ICSVParserService
    {
        public IEnumerable<Person> ParseCSV(string csvFilePath)
        {
            var fileHelperEngine = new FileHelperEngine<Person>();
            var records = fileHelperEngine.ReadFile(csvFilePath);
            List<Person> people = new List<Person>();

            foreach (var record in records)
            {
                people.Add(record);
            }

            return people;
        }
    }
}
