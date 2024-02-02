using CSVHandler.UI.Models;

namespace CSVHandler.UI.Services.Abstract
{
    public interface IExcelService
    {
        public Task SavePeopleToExcelAsync(IEnumerable<Person> peopleData);
    }
}
