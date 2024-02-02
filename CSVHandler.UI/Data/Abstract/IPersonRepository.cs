using CSVHandler.UI.Models;

namespace CSVHandler.UI.Data.Abstract
{
    public interface IPersonRepository
    {
        public Task SaveManyAsync(IEnumerable<Person> people);
        public IAsyncEnumerable<IEnumerable<Person>> GetFilteredChunkAsync(PersonFilter filter);
    }
}
