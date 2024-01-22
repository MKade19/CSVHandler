using CSVHandler.UI.Models;

namespace CSVHandler.UI.Data.Abstract
{
    public interface IPeopleRepository
    {
        public Task SaveManyAsync(IEnumerable<Person> people);
        public Task<IEnumerable<Person>> GetAllAsync();
    }
}
