using CSVHandler.UI.Models;
using CSVHandler.UI.Services.Abstract;

namespace CSVHandler.UI.Services
{
    public class CSVParserService : ICSVParserService
    {
        private IFileService FileService { get; }

        public CSVParserService(IFileService fileService) 
        {
            FileService = fileService;
        }

        public async Task<IEnumerable<Person>> ParsePeopleCSV(string csvFilePath)
        {
            List<Person> people = new List<Person>();
            string fileContent = await FileService.RestoreAsync(csvFilePath);

            foreach (string line in fileContent.Split("\r\n"))
            {
                string[] values = line.Split(';');
                DateTime publishDate = DateTime.Parse(values[0]);
                people.Add(new Person(publishDate, values[1], values[2], values[3], values[4], values[5]));
            }

            return people;
        }
    }
}
