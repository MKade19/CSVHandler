using CSVHandler.UI.Services.Abstract;
using System.IO;
using CSVHandler.UI.Constants;

namespace CSVHandler.UI.Services
{
    public class FileService : IFileService
    {
        public async IAsyncEnumerable<IEnumerable<string>> RestoreChunkAsync(string filePath)
        {
            using  (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    List<string> content = new List<string>();

                    for (int i = 0; i < DataConstants.RecordsLimit; i++)
                    {
                        string? line = await sr.ReadLineAsync();

                        if (line == null)
                        {
                            break;
                        }

                        content.Add(line);
                    }

                    yield return content;
                }
            }
        }
    }
}
