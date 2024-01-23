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

        public async IAsyncEnumerable<IEnumerable<Person>> ParsePeopleCSV(string csvFilePath)
        {
            await foreach (var dataChunk in FileService.RestoreChunkAsync(csvFilePath))
            {
                List<Person> people = new List<Person>();

                foreach (string personData in dataChunk)
                {
                    string[] values = personData.Split(';');
                    DateTime publishDate = DateTime.Parse(values[0]);
                    people.Add(new Person(publishDate, values[1], values[2], values[3], values[4], values[5]));
                }

                yield return people;
            }
        }
    }
}
