using CSVHandler.UI.Data.Abstract;
using CSVHandler.UI.Models;
using CSVHandler.UI.Constants;
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

        public async IAsyncEnumerable<IEnumerable<Person>> GetChunkAsync()
        {
            using (ApplicationContext db = Db)
            {
                for (int i = 0; i < db.People.Count(); i += DataConstants.RecordsLimit)
                {
                    yield return await db.People.Skip(i)
                        .Take(DataConstants.RecordsLimit)
                        .ToListAsync();
                }
            }
        }
    }
}
