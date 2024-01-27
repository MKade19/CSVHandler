using CSVHandler.UI.Constants;
using CSVHandler.UI.Data.Abstract;
using CSVHandler.UI.Models;
using CSVHandler.UI.Services.Abstract;

namespace CSVHandler.UI.Services
{
    public class PersonService : IPersonService
    {
        private IPersonRepository PeopleRepository;

        public PersonService(IPersonRepository personRepository)
        {
            PeopleRepository = personRepository;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await PeopleRepository.GetAllAsync();
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

                await PeopleRepository.SaveManyAsync(peopleChunk);
            }
        }
    }
}
