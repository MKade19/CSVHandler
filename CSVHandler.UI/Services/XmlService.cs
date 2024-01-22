using CSVHandler.UI.Models;
using CSVHandler.UI.Services.Abstract;
using System.IO;
using System.Xml.Serialization;

namespace CSVHandler.UI.Services
{
    public class XmlService : IXmlService
    {
        public async Task SavePeopleToFile(List<Person> people, string filepath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Person>));

            using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate))
            {
                await Task.Run(() => { xmlSerializer.Serialize(fs, people); });
            }
        }
    }
}
