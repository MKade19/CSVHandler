using CSVHandler.UI.Data.Abstract;
using CSVHandler.UI.Models;
using Microsoft.EntityFrameworkCore;

namespace CSVHandler.UI.Data
{
    public class PeopleRepository : IPeopleRepository
    {
        private ApplicationContext Db { get; }

        public PeopleRepository(ApplicationContext context) 
        {
            Db = context;
        }
        public async Task SaveManyAsync(IEnumerable<Person> people)
        {
            using(ApplicationContext db = Db)
            {
                await db.People.AddRangeAsync(people);
                await db.SaveChangesAsync();
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
