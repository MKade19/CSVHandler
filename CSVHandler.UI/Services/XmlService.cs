using CSVHandler.UI.Models;
using CSVHandler.UI.Services.Abstract;
using System.IO;
using System.Xml.Serialization;

namespace CSVHandler.UI.Services
{
    public class XmlService : IXmlService
    {
        public async Task SavePeopleToFileAsync(IEnumerable<Person> peopleData, string filepath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Person>));
            List<Person> people = new List<Person>(peopleData);

            using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate))
            {
                await Task.Run(() => { xmlSerializer.Serialize(fs, people); });
            }
        }
    }
}
