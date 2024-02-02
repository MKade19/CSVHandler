using CSVHandler.UI.Models;
using CSVHandler.UI.Services.Abstract;

namespace CSVHandler.UI.Services
{
    public class ExcelService : IExcelService
    {
        public async Task SavePeopleToExcelAsync(IEnumerable<Person> peopleData)
        {
            await Task.Run(() => 
            {
                List<Person> people = peopleData.ToList();

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                excel.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbook excelworkBook = excel.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                excelSheet.Name = "People data";

                for (int i = 0; i < people.Count; i++)
                {
                    excelSheet.Cells[i + 1, 1] = people[i].PublishDate;
                    excelSheet.Cells[i + 1, 2] = people[i].FirstName;
                    excelSheet.Cells[i + 1, 3] = people[i].MiddleName;
                    excelSheet.Cells[i + 1, 4] = people[i].LastName;
                    excelSheet.Cells[i + 1, 5] = people[i].Town;
                    excelSheet.Cells[i + 1, 6] = people[i].Country;
                }
            });
        }
    }
}
