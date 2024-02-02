using CSVHandler.UI.Constants;
using CSVHandler.UI.Data.Abstract;
using CSVHandler.UI.Models;
using Microsoft.EntityFrameworkCore;

namespace CSVHandler.UI.Data
{
    public class PersonRepository : IPersonRepository
    {
        private ApplicationContext Db { get; }

        public PersonRepository(ApplicationContext context) 
        {
            Db = context;
        }
        public async Task SaveManyAsync(IEnumerable<Person> people)
        {
            using (ApplicationContext db = Db)
            {
                await db.BulkInsertAsync(people);
                await db.BulkSaveChangesAsync();
            }
        }

        public async IAsyncEnumerable<IEnumerable<Person>> GetFilteredChunkAsync(PersonFilter filter)
        {
            using (ApplicationContext db = Db) { 
                for (int i = 0; i < db.People.Count(); i += DataConstants.RecordsLimit)
                {
                    yield return await db.People.Skip(i)
                        .Take(DataConstants.RecordsLimit)
                        .Where(p => (p.PublishDate >= filter.StartDate && p.PublishDate <= filter.EndDate)
                            || (p.FirstName.Contains(filter.FirstName) && !string.IsNullOrEmpty(filter.FirstName))
                            || (p.MiddleName.Contains(filter.MiddleName)  && !string.IsNullOrEmpty(filter.MiddleName))
                            || (p.LastName.Contains(filter.LastName) && !string.IsNullOrEmpty(filter.LastName))
                            || (p.Town.Contains(filter.Town) && !string.IsNullOrEmpty(filter.Town))
                            || (p.Country.Contains(filter.Country)) && !string.IsNullOrEmpty(filter.Country))
                        .ToListAsync();
                }
            }
        }
    }
}
