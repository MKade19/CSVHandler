using CSVHandler.UI.Services.Abstract;
using System.IO;

namespace CSVHandler.UI.Services
{
    public class FileService : IFileService
    {
        private const int LinesLimit = 1000;

        public async IAsyncEnumerable<IEnumerable<string>> RestoreChunkAsync(string filePath)
        {
            using  (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    List<string> content = new List<string>();

                    for (int i = 0; i < LinesLimit; i++)
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
