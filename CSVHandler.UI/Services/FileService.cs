using CSVHandler.UI.Services.Abstract;
using System.IO;

namespace CSVHandler.UI.Services
{
    public class FileService : IFileService
    {
        public async Task<string> RestoreAsync(string filePath)
        {
            StreamReader? sr = null;
            try
            {
                sr = new StreamReader(filePath);
                string content = (await sr.ReadToEndAsync()).Trim();
                return content;
            }
            catch (FileNotFoundException)
            {
                return String.Empty;
            }
            catch (Exception e)
            {
                return String.Empty;
            }
            finally
            {
                if (sr is not null)
                {
                    sr.Close();
                }
            }
        }
    }
}
