using CSVHandler.UI.Models;
using CSVHandler.UI.Services.Abstract;
using System.IO;
using System.Xml.Serialization;

namespace CSVHandler.UI.Services
{
    public class XmlService : IXmlService
    {
        public void SaveToFile(List<Person> people, string filepath) //REMOVE DEFAULT VALUE!!!!!!
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Person>));

            using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, people);
            }
        }
    }
}
