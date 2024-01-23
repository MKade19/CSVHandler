namespace CSVHandler.UI.Services.Abstract
{
    public interface IFileService
    {
        public IAsyncEnumerable<IEnumerable<string>> RestoreChunkAsync(string filePath);
    }
}
