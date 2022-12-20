using Microsoft.AspNetCore.Mvc;
using ReadFileService.Models;
using ReadFileService.Services;

namespace ReadFileService.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class FileDataProcessController : ControllerBase
    {
        private readonly IProcessFileDataService _processFileDataService;

        public FileDataProcessController(IProcessFileDataService processFileDataService)
        {
            _processFileDataService = processFileDataService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Dictionary<string, int>>> CreditLimitValidatorsAsync(string path)
        => (await _processFileDataService.GetUniqueWordsCountAsync(path)).ToContent();

        [HttpPut("[action]")]
        public async Task<ActionResult<string>> SaveFileAsync([FromBody] CreateFileRequest path)
        => (_processFileDataService.CreateFile(path)).ToContent();
    }
}