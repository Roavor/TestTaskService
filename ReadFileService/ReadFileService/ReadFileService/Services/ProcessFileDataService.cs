using ReadFileService.Models;

namespace ReadFileService.Services
{
    public class ProcessFileDataService : IProcessFileDataService
    {
        private readonly ReadTXTText _readTXTText;

        public ProcessFileDataService()
        {
            _readTXTText = new ReadTXTText();
        }

        public async Task<ReturnInfo<Dictionary<string, int>>> GetUniqueWordsCountAsync(string path)
        {
            var result = await _readTXTText.TemplateAsync(path);
            return result;
        }

        public ReturnInfo<string> CreateFile(CreateFileRequest request)
        {
            try
            {
                File.WriteAllText(".\\Files\\" + request.Name, request.Text);
                return new("Sucessfully saved!");
            }
            catch (Exception ex)
            {
                return new(new HttpError(500, ex.Message));
            }
        }
    }

    public interface IProcessFileDataService
    {
        Task<ReturnInfo<Dictionary<string, int>>> GetUniqueWordsCountAsync(string path);

        ReturnInfo<string> CreateFile(CreateFileRequest request);
    }
}