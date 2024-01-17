using CSVHandler.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVHandler.UI.Data.Abstract
{
    public interface IPeopleRepository
    {
        public Task SaveManyAsync(IEnumerable<Person> people);
        public IEnumerable<Person> GetAll();
    }
}
