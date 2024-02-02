namespace CSVHandler.UI.Models
{
    public class PersonFilter
    {
        public DateTime StartDate { get; set; } = DateTime.MinValue;

        public DateTime EndDate { get; set; } = DateTime.Now;

        public string FirstName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Town { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public PersonFilter(DateTime startDate, DateTime endDate, string firstName, string middleName, string lastName, string town, string country)
        {
            StartDate = startDate;
            EndDate = endDate;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Town = town;
            Country = country;
        }

        public PersonFilter() { }
    }
}
