namespace CSVHandler.UI.Services.Abstract
{
    public interface IFileService
    {
        public Task<string> RestoreAsync(string filePath);
    }
}
