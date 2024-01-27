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

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            using (ApplicationContext db = Db)
            {
                return await db.People.ToListAsync();
            }
        }
    }
}
