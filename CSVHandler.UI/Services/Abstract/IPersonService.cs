using CSVHandler.UI.Models;

namespace CSVHandler.UI.Services.Abstract
{
    public interface IPersonService
    {
        public Task SaveManyAsync(IEnumerable<Person> people);
        public Task<IEnumerable<Person>> GetAllAsync();
    }
}
