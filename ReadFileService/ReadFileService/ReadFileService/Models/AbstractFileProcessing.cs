namespace ReadFileService.Models
{
    public abstract class AbstractFileProcessing<T>
    {
        public async Task<ReturnInfo<T>> TemplateAsync(string path)
        {
            var result = await this.ReadText(path);
            if (result.IsException)
            {
                return new(result.ExceptionError);
            }
            return this.ProcessText(result.ToContent().Value);
        }

        public abstract Task<ReturnInfo<string>> ReadText(string path);

        public abstract ReturnInfo<T> ProcessText(string fileText);
    }
}