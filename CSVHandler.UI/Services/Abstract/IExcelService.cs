using CSVHandler.UI.Models;

namespace CSVHandler.UI.Services.Abstract
{
    public interface IExcelService
    {
        public void SavePeopleToExcelAsync(IEnumerable<Person> peopleData);
    }
}
