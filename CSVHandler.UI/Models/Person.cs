using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSVHandler.UI.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime PublishDate { get; set; } = DateTime.Now;

        public string FirstName { get; set; } = string.Empty;
         
        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Town { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public Person(DateTime date, string firstName, string middleName, string lastName, string town, string country)
        {
            PublishDate = date;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Town = town;
            Country = country;
        }

        public Person() { }
    }
}
