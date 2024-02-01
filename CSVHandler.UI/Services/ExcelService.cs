using CSVHandler.UI.Models;
using CSVHandler.UI.Services.Abstract;
using System.ComponentModel;
using SData = System.Data;

namespace CSVHandler.UI.Services
{
    public class ExcelService : IExcelService
    {
        public void SavePeopleToExcelAsync(IEnumerable<Person> peopleData)
        {
            List<Person> people = peopleData.ToList();
            SData.DataTable dataTable = ConvertToDataTable(people);

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = false;
            excel.DisplayAlerts = false;

            Microsoft.Office.Interop.Excel.Workbook excelworkBook = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
            excelSheet.Name = "People data";

            excelSheet.Cells[1, 1] = "Sample test data";
            excelSheet.Cells[1, 2] = "Date : " + DateTime.Now.ToShortDateString();

            Microsoft.Office.Interop.Excel.Range excelCellrange;
        }

        public SData.DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            SData.DataTable table = new SData.DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                SData.DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
