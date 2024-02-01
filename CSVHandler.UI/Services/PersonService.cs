using CSVHandler.UI.Constants;
using CSVHandler.UI.Data.Abstract;
using CSVHandler.UI.Models;
using CSVHandler.UI.Services.Abstract;

namespace CSVHandler.UI.Services
{
    public class PersonService : IPersonService
    {
        private IPersonRepository PersonRepository;

        public PersonService(IPersonRepository personRepository)
        {
            PersonRepository = personRepository;
        }

        public async IAsyncEnumerable<IEnumerable<Person>> GetChunkAsync()
        {
            await foreach (var chunk in PersonRepository.GetChunkAsync())
            {
                yield return chunk;
            }
        }

        public async Task SaveManyAsync(IEnumerable<Person> people)
        {
            List<Person> peopleList = new List<Person>(people);

            for (int i = 0; i < peopleList.Count; i += DataConstants.RecordsLimit)
            {
                List<Person> peopleChunk = new List<Person>();

                for (int j = i; j < i + DataConstants.RecordsLimit && j < peopleList.Count; j++)
                {
                    peopleChunk.Add(peopleList[j]);
                }

                await PersonRepository.SaveManyAsync(peopleChunk);
            }
        }
    }
}
