using CSVHandler.UI.Data.Abstract;
using CSVHandler.UI.Models;

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
            await Db.People.AddRangeAsync(people);
            await Db.SaveChangesAsync();
        }

        public IEnumerable<Person> GetAll()
        {
            return Db.People.ToList();
        }
    }
}
